using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;
using System;
using ToTheEndOfTheWorld.Context.StaticRepositories;
using ModelLibrary.Concrete;
using ModelLibrary.Concrete.PlayerShipComponents;
using ModelLibrary.Concrete.Grids;
using ModelLibrary.Concrete.Blocks;
using ModelLibrary.Enums;
using System.Linq;
using UtilityLibrary;
using ModelLibrary.Context;
using System.Threading.Tasks;

namespace ToTheEndOfTheWorld
{
    public class MainGame : Game
    {
        private static readonly string GameTitle = "To The End Of The World";
        private static readonly string GameVersion = "V0.01";
        private static readonly int _pixels = 64;

        private readonly GraphicsDeviceManager graphics;
        private SpriteBatch spriteBatch;
        private WorldInteractionsRepository interactions;
        private WorldElementsRepository blocks;
        private GameItemsRepository items;
        private World world;

        // private ItemSpriteRepository _items;

        public MainGame()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override async void Initialize()
        {
            blocks = new WorldElementsRepository(Content);
            items = new GameItemsRepository(Content);

            var _blocksWide = (GraphicsDevice.DisplayMode.Width - (GraphicsDevice.DisplayMode.Width % _pixels)) / _pixels;
            var _blocksHigh = (GraphicsDevice.DisplayMode.Height - (GraphicsDevice.DisplayMode.Height % _pixels)) / _pixels;

            _blocksWide -= _blocksWide % 2;
            _blocksHigh -= _blocksHigh % 2;

            Window.Title = $"{GameTitle} {GameVersion}";
            graphics.PreferredBackBufferWidth = _blocksWide * _pixels;
            graphics.PreferredBackBufferHeight = _blocksHigh * _pixels;
            graphics.IsFullScreen = false;
            graphics.ApplyChanges();

            interactions = new WorldInteractionsRepository();

            world = ContextHandler.LoadWorld();

            world ??= CreateNewWorld(_blocksWide, _blocksHigh);

            base.Initialize();
        }

        private World CreateNewWorld(int _blocksWide, int _blocksHigh)
        {
            var player = new Player(
                Engine: new Engine(items[3].type as Engine),
                Hull: new Hull(items[1].type as Hull),
                Drill: new Drill(items[2].type as Drill),
                Inventory: new Inventory(ID: 100, new Grid(ID: 99, new Vector2(0, 0), new GridBox[3, 3]), SizeLimit: 576, Name: "Starter Inventory", Worth: 10, Weight: 0),
                Thruster: new Thruster(items[5].type as Thruster),
                FuelTank: new FuelTank(items[4].type as FuelTank)
            )
            {
                Coordinates = new Vector2((float)Math.Floor(_blocksWide / 2.0d), (float)Math.Floor(_blocksHigh / 2.0d))
            };

            var createdWorldRender = new Dictionary<Vector2, Vector2>();

            for (var x = 0; x <= _blocksWide; x++)
            {
                for (var y = 0; y <= _blocksHigh; y++)
                {
                    createdWorldRender.Add(new Vector2(x, y), new Vector2(x, y));
                }
            }

            return new World(
                Player: player,                                  // ContextHandler.LoadPlayer();
                Buildings: null,                                 // ContextHandler.LoadBuildings();
                BlocksWide: _blocksWide,                         // Calculated
                BlocksHigh: _blocksHigh,                         // Calculated
                WorldRender: createdWorldRender,                 // Dynamically updated
                WorldTrails: new Dictionary<Vector2, bool>()     // ContextHandler.LoadWorldTrails();
            );
        }

