using EksamensProjekt.DesignPatterns.ComponentPattern;
using EksamensProjekt.MapGeneration;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EksamensProjekt.Towers
{
    internal class BasicPenguin : GameObject
    {
        public Texture2D sael;
        private GraphicsDevice graphicsDevice; 
        private ContentManager contentManager; 
        private SpriteBatch spriteBatch;
        private float rotation = 0;
        public Vector2 pos { get; private set; }
        public BasicPenguin(GraphicsDevice graphicsDevice, ContentManager contentManager, SpriteBatch spriteBatch) : base(graphicsDevice, contentManager, spriteBatch)
        {
            MouseState mouseState = Mouse.GetState();
            sael = contentManager.Load<Texture2D>("NormalPingvin");
            pos = new Vector2(mouseState.Position.X-sael.Width/2, mouseState.Position.Y-sael.Height/2);
            this.graphicsDevice = graphicsDevice;
            this.contentManager = contentManager;
            this.spriteBatch = spriteBatch;
            rect = new Rectangle((int)pos.X, (int)pos.Y, sael.Width, sael.Height);



        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(sael, rect, null, Color.White, rotation, Vector2.One, 0, 0);
        }

        public override void Update(GameTime gameTime)
        {
            if (Globals.enemies != null)
            {
                foreach (Enemy enm in Globals.enemies)
                {
                    if (Vector2.Distance(pos, enm.position) < 50)
                    {
                        Vector2 dir = enm.position - pos;
                        float rotTarget = (float)Math.Atan2(-dir.Y, -dir.X) + MathHelper.Pi;
                        rotation = ShortestRotation(rotTarget, rotation)- (MathHelper.Pi/3);
                    }
                }
            }
            
            
        }


        public float ShortestRotation(float targetRotation, float currentRotation)
        {
            float delta = MathHelper.WrapAngle(targetRotation - currentRotation);

            if (delta > MathHelper.Pi)
            {
                delta -= MathHelper.TwoPi;
            }
            else if (delta < -MathHelper.Pi)
            {
                delta += MathHelper.TwoPi;
            }

            return currentRotation + delta;
        }


    }


}
