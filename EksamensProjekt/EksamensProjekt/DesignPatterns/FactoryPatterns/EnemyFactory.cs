using EksamensProjekt.DesignPatterns.ComponentPattern;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
        }

        public Enemy CreateEnemy(Vector2 position, List<Vector2> path, float speed, float tileSize, bool isStrong)
        {
            if (isStrong)
            {
                return new Enemy(strongEnemyTexture, position, path, speed, tileSize, 100); // Strong enemy with more health
            }
            else
            {
                return new Enemy(normalEnemyTexture, position, path, speed, tileSize, 50); // Normal enemy
            }
        }
    }
}
