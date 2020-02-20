using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace PlatformerExample
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class Game1 : Game
    {

        Random rand = new Random();
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        SpriteSheet sheet;
        Player player;
        List<Platform> platforms;
        List<Rocks> rocks;
        AxisList world;
        AxisList rockWorld;
        Rectangle keyRect = new Rectangle(1125, 570, 40, 40);
        Sprite keySprite;
        int score = 0;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            platforms = new List<Platform>();
            rocks = new List<Rocks>();
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            graphics.PreferredBackBufferWidth = 1200;
            graphics.PreferredBackBufferHeight = 1000;
            graphics.ApplyChanges();


            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);

            // TODO: use this.Content to load your game content here
            var t = Content.Load<Texture2D>("spritesheet");
            sheet = new SpriteSheet(t, 21, 21, 3, 2);

            // Create the player with the corresponding frames from the spritesheet
            var playerFrames = from index in Enumerable.Range(19, 30) select sheet[index];
            player = new Player(playerFrames);

            // Create the platforms
            platforms.Add(new Platform(new BoundingRectangle(0, 979, 1250, 40), sheet[3]));
            platforms.Add(new Platform(new BoundingRectangle(0, 860, 200, 40), sheet[3]));
            platforms.Add(new Platform(new BoundingRectangle(0, 860, 200, 40), sheet[3]));
            platforms.Add(new Platform(new BoundingRectangle(300, 760, 400, 40), sheet[3]));
            platforms.Add(new Platform(new BoundingRectangle(900, 710, 200, 40), sheet[3]));
            platforms.Add(new Platform(new BoundingRectangle(1100, 620, 100, 40), sheet[3]));

            rocks.Add(new Rocks(new BoundingRectangle(rand.Next(900, 1150), -100, 40, 40), sheet[376]));
            rocks.Add(new Rocks(new BoundingRectangle(rand.Next(650, 900), -100, 40, 40), sheet[376]));
            rocks.Add(new Rocks(new BoundingRectangle(rand.Next(450, 600), -100, 40, 40), sheet[376]));
            rocks.Add(new Rocks(new BoundingRectangle(rand.Next(250, 450), -100, 40, 40), sheet[376]));
            rocks.Add(new Rocks(new BoundingRectangle(rand.Next(250), -100, 40, 40), sheet[376]));


            // Add the platforms to the axis list
            world = new AxisList();
            rockWorld = new AxisList();
            foreach (Platform platform in platforms)
            {
                world.AddGameObject(platform);
            }

            foreach (Rocks rock in rocks)
            {
                rockWorld.AddGameObject(rock);
            }

            keySprite = sheet[14];
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// game-specific content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            // TODO: Add your update logic here
            player.Update(gameTime);

            //Debug.WriteLine($"Checking collisions against {rocks.Count()} rocks");
            foreach (Rocks rock in rocks)
            {
                rock.fall(rand.Next(1160), rand.Next(-200, -50), (float)(1+rand.NextDouble()));
            }
            

            // Check for platform collisions
            var platformQuery = world.QueryRange(player.Bounds.X-50, player.Bounds.X + player.Bounds.Width);
            var rockQuery = rockWorld.QueryRange(0,1200);
            player.CheckForPlatformCollision(platformQuery);
            player.CheckForRockCollisions(rockQuery);

            if (keyRect.Intersects(player.Bounds)) Exit();

            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.DarkSlateBlue);

            // TODO: Add your drawing code here
            spriteBatch.Begin();

            keySprite.Draw(spriteBatch, keyRect, Color.Yellow);

            // Draw the platforms 
            platforms.ForEach(platform =>
            {
                platform.Draw(spriteBatch);
            });

            rocks.ForEach(rock =>
            {
                rock.Draw(spriteBatch);
            });

            // Draw the player
            player.Draw(spriteBatch);
            
            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
