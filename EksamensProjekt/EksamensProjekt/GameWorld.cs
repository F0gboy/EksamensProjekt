using EksamensProjekt.DesignPatterns.ComponentPattern;
using EksamensProjekt.MapGeneration;
using EksamensProjekt.State_Pattern;
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
        private Menu menu;
        private BuildMenu buildMenu;
        private EndMenu endMenu;

        private GameManager _gameManager;
        private WaveManager waveManager;
        private Texture2D enemyTexture;
        private UI_liv_money uI_Liv_Money;
        private GraphicsDevice _graphicsDevice;
        private StartGame_State_Menu startGameState;

        

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
            buildMenu  = new BuildMenu(GraphicsDevice, Content, _spriteBatch);
            endMenu  = new EndMenu(GraphicsDevice, Content, _spriteBatch, this);

            Globals.WindowSize = new(1920, 1080);

            menu = new Menu(GraphicsDevice, Content);

            _graphics.IsFullScreen = false;
            _graphics.ApplyChanges();

            uI_Liv_Money = new UI_liv_money(Content);
            Globals.Content = Content;

            Texture2D normalEnemyTexture = Content.Load<Texture2D>("sæl");
            Texture2D strongEnemyTexture = Content.Load<Texture2D>("orca");

            _gameManager = new GameManager();
            List<Vector2> pathPoints = _gameManager.PathPoints;

            waveManager = new WaveManager(normalEnemyTexture, strongEnemyTexture, pathPoints, 0.5f, 60f, 3.0f); // Adjust timeBetweenSpawns, enemySpeed, and timeBetweenWaves as needed

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

            

            if (Globals.life > 0)
            {

            _gameManager.Update();


            if (menu.gameStart)
            {
               buildMenu.Update(gameTime, waveManager.GetEnemies());
            }

            menu.Update(gameTime);

            if (Globals.gameStarted)
            {
                Globals.Update(gameTime);
                waveManager.Update(gameTime);
                uI_Liv_Money.Update(gameTime);
                Globals.gameTime = gameTime;
            }
            
            menu.Update(gameTime);
            }
            else
            {
               endMenu.Update(gameTime);

            }



            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.White);

            _spriteBatch.Begin();


            if (Globals.life > 0)
            {

            _gameManager.Draw();

            waveManager.Draw(gameTime, _spriteBatch);
            menu.Draw(_spriteBatch);

            if (menu.gameStart)
            {
                buildMenu.Draw(_spriteBatch);
                uI_Liv_Money.Draw(_spriteBatch);
            }
            }
            else
            {
                endMenu.Draw(_spriteBatch);

            }


            _spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}