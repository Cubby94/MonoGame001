using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections;
using System;
using MonoGame.Extended;
using MonoGame.Extended.ViewportAdapters;

namespace MonoGame001
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class Game1 : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        int worldSizeSquare;

        int[,] terrainArray;

        Texture2D grass;
        Texture2D dirt;

        Random rand;

        Camera2D camera;

        public Game1()
        { 
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
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
            worldSizeSquare = 64;
            var viewportAdapter = new BoxingViewportAdapter(Window, GraphicsDevice, 800, 480);
            rand = new Random();
            terrainArray = new int[worldSizeSquare, worldSizeSquare];
            for (int x = 0; x < worldSizeSquare; x++)
            {
                for (int y = 0; y < worldSizeSquare; y++)
                {
                    terrainArray[x, y] = rand.Next(0, 2);
                }
            }

            camera = new Camera2D(viewportAdapter);

            base.Initialize();
            graphics.PreferredBackBufferWidth = GraphicsDevice.DisplayMode.Width;
            graphics.PreferredBackBufferHeight = GraphicsDevice.DisplayMode.Height;
            graphics.IsFullScreen = true;
            graphics.ApplyChanges();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);
            grass = this.Content.Load<Texture2D>("grass1");
            dirt = Content.Load<Texture2D>("DirtTerrain");


            // TODO: use this.Content to load your game content here
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

            var keyboardState = Keyboard.GetState();
            const float movementSpeed = 0.25f;

            if (keyboardState.IsKeyDown(Keys.W))
                camera.Move(new Vector2(0, -movementSpeed) * gameTime.ElapsedGameTime.Milliseconds);
            if (keyboardState.IsKeyDown(Keys.S))
                camera.Move(new Vector2(0, movementSpeed) * gameTime.ElapsedGameTime.Milliseconds);
            if (keyboardState.IsKeyDown(Keys.A))
                camera.Move(new Vector2(-movementSpeed, 0) * gameTime.ElapsedGameTime.Milliseconds);
            if (keyboardState.IsKeyDown(Keys.D))
                camera.Move(new Vector2(movementSpeed, 0) * gameTime.ElapsedGameTime.Milliseconds);
            if (keyboardState.IsKeyDown(Keys.PageUp))
            {
                camera.ZoomIn(0.1f);
            }
            if (keyboardState.IsKeyDown(Keys.PageDown))
            {
                camera.ZoomOut(0.1f);
            }
            var mouseState = Mouse.GetState();
            //_worldPosition = _camera.ScreenToWorld(new Vector2(mouseState.X, mouseState.Y));

            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);
            var transformMatrix = camera.GetViewMatrix();
            spriteBatch.Begin(transformMatrix: transformMatrix);

            for (int x = 0; x < worldSizeSquare; x++)
            {
                for (int y = 0; y < worldSizeSquare; y++)
                {
                    if (terrainArray[x, y] == 0)
                    {
                        spriteBatch.Draw(grass, new Rectangle(x * 16, y * 16, 16, 16), Color.White);
                    }
                    else
                    {
                        spriteBatch.Draw(dirt, new Rectangle(x * 16, y * 16, 16, 16), Color.White);
                    }
                }
            }

            
            spriteBatch.End();

            // TODO: Add your drawing code here

            base.Draw(gameTime);
        }
    }
}
