using System;
using System.Collections.Generic;
using System.Text;

namespace ManiaPlanetSharp.GameBox
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
        public ParsingErrorEventArgs(int chunkId, string stackTrace)
        {
            this.ChunkId = chunkId;
            this.StackTrace = stackTrace;
        }
        
        public int ChunkId { get; private set; }
        public string StackTrace { get; private set; }

        public override string ToString() => $"Chunk 0x{this.ChunkId:X8}.\n\n{this.StackTrace}";
    }

    public class BodyParsingErrorEventArgs
        : ParsingErrorEventArgs
    {
        public BodyParsingErrorEventArgs(int chunkId, long position, string stackTrace)
            : base(chunkId, stackTrace)
        {
            this.Position = position;
        }
        
        public long Position { get; private set; }

        public override string ToString() => $"Chunk 0x{this.ChunkId:X8} at {this.Position}.\n\n{this.StackTrace}";
    }

    public class NodeInternalParsingErrorEventArgs
        : ParsingErrorEventArgs
    {
        public NodeInternalParsingErrorEventArgs(Node node, int chunkId, string stackTrace)
            : base(chunkId, stackTrace)
        {
            this.ClassId = node.Class;
        }
        
        public uint ClassId { get; private set; }

        public override string ToString() => $"Chunk 0x{this.ChunkId:X8} in Node 0x{this.ClassId:X8}.\n\n{this.StackTrace}";
    }
}