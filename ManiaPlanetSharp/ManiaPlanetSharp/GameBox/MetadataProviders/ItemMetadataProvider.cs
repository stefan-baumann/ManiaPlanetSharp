using ManiaPlanetSharp.GameBox.Classes.Collector;
using ManiaPlanetSharp.GameBox.Classes.Object;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;

namespace ManiaPlanetSharp.GameBox.MetadataProviders
{
    public class ItemMetadataProvider
        : MetadataProvider
    {
        public ItemMetadataProvider(Stream stream)
            : base(stream)
        { }

        public ItemMetadataProvider(GameBoxFile file)
            : base(file)
        { }
        
        public string Name => this.GetHeaderNode<GbxCollectorMainDescriptionClass>()?.Name ?? this.GetBodyNode<GbxCollectorNameClass>()?.Name ?? this.GetBodyNode<GbxCollectorBasicMetadataClass>()?.Name;
        public string Author => this.GetHeaderNode<GbxCollectorMainDescriptionClass>()?.Author ?? this.GetBodyNode<GbxCollectorBasicMetadataClass>()?.Author;
        public string Collection => this.GetHeaderNode<GbxCollectorMainDescriptionClass>()?.Collection ?? this.GetBodyNode<GbxCollectorBasicMetadataClass>()?.Collection;
        public string Description => this.GetBodyNode<GbxCollectorDescriptionClass>()?.Description;
        public ObjectType? Type => this.GetHeaderNode<ObjectTypeInfo>()?.ObjectType;
        public string Path => this.GetHeaderNode<GbxCollectorMainDescriptionClass>()?.Path ?? this.GetBodyNode<GbxCollectorBrowserMetadataClass>()?.PagePath;
        public ProductState? ProductState => this.GetHeaderNode<GbxCollectorMainDescriptionClass>()?.ProductState;
        public byte[] IconData => this.GetHeaderNode<GbxCollectorIcon>()?.IconData;
        public Size? IconSize => this.GetHeaderNode<GbxCollectorIcon>()?.Size;
        public bool? UseAutoRenderedIcon => this.GetBodyNode<GbxCollectorIconMetadataClass>()?.UseAutoRenderedIcon;
        public uint? IconQuarterRotations => this.GetBodyNode<GbxCollectorIconMetadataClass>()?.QuarterRotationY;
        public Vector3D? GroundPoint => this.GetBodyNode<ObjectGroundPoint>()?.GroundPoint;
        public float? PainterGroundMargin => this.GetBodyNode<ObjectGroundPoint>()?.PainterGroundMargin;
        public float? OrbitalCenterHeightFromGround => this.GetBodyNode<ObjectGroundPoint>()?.OrbitalCenterHeightFromGround;
        public float? OrbitalRadiusBase => this.GetBodyNode<ObjectGroundPoint>()?.OrbitalRadiusBase;
        public float? OrbitalPreviewAngle => this.GetBodyNode<ObjectGroundPoint>().OrbitalPreviewAngle;

        public Bitmap GenerateIconBitmap()
        {
            if (this.IconData == null || this.IconSize == null)
            {
                return null;
            }

            byte[] aligned = new byte[this.IconSize.Value.Width * this.IconSize.Value.Height * 4];
            for (int i = 0; i < this.IconData.Length - 3; i += 4)
            {
                aligned[i] = this.IconData[i + 3];
                aligned[i + 1] = this.IconData[i];
                aligned[i + 2] = this.IconData[i + 1];
                aligned[i + 3] = this.IconData[i + 2];
            }

            Bitmap bmp = new Bitmap(this.IconSize.Value.Width, this.IconSize.Value.Height, PixelFormat.Format32bppArgb);
            BitmapData data = bmp.LockBits(new Rectangle(0, 0, this.IconSize.Value.Width, this.IconSize.Value.Height), ImageLockMode.WriteOnly, PixelFormat.Format32bppArgb);
            Marshal.Copy(aligned, 0, data.Scan0, aligned.Length);
            bmp.UnlockBits(data);

            bmp.RotateFlip((new[] { RotateFlipType.RotateNoneFlipY, RotateFlipType.Rotate90FlipY, RotateFlipType.Rotate180FlipY, RotateFlipType.Rotate270FlipY })[this.IconQuarterRotations ?? 0]);
            return bmp;
        }

        /** Todo
         * - [ ] Skins
         * - [ ] Cameras
         * - [ ] RaceInterface
         * - [ ] InCarAudioEnvironment
         * - [ ] PhysicalModel/VisualModel/VisualModelStatic
         */
    }
}
