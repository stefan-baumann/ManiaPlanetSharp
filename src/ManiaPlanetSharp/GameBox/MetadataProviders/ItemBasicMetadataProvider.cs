using ManiaPlanetSharp.GameBox.Parsing.Chunks;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;

namespace ManiaPlanetSharp.GameBox.MetadataProviders
{
    public class ItemBasicMetadataProvider
        : MetadataProvider
    {
        public ItemBasicMetadataProvider(GameBoxFile file)
            : base(file)
        {
            this.Name = this.GetHeaderNodes<CollectorMetadataChunk>()?.FirstOrDefault()?.Name2 ?? this.GetHeaderNodes<CollectorMetadataChunk>()?.FirstOrDefault()?.Name;
            this.Author = this.GetHeaderNodes<CollectorMetadataChunk>()?.FirstOrDefault()?.Author;
            this.Collection = this.GetHeaderNodes<CollectorMetadataChunk>()?.FirstOrDefault()?.Collection;
            this.Type = this.GetHeaderNodes<ObjectItemTypeChunk>()?.FirstOrDefault()?.ItemType;
            this.Path = this.GetHeaderNodes<CollectorMetadataChunk>()?.FirstOrDefault()?.Path;
            this.ProductState = this.GetHeaderNodes<CollectorMetadataChunk>()?.FirstOrDefault()?.ProductState;
            this.IconData = this.GetHeaderNodes<CollectorIconChunk>()?.FirstOrDefault()?.IconData;
            this.IconSize = this.GetHeaderNodes<CollectorIconChunk>()?.FirstOrDefault()?.Size;
        }

        public virtual string Name { get; protected set; }

        public virtual string Author { get; protected set; }

        public virtual string Collection { get; protected set; }

        public virtual ObjectType? Type { get; protected set; }

        public virtual string Path { get; protected set; }

        public virtual ProductState? ProductState { get; protected set; }

        public virtual byte[] IconData { get; protected set; }

        public virtual Size? IconSize { get; protected set; }

        //Not assigned currently
        public virtual uint? IconQuarterRotations { get; protected set; }

        public Bitmap GenerateIconBitmap()
        {
            if (this.IconData == null || this.IconSize == null)
            {
                return null;
            }

            Bitmap bmp = new Bitmap(this.IconSize.Value.Width, this.IconSize.Value.Height, PixelFormat.Format32bppArgb);
            BitmapData data = bmp.LockBits(new Rectangle(0, 0, this.IconSize.Value.Width, this.IconSize.Value.Height), ImageLockMode.WriteOnly, PixelFormat.Format32bppArgb);
            Marshal.Copy(this.IconData, 0, data.Scan0, this.IconData.Length);
            bmp.UnlockBits(data);

            bmp.RotateFlip((new[] { RotateFlipType.RotateNoneFlipY, RotateFlipType.Rotate90FlipY, RotateFlipType.Rotate180FlipY, RotateFlipType.Rotate270FlipY })[this.IconQuarterRotations ?? 0]);
            return bmp;
        }
    }
}
