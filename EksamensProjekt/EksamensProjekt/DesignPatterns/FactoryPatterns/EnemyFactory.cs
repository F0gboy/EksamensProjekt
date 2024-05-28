﻿using EksamensProjekt.DesignPatterns.ComponentPattern;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EksamensProjekt.MapGeneration;
using Microsoft.Xna.Framework;

namespace EksamensProjekt.DesignPatterns.FactoryPatterns
{
    public class EnemyFactory
    {
        private Texture2D normalEnemyTexture;
        private Texture2D strongEnemyTexture;

        public EnemyFactory(Texture2D normalEnemyTexture, Texture2D strongEnemyTexture)
        {
            this.normalEnemyTexture = normalEnemyTexture;
            this.strongEnemyTexture = strongEnemyTexture;

            Globals.normalEnemyHealth = 2;
            Globals.hardEnemyHealth = 10;
        }

        public Enemy CreateEnemy(Vector2 position, List<Vector2> path, float speed, float tileSize, bool isStrong)
        {
            if (isStrong)
            {
                return new Enemy(strongEnemyTexture, position, path, speed, tileSize, Globals.hardEnemyHealth, 5, 1); // Strong enemy with more health
            }
            else
            {
                return new Enemy(normalEnemyTexture, position, path, speed, tileSize, Globals.normalEnemyHealth, 1, 1); // Normal enemy
            }
        }
    }
}
