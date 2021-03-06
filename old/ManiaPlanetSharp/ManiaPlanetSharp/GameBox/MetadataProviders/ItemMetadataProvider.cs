﻿using ManiaPlanetSharp.GameBox.Classes.Collector;
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
        public float? OrbitalPreviewAngle => this.GetBodyNode<ObjectGroundPoint>()?.OrbitalPreviewAngle;

        public string MeshName => this.GetBodyNode<ObjectModel>()?.MeshName;
        public string ShapeName => this.GetBodyNode<ObjectModel>()?.ShapeName;
        public string TriggerShapeName => this.GetBodyNode<ObjectModel>()?.TriggerShapeName;

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

        /** Todo
         * - [ ] Skins
         * - [ ] Cameras
         * - [ ] RaceInterface
         * - [ ] InCarAudioEnvironment
         * - [ ] PhysicalModel/VisualModel/VisualModelStatic
         */
    }
}