        protected override async void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);
        }

        protected override async void Update(GameTime gameTime)
        {
            KeyboardState state = Keyboard.GetState();
            if (state.IsKeyDown(Keys.LeftControl) && state.IsKeyDown(Keys.S))
            {
                ContextHandler.SaveWorld(world);
            }

            var player = world.Player;
            var oldDirection = player.Direction;
            var newDirection = player.SetDirectionFromInput(state);
            var location = world.WorldRender[new Vector2(player.Coordinates.X, player.Coordinates.Y)];
            var nextBlockVector = new Vector2(location.X + newDirection.X, location.Y + newDirection.Y);
            var nextBlock = GetWorldBlock(nextBlockVector.X, nextBlockVector.Y).Value.Block;

            if (oldDirection != newDirection || (newDirection.X == 0 && newDirection.Y == 0))
            {
                player.ResetOffset();
                player.Mining = false;
            }


            if (!Obstructed(nextBlock, nextBlockVector))
            {
                player.UpdateVelocity();
                player.UpdateOffset();

                var blocksToMove = player.BlocksToMove(_pixels);

                if (blocksToMove > 0)
                {
                    Block checkBlock;
                    Vector2 checkBlockVector;
                    float checkLocationX;
                    float checkLocationY;

                    var i = 1;
                    while (i < blocksToMove)
                    {
                        checkLocationX = location.X + (newDirection.X * i);
                        checkLocationY = location.Y + (newDirection.Y * i);

                        checkBlockVector = new Vector2(checkLocationX, checkLocationY);
                        checkBlock = GetWorldBlock(checkBlockVector.X, checkBlockVector.Y).Value.Block;

                        if (Obstructed(checkBlock, checkBlockVector)) break;

                        i++;
                    }
                    player.SubstractOffset(_pixels * i);
                    MoveScreen(newDirection.X * i, newDirection.Y * i);
                }
            }

            if (Obstructed(nextBlock, nextBlockVector))
            {
                player.Mining = true;
                player.ResetVelocity();
                player.ResetOffset();
                DealDamageToBlock(nextBlockVector.X, nextBlockVector.Y);

                if (!Obstructed(nextBlock, nextBlockVector))
                {
                    MoveScreen(newDirection.X, newDirection.Y);
                }
            }

            base.Update(gameTime);
        }

        protected override async void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            spriteBatch.Begin();

            DrawRenderedWorld();
            //DrawRenderedBuildings();
            DrawPlayerShip();
            DrawStatistics();

            spriteBatch.End();

            base.Draw(gameTime);
        }

        private void DrawStatistics()
        {
            var font = Content.Load<SpriteFont>("Fonts/text");
            var first = world.WorldRender.OrderBy(x => x.Key.X).OrderBy(x => x.Key.Y).FirstOrDefault();
            var player = world.Player;
            spriteBatch.DrawString(font, $"World position: X: {first.Value.X}, Y: {first.Value.Y}", new Vector2(5, 5), Color.Black);
            spriteBatch.DrawString(font, $"Player velocity: X: {player.XVelocity}, Y: {player.YVelocity}", new Vector2(5, 25), Color.Black);
            spriteBatch.DrawString(font, $"Player offset: X: {player.XOffset}, Y: {player.YOffset}", new Vector2(5, 45), Color.Black);
        }

        private void DrawRenderedWorld()
        {
            foreach (var pair in world.WorldRender)
            {
                var player = world.Player;
                //var location = new Vector2((pair.Key.X * _pixels) - (player.XOffset), (pair.Key.Y * _pixels) - (player.YOffset));
                var location = new Vector2(pair.Key.X * _pixels, pair.Key.Y * _pixels);

                if (world.WorldTrails.ContainsKey(pair.Value))
                {
                    spriteBatch.Draw(blocks.First(x => x.Key == -1).Value.Texture, location, Color.White);
                }
                else
                {
                    var block = GetWorldBlock(pair.Value.X, pair.Value.Y);

                    spriteBatch.Draw(block.Value.Texture, location, Color.White);

                    if (interactions.ContainsKey(pair.Value))
                    {
                        var percentDamaged = interactions[pair.Value].PercentDamaged();

                        spriteBatch.Draw(blocks.First(x => x.Key == -2).Value.Texture, location, Color.White * percentDamaged);
                    }
                }
            }
        }

        private void DrawPlayerShip()
        {
            Vector2 PlayerPosition = new Vector2(
                GetCenterScreenCoordinates().X,
                GetCenterScreenCoordinates().Y
            );

            var player = world.Player;
            var orientation = player.Orientation;
            var mining = player.Mining;
            var drill = items[player.Drill.ID];
            var hull = items[player.Hull.ID];

            if (orientation.Equals(PlayerOrientation.Base))
            {
                spriteBatch.Draw(hull.Textures[PlayerOrientation.Base], PlayerPosition, Color.White);
            }
            else
            {
                if (mining)
                {
                    var drillPositionX = GetCenterScreenCoordinates().X + (player.Direction.X * _pixels);
                    var drillPositionY = GetCenterScreenCoordinates().Y + (player.Direction.Y * _pixels);

                    spriteBatch.Draw(drill.Textures[orientation], new Vector2(drillPositionX, drillPositionY), Color.White);

                    spriteBatch.Draw(hull.Textures[orientation], PlayerPosition, Color.White);
                }
                else
                {

                    spriteBatch.Draw(hull.Textures[PlayerOrientation.Base], PlayerPosition, Color.White);
                }
                if (player.MaximumActiveVelocity > 0)
                {
                    // draw thrusters
                }
            }
        }

        private Vector2 GetCenterScreenCoordinates()
        {
            return new Vector2(
                (float)(graphics.PreferredBackBufferWidth / 2.0),
                (float)(graphics.PreferredBackBufferHeight / 2.0)
            );
        }

        private void DealDamageToBlock(float x, float y)
        {
            var vector = new Vector2(x, y);
            var block = GetWorldBlock(x, y).Value.Block;

            if (interactions.ContainsKey(vector) == false)
            {
                block.OnBlockDestroyed += (sender, e) => OnBlockDestroyed(sender as Block, e, vector);
                interactions.Add(vector, block);
            }

            if (interactions[vector].Hardness <= world.Player.Drill.Hardness)
            {
                interactions[vector].TakeDamage(world.Player.Drill.Damage);
            }
        }

        private void OnBlockDestroyed(Block block, EventArgs e, Vector2 location)
        {
            world.WorldTrails.Add(location, true);
            interactions.Remove(location);
        }

        private KeyValuePair<int, (string Name, Texture2D Texture, Block Block)> GetWorldBlock(float x, float y)
        {
            var simplex = (float)SimplexNoise.Singleton.Noise01(x, y) * 100.0f;

            foreach (var block in blocks.OrderByDescending(x => x.Key))
            {
                var info = block.Value.block.Info;

                if (y > info.MaximumDepth || y < info.MinimumDepth)
                {
                    continue;
                }

                if (simplex >= info.OccurrenceSpan.X && simplex <= info.OccurrenceSpan.Y)
                {
                    var keyValuePair = new KeyValuePair<int, (string Name, Texture2D Texture, Block Block)>
                    (
                        block.Key, (block.Value.Name, block.Value.Texture, new Block(block.Value.block))
                    );

                    if (block.Key == 2 && x > 0) // Dirt gets compressed slowly
                    {
                        //keyValuePair.Value.Block.Hardness += 0.01f * x; // TODO: Implement this.
                        keyValuePair.Value.Block.CurrentHealth += 0.01f * x;
                        keyValuePair.Value.Block.MaximumHealth += 0.01f * x;
                    }
                    return keyValuePair;
                }
            }

            return new KeyValuePair<int, (string Name, Texture2D Texture, Block Block)>(-1, (null, null, null));
        }

        private bool Obstructed(Block block, Vector2 nextBlock)
        {
            if (block.Ethereal || world.WorldTrails.ContainsKey(nextBlock))
            {
                return false;
            }
            return true;
        }

        private void MoveScreen(float x, float y)
        {
            var updated = new Dictionary<Vector2, Vector2>();

            foreach (var block in world.WorldRender)
            {
                updated.Add(new Vector2(block.Key.X, block.Key.Y), new Vector2(block.Value.X + x, block.Value.Y + y));
            }

            world.WorldRender = updated;
        }
    }
}