using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace ManiaPlanetSharp.GameBox
{
    /// <summary>
    /// Base class for all nodes and chunks.
    /// </summary>
    public class Node
    {
        /// <summary>
        /// Creates a new empty instance of the <c>Node</c> class.
        /// </summary>
        protected Node()
        { }

        /// <summary>
        /// Creates a new empty instance of the <c>Node</c> class with a specified id.
        /// </summary>
        /// <param name="id">The engine id of this node.</param>
        public Node(uint id)
            : this()
        {
            this.Id = id;
        }


        /// <summary>
        /// Creates a new instance of the <c>Node</c> class with a specified id.
        /// </summary>
        /// <param name="id">The engine id of this node.</param>
        /// <param name="chunks">The chunks of this node.</param>
        public Node(uint id, IEnumerable<Chunk> chunks)
            : this(id)
        {
            this.Chunks = chunks.ToList();
        }



        /// <summary>
        /// The id of this node.
        /// </summary>
        public virtual uint Id { get; set; }

        public virtual List<Chunk> Chunks { get; private set; } = new List<Chunk>();



        /// <summary>
        /// Returns the class name associated with this node's class id.
        /// </summary>
        /// <returns></returns>
        public virtual string GetClassName()
        {
            return GameBox.ClassIds.GetClassName(this.Id);
        }

        /// <summary>
        /// Returns a string that represents the current node.
        /// </summary>
        public override string ToString()
        {
            return $"{this.GetType().Name} (0x{this.Id:X8}/{this.GetClassName()})";
        }
    }
}
