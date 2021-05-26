using System;
using System.Collections.Generic;
using System.Text;

namespace ManiaPlanetSharp.GameBox.Parsing
{
    public static class ParsingErrorLogger
    {
        public static event EventHandler<ParsingErrorEventArgs> ParsingErrorOccured;

        internal static void OnParsingErrorOccured(object sender, ParsingErrorEventArgs args)
        {
            ParsingErrorOccured?.Invoke(sender, args);
        }
    }

    public class ParsingErrorEventArgs
        : EventArgs
    {
        public ParsingErrorEventArgs(uint chunkId, string message)
        {
            this.ChunkId = chunkId;
            this.Message = message;
        }

        public uint ChunkId { get; private set; }
        public string Message { get; private set; }

        public override string ToString() => $"Chunk 0x{this.ChunkId:X8}.\n\n{this.Message}";
    }

    public class BodyParsingErrorEventArgs
        : ParsingErrorEventArgs
    {
        public BodyParsingErrorEventArgs(uint chunkId, long position, string message)
            : base(chunkId, message)
        {
            this.Position = position;
        }

        public long Position { get; private set; }

        public override string ToString() => $"Chunk 0x{this.ChunkId:X8} at {this.Position}.\n\n{this.Message}";
    }

    public class NodeInternalParsingErrorEventArgs
        : ParsingErrorEventArgs
    {
        public NodeInternalParsingErrorEventArgs(Node node, uint chunkId, string message)
            : base(chunkId, message)
        {
            this.ClassId = node?.Id ?? 0;
        }

        public uint ClassId { get; private set; }

        public override string ToString() => $"Chunk 0x{this.ChunkId:X8} in Node 0x{this.ClassId:X8}.\n\n{this.Message}";
    }
}
