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
        public ParsingErrorEventArgs(int chunkId, string message)
        {
            this.ChunkId = chunkId;
            this.Message = message;
        }
        
        public int ChunkId { get; private set; }
        public string Message { get; private set; }

        public override string ToString() => $"Chunk 0x{this.ChunkId:X8}.\n\n{this.Message}";
    }

    public class BodyParsingErrorEventArgs
        : ParsingErrorEventArgs
    {
        public BodyParsingErrorEventArgs(int chunkId, long position, string message)
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
        public NodeInternalParsingErrorEventArgs(Node node, int chunkId, string message)
            : base(chunkId, message)
        {
            this.ClassId = node.Class;
        }
        
        public uint ClassId { get; private set; }

        public override string ToString() => $"Chunk 0x{this.ChunkId:X8} in Node 0x{this.ClassId:X8}.\n\n{this.Message}";
    }
}