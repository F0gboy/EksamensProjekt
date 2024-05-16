using EksamensProjekt.MapGeneration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EksamensProjekt
{
    internal class GameManager
    {
        private readonly Map _map;

        public GameManager()
        {
            _map = new();

            Pathfinder.Init(_map);

            GenerateMap();
        }

        public void GenerateMap()
        {
            Random r = new Random();

            int randomY = r.Next(0, 8);

            Random random = new();
            int tempRandomX = random.Next(2, 4);
            
            Pathfinder.AStarPathfinding((int) 0, randomY, tempRandomX, randomY);

            Random random1 = new();
            int tempRandomX1 = random.Next(6, 8);
            int tempRandomY1 = random.Next(0, 8);

            Pathfinder.AStarPathfinding(tempRandomX, randomY, tempRandomX1, tempRandomY1);

            Random random2 = new();
            int tempRandomX2 = random.Next(9, 11);
            int tempRandomY2 = random.Next(0, 8);
            Pathfinder.AStarPathfinding(tempRandomX1, tempRandomY1, tempRandomX2, tempRandomY2);

            Random random5 = new();
            int tempRandomY5 = random.Next(0, 8);
            Pathfinder.AStarPathfinding(tempRandomX2, tempRandomY2, 12, tempRandomY5);
        }

        public void Update()
        {
            // Update logic goes here
            InputManager.Update();
            _map.Update();
        }

        public void Draw()
        {
            Globals.SpriteBatch.Begin();

            _map.Draw();

            Globals.SpriteBatch.End();
        }
    }
}
