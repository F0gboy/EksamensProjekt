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

        public void Update(GameTime gameTime)
        {
            fireTimer += (float)gameTime.ElapsedGameTime.TotalSeconds;

            if (fireTimer >= fireRate)
            {
                lock (enemiesLock)
                {
                    enemiesInRange.RemoveAll(e => e.IsDead);

                    if (enemiesInRange.Count > 0)
                    {
                        Fire(enemiesInRange[0]);
                        fireTimer = 0f;
                    }
                }
            }

            foreach (var projectile in projectiles)
            {
                projectile.Update(gameTime);
            }

            CheckProjectileCollisions();

            projectiles.RemoveAll(p => !IsInBounds(p.Position) || !p.IsActive);
        }

        public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, position, null, Color.White, rotation, new Vector2(texture.Width / 2, texture.Height / 2), 1f, SpriteEffects.None, 0f);

            foreach (var projectile in projectiles)
            {
                projectile.Draw(gameTime, spriteBatch);
            }
        }

        private void Fire(Enemy target)
        {
            Vector2 direction = target.Position - position;
            direction.Normalize();
            rotation = (float)Math.Atan2(direction.Y, direction.X) - MathHelper.PiOver2;

            Projectile newProjectile = new Projectile(position, direction, projectileSpeed, projectileDamage, projectileTexture);
            projectiles.Add(newProjectile);
        }

        private void CheckProjectileCollisions()
        {
            lock (enemiesLock)
            {
                foreach (var projectile in projectiles)
                {
                    foreach (var enemy in enemiesInRange)
                    {
                        if (Vector2.Distance(projectile.Position, enemy.Position) < projectile.Radius + enemy.Radius)
                        {
                            enemy.TakeDamage(projectileDamage);
                            projectile.IsActive = false;
                            break;
                        }
                    }
                }
            }

            projectiles.RemoveAll(p => !p.IsActive);
        }

        // IObserver Update method implementation
        public void Update(Enemy enemy)
        {
            lock (enemiesLock)
            {
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
