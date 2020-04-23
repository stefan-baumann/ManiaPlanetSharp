using System;
using System.Collections.Generic;
using System.Text;

namespace ManiaPlanetSharp.GameBox.MetadataProviders
{
    public class MacroBlockMetadataProvider
        : CollectorMetadataProvider
    {
        public MacroBlockMetadataProvider(GameBoxFile file)
            : base(file)
        { }
    }
}
