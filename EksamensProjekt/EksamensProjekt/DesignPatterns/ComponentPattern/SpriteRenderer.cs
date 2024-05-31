using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EksamensProjekt.MapGeneration;

namespace EksamensProjekt.DesignPatterns.ComponentPattern
{
    public class SpriteRenderer : Component
    {
        // Jasper
        public Texture2D texture { get; set; }
        public Vector2 Position { get; set; }
        public Vector2 Origin { get; protected set; }
        public Color Color { get; set; }
        public Rectangle Rectangle => new((int)Position.X, (int)Position.Y, texture.Width, texture.Height);

        public SpriteRenderer(Game game, Texture2D texture, Vector2 position)
            : base(game)
        {
            this.texture = texture;
            Position = position;
            Origin = Vector2.Zero;
            Color = Color.White;
        }

        public override void Update(GameTime gameTime)
        {
            // Update logic goes here
        }

        public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
             Globals.SpriteBatch.Draw(texture, Position, null, Color, 0f, Origin, 1f, SpriteEffects.None, 0f);
        }

        public void SetPosition(Vector2 newPosition)
        {
            Position = newPosition;
        }
    }
}
