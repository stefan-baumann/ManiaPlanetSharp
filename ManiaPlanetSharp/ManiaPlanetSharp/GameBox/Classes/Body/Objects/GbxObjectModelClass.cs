using System;
using System.Collections.Generic;
using System.Text;

namespace ManiaPlanetSharp.GameBox
{
    public class GbxObjectModelClass
        : GbxClass
    {
        public GbxObjectModelClass() { }

        public int Version { get; set; }
        public GbxNode PhysicalModel { get; set; }
        public GbxNode VisualModel { get; set; }
        public GbxNode VisualModelStatic { get; set; }
    }

    public class GbxObjectModelClassParser
        : GbxClassParser<GbxObjectModelClass>
    {
        protected override int ChunkId => 0x2E002019;

        protected override GbxObjectModelClass ParseChunkInternal(GbxReader reader)
        {
            var result = new GbxObjectModelClass()
            {
                Version = reader.ReadInt32(),
                PhysicalModel = reader.ReadNodeReference(),
                VisualModel = reader.ReadNodeReference()
            };
            if (result.Version >= 1)
            {
                result.VisualModelStatic = reader.ReadNodeReference();
            }

            return result;
        }
    }
}
