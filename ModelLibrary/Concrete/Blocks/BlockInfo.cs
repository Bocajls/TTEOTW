using Microsoft.Xna.Framework;
using Newtonsoft.Json;
using ModelLibrary.Abstract.Blocks;

namespace ModelLibrary.Concrete.Blocks
{
    public class BlockInfo : ABlockInfo
    {
        public BlockInfo()
        {

        }

        [JsonConstructor]
        public BlockInfo(int MinimumDepth = -int.MaxValue, int MaximumDepth = int.MaxValue, Vector2 OccurrenceSpan = new Vector2())
        {
            this.MinimumDepth = MinimumDepth;
            this.MaximumDepth = MaximumDepth;

            if (OccurrenceSpan is { })
            {
                this.OccurrenceSpan = OccurrenceSpan;
            }
            else
            {
                this.OccurrenceSpan = new Vector2(0, 0);
            }
        }
    }
}