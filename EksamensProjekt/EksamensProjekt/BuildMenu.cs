﻿using Microsoft.Xna.Framework.Content;
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
        //Leonard

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


        int screenWidth = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Width;
        int screenHeight = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Height;

        public BuildMenu(GraphicsDevice graphicsDevice, ContentManager contentManager, SpriteBatch spriteBatch)
        {
            background = contentManager.Load<Texture2D>("Background 3");
            button = contentManager.Load<Texture2D>("Button");
            font = contentManager.Load<SpriteFont>("UIfont");
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

            // Debugging output for mouse position and button rectangles
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
                        //build system
                        buildActive = false;
                        switch (buildInt)
                        {
                            case 1:
                                if (Globals.money >= 150)
                                {
                                    // if money is more than 100 then add a new penguin
                                    PinguObjects.Add(new BasicPenguin(mouseState.Position.ToVector2(), contentManager.Load<Texture2D>("p1"), contentManager.Load<Texture2D>("BulletS"), 500, 1, 1, 500));
                                      Globals.money -= 150;
                                }
                                break;

                            case 2:
                                if (Globals.money >= 350)
                                {
                                    // if money is more than 250 then add a new penguin
                                    PinguObjects.Add(new BasicPenguin(mouseState.Position.ToVector2(), contentManager.Load<Texture2D>("pingvintank2"), contentManager.Load<Texture2D>("Bullet"), 500, 10, 3, 500));
                                    Globals.money -= 350;

                                }
                                break;

                            case 3:
                                if (Globals.money >= 650)
                                {
                                    // if money is more than 500 then add a new penguin
                                    PinguObjects.Add(new BasicPenguin(mouseState.Position.ToVector2(), contentManager.Load<Texture2D>("p2"), contentManager.Load<Texture2D>("BulletS"), 500, 1, 0.2f, 500));
                                    Globals.money -= 650;

                                }
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

            // Update all penguins
            foreach (BasicPenguin go in PinguObjects)
            {
                go.Update(gameTime);

                foreach (var enemy in enemies)
                {
                    go.Update(enemy);
                }
            }


           
        }

        // Draw all penguins
        public void Draw(SpriteBatch spriteBatch)
        {
            //spriteBatch.Draw(background, Vector2.Zero, Color.White);
            spriteBatch.Draw(button, new Vector2(screenWidth-button.Width, screenHeight/2-20), null, Color.AliceBlue, 1.57079633f, new Vector2(button.Width / 2, button.Height / 2), 6, SpriteEffects.None, 0);
            //spriteBatch.Draw(button, new Vector2(buttonLogin.Center.X, buttonLogin.Center.Y), null, Color.White, 0, original, 1, SpriteEffects.None, 0);
            spriteBatch.Draw(button, new Vector2(screenWidth - button.Width, screenHeight / 2+20), null, Color.White, 0, new Vector2(button.Width / 2, button.Height / 2), 1.75f, SpriteEffects.None, 0);
            spriteBatch.Draw(button, new Vector2(screenWidth - button.Width, screenHeight / 2-280), null, Color.White, 0, new Vector2(button.Width / 2, button.Height / 2), 1.75f, SpriteEffects.None, 0);
            spriteBatch.Draw(button, new Vector2(screenWidth - button.Width, screenHeight / 2+320), null, Color.White, 0, new Vector2(button.Width / 2, button.Height / 2), 1.75f, SpriteEffects.None, 0);
            
            
            spriteBatch.DrawString(font, "Penguin\n   $150", new Vector2(screenWidth - button.Width - font.MeasureString("Penguin").Length()*1.6f / 2, screenHeight / 2 - 45), Color.White, 0, new Vector2(0, 0), 1.7f, SpriteEffects.None, 0);
            spriteBatch.DrawString(font, "Penguin Tank\n      $350", new Vector2(screenWidth - button.Width - font.MeasureString("Penguin Tank").Length()*1.6f / 2, screenHeight / 2 - 45 - 300), Color.White, 0, new Vector2(0, 0), 1.7f, SpriteEffects.None, 0);
            spriteBatch.DrawString(font, "Penguin gunner\n        $650", new Vector2(screenWidth - button.Width - font.MeasureString("Penguin gunner").Length()*1.65f / 2, screenHeight / 2 - 45 + 300), Color.White, 0, new Vector2(0, 0), 1.7f, SpriteEffects.None, 0);
            
            MouseState mouseState = Mouse.GetState();

            if (buildActive)
            {
                // Draw the tile
                 spriteBatch.Draw(tile, new Vector2(mouseState.Position.X, mouseState.Position.Y), null, Color.Green, 0, new Vector2(tile.Width / 2*0.5f, tile.Height / 2*0.5f), 0.5f, SpriteEffects.None, 0);
            }

            foreach (BasicPenguin go in PinguObjects)
            {
                go.Draw(Globals.gameTime, spriteBatch);
            }

            

            //DrawRectangle(penguin1, Color.Red, spriteBatch);
            //DrawRectangle(penguin2, Color.Red, spriteBatch);
            //DrawRectangle(penguin3, Color.Red, spriteBatch);

        }

        private static Texture2D rect;

        // Draw a rectangle
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
