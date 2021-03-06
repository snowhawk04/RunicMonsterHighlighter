using ExileCore.Shared.Attributes;
using ExileCore.Shared.Interfaces;
using ExileCore.Shared.Nodes;
using SharpDX;

namespace RunicMonsterHighlighter
{
    public class RunicMonsterHighlighterSettings : ISettings
    {
        public RunicMonsterHighlighterSettings()
        {
            Enable = new ToggleNode(false);
            Scale = new RangeNode<float>(1.5f, 0.0f, 2.0f);
            BorderColor = new ColorNode(Color.Green);
            Size = new RangeNode<int>(16, 1, 50);
        }

        [Menu("Enable", 0)] public ToggleNode Enable { get; set; }
        [Menu("Scale", 1)] public RangeNode<float> Scale { get; set; }
        [Menu("Color", 2)] public ColorNode BorderColor { get; set; }
        [Menu("Size", 3)] public RangeNode<int> Size { get; set; }
    }
}