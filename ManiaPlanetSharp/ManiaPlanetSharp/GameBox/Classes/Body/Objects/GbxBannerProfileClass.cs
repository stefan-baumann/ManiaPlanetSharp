using System;
using System.Collections.Generic;
using System.Text;

namespace ManiaPlanetSharp.GameBox
{
    class GbxBannerProfileClass
        : GbxBodyClass
    {
        public GbxBannerProfileClass() { }

        [GbxAutoProperty(0)]
        GbxFileReference BannerProfile { get; set; }
    }
}
