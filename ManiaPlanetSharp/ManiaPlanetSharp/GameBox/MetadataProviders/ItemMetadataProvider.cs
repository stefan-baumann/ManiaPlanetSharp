using ManiaPlanetSharp.GameBox.Classes.Collector;
using ManiaPlanetSharp.GameBox.Classes.Object;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
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
        public byte[] Icon => this.GetHeaderNode<GbxCollectorIcon>()?.IconData;
        public bool? UseAutoRenderedIcon => this.GetBodyNode<GbxCollectorIconMetadataClass>()?.UseAutoRenderedIcon;
        public uint? IconQuarterRotations => this.GetBodyNode<GbxCollectorIconMetadataClass>()?.QuarterRotationY;
        public Vector3D? GroundPoint => this.GetBodyNode<ObjectGroundPoint>()?.GroundPoint;
        public float? PainterGroundMargin => this.GetBodyNode<ObjectGroundPoint>()?.PainterGroundMargin;
        public float? OrbitalCenterHeightFromGround => this.GetBodyNode<ObjectGroundPoint>()?.OrbitalCenterHeightFromGround;
        public float? OrbitalRadiusBase => this.GetBodyNode<ObjectGroundPoint>()?.OrbitalRadiusBase;
        public float? OrbitalPreviewAngle => this.GetBodyNode<ObjectGroundPoint>().OrbitalPreviewAngle;

        /** Todo
         * - [ ] Skins
         * - [ ] Cameras
         * - [ ] RaceInterface
         * - [ ] InCarAudioEnvironment
         * - [ ] PhysicalModel/VisualModel/VisualModelStatic
         */
    }
}
