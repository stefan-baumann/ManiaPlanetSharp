﻿using System;
using System.Collections.Generic;
using System.Text;

namespace ManiaPlanetSharp.GameBox
{
    class GbxBannerProfileClass
        : GbxClass
    {
        public GbxBannerProfileClass() { }

        [GbxAutoProperty(0)]
        GbxFileReference BannerProfile { get; set; }
    }
}
