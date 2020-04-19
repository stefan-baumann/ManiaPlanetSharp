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
        { }

        public virtual string Description => this.GetBufferedBodyValue((CollectorDescriptionChunk c) => c.Description)
            .IgnoreIfEmpty();

        public virtual bool? UseAutoRenderedIcon => this.GetBufferedBodyValue((CollectorIconMetadataChunk c) => c?.UseAutoRenderedIcon);

        public override int? IconQuarterRotations => this.GetBufferedBodyValue((CollectorIconMetadataChunk c) => (int?)c.QuarterRotationY);

        public virtual Vector3D? GroundPoint => this.GetBufferedBodyValue((ObjectGroundPointChunk c) => c?.GroundPoint);

        public virtual float? PainterGroundMargin => this.GetBufferedBodyValue((ObjectGroundPointChunk c) => c?.PainterGroundMargin);

        public virtual float? OrbitalCenterHeightFromGround => this.GetBufferedBodyValue((ObjectGroundPointChunk c) => c?.OrbitalCenterHeightFromGround);

        public virtual float? OrbitalRadiusBase => this.GetBufferedBodyValue((ObjectGroundPointChunk c) => c?.OrbitalRadiusBase);

        public virtual float? OrbitalPreviewAngle => this.GetBufferedBodyValue((ObjectGroundPointChunk c) => c?.OrbitalPreviewAngle);

        public virtual string MeshName => this.GetBufferedBodyValue((ObjectModelChunk c) => c.MeshName)
            .IgnoreIfEmpty();

        public virtual string ShapeName => this.GetBufferedBodyValue((ObjectModelChunk c) => c.ShapeName)
            .IgnoreIfEmpty();

        public virtual string TriggerShapeName => this.GetBufferedBodyValue((ObjectModelChunk c) => c.TriggerShapeName)
            .IgnoreIfEmpty();
    }
}