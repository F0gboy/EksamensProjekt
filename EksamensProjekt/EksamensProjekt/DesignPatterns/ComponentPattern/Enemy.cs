using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using EksamensProjekt.DesignPatterns.ObserverPatterns;
using EksamensProjekt.MapGeneration;

namespace EksamensProjekt.DesignPatterns.ComponentPattern
{
    public class Enemy : IObservable
    {
        private Texture2D texture;
        private List<Vector2> path;
        private int currentPathIndex;
        private float speed;
        private float tileSize;
        public int health { get; private set; }

        private bool isRunning;
        private Thread updateThread;
        public int value;
        private List<IObserver> observers;

        public bool IsDead { get; private set; }
        public bool HasPassed { get; private set; }

        public float Radius { get; private set; }
        public Vector2 position { get; private set; }
        private float rotation; 
        private float targetRotation; 
        private float rotationSpeed = 3f; 

        public Enemy(Texture2D texture, Vector2 startPosition, List<Vector2> path, float speed, float tileSize, int health, int value, float radius)
        {
            this.texture = texture;
            this.position = startPosition + new Vector2(tileSize / 2, tileSize / 2); 
            this.path = path;
            this.currentPathIndex = 0;
            this.speed = speed;
            this.tileSize = tileSize;
            this.health = health;
            this.IsDead = false;
            this.isRunning = true;
            this.Radius = radius;
            IsDead = false;

            this.updateThread = new Thread(UpdateLoop)
            {
                IsBackground = true // Set the thread as a background thread
            };

            this.updateThread.Start();
            this.value = value;
            this.observers = new List<IObserver>();
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

            Vector2 target = path[currentPathIndex] + new Vector2(tileSize / 2, tileSize / 2); 
            Vector2 direction = target - position;

            if (direction.LengthSquared() > 0)
            {
                direction.Normalize();
                position += direction * speed * (float)gameTime.ElapsedGameTime.TotalSeconds;
                targetRotation = (float)Math.Atan2(direction.Y, direction.X); 
            }

            float rotationDifference = targetRotation - rotation;
            rotation += MathHelper.Clamp(rotationDifference, -rotationSpeed * (float)gameTime.ElapsedGameTime.TotalSeconds, rotationSpeed * (float)gameTime.ElapsedGameTime.TotalSeconds);

            if (Vector2.DistanceSquared(position, target) < (speed * (float)gameTime.ElapsedGameTime.TotalSeconds) * (speed * (float)gameTime.ElapsedGameTime.TotalSeconds))
            {
                position = target; 
                currentPathIndex++;
            }

            NotifyObservers();
        }

        public void Stop()
        {
            isRunning = false;
            updateThread.Join();
        }

        public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            // Draw with rotation
            spriteBatch.Draw(texture, position, null, Color.White, rotation, new Vector2(texture.Width / 2, texture.Height / 2), 1f, SpriteEffects.None, 0f);
        }

        public void RegisterObserver(IObserver observer)
        {
            if (!observers.Contains(observer))
            {
                observers.Add(observer);
            }
        }

        public void TakeDamage(int damage)
        {
            health -= damage;
            if (health <= 0)
            {
                IsDead = true;
            }
        }

        public void RemoveObserver(IObserver observer)
        {
            observers.Remove(observer);
        }

        public void NotifyObservers()
        {
            foreach (var observer in observers)
            {
                observer.Update(this);
            }
        }

        public Vector2 Position => position;
    }
}


