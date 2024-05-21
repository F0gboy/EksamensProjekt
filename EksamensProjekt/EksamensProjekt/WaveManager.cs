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
        private Texture2D enemyTexture;
        private List<Vector2> pathPoints;
        private float enemySpeed;

        public WaveManager(Texture2D enemyTexture, List<Vector2> pathPoints, int enemiesPerWave, float timeBetweenSpawns, float enemySpeed)
        {
            this.waveNumber = 1;
            this.spawnTimer = 0f;
            this.enemiesPerWave = enemiesPerWave;
            this.timeBetweenSpawns = timeBetweenSpawns;
            this.enemies = new List<Enemy>();
            this.enemyTexture = enemyTexture;
            this.pathPoints = pathPoints;
            this.enemySpeed = enemySpeed;
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
            Vector2 spawnPosition = pathPoints[0];

            if (!float.IsNaN(spawnPosition.X) && !float.IsNaN(spawnPosition.Y))
            {
                Enemy newEnemy = new Enemy(enemyTexture, spawnPosition, new List<Vector2>(pathPoints), enemySpeed);
                enemies.Add(newEnemy);
            }
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
