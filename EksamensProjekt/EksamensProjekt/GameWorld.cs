using EksamensProjekt.MapGeneration;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;

namespace EksamensProjekt
{
    public class GameWorld : Game
    {
        private static GameWorld instance;
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        private GameManager _gameManager;
        private Menu menu;

        private WaveManager waveManager;
        private Texture2D enemyTexture;
        private Vector2 enemySpawnPosition = new Vector2(50, 50); // Example position

        private BuildMenu buildMenu;
       
        public GameWorld()
        {
            _graphics = new GraphicsDeviceManager(this);
            _graphics.PreferredBackBufferWidth = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Width;
            _graphics.PreferredBackBufferHeight = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Width;

            Content.RootDirectory = "Content";
            IsMouseVisible = true;

        }
        public static GameWorld GetInstance()
        {
            if (instance == null)
            {
                instance = new GameWorld();
            }
            return instance;
        }

        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            menu  = new Menu(GraphicsDevice, Content);
           // buildMenu  = new BuildMenu(GraphicsDevice, Content);

            Globals.WindowSize = new(1920, 1080);
            //_graphics.PreferredBackBufferWidth = Globals.WindowSize.X;
            //_graphics.PreferredBackBufferHeight = Globals.WindowSize.Y;
            _graphics.IsFullScreen = false;
            _graphics.ApplyChanges();

            Globals.Content = Content;
            _gameManager = new();

            //List<Vector2> path = Pathfinder.AStarPathfinding(goalX, goalY);
            //waveManager = new WaveManager(enemyTexture, enemySpawnPosition, path, 10, 1.0f); // 10 enemies per wave, 1 second between spawns
            base.Initialize();

            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            Globals.SpriteBatch = _spriteBatch;

            // TODO: use this.Content to load your game content here
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            _gameManager.Update();

            Globals.Update(gameTime);

            //waveManager.Update(gameTime);

            //if (waveManager.WaveCleared())
            //{
            //    waveManager.StartNextWave();
            //}

            // TODO: Add your update logic here

            menu.Update(gameTime);  
            //buildMenu.Update(gameTime);  

            base.Update(gameTime);
        }













        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            _gameManager.Draw();
            // TODO: Add your drawing code here
            _spriteBatch.Begin();
           menu.Draw(_spriteBatch);
           //buildMenu.Draw(_spriteBatch);
            _spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}