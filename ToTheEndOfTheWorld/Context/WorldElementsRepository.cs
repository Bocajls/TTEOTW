using ModelLibrary.Concrete.Blocks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;

namespace ToTheEndOfTheWorld.Context.StaticRepositories
{
    public class WorldElementsRepository : Dictionary<int, (string Name, Texture2D Texture, Block block)>
    {
        public WorldElementsRepository(ContentManager manager)
        {
            InitializeCollection(manager);
        }

        private void InitializeCollection(ContentManager manager)
        {
            var BreakingBlockInfo = new BlockInfo();
            var BreakingBlock = new Block(ID: -2, Ethereal: true, Info: BreakingBlockInfo);
            Add(-2, ("Breaking", manager.Load<Texture2D>("Graphics/World/Blocks/Breaking"), BreakingBlock));

            var BackgroundBlockInfo = new BlockInfo();
            var BackgroundBlock = new Block(ID: -1, Ethereal: true, Info: BackgroundBlockInfo);
            Add(-1, ("Background", manager.Load<Texture2D>("Graphics/World/Blocks/Background"), BackgroundBlock));

            var AirBlockInfo = new BlockInfo(MaximumDepth: 11, OccurrenceSpan: new Vector2(0f, 100f));
            var AirBlock = new Block(ID: 0, Ethereal: true, Info: AirBlockInfo);
            Add(0, ("Air", manager.Load<Texture2D>("Graphics/World/Blocks/Air"), AirBlock));

            var GrassBlockInfo = new BlockInfo(MinimumDepth: 12, MaximumDepth: 12, OccurrenceSpan: new Vector2(0f, 100f));
            var GrassBlock = new Block(ID: 1, Ethereal: false, Hardness: 0, Health: 50, Worth: 0, Info: GrassBlockInfo);
            Add(1, ("Grass", manager.Load<Texture2D>("Graphics/World/Blocks/Grass"), GrassBlock));

            var DirtBlockInfo = new BlockInfo(MinimumDepth: 13, OccurrenceSpan: new Vector2(0f, 100f));
            var DirtBlock = new Block(ID: 2, Ethereal: false, Hardness: 0, Health: 10, Worth: 0, Info: DirtBlockInfo);
            Add(2, ("Dirt", manager.Load<Texture2D>("Graphics/World/Blocks/Dirt"), DirtBlock));

            var GravelBlockInfo = new BlockInfo(MinimumDepth: 15, MaximumDepth: 150, OccurrenceSpan: new Vector2(0f, 2f));
            var GravelBlock = new Block(ID: 3, Ethereal: false, Hardness: 1, Health: 5, Worth: 1, Info: GravelBlockInfo);
            Add(3, ("Gravel", manager.Load<Texture2D>("Graphics/World/Blocks/Gravel"), GravelBlock));

            var StoneBlockInfo = new BlockInfo(MinimumDepth: 13, OccurrenceSpan: new Vector2(2f, 4f));
            var StoneBlock = new Block(ID: 4, Ethereal: false, Hardness: 20, Health: 50, Worth: 0, Info: StoneBlockInfo);
            Add(4, ("Stone", manager.Load<Texture2D>("Graphics/World/Blocks/Stone"), StoneBlock));

            var GraniteBlockInfo = new BlockInfo(MinimumDepth: 8000, OccurrenceSpan: new Vector2(4f, 5f));
            var GraniteBlock = new Block(ID: 5, Ethereal: false, Hardness: 50, Health: 100, Worth: 5, Info: GraniteBlockInfo);
            Add(5, ("Granite", manager.Load<Texture2D>("Graphics/World/Blocks/Granite"), GraniteBlock));

            var IronBlockInfo = new BlockInfo(MinimumDepth: 100, MaximumDepth: 800000, new Vector2(5f, 6f));
            var IronBlock = new Block(ID: 6, Ethereal: false, Hardness: 15, Health: 75, Worth: 10, Info: IronBlockInfo);
            Add(6, ("Iron", manager.Load<Texture2D>("Graphics/World/Blocks/Iron"), IronBlock));

            var LeadBlockInfo = new BlockInfo(MinimumDepth: 200, MaximumDepth: 40000, new Vector2(6f, 6.5f));
            var LeadBlock = new Block(ID: 7, Ethereal: false, Hardness: 5, Health: 50, Worth: 25, Info: LeadBlockInfo);
            Add(7, ("Lead", manager.Load<Texture2D>("Graphics/World/Blocks/Lead"), LeadBlock));

            var AluminumBlockInfo = new BlockInfo(MinimumDepth: 300, MaximumDepth: 60000, new Vector2(6.5f, 7f));
            var AluminumBlock = new Block(ID: 8, Ethereal: false, Hardness: 15, Health: 100, Worth: 30, Info: AluminumBlockInfo);
            Add(8, ("Aluminum", manager.Load<Texture2D>("Graphics/World/Blocks/Aluminum"), AluminumBlock));

            var ZincBlockInfo = new BlockInfo(MinimumDepth: 500, MaximumDepth: 80000, new Vector2(7f, 8f));
            var ZincBlock = new Block(ID: 9, Ethereal: false, Hardness: 20, Health: 150, Worth: 35, Info: ZincBlockInfo);
            Add(9, ("Zinc", manager.Load<Texture2D>("Graphics/World/Blocks/Zinc"), ZincBlock));

            var CopperBlockInfo = new BlockInfo(MinimumDepth: 800, MaximumDepth: 100000, new Vector2(8f, 8.5f));
            var CopperBlock = new Block(ID: 10, Ethereal: false, Hardness: 25, Health: 200, Worth: 95, Info: CopperBlockInfo);
            Add(10, ("Copper", manager.Load<Texture2D>("Graphics/World/Blocks/Copper"), CopperBlock));

            var NickelBlockInfo = new BlockInfo(MinimumDepth: 1200, MaximumDepth: 140000, new Vector2(8.5f, 9f));
            var NickelBlock = new Block(ID: 11, Ethereal: false, Hardness: 35, Health: 300, Worth: 250, Info: NickelBlockInfo);
            Add(11, ("Nickel", manager.Load<Texture2D>("Graphics/World/Blocks/Nickel"), NickelBlock));

            var TinBlockInfo = new BlockInfo(MinimumDepth: 400, MaximumDepth: 20000, new Vector2(9f, 9.5f));
            var TinBlock = new Block(ID: 12, Ethereal: false, Hardness: 10, Health: 50, Worth: 450, Info: TinBlockInfo);
            Add(12, ("Tin", manager.Load<Texture2D>("Graphics/World/Blocks/Tin"), TinBlock));

            var SilverBlockInfo = new BlockInfo(MinimumDepth: 1600, MaximumDepth: 200000, new Vector2(9.5f, 10f));
            var SilverBlock = new Block(ID: 13, Ethereal: false, Hardness: 20, Health: 85, Worth: 7500, Info: SilverBlockInfo);
            Add(13, ("Silver", manager.Load<Texture2D>("Graphics/World/Blocks/Silver"), SilverBlock));

            var GoldBlockInfo = new BlockInfo(MinimumDepth: 2400, MaximumDepth: 400000, new Vector2(10f, 10.25f));
            var GoldBlock = new Block(ID: 14, Ethereal: false, Hardness: 20, Health: 250, Worth: 58000, Info: GoldBlockInfo);
            Add(14, ("Gold", manager.Load<Texture2D>("Graphics/World/Blocks/Gold"), GoldBlock));

            var MythrilBlockInfo = new BlockInfo(MinimumDepth: 3600, MaximumDepth: 800000, new Vector2(10.25f, 10.50f));
            var MythrilBlock = new Block(ID: 15, Ethereal: false, Hardness: 500, Health: 2500, Worth: 250000, Info: MythrilBlockInfo);
            Add(15, ("Mythril", manager.Load<Texture2D>("Graphics/World/Blocks/Mythril"), MythrilBlock));

            var AdamantBlockInfo = new BlockInfo(MinimumDepth: 5400, MaximumDepth: 1600000, new Vector2(10.50f, 10.625f));
            var AdamantBlock = new Block(ID: 16, Ethereal: false, Hardness: 1500, Health: 8500, Worth: 1000000, Info: AdamantBlockInfo);
            Add(16, ("Adamant", manager.Load<Texture2D>("Graphics/World/Blocks/Adamant"), AdamantBlock));
        }
    }
}