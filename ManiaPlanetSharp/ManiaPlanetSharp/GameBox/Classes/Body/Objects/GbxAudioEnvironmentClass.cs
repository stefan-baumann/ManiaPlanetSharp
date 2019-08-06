using System;
using System.Collections.Generic;
using System.Text;

namespace ManiaPlanetSharp.GameBox
{
    public class GbxAudioEnvironmentClass
        : GbxBodyClass
    {
        public GbxAudioEnvironmentClass() { }

        [GbxAutoProperty(0)]
        GbxNode InCarAudioEnvironment { get; set; }
    }
}
