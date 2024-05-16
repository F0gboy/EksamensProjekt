using EksamensProjekt.DesignPatterns.ComponentPattern;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EksamensProjekt
{
    public class WaveManager
    {
        private int waveNumber;
        private float spawnTimer;
        private int enemiesPerWave;
        private float timeBetweenSpawns;
        private List<Enemy> enemies;
        private Vector2 spawnPosition;
        private Texture2D enemyTexture;
        private List<Vector2> path;

        public WaveManager(Texture2D enemyTexture, Vector2 spawnPosition, List<Vector2> path, int enemiesPerWave, float timeBetweenSpawns)
        {
            this.waveNumber = 1;
            this.spawnTimer = 0f;
            this.enemiesPerWave = enemiesPerWave;
            this.timeBetweenSpawns = timeBetweenSpawns;
            this.enemies = new List<Enemy>();
            this.spawnPosition = spawnPosition;
            this.enemyTexture = enemyTexture;
            this.path = path;
        }

        public void Update(GameTime gameTime)
        {
            spawnTimer += (float)gameTime.ElapsedGameTime.TotalSeconds;

            if (spawnTimer >= timeBetweenSpawns && enemies.Count < enemiesPerWave)
            {
                SpawnEnemy();
                spawnTimer = 0f;
            }

            for (int i = enemies.Count - 1; i >= 0; i--)
            {
                enemies[i].Update(gameTime);
                if (enemies[i].IsDead)
                {
                    enemies.RemoveAt(i);
                }
            }
        }

        private void SpawnEnemy()
        {
            // Create a new enemy instance at the spawn position
            Enemy newEnemy = new Enemy(enemyTexture, spawnPosition, new List<Vector2>(path)); // Pass the path
            enemies.Add(newEnemy);
        }

        public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            foreach (var enemy in enemies)
            {
                enemy.Draw(gameTime, spriteBatch);
            }
        }

        public void StartNextWave()
        {
            waveNumber++;
            enemies.Clear();
        }

        public bool WaveCleared()
        {
            return enemies.Count == 0 && waveNumber > 1;
        }
    }
}
