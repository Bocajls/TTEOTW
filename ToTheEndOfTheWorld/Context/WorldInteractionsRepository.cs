using Microsoft.Xna.Framework;
using System.Collections.Generic;
using ModelLibrary.Concrete.Blocks;

namespace ToTheEndOfTheWorld.Context.StaticRepositories
{
    public class WorldInteractionsRepository : Dictionary<Vector2, Block>
    {
        public WorldInteractionsRepository()
        {
            // To contains previous X interactions user has had in the world.
            // Will contain a MetaBlock for X, Y coordinate with information.
        }
    }
}