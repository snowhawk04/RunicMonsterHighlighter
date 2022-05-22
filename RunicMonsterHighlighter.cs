namespace RunicMonsterHighlighter
{
    using System.Collections.Generic;
    using ExileCore;
    using ExileCore.PoEMemory.Components;
    using ExileCore.PoEMemory.MemoryObjects;
    using SharpDX;

    public class RunicMonsterHighlighter : BaseSettingsPlugin<RunicMonsterHighlighterSettings>
    {
        private readonly HashSet<Entity> _HighlightedEntities = new HashSet<Entity>();

        public override void EntityAdded(Entity entity)
        {
            if (entity != null
             && entity.HasComponent<Positioned>()
             && entity.Metadata.EndsWith("ExpeditionMarker")
             && entity.HasComponent<Animated>()
             && entity.GetComponent<Animated>().BaseAnimatedObjectEntity.Metadata.Contains("elitemarker"))
                this._HighlightedEntities.Add(entity);
        }

        public override void EntityRemoved(Entity entity)
        {
            this._HighlightedEntities.Remove(entity);
        }

        public override void AreaChange(AreaInstance area)
        {
            base.AreaChange(area);
            this._HighlightedEntities.Clear();
        }

        public override void Render()
        {
            this.RemoveInvalidEntities();

            foreach (var highlightedEntity in this._HighlightedEntities)
            {
                var color = this.Settings.BorderColor.Value;
                var size = this.Settings.Size.Value;
                this.DrawEntity(highlightedEntity, color, size);
            }
        }

        private void RemoveInvalidEntities()
        {
            this._HighlightedEntities.RemoveWhere(entity => !entity.IsValid);
        }

        private void DrawEntity(Entity entity, Color color, int size)
        {
            var entityPos = this.GameController.IngameState.Camera.WorldToScreen(entity.Pos);
            this.Graphics.DrawFrame(new RectangleF(entityPos.X, entityPos.Y, size, size), color, size);
        }
    }
}
