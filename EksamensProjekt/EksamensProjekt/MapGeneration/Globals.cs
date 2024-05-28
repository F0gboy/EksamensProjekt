using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EksamensProjekt.DesignPatterns.ComponentPattern;

namespace EksamensProjekt.MapGeneration
{
    public static class Globals
    {
        public static float Time { get; set; }
        public static ContentManager Content { get; set; }
        public static SpriteBatch SpriteBatch { get; set; }
        public static Point WindowSize { get; set; }

        public static List<Enemy> enemies;

        public static bool gameStarted { get; set; }

        public static GameTime gameTime { get; set; }

        public static int life { get; set; }

        public static int money { get; set; }
        public static int totalMoney { get; set; }
        public static int kills { get; set; }

        public static int normalEnemyHealth { get; set; }

        public static int hardEnemyHealth { get; set; }

        public static void Update(GameTime gt)
        {
            Time = (float)gt.ElapsedGameTime.TotalSeconds;
        }
    }
}
