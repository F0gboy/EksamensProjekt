using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EksamensProjekt.DesignPatterns.ComponentPattern;
using EksamensProjekt.MapGeneration;
using EksamensProjekt.Towers;

namespace EksamensProjekt
{
    internal class BuildMenu
    {

        private Texture2D background;
        private Texture2D button;
        private Texture2D tile;
        private SpriteFont font;
        private Rectangle penguin1;
        private Rectangle penguin2;
        private Rectangle penguin3;
        private bool clicked;
        private bool buildActive;
        private int buildInt;
        private GraphicsDevice graphicsDevice;
        private ContentManager contentManager;
        private SpriteBatch spriteBatch;

        private List<BasicPenguin> PinguObjects = new List<BasicPenguin>();
        private List<Tank> TankObjects = new List<Tank>();


        int screenWidth = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Width;
        int screenHeight = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Height;

        public BuildMenu(GraphicsDevice graphicsDevice, ContentManager contentManager, SpriteBatch spriteBatch)
        {
            background = contentManager.Load<Texture2D>("Background");
            button = contentManager.Load<Texture2D>("Button");
            font = contentManager.Load<SpriteFont>("font");
            tile = contentManager.Load<Texture2D>("iceTile");
            penguin1 = new Rectangle(screenWidth - button.Width-(int)(button.Width*1.75f/2), screenHeight / 2 - 20-(int)(1.75f*button.Height/2), (int)(button.Width*2.5f), (int)(button.Height * 2.5f));
            penguin2 = new Rectangle(screenWidth - button.Width - (int)(button.Width * 1.75f / 2), screenHeight / 2 - 20-300 - (int)(1.75f * button.Height / 2), (int)(button.Width * 2.5f), (int)(button.Height * 2.5f));
            penguin3 = new Rectangle(screenWidth - button.Width - (int)(button.Width * 1.75f / 2), screenHeight / 2 - 20+300 - (int)(1.75f * button.Height / 2), (int)(button.Width*2.5f), (int)(button.Height * 2.5f));
            this.graphicsDevice = graphicsDevice;
            this.contentManager = contentManager;
            this.spriteBatch = spriteBatch;


        }
        public void Update(GameTime gameTime, List<Enemy> enemies)
        {
            MouseState mouseState = Mouse.GetState();

            foreach  (BasicPenguin go in PinguObjects)
            {
                go.Update(gameTime);
            }


            // check if left button is pressed
            if (mouseState.LeftButton == ButtonState.Pressed)
            {
                
                if (clicked == false)
                {
                    if (penguin1.Contains(mouseState.Position))
                    {
                        //build system
                        if (buildActive)
                        {
                            buildInt = 0;
                            buildActive = !buildActive;
                        }
                        else
                        {
                            buildInt = 1;
                            buildActive = true;
                        }
                        
                    }

                    else if (penguin2.Contains(mouseState.Position))
                    {
                        //build system
                        if (buildActive)
                        {
                            buildInt = 0;
                            buildActive = !buildActive;
                        }
                        else
                        {
                            buildInt = 2;
                            buildActive = true;
                        }
                    }

                    else if (penguin3.Contains(mouseState.Position))
                    {
                        //build system
                        if (buildActive)
                        {
                            buildInt = 0;
                            buildActive = !buildActive;
                        }
                        else
                        {
                            buildInt = 3;
                            buildActive = true;
                        }
                    }


                    else if (buildActive)
                    {
                        buildActive = false;
                        switch (buildInt)
                        {
                            case 1:
                                PinguObjects.Add(new BasicPenguin(mouseState.Position.ToVector2(), contentManager.Load<Texture2D>("NormalPingvin"), contentManager.Load<Texture2D>("Bullet"), 500, 1, 0.2f, 500));
                                break;

                            case 2:

                                TankObjects.Add(new Tank(mouseState.Position.ToVector2(), contentManager.Load<Texture2D>("pingvintank2"), contentManager.Load<Texture2D>("Bullet"), 500, 10, 2, 500));
                                break;

                            case 3:

                                break;




                            default:
                                break;
                        }
                    }
                }

                clicked = true;
            }
            else
            {
                clicked = false;
            }

            if (mouseState.RightButton == ButtonState.Pressed)
            {

            }


            foreach (BasicPenguin go in PinguObjects)
            {
                go.Update(gameTime);

                foreach (var enemy in enemies)
                {
                    go.Update(enemy);
                }
            }


            foreach (Tank tank in TankObjects)
            {
                tank.Update(gameTime);

                foreach (var enemy in enemies)
                {
                    tank.Update(enemy);
                }
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            //spriteBatch.Draw(background, Vector2.Zero, Color.White);
            spriteBatch.Draw(button, new Vector2(screenWidth-button.Width, screenHeight/2-20), null, Color.AliceBlue, 1.57079633f, new Vector2(button.Width / 2, button.Height / 2), 6, SpriteEffects.None, 0);
            //spriteBatch.Draw(button, new Vector2(buttonLogin.Center.X, buttonLogin.Center.Y), null, Color.White, 0, original, 1, SpriteEffects.None, 0);
            spriteBatch.Draw(button, new Vector2(screenWidth - button.Width, screenHeight / 2+20), null, Color.White, 0, new Vector2(button.Width / 2, button.Height / 2), 1.75f, SpriteEffects.None, 0);
            spriteBatch.Draw(button, new Vector2(screenWidth - button.Width, screenHeight / 2-280), null, Color.White, 0, new Vector2(button.Width / 2, button.Height / 2), 1.75f, SpriteEffects.None, 0);
            spriteBatch.Draw(button, new Vector2(screenWidth - button.Width, screenHeight / 2+320), null, Color.White, 0, new Vector2(button.Width / 2, button.Height / 2), 1.75f, SpriteEffects.None, 0);
            
            
            spriteBatch.DrawString(font, "Penguin1", new Vector2(screenWidth - button.Width - font.MeasureString("Penguin1").Length()*2.5f / 2, screenHeight / 2 - 25), Color.White, 0, new Vector2(0, 0), 2.5f, SpriteEffects.None, 0);
            spriteBatch.DrawString(font, "Penguin2", new Vector2(screenWidth - button.Width - font.MeasureString("Penguin2").Length()*2.5f / 2, screenHeight / 2 - 25 - 300), Color.White, 0, new Vector2(0, 0), 2.5f, SpriteEffects.None, 0);
            spriteBatch.DrawString(font, "Penguin3", new Vector2(screenWidth - button.Width - font.MeasureString("Penguin3").Length()*2.5f / 2, screenHeight / 2 - 25 + 300), Color.White, 0, new Vector2(0, 0), 2.5f, SpriteEffects.None, 0);
            
            MouseState mouseState = Mouse.GetState();

            if (buildActive)
            {
                 spriteBatch.Draw(tile, new Vector2(mouseState.Position.X, mouseState.Position.Y), null, Color.Green, 0, new Vector2(tile.Width / 2*0.5f, tile.Height / 2*0.5f), 0.5f, SpriteEffects.None, 0);
            }

            foreach (BasicPenguin go in PinguObjects)
            {
                go.Draw(Globals.gameTime, spriteBatch);
            }

            foreach (Tank tanks in TankObjects)
            {
                tanks.Draw(Globals.gameTime, spriteBatch);
            }

            //DrawRectangle(penguin1, Color.Red, spriteBatch);
            //DrawRectangle(penguin2, Color.Red, spriteBatch);
            //DrawRectangle(penguin3, Color.Red, spriteBatch);

        }

        private static Texture2D rect;

        private void DrawRectangle(Rectangle coords, Color color, SpriteBatch spriteBatch)
        {
            if (rect == null)
            {
                rect = new Texture2D(graphicsDevice, 1, 1);
                rect.SetData(new[] { Color.White });
            }
            spriteBatch.Draw(rect, coords, color);
        }
    }
}
