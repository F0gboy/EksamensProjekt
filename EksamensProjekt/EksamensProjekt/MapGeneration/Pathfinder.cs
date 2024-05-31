using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;

namespace EksamensProjekt.MapGeneration
{
    public static class Pathfinder
    {
        //Jasper

        // Node class for A* pathfinding
        class Node
        {
            public int x;
            public int y;
            public Node parent;
            public bool visited;

            public Node(int x, int y)
            {
                this.x = x;
                this.y = y;
                this.visited = false;
                this.parent = null;
            }
        }

        private static Node[,] _nodeMap;
        private static Map _map;

        private static readonly int[] row = { -1, 0, 0, 1 };
        private static readonly int[] col = { 0, -1, 1, 0 };

        // Initialize the pathfinder with the map
        public static void Init(Map map)
        {
            _map = map;
            CreateNodeMap();
        }

        // Convert screen coordinates to map coordinates
        public static (int x, int y) ScreenToMap(Vector2 pos)
        {
            return _map.ScreenToMap((int)pos.X, (int)pos.Y);
        }

        // Check if a position is valid
        private static bool IsValid(int x, int y)
        {
            return x >= 0 && x < _nodeMap.GetLength(0) && y >= 0 && y < _nodeMap.GetLength(1);
        }

        // Create a node map from the map
        private static void CreateNodeMap()
        {
            _nodeMap = new Node[_map.Size.X, _map.Size.Y];

            // Create a node for each tile
            for (int i = 0; i < _map.Size.X; i++)
            {
                for (int j = 0; j < _map.Size.Y; j++)
                {
                    _nodeMap[i, j] = new Node(i, j);
                }
            }
        }

        // Reset the node map
        private static void ResetNodeMap()
        {
            for (int i = 0; i < _map.Size.X; i++)
            {
                for (int j = 0; j < _map.Size.Y; j++)
                {
                    _nodeMap[i, j].visited = false;
                    _nodeMap[i, j].parent = null;
                }
            }
        }

        // A* pathfinding algorithm
        public static List<Point> AStarPathfinding(Point start, Point goal)
        {
           ResetNodeMap();

           // Open and closed list for the algorithm
            List<Node> openList = new List<Node>();
            HashSet<Node> closedList = new HashSet<Node>();

            Node startNode = _nodeMap[start.X, start.Y];
            startNode.visited = true;
            openList.Add(startNode);

            // A* algorithm
            while (openList.Count > 0)
            {
                Node current = openList.OrderBy(node => node.x + node.y + Heuristic(node.x, node.y, goal.X, goal.Y)).First();

                // If the goal is reached, retrace the path
                if (current.x == goal.X && current.y == goal.Y)
                {
                    return RetracePath(goal.X, goal.Y);
                }

                openList.Remove(current);
                closedList.Add(current);

                // Check neighbors
                for (int i = 0; i < row.Length; i++)
                {
                    int newX = current.x + row[i];
                    int newY = current.y + col[i];

                    // Check if the neighbor is valid and not blocked
                    if (IsValid(newX, newY) && !_nodeMap[newX, newY].visited && !_map.Tiles[newX, newY].Blocked)
                    {
                        Node neighbor = _nodeMap[newX, newY];
                        if (closedList.Contains(neighbor)) continue;

                        neighbor.parent = current;

                        // Add the neighbor to the open list
                        if (!openList.Contains(neighbor))
                        {
                            openList.Add(neighbor);
                            _nodeMap[newX, newY].visited = true;
                        }
                    }
                }
            }

            return null;
        }

        // Heuristic function for A*
        private static int Heuristic(int x, int y, int goalX, int goalY)
        {
            return Math.Abs(x - goalX) + Math.Abs(y - goalY);
        }

        // Retrace the path from the goal to the start
        private static List<Point> RetracePath(int goalX, int goalY)
        {
            Stack<Point> stack = new Stack<Point>();
            List<Point> result = new List<Point>();
            Node curr = _nodeMap[goalX, goalY];

            while (curr != null)
            {
                // Mark the path on the map
                _map.Tiles[curr.x, curr.y].Path = true;
                stack.Push(new Point(curr.x, curr.y));
                curr = curr.parent;
            }

            while (stack.Count > 0) result.Add(stack.Pop());

            return result;
        }
    }
}
