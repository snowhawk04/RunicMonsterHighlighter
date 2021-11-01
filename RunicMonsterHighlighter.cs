using ExileCore;
using ExileCore.PoEMemory.Components;
using ExileCore.PoEMemory.MemoryObjects;
using SharpDX;
using System.Collections.Generic;

namespace RunicMonsterHighlighter
{
    public class RunicMonsterHighlighter : BaseSettingsPlugin<RunicMonsterHighlighterSettings>
    {
        private readonly Dictionary<Entity, float> _HighlightedEntities = new Dictionary<Entity, float>();

        public override void EntityAdded(Entity entity)
        {
            if (entity != null && entity.HasComponent<Positioned>() && entity.Metadata.EndsWith("ExpeditionMarker"))
            {
                _HighlightedEntities.Add(entity, entity.GetComponent<Positioned>().Scale);
            }
        }

        public override void EntityRemoved(Entity entity)
        {
            if (_HighlightedEntities.ContainsKey(entity))
            {
                _HighlightedEntities.Remove(entity);
            }
        }

        public override void AreaChange(AreaInstance area)
        {
            base.AreaChange(area);
            _HighlightedEntities.Clear();
        }

        public override void Render()
        {
            RemoveInvalidEntities();
            foreach (var highlightedEntity in _HighlightedEntities)
            {
                if (highlightedEntity.Value < Settings.Scale.Value)
                {
                    continue;
                }

                var color = Settings.BorderColor.Value;
                var size = Settings.Size.Value;
                DrawEntity(highlightedEntity.Key, color, size);
            }
        }

        private void RemoveInvalidEntities()
        {
            _HighlightedEntities.RemoveAllByKey(entity => !entity.IsValid);
        }

        private void DrawEntity(Entity entity, Color color, int size)
        {
            var entityPos = GameController.IngameState.Camera.WorldToScreen(entity.Pos);
            Graphics.DrawFrame(new RectangleF(entityPos.X, entityPos.Y, size, size), color, size);
        }
    }
}