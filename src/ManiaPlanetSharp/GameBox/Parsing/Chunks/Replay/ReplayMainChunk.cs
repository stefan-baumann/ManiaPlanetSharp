using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace ManiaPlanetSharp.GameBox.Parsing.Chunks
{
    [Chunk(0x03093002)]
    public class ReplayMainChunk
        : Chunk
    {
        [Property, CustomParserMethod(nameof(ParseContent))]
        public ReplayMainChunkContent Content { get; set; }

        public ReplayMainChunkContent ParseContent(GameBoxReader reader)
        {
            if (reader.BodyMode)
            {
                return ParserFactory.GetCustomStructParser<ReplayMainChunkBodyContent>().Parse(reader);
            }
            else
            {
                return ParserFactory.GetCustomStructParser<ReplayMainChunkHeaderContent>().Parse(reader);
            }
        }
    }

    public class ReplayMainChunkContent
    { }

    [CustomStruct]
    public class ReplayMainChunkHeaderContent
        : ReplayMainChunkContent
    {
        [Property]
        public uint Version { get; set; }

        [Property]
        public uint AuthorVersion { get; set; }

        [Property]
        public string Login { get; set; }

        [Property]
        public string Nickname { get; set; }

        [Property]
        public string Zone { get; set; }

        [Property]
        public string ExtraInfo { get; set; }
    }

    [CustomStruct]
    public class ReplayMainChunkBodyContent
        : ReplayMainChunkContent
    {
        [Property, Array]
        public byte[] MapData { get; set; }

        public GameBoxFile ParseMap()
        {
            if (this.MapData == null)
            {
                throw new ArgumentNullException(nameof(MapData));
            }
            if (this.MapData.Length == 0)
            {
                return new GameBoxFile();
            }

            using (MemoryStream stream = new MemoryStream(this.MapData))
            {
                return GameBoxFile.Parse(stream);
            }
        }
    }
}
