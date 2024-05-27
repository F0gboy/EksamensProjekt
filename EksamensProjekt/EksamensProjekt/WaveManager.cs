using EksamensProjekt.DesignPatterns.ComponentPattern;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EksamensProjekt.DesignPatterns.FactoryPatterns;
using EksamensProjekt.MapGeneration;

namespace EksamensProjekt
{
    public class WaveManager
    {
        private int waveNumber;
        private float spawnTimer;
        private float waveTimer;
        private int enemiesPerWave;
        private float timeBetweenSpawns;
        private float timeBetweenWaves;
        private List<Enemy> enemies;
        private EnemyFactory enemyFactory;
        private List<Vector2> pathPoints;
        private float enemySpeed;
        private int baseEnemyCount;
        private int strongEnemyThreshold;
        private bool waveInProgress;
        private int totalEnemiesSpawned;
        private int strongEnemiesCount;
        private readonly object enemyListLock = new object(); // Lock for thread safety

        public WaveManager(Texture2D normalEnemyTexture, Texture2D strongEnemyTexture, List<Vector2> pathPoints, float timeBetweenSpawns, float enemySpeed, float timeBetweenWaves)
        {
            this.waveNumber = 1;
            this.spawnTimer = 0f;
            this.waveTimer = 0f;
            this.baseEnemyCount = 5;
            this.enemiesPerWave = baseEnemyCount;
            this.timeBetweenSpawns = timeBetweenSpawns;
            this.enemies = new List<Enemy>();
            this.pathPoints = pathPoints;
            this.enemySpeed = enemySpeed;
            this.enemyFactory = new EnemyFactory(normalEnemyTexture, strongEnemyTexture);
            this.strongEnemyThreshold = 5; // Start spawning strong enemies from wave 5
            this.waveInProgress = true;
            this.timeBetweenWaves = timeBetweenWaves;
            this.totalEnemiesSpawned = 0; // Initialize total enemies spawned
            this.strongEnemiesCount = 0; // Initialize strong enemies count
        }

        public void Update(GameTime gameTime)
        {
            Globals.enemies = enemies;
            if (waveInProgress)
            {
                spawnTimer += (float)gameTime.ElapsedGameTime.TotalSeconds;

                if (spawnTimer >= timeBetweenSpawns && totalEnemiesSpawned < enemiesPerWave)
                {
                    Task.Run(() => SpawnEnemy());
                    spawnTimer = 0f;
                    totalEnemiesSpawned++;
                }

                lock (enemyListLock)
                {
                    for (int i = enemies.Count - 1; i >= 0; i--)
                    {
                        enemies[i].Update(gameTime);
                        if (enemies[i].HasPassed || enemies[i].IsDead)
                        {
                            enemies[i].Stop();
                            enemies.RemoveAt(i);
                        }
                    }
                }

                if (totalEnemiesSpawned >= enemiesPerWave && enemies.Count == 0)
                {
                    waveInProgress = false;
                    waveTimer = 0f;
                }
            }
            else
            {
                waveTimer += (float)gameTime.ElapsedGameTime.TotalSeconds;

                if (waveTimer >= timeBetweenWaves)
                {
                    StartNextWave();
                }
            }
        }

        private void StartNextWave()
        {
            waveNumber++;
            lock (enemyListLock)
            {
                foreach (var enemy in enemies)
                {
                    enemy.Stop();
                }
                enemies.Clear();
            }
            enemiesPerWave = baseEnemyCount + (waveNumber - 1) * 2; // Increase enemy count per wave
            enemySpeed += 0.5f; // Increase enemy speed for difficulty
            waveInProgress = true;
            spawnTimer = 0f; // Reset spawn timer for the new wave
            totalEnemiesSpawned = 0; // Reset total enemies spawned for the new wave

            // Introduce strong enemies based on the wave number
            if (waveNumber >= strongEnemyThreshold)
            {
                strongEnemiesCount = waveNumber - strongEnemyThreshold + 1; // Increase strong enemies count gradually
            }
        }

        private void SpawnEnemy()
        {
            Vector2 spawnPosition = pathPoints[0] + new Vector2(-10f, -10f); // Center the spawn position
            bool isStrong = waveNumber >= strongEnemyThreshold && (totalEnemiesSpawned % (enemiesPerWave / (strongEnemiesCount + 1)) == 0); // Introduce strong enemies gradually
            Enemy newEnemy = enemyFactory.CreateEnemy(spawnPosition, new List<Vector2>(pathPoints), enemySpeed, 120f, isStrong);

            lock (enemyListLock)
            {
                enemies.Add(newEnemy);
            }
        }

        public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            lock (enemyListLock)
            {
                foreach (var enemy in enemies)
                {
                    enemy.Draw(gameTime, spriteBatch);
                }
            }
        }

        private bool WaveCleared()
        {
            return enemies.Count == 0;
        }
    }
}
