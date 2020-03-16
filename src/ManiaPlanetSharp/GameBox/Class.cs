using System;
using System.Collections.Generic;
using System.Text;

namespace ManiaPlanetSharp.GameBox
{
    public abstract class Class
        : Node
    {
        public override uint Id
        {
            get => base.Id;
            set
            {
                if (value != (value & ~0xFFF)) throw new ArgumentException("The id is not a valid class id.");
                base.Id = value;
            }
        }
    }
}
