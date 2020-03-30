using ManiaPlanetSharp.GameBox.Parsing;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;

namespace ManiaPlanetSharp.GameBox.MetadataProviders
{
    [DebuggerNonUserCode]
    public abstract class MetadataProvider
    {
        protected MetadataProvider(GameBoxFile file)
        {
            this.File = file ?? throw new ArgumentNullException(nameof(file));

            this.headerNodes = this.File.HeaderChunks?
                .Where(node => node.GetType() != typeof(UnknownChunk))
                .GroupBy(node => node.GetType())
                .ToDictionary(group => group.Key, group => group.ToArray());
        }

        public GameBoxFile File { get; private set; }

        /// <summary>
        /// Indicates whether the metadata provider should validate whether the values in the GameBox file should be validated against each other. If set to <c>true</c>, a <c>InvalidDataException</c> will be raised when a property with mismatching values is accessed.
        /// </summary>
        public bool ValidatingMode { get; set; } = false;

        public void ParseBody()
        {
            if (this.bodyNodes == null)
            {
                this.bodyNodes = this.File.ParseBody()
                    .Where(node => node.GetType() != typeof(UnknownChunk))
                    .GroupBy(node => node.GetType())
                    .ToDictionary(group => group.Key, group => group.ToArray());
            }
        }

        private Dictionary<Type, Node[]> headerNodes = new Dictionary<Type, Node[]>();
        protected TChunk[] GetHeaderNodes<TChunk>()
            where TChunk : Node
        {
            if (this.headerNodes?.ContainsKey(typeof(TChunk)) ?? false)
            {
                return this.headerNodes[typeof(TChunk)].OfType<TChunk>().ToArray();
            }
            return null;
        }

        private Dictionary<Type, Node[]> bodyNodes;
        protected TChunk[] GetBodyNodes<TChunk>()
            where TChunk : Node
        {
            if (this.bodyNodes == null)
            {
                this.ParseBody();
            }
            if (this.bodyNodes.ContainsKey(typeof(TChunk)))
            {
                return this.bodyNodes[typeof(TChunk)].OfType<TChunk>().ToArray();
            }
            return null;
        }



        private Dictionary<string, object> BufferedValues { get; } = new Dictionary<string, object>();
        [MethodImpl(MethodImplOptions.NoInlining)]
        protected TValue GetBufferedValue<TValue>(Func<TValue> factory, [CallerMemberName] string name = null)
        {
            if (name == null)
            {
                throw new ArgumentNullException(nameof(name));
            }

            if (!this.BufferedValues.ContainsKey(name))
            {
                if (factory == null)
                {
                    throw new ArgumentNullException(nameof(factory));
                }
                this.BufferedValues.Add(name, factory());
            }

            return (TValue)this.BufferedValues[name];
        }

        [MethodImpl(MethodImplOptions.NoInlining)]
        protected BufferedHeaderValue<TChunk, TValue> GetBufferedHeaderValue<TChunk, TValue>(Func<TChunk, TValue> factory, [CallerMemberName] string name = null)
            where TChunk : Chunk
        {
            return new BufferedHeaderValue<TChunk, TValue>(this, name, factory);
        }

        [MethodImpl(MethodImplOptions.NoInlining)]
        protected BufferedBodyValue<TChunk, TValue> GetBufferedBodyValue<TChunk, TValue>(Func<TChunk, TValue> factory, [CallerMemberName] string name = null)
            where TChunk : Chunk
        {
            return new BufferedBodyValue<TChunk, TValue>(this, name, factory);
        }



        private class NodeTypeComparer
            : IEqualityComparer<Tuple<Type, Node>>
        {
            bool IEqualityComparer<Tuple<Type, Node>>.Equals(Tuple<Type, Node> x, Tuple<Type, Node> y)
            {
                return x?.Item1 == y?.Item1;
            }

            int IEqualityComparer<Tuple<Type, Node>>.GetHashCode(Tuple<Type, Node> obj)
            {
                return obj?.Item1.GetType().GetHashCode() ?? 0;
            }
        }

        protected interface IBufferedChunkValue<out TChunk, TValue>
            where TChunk : Chunk
        {
            MetadataProvider Provider { get; }

            TValue GetBufferedValue();

            TValue GetValue();
        }

