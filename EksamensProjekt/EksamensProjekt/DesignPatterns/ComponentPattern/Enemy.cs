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
        private float tileSize;
        private int health;
        public bool HasPassed { get; private set; }

        public bool IsDead { get; private set; }

        public Enemy(Texture2D texture, Vector2 startPosition, List<Vector2> path, float speed, float tileSize,
            int health)
        {
            this.texture = texture;
            this.position = startPosition + new Vector2(tileSize / 2, tileSize / 2); // Center the starting position
            this.path = path.Select(p => p + new Vector2(tileSize / 2, tileSize / 2))
                .ToList(); // Center all path points
            this.currentPathIndex = 0;
            this.speed = speed;
            this.tileSize = tileSize;
            this.health = health;
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

            if (Vector2.DistanceSquared(position, target) < (speed * (float)gameTime.ElapsedGameTime.TotalSeconds) *
                (speed * (float)gameTime.ElapsedGameTime.TotalSeconds))
            {
                position = target; // Snap to target to avoid precision issues
                currentPathIndex++;
            }

            if (currentPathIndex >= path.Count)
            {
                HasPassed = true;
            }
        }

        public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, position - new Vector2(texture.Width / 2, texture.Height / 2),
                Color.White); // Center the texture on the position
        }

        public void TakeDamage(int damage)
        {
            health -= damage;
            if (health <= 0)
            {
                IsDead = true;
            }
        }
    }
}

