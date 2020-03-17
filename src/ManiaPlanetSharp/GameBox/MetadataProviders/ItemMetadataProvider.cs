using ManiaPlanetSharp.GameBox.Parsing.Chunks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ManiaPlanetSharp.GameBox.MetadataProviders
{
    public class ItemMetadataProvider
        : ItemBasicMetadataProvider
    {
        public ItemMetadataProvider(GameBoxFile file)
            : base(file)
        {
            this.ParseBody();
        }

        private string description;
        public virtual string Description => this.description ?? (this.description = this.GetBodyNodes<CollectorDescriptionChunk>()?.FirstOrDefault()?.Description);

        private bool? useAutoRenderedIcon;
        public virtual bool? UseAutoRenderedIcon => this.useAutoRenderedIcon ?? (this.useAutoRenderedIcon = this.GetBodyNodes<CollectorIconMetadataChunk>()?.FirstOrDefault()?.UseAutoRenderedIcon);

        private uint? quarterRotationY;
        public override uint? IconQuarterRotations => this.quarterRotationY ?? (this.quarterRotationY = this.GetBodyNodes<CollectorIconMetadataChunk>()?.FirstOrDefault()?.QuarterRotationY);

        private Vector3D? groundPoint;
        public virtual Vector3D? GroundPoint => this.groundPoint ?? (this.groundPoint = this.GetBodyNodes<ObjectGroundPointChunk>()?.FirstOrDefault()?.GroundPoint);

        private float? painterGroundMargin;
        public virtual float? PainterGroundMargin => this.painterGroundMargin ?? (this.painterGroundMargin = this.GetBodyNodes<ObjectGroundPointChunk>()?.FirstOrDefault()?.PainterGroundMargin);

        private float? orbitalCenterHeightFromGround;
        public virtual float? OrbitalCenterHeightFromGround => this.orbitalCenterHeightFromGround ?? (this.orbitalCenterHeightFromGround = this.GetBodyNodes<ObjectGroundPointChunk>()?.FirstOrDefault()?.OrbitalCenterHeightFromGround);

        private float? orbitalRadiusBase;
        public virtual float? OrbitalRadiusBase => this.orbitalRadiusBase ?? (this.orbitalRadiusBase = this.GetBodyNodes<ObjectGroundPointChunk>()?.FirstOrDefault()?.OrbitalRadiusBase);

        private float? orbitalPreviewAngle;
        public virtual float? OrbitalPreviewAngle => this.orbitalPreviewAngle ?? (this.orbitalPreviewAngle = this.GetBodyNodes<ObjectGroundPointChunk>()?.FirstOrDefault()?.OrbitalPreviewAngle);

        private string meshName;
        public virtual string MeshName => this.meshName ?? (this.meshName = this.GetBodyNodes<ObjectModelChunk>()?.FirstOrDefault()?.MeshName);

        private string shapeName;
        public virtual string ShapeName => this.shapeName ?? (this.shapeName = this.GetBodyNodes<ObjectModelChunk>()?.FirstOrDefault()?.ShapeName);

        private string triggerShapeName;
        public virtual string TriggerShapeName => this.triggerShapeName ?? (this.triggerShapeName = this.GetBodyNodes<ObjectModelChunk>()?.FirstOrDefault()?.TriggerShapeName);
    }
}