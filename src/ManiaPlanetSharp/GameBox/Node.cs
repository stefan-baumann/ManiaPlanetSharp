using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace ManiaPlanetSharp.GameBox
{
    /// <summary>
    /// Base class for all nodes and chunks.
    /// </summary>
    public abstract class Node
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
        protected Node(uint id)
            : this()
        {
            this.Id = id;
        }


        /// <summary>
        /// Creates a new instance of the <c>Node</c> class with a specified id.
        /// </summary>
        /// <param name="id">The engine id of this node.</param>
        /// <param name="data">The raw data of this node.</param>
        protected Node(uint id, byte[] data)
            : this(id)
        {
            this.Data = data;
        }



        /// <summary>
        /// The full id of this node.
        /// </summary>
        public virtual uint Id { get; set; }

        /// <summary>
        /// The class part of the id of this node.
        /// </summary>
        public virtual uint ClassId => this.Id & 0xFFFFF000;

        /// <summary>
        /// The chunk part of the id of this node.
        /// </summary>
        public virtual uint ChunkId => this.Id & 0xFFF;

        /// <summary>
        /// The raw data of this node.
        /// </summary>
        public byte[] Data { get; set; }



        /// <summary>
        /// Returns the class name associated with this node's class id.
        /// </summary>
        /// <returns></returns>
        public virtual string GetClassName()
        {
            return GameBox.ClassIds.GetClassName(this.ClassId);
        }

        /// <summary>
        /// Creates a <c>MemoryStream</c> with the raw data of this node.
        /// </summary>
        /// <returns></returns>
        public virtual Stream GetStream()
        {
            return new MemoryStream(this.Data ?? throw new NullReferenceException("This node does not have any stored data."));
        }

        /// <summary>
        /// Returns a string that represents the current node.
        /// </summary>
        public override string ToString()
        {
            if (ClassIds.TryMapToNewEngine(this.Id, out uint mappedId))
            {
                return $"{this.GetType().Name} (0x{this.Id:X8} -> 0x{mappedId:X8}/{this.GetClassName()})";
            }
            else
            {
                return $"{this.GetType().Name} (0x{this.Id:X8}/{this.GetClassName()})";
            }
        }
    }
}
