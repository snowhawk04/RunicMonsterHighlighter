using ExileCore;
using ExileCore.PoEMemory.Components;
using ExileCore.PoEMemory.MemoryObjects;
using ExileCore.Shared;
using ExileCore.Shared.Abstract;
using ExileCore.Shared.Cache;
using ExileCore.Shared.Enums;
using ExileCore.Shared.Helpers;
using SharpDX;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RunicMonsterHighlighter
{
    public class RunicMonsterHighlighter : BaseSettingsPlugin<RunicMonsterHighlighterSettings>
    {
        private Dictionary<Entity, float> HighlightedEntities = new Dictionary<Entity, float>();


        public override void EntityAdded(Entity entity)
        {
            if (entity.Metadata.EndsWith("ExpeditionMarker"))
            {
                HighlightedEntities.Add(entity, entity.GetComponent<Positioned>().Scale);
                return;
            }
        }

        public override void EntityRemoved(Entity entity)
        {
            if (HighlightedEntities.ContainsKey(entity))
            {
                HighlightedEntities.Remove(entity);
            }
        }

        public override void Render()
        {
            RemoveNotValidEntities();
            foreach (var highlightedEntity in HighlightedEntities)
            {
                if (highlightedEntity.Value < Settings.Scale.Value) continue;

                var color = Settings.Color.Value;
                var size = Settings.Size.Value;
                DrawEntity(highlightedEntity.Key, color, size);
            }
        }

        private void RemoveNotValidEntities()
        {
            var removeList = new List<Entity>();
            foreach (var highlightedEntity in HighlightedEntities.Keys)
            {
                if (!highlightedEntity.IsValid)
                {
                    removeList.Add(highlightedEntity);
                }
            }
            foreach (var entityToBeRemoved in removeList)
            {
                HighlightedEntities.Remove(entityToBeRemoved);
            }
        }

        private void DrawEntity(Entity entity, Color color, int size)
        {
            var entityPos = GameController.IngameState.Camera.WorldToScreen(entity.Pos);
            Graphics.DrawFrame(new RectangleF(entityPos.X, entityPos.Y, size, size), color, size);
        }
    }
}
