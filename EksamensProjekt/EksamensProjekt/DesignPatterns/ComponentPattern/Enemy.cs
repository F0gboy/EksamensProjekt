using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using EksamensProjekt.MapGeneration;

namespace EksamensProjekt.DesignPatterns.ComponentPattern
{
    public class Enemy
    {
        private Texture2D texture;
        public Vector2 position;
        private List<Vector2> path;
        private int currentPathIndex;
        private float speed;
        private float tileSize;
        private int health;
        private bool isRunning;
        private Thread updateThread;

        public bool IsDead { get; private set; }
        public bool HasPassed { get; private set; }

        public Enemy(Texture2D texture, Vector2 startPosition, List<Vector2> path, float speed, float tileSize, int health)
        {
            this.texture = texture;
            this.position = startPosition + new Vector2(tileSize / 2, tileSize / 2); // Center the enemy on the tile
            this.path = path;
            this.currentPathIndex = 0;
            this.speed = speed;
            this.tileSize = tileSize;
            this.health = health;
            this.IsDead = false;
            this.HasPassed = false;
            this.isRunning = true;
            this.updateThread = new Thread(UpdateLoop)
            {
                IsBackground = true // Set the thread as a background thread
            };
            this.updateThread.Start();
        }

        private void UpdateLoop()
        {
            while (isRunning)
            {
                Update(Globals.gameTime);
                Thread.Sleep(16); // Approximate 60 FPS update rate
            }
        }

        public void Update(GameTime gameTime)
        {
            if (currentPathIndex >= path.Count)
            {
                HasPassed = true;
                return;
            }

            Vector2 target = path[currentPathIndex] + new Vector2(tileSize / 2 - 15, tileSize / 2 - 10); // Center the target position
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

        public void Stop()
        {
            isRunning = false;
            updateThread.Join();
        }

        public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, position, Color.White);
        }
    }
}


