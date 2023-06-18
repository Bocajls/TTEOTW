using ModelLibrary.Concrete.PlayerShipComponents;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using ModelLibrary.Abstract.Types;
using ModelLibrary.Enums;
using System.Collections.Generic;

namespace ToTheEndOfTheWorld.Context.StaticRepositories
{
    public class GameItemsRepository : Dictionary<int, (string Name, Dictionary<PlayerOrientation, Texture2D> Textures, IType type)>
    {
        public GameItemsRepository(ContentManager manager)
        {
            InitializeCollection(manager);
        }

        private void InitializeCollection(ContentManager manager)
        {
            var ScrapPlating = new ThermalPlating(ID: 0, 0, 100, 2, "Scrap Thermal Plating", 5, 5);

            var ScrapThermalPlatingTextures = new Dictionary<PlayerOrientation, Texture2D>
            {
                { PlayerOrientation.Base, manager.Load<Texture2D>("Graphics/Player/ThermalPlatings/ScrapThermalPlating/ScrapThermalPlating") },
            };
            Add(0, ("ScrapThermalPlating", ScrapThermalPlatingTextures, ScrapPlating));


            var ScrapHullTextures = new Dictionary<PlayerOrientation, Texture2D>
            {
                { PlayerOrientation.Base, manager.Load<Texture2D>("Graphics/Player/Hulls/ScrapHull/Player") },
                { PlayerOrientation.Left, manager.Load<Texture2D>("Graphics/Player/Hulls/ScrapHull/PlayerLeft") },
                { PlayerOrientation.Up, manager.Load<Texture2D>("Graphics/Player/Hulls/ScrapHull/PlayerUp") },
                { PlayerOrientation.Right, manager.Load<Texture2D>("Graphics/Player/Hulls/ScrapHull/PlayerRight") },
                { PlayerOrientation.Down, manager.Load<Texture2D>("Graphics/Player/Hulls/ScrapHull/PlayerDown") }
            };
            Add(1, ("ScrapHull", ScrapHullTextures, new Hull(ID: 1, Durability: 5, Health: 50, Plating: new ThermalPlating(ScrapPlating), Name: "Scrap Hull", Worth: 5, Weight: 5)));


            var ScrapDrillTextures = new Dictionary<PlayerOrientation, Texture2D>
            {
                { PlayerOrientation.Left, manager.Load<Texture2D>("Graphics/Player/Drills/ScrapDrill/ScrapDrillLeft") },
                { PlayerOrientation.Up, manager.Load<Texture2D>("Graphics/Player/Drills/ScrapDrill/ScrapDrillUp") },
                { PlayerOrientation.Right, manager.Load<Texture2D>("Graphics/Player/Drills/ScrapDrill/ScrapDrillRight") },
                { PlayerOrientation.Down, manager.Load<Texture2D>("Graphics/Player/Drills/ScrapDrill/ScrapDrillDown") }
            };
            Add(2, ("ScrapDrill", ScrapDrillTextures, new Drill(ID: 2, Hardness: 5f, Damage: 0.1f, Plating: new ThermalPlating(ScrapPlating), Name: "Scrap Drill")));


            var ScrapEngineTextures = new Dictionary<PlayerOrientation, Texture2D>
            {
                { PlayerOrientation.Base, manager.Load<Texture2D>("Graphics/Player/Engines/ScrapEngine/ScrapEngine") }
            };
            Add(3, ("ScrapEngine", ScrapEngineTextures, new Engine(ID: 3, Speed: 2, Plating: new ThermalPlating(ScrapPlating), Name: "Scrap Engine", ActiveFuelConsumption: 1, Worth: 5, Weight: 5)));


            var ScrapFuelTankTextures = new Dictionary<PlayerOrientation, Texture2D>
            {
                { PlayerOrientation.Base, manager.Load<Texture2D>("Graphics/Player/FuelTanks/ScrapFuelTank/ScrapFuelTank") }
            };
            Add(4, ("ScrapFuelTank", ScrapFuelTankTextures, new FuelTank(ID: 4, Capacity: 200, Fuel: 200, Name: "Scrap Fuel Tank", Worth: 5, Weight: 5)));


            var ScrapThrusterTextures = new Dictionary<PlayerOrientation, Texture2D>
            {
                { PlayerOrientation.Base, manager.Load<Texture2D>("Graphics/Player/Thrusters/ScrapThruster/ScrapThruster") }
            };
            Add(5, ("ScrapThruster", ScrapThrusterTextures, new Thruster(ID: 5, Speed: 8, Acceleration: 0.1f, Power: 50, Plating: new ThermalPlating(ScrapPlating), Name: "Scrap Thruster", ActiveFuelConsumption: 2, Weight: 5, Worth: 5, MinimumVelocity: 2f)));
        }
    }
}