namespace ManiaPlanetSharp.GameBox
{
    public class GbxFileReference
    {
        public byte Version { get; set; }
        public byte[] Checksum { get; set; }
        public string FilePath { get; set; }
        public bool RelativePath { get => this.Version < 2; }
        public string LocatorUrl { get; set; }
    }
}