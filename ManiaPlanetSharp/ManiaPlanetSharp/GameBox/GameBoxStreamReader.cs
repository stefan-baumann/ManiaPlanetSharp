using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace ManiaPlanetSharp.GameBox
{
    /// <summary>
    /// Provides basic methods used for reading information from .gbx files.
    /// </summary>
    public class GameBoxStreamReader
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="GameBoxStreamReader"/> class.
        /// </summary>
        /// <param name="stream">The stream the gbx data should be read from.</param>
        public GameBoxStreamReader(Stream stream)
        {
            this.Stream = stream;
            this.Reader = new BinaryReader(stream);
        }



        /// <summary>
        /// Returns the stream the gbx data is read from.
        /// </summary>
        /// <value>
        /// The stream.
        /// </value>
        public Stream Stream { get; private set; }

        /// <summary>
        /// Returns the reader used for reading information from the gbx data stream.
        /// </summary>
        /// <value>
        /// The reader.
        /// </value>
        protected BinaryReader Reader { get; private set; }



        /// <summary>
        /// Skips the specified number of bytes.
        /// </summary>
        /// <param name="length">The length.</param>
        public void Skip(int length)
        {
            this.Reader.ReadBytes(length);
        }

        /// <summary>
        /// Fetches raw data.
        /// </summary>
        /// <param name="length">The length.</param>
        public byte[] ReadRaw(int length)
        {
            return this.Reader.ReadBytes(length);
        }

        public byte ReadByte()
        {
            return this.Reader.ReadByte();
        }

        public short ReadShort()
        {
            return this.Reader.ReadInt16();
        }

        public int ReadLong()
        {
            return this.Reader.ReadInt32();
        }

        public float ReadFloat()
        {
            return this.Reader.ReadSingle();
        }

        public string ReadChecksum()
        {
            //Original PHP code by Nadeo which does this
            //$checksum = unpack('H64', fread($fp, 32));
            //return $checksum[1];

            throw new NotImplementedException();
        }

        public string ReadString()
        {
            int length = this.ReadLong();
            if (length == 0)
            {
                return string.Empty;
            }
            byte[] data = this.Reader.ReadBytes(length);
            return Encoding.UTF8.GetString(data);
        }

        public string ReadLookbackString()
        {
            //Original PHP code by Nadeo which does this
            //if (empty(self::$lookbackStrings))
            //    self::ignore($fp, 4);
            //
            //$index = self::fetchLong($fp) & 0x3fffffff;
            //if ($index)
            //return self::$lookbackStrings[$index - 1];
            //
            //self::$lookbackStrings[] = $string = self::fetchString($fp);
            //return $string;

            throw new NotImplementedException();
        }

        public DateTime ReadDate()
        {
            //Original PHP code by Nadeo which does this
            //$date = unpack('v4', fread($fp, 8));
            //// create an int64 string representing the number of 100-nanoseconds since 01/01/1601 00:00:00
            //$date = array_reduce(array_reverse($date), function(&$res, $value) { return bcadd($value, bcmul($res, '65536')); }, '0');
            //// convert it to a number of seconds
            //$date = bcdiv($date, '10000000');
            //// substract the difference with EPOCH to get a Unix timestamp
            //$date = bcsub($date, '11644473600');
            //// return the DateTime object
            //return new \DateTime('@'.$date);

            throw new NotImplementedException();
        }
    }
}
