﻿using System;
using System.Collections.Generic;
using System.Text;

namespace ManiaPlanetSharp.GameBox.Classes.Collector
{
    public class GbxCollectorDescriptionClass
    : Node
    {
        [AutoParserStringProperty(0, false)]
        public string Description { get; set; }
    }
}
