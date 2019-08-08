using System;
using System.Collections.Generic;
using System.Text;

namespace ManiaPlanetSharp.GameBox.Classes.Object
{
    public class ObjectModel
        : Node
    {
        public int Version { get; set; }
        public Node PhysicalModel { get; set; }
        public Node VisualModel { get; set; }
        public Node VisualModelStatic { get; set; }
    }

    public class ObjectModelParser
        : ClassParser<ObjectModel>
    {
        protected override int ChunkId => 0x2E002019;

        protected override ObjectModel ParseChunkInternal(GameBoxReader reader)
        {
            var result = new ObjectModel()
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
