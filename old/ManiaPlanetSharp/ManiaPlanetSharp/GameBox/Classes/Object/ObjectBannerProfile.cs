using System;
using System.Collections.Generic;
using System.Text;

namespace ManiaPlanetSharp.GameBox.Classes.Object
{
    public class ObjectBannerProfile
        : Node
    {
        [AutoParserProperty(0)]
        FileReference BannerProfile { get; set; }
    }
}
