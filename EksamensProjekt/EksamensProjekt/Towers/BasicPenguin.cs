using EksamensProjekt.DesignPatterns.ComponentPattern;
using EksamensProjekt.DesignPatterns.ObserverPatterns;
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
    internal class BasicPenguin : IObserver
    {
        private Vector2 position;
        private Texture2D texture;
        private Texture2D projectileTexture;
        private List<Projectile> projectiles;
        private float projectileSpeed;
        private int projectileDamage;
        private float fireRate;
        private float fireTimer;
        private float range;
        private List<Enemy> enemiesInRange;
        private readonly object enemiesLock = new object();
        private float rotation;

        public BasicPenguin(Vector2 position, Texture2D texture, Texture2D projectileTexture, float projectileSpeed,
            int projectileDamage, float fireRate, float range)
        {
            // Initialize the tower with the given parameters
            this.position = position;
            this.texture = texture;
            this.projectileTexture = projectileTexture;
            this.projectileSpeed = projectileSpeed;
            this.projectileDamage = projectileDamage;
            this.fireRate = fireRate;
            this.range = range;
            this.projectiles = new List<Projectile>();
            this.enemiesInRange = new List<Enemy>();
            this.fireTimer = 0f;
            this.rotation = 0f;
        }

        // IObserver Update method implementation
        public void Update(GameTime gameTime)
        {
            fireTimer += (float)gameTime.ElapsedGameTime.TotalSeconds;

            if (fireTimer >= fireRate)
            {
                // Check if there are any enemies in range
                lock (enemiesLock)
                {
                    enemiesInRange.RemoveAll(e => e.IsDead);

                    // If there are enemies in range, fire at the first one
                    if (enemiesInRange.Count > 0)
                    {
                        Fire(enemiesInRange[0]);
                        fireTimer = 0f;
                    }
                }
            }

            // Update the tower's projectiles
            foreach (var projectile in projectiles)
            {
                projectile.Update(gameTime);
            }

            // Check for projectile collisions
            CheckProjectileCollisions();

            // Remove inactive projectiles
            projectiles.RemoveAll(p => !IsInBounds(p.Position) || !p.IsActive);
        }

        public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            // Draw the tower
            spriteBatch.Draw(texture, position, null, Color.White, rotation,
                new Vector2(texture.Width / 2, texture.Height / 2), 1f, SpriteEffects.None, 0f);

            // Draw the tower's projectiles
            foreach (var projectile in projectiles)
            {
                projectile.Draw(gameTime, spriteBatch);
            }
        }

        // Method to fire a projectile at a target enemy
        private void Fire(Enemy target)
        {
            // Calculate the direction to the target enemy
            Vector2 direction = target.Position - position;
            direction.Normalize();
            rotation = (float)Math.Atan2(direction.Y, direction.X) - MathHelper.PiOver2;

            // Create a new projectile and add it to the list
            Projectile newProjectile =
                new Projectile(position, direction, projectileSpeed, projectileDamage, projectileTexture);
            projectiles.Add(newProjectile);
        }

        private void CheckProjectileCollisions()
        {
            // Check for collisions between projectiles and enemies
            lock (enemiesLock)
            {
                // Iterate over all projectiles and enemies in range
                foreach (var projectile in projectiles)
                {
                    foreach (var enemy in enemiesInRange)
                    {
                        // If the projectile collides with an enemy, deal damage and mark the projectile as inactive
                        if (Vector2.Distance(projectile.Position, enemy.Position) < projectile.Radius + enemy.Radius)
                        {
                            enemy.TakeDamage(projectileDamage);
                            projectile.IsActive = false; // Mark projectile as inactive on hit
                            break;
                        }
                    }
                }
            }
        }

        // IObserver Update method implementation
        public void Update(Enemy enemy)
        {
            lock (enemiesLock)
            {
                // Update the list of enemies in range
                if (Vector2.Distance(position, enemy.Position) <= range)
                {
                    if (!enemiesInRange.Contains(enemy))
                    {
                        enemiesInRange.Add(enemy);
                    }
                }
                else
                {
                    enemiesInRange.Remove(enemy);
                }
            }
        }

        private bool IsInBounds(Vector2 position)
        {
            // Check if the projectile is within the game bounds, implement as needed
            return true;
        }
    }
}
