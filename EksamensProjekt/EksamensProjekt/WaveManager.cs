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
        private readonly object enemyListLock = new object();

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
            this.strongEnemyThreshold = 5;
            this.waveInProgress = true;
            this.timeBetweenWaves = timeBetweenWaves;
            this.totalEnemiesSpawned = 0;
            this.strongEnemiesCount = 0;
        }

        public void Update(GameTime gameTime)
        {
            Globals.enemies = enemies;

            lock (enemyListLock)
            {
                // Update each enemy and check if they have passed or are dead
                for (int i = enemies.Count - 1; i >= 0; i--)
                {
                    var enemy = enemies[i];
                    enemy.Update(gameTime);

                    if (enemy.HasPassed)
                    {
                        Globals.life -= enemy.value;
                        enemies.RemoveAt(i);
                    }
                    else if (enemy.IsDead)
                    {
                        Globals.money += enemy.value * 10;
                        enemies.RemoveAt(i);
                    }
                }

                // Spawn new enemies if the wave is in progress
                if (waveInProgress)
                {
                    spawnTimer += (float)gameTime.ElapsedGameTime.TotalSeconds;

                    if (spawnTimer >= timeBetweenSpawns && totalEnemiesSpawned < enemiesPerWave)
                    {
                        Task.Run(() => SpawnEnemy());
                        spawnTimer = 0f;
                        totalEnemiesSpawned++;
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
        }

        private void StartNextWave()
        {
            waveNumber++;

            lock (enemyListLock)
            {
                enemies.Clear();
            }

            enemiesPerWave = baseEnemyCount + (waveNumber - 1) * 2;
            enemySpeed += 5f;
            waveInProgress = true;
            spawnTimer = 0f;
            totalEnemiesSpawned = 0;

            if (waveNumber >= strongEnemyThreshold)
            {
                strongEnemiesCount = waveNumber - strongEnemyThreshold + 1;
            }
        }

        private void SpawnEnemy()
        {
            Vector2 spawnPosition = pathPoints[0] + new Vector2(-30f, -10f);
            bool isStrong = waveNumber >= strongEnemyThreshold && (totalEnemiesSpawned % (enemiesPerWave / (strongEnemiesCount + 1)) == 0);
            Enemy newEnemy = enemyFactory.CreateEnemy(spawnPosition, new List<Vector2>(pathPoints), enemySpeed, 120f, isStrong);

            lock (enemyListLock)
            {
                enemies.Add(newEnemy);
            }
        }

        public void OnEnemyKilled(Enemy enemy)
        {
            lock (enemyListLock)
            {
                enemies.Remove(enemy);
            }

            Globals.money += enemy.value;
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

        public List<Enemy> GetEnemies()
        {
            lock (enemyListLock)
            {
                return new List<Enemy>(enemies);
            }
        }

        private bool WaveCleared()
        {
            return enemies.Count == 0;
        }
    }
}
