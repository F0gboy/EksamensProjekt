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
        public Vector2 Position { get; private set; }
        public bool IsDead { get; private set; }
        private Texture2D texture;
        private List<Vector2> path;
        private int currentWaypointIndex;
        private float speed;

        public Enemy(Texture2D texture, Vector2 spawnPosition, List<Vector2> path, float speed = 100f)
        {
            this.texture = texture;
            this.Position = spawnPosition;
            this.path = path;
            this.currentWaypointIndex = 0;
            this.speed = speed;
            this.IsDead = false;
        }

        public void Update(GameTime gameTime)
        {
            if (path == null || path.Count == 0 || IsDead) return;

            // Move towards the current waypoint
            Vector2 target = path[currentWaypointIndex];
            Vector2 direction = target - Position;
            if (direction.Length() > speed * (float)gameTime.ElapsedGameTime.TotalSeconds)
            {
                direction.Normalize();
                Position += direction * speed * (float)gameTime.ElapsedGameTime.TotalSeconds;
            }
            else
            {
                Position = target;
                currentWaypointIndex++;
                if (currentWaypointIndex >= path.Count)
                {
                    // Reached the end of the path
                    IsDead = true; // Mark enemy as "dead" or handle reaching the goal differently
                }
            }
        }

        public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, Position, Color.White);
        }
    }
}
