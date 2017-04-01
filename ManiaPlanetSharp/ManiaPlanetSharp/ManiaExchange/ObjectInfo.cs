using System;
using System.Collections.Generic;
using System.Text;

namespace ManiaPlanetSharp.ManiaExchange
{
    public class ObjectInfo
    {
        public ObjectInfo()
        { }

        public string ObjectPath { get; set; }
        public string ObjectAuthor { get; set; }
        public object ExternalLink { get; set; }
        public string Name { get; set; }
    }
}
