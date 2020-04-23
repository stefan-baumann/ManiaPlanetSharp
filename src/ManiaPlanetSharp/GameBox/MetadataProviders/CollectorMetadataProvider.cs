﻿using ManiaPlanetSharp.GameBox.Parsing.Chunks;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Runtime.InteropServices;
using System.Text;

namespace ManiaPlanetSharp.GameBox.MetadataProviders
{
    public class CollectorMetadataProvider
        : MetadataProvider
    {
        public CollectorMetadataProvider(GameBoxFile file)
            : base(file)
        { }

        public virtual string Name => this.GetBufferedHeaderValue((CollectorMetadataChunk c) => c.Name2)
            .IfNull((CollectorMetadataChunk c) => c.Name)
            .IgnoreIfEmpty();

        public virtual string Author => this.GetBufferedHeaderValue((CollectorMetadataChunk c) => c.Author)
            .IgnoreIfEmpty();

        public virtual string Collection => this.GetBufferedHeaderValue((CollectorMetadataChunk c) => c.Collection)
            .IgnoreIfEmpty();

        public virtual string Description => this.GetBufferedBodyValue((CollectorDescriptionChunk c) => c.Description)
            .IgnoreIfEmpty();

        public virtual string Path => this.GetBufferedHeaderValue((CollectorMetadataChunk c) => c.Path)
            .IgnoreIfEmpty();

        public virtual ProductState? ProductState => this.GetBufferedHeaderValue((CollectorMetadataChunk c) => c?.ProductState);

        public virtual byte[] IconData => this.GetBufferedHeaderValue((CollectorIconChunk c) => c.IconData);

        public virtual Size? IconSize => this.GetBufferedHeaderValue((CollectorIconChunk c) => c?.Size);

        public virtual bool? UseAutoRenderedIcon => this.GetBufferedBodyValue((CollectorIconMetadataChunk c) => c?.UseAutoRenderedIcon);

        public virtual int? IconQuarterRotations => this.GetBufferedBodyValue((CollectorIconMetadataChunk c) => (int?)c.QuarterRotationY);



        public Bitmap GenerateIconBitmap()
        {
            if (this.IconData == null || this.IconSize == null)
            {
                return null;
            }

            using (Bitmap bmp = new Bitmap(this.IconSize.Value.Width, this.IconSize.Value.Height, PixelFormat.Format32bppArgb))
            {
                BitmapData data = bmp.LockBits(new Rectangle(0, 0, this.IconSize.Value.Width, this.IconSize.Value.Height), ImageLockMode.WriteOnly, PixelFormat.Format32bppArgb);
                Marshal.Copy(this.IconData, 0, data.Scan0, this.IconData.Length);
                bmp.UnlockBits(data);

                bmp.RotateFlip((new[] { RotateFlipType.RotateNoneFlipY, RotateFlipType.Rotate90FlipY, RotateFlipType.Rotate180FlipY, RotateFlipType.Rotate270FlipY })[this.IconQuarterRotations ?? 0]);

                return bmp.Clone(new Rectangle(0, 0, bmp.Width, bmp.Height), PixelFormat.Format32bppArgb);
            }
        }
    }
}