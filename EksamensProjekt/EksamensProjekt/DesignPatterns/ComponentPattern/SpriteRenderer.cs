using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EksamensProjekt.DesignPatterns.ComponentPattern
{
    public class SpriteRenderer : Component
    {
        private Texture2D texture;
        private Vector2 position;

        public SpriteRenderer(Game game, Texture2D texture, Vector2 position)
            : base(game)
        {
            this.texture = texture;
            this.position = position;
        }

        public override void Update(GameTime gameTime)
        {
            // Update logic goes here
        }

        public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, position, Color.White);
        }

        public void SetPosition(Vector2 newPosition)
        {
            position = newPosition;
        }
    }
}
