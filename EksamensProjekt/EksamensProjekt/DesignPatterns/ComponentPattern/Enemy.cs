using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EksamensProjekt.DesignPatterns.ComponentPattern
{
    public class Enemy
    {
        private Texture2D texture;
        private Vector2 position;
        private List<Vector2> path;
        private int currentPathIndex;
        private float speed;
        private Vector2 offset;

        public bool IsDead { get; private set; }

        public Enemy(Texture2D texture, Vector2 startPosition, List<Vector2> path, float speed)
        {
            this.texture = texture;
            this.position = startPosition;
            this.path = path;
            this.currentPathIndex = 0;
            this.speed = speed;
            this.IsDead = false;
        }

        public void Update(GameTime gameTime)
        {
            if (currentPathIndex >= path.Count)
            {
                IsDead = true;
                return;
            }

            Vector2 target = path[currentPathIndex];
            Vector2 direction = target - position;

            if (direction.LengthSquared() > 0)
            {
                direction.Normalize();
                position += direction * speed * (float)gameTime.ElapsedGameTime.TotalSeconds;
            }

            if (Vector2.DistanceSquared(position, target) < (speed * (float)gameTime.ElapsedGameTime.TotalSeconds) * (speed * (float)gameTime.ElapsedGameTime.TotalSeconds))
            {
                position = target; // Snap to target to avoid precision issues
                currentPathIndex++;
            }
        }

        public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, position, Color.White);
        }
    }
}

