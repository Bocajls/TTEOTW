using Microsoft.Xna.Framework;
using ModelLibrary.Abstract.Buildings;
using ModelLibrary.Abstract;

namespace ModelLibrary.Concrete
{
    public class World : AWorld
    {
        public World(Player Player, List<ABuilding> Buildings, int BlocksWide, int BlocksHigh, Dictionary<Vector2, Vector2> WorldRender, Dictionary<Vector2, bool> WorldTrails)
        {
            this.Player = Player;
            this.Buildings = Buildings;
            this.BlocksWide = BlocksWide;
            this.BlocksHigh = BlocksHigh;
            this.WorldRender = WorldRender;
            this.WorldTrails = WorldTrails;
        }
    }
}