        protected abstract class BufferedChunkValue<TChunk, TValue>
            : IBufferedChunkValue<TChunk, TValue>
            where TChunk : Chunk
        {
            protected BufferedChunkValue(MetadataProvider provider, string name, Func<TChunk, TValue> factory)
            {
                this.Provider = provider ?? throw new ArgumentNullException(nameof(provider));
                this.Name = name ?? throw new ArgumentNullException(nameof(name));
                this.Factory = factory ?? throw new ArgumentNullException(nameof(factory));
            }

            protected BufferedChunkValue(IBufferedChunkValue<Chunk, TValue> previous, MetadataProvider provider, string name, Func<TChunk, TValue> factory)
                : this(provider, name, factory)
            {
                this.Previous = previous ?? throw new ArgumentNullException(nameof(previous));
            }

            public MetadataProvider Provider { get; private set; }

            public string Name { get; private set; }

            protected Func<TChunk, TValue> Factory { get; private set; }

            protected IBufferedChunkValue<Chunk, TValue> Previous { get; private set; }

            public TValue GetBufferedValue()
            {
                return this.Provider.GetBufferedValue(() => this.GetValue(), this.Name);
            }

            public TValue GetValue()
            {
#if !VALIDATE_METADATA_SOURCES
                if (this.Previous != null)
                {
                    var previousValue = this.Previous.GetValue();
                    if (previousValue != null)
                    {
                        var value = this.GetValueInternal();
                        if (value != null && !previousValue.Equals(value) && this.Provider.ValidatingMode)
                        {
                            throw new InvalidDataException($"Parameter {this.Name} failed cross validation.");
                        }
                        return previousValue;
                    }
                }
                return this.GetValueInternal();
#else
                var value = this.GetValueInternal();
                if (value == null)
                {
                    Console.WriteLine($"[Buffered Value][Warning] {this.Name}: could not be extracted from {typeof(TChunk)} ({(this is BufferedHeaderValue<TChunk, TValue> ? "Header" : "Body")})");
                }
                else
                {
                    Console.WriteLine($" => {value}");
                }

                if (this.Previous == null)
                {
                    return value;
                }
                else
                {
                    var previousValue = this.Previous.GetValue();
                    if (previousValue != null)
                    {
                        if (value != null && !previousValue.Equals(value))
                        {
                            //Console.WriteLine($"[Buffered Value][Error] {this.Name}: Received different value (\"{value}\") from previous source (\"{previousValue}\")!");
                            if (this.Provider.ValidatingMode)
                            {
                                throw new InvalidDataException($"Parameter {this.Name} failed cross validation.");
                            }
                        }
                        return previousValue;
                    }
                    else
                    {
                        return value;
                    }
                }
#endif
            }

            protected abstract TValue GetValueInternal();

            public static implicit operator TValue(BufferedChunkValue<TChunk, TValue> bufferedValue)
            {
                return bufferedValue.GetBufferedValue();
            }
        }

        protected class BufferedHeaderValue<TChunk, TValue>
            : BufferedChunkValue<TChunk, TValue>
            where TChunk : Chunk
        {
            public BufferedHeaderValue(MetadataProvider provider, string name, Func<TChunk, TValue> factory)
                : base(provider, name, factory)
            { }

            public BufferedHeaderValue(IBufferedChunkValue<Chunk, TValue> previous, MetadataProvider provider, string name, Func<TChunk, TValue> factory)
                : base(previous, provider, name, factory)
            { }

            public BufferedHeaderValue<TChunk2, TValue> IfNull<TChunk2>(Func<TChunk2, TValue> factory)
                where TChunk2 : Chunk
            {
                return new BufferedHeaderValue<TChunk2, TValue>(this, this.Provider, this.Name, factory);
            }

            public BufferedBodyValue<TChunk2, TValue> IfNullBody<TChunk2>(Func<TChunk2, TValue> factory)
                where TChunk2 : Chunk
            {
                return new BufferedBodyValue<TChunk2, TValue>(this, this.Provider, this.Name, factory);
            }

            protected override TValue GetValueInternal()
            {
                var chunks = this.Provider.GetHeaderNodes<TChunk>();
#if VALIDATE_METADATA_SOURCES
                Console.WriteLine($"[Buffered Value][Info] {this.Name}: from {typeof(TChunk)} (Header): {chunks?.Length.ToString() ?? "no"} chunks found.");
#endif
                foreach (TChunk chunk in chunks ?? Array.Empty<TChunk>())
                {
                    if (chunk != null)
                    {
                        TValue result = this.Factory(chunk);
                        if (result != null)
                        {
                            return result;
                        }
                    }
                }
                return default(TValue);
            }

            //public static implicit operator TValue(BufferedHeaderValue<TChunk, TValue> bufferedValue)
            //{
            //    return bufferedValue?.GetBufferedValue();
            //}
        }

        protected class BufferedBodyValue<TChunk, TValue>
            : BufferedChunkValue<TChunk, TValue>
            where TChunk : Chunk
        {
            public BufferedBodyValue(MetadataProvider provider, string name, Func<TChunk, TValue> factory)
                : base(provider, name, factory)
            { }

            public BufferedBodyValue(IBufferedChunkValue<Chunk, TValue> previous, MetadataProvider provider, string name, Func<TChunk, TValue> factory)
                : base(previous, provider, name, factory)
            { }

            public BufferedBodyValue<TChunk2, TValue> IfNull<TChunk2>(Func<TChunk2, TValue> factory)
                where TChunk2 : Chunk
            {
                return new BufferedBodyValue<TChunk2, TValue>(this, this.Provider, this.Name, factory);
            }

            protected override TValue GetValueInternal()
            {
                var chunks = this.Provider.GetBodyNodes<TChunk>();
#if VALIDATE_METADATA_SOURCES
                Console.WriteLine($"[Buffered Value][Info] {this.Name}: from {typeof(TChunk)} (Body): {chunks?.Length.ToString() ?? "no"} chunks found.");
#endif
                foreach (TChunk chunk in chunks ?? Array.Empty<TChunk>())
                {
                    if (chunk != null)
                    {
                        TValue result = this.Factory(chunk);
                        if (result != null)
                        {
                            return result;
                        }
                    }
                }
                return default(TValue);
            }

            //public static implicit operator TValue(BufferedBodyValue<TChunk, TValue> bufferedValue)
            //{
            //    return bufferedValue?.GetBufferedValue();
            //}
        }
    }
}
