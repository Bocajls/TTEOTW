using Microsoft.Xna.Framework;

namespace ModelLibrary.Abstract.Blocks
{
    public abstract class ABlockInfo
    {
        public int MinimumDepth { get; set; }
        public int MaximumDepth { get; set; }
        public Vector2 OccurrenceSpan { get; set; }
    }
}
