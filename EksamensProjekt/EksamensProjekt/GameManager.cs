using EksamensProjekt.MapGeneration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;

namespace EksamensProjekt
{
    internal class GameManager
    {
        public List<Vector2> PathPoints { get; private set; }
        private Map _map;
        private int tileSize = 120;

        public GameManager()
        {
            _map = new Map();
            Pathfinder.Init(_map);
            GenerateMap();
        }

        private Vector2 GridToScreen(Point gridPos)
        {
            return new Vector2(gridPos.X * tileSize, gridPos.Y * tileSize);
        }

        public void GenerateMap()
        {
            Random r = new Random();

            int randomY = r.Next(1, 8);
            int tempRandomX = new Random().Next(2, 4);
            List<Point> path1 = Pathfinder.AStarPathfinding(new Point(0, randomY), new Point(tempRandomX, randomY));
            if (path1 == null) { Console.WriteLine("Path1 is null"); }

            int tempRandomX1 = new Random().Next(6, 8);
            int tempRandomY1 = new Random().Next(1, 8);
            List<Point> path2 = Pathfinder.AStarPathfinding(new Point(tempRandomX, randomY), new Point(tempRandomX1, tempRandomY1));
            if (path2 == null) { Console.WriteLine("Path2 is null"); }

            int tempRandomX2 = new Random().Next(9, 11);
            int tempRandomY2 = new Random().Next(1, 8);
            List<Point> path3 = Pathfinder.AStarPathfinding(new Point(tempRandomX1, tempRandomY1), new Point(tempRandomX2, tempRandomY2));
            if (path3 == null) { Console.WriteLine("Path3 is null"); }

            int tempRandomY5 = new Random().Next(1, 8);
            List<Point> path4 = Pathfinder.AStarPathfinding(new Point(tempRandomX2, tempRandomY2), new Point(12, tempRandomY5));
            if (path4 == null) { Console.WriteLine("Path4 is null"); }

            PathPoints = new List<Vector2>();
            AddPathPoints(path1);
            AddPathPoints(path2);
            AddPathPoints(path3);
            AddPathPoints(path4);
        }

        private void AddPathPoints(List<Point> path)
        {
            foreach (var point in path)
            {
                Vector2 screenPos = GridToScreen(point);
                PathPoints.Add(screenPos);
            }
        }

        public void Update()
        {
            // Update logic goes here
            InputManager.Update();
            _map.Update();
        }

        public void Draw()
        {
            _map.Draw();
        }
    }
}
