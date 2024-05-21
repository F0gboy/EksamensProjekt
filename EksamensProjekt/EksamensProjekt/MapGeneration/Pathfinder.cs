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

        public static void Init(Map map)
        {
            _map = map;
            CreateNodeMap();
        }

        public static (int x, int y) ScreenToMap(Vector2 pos)
        {
            return _map.ScreenToMap((int)pos.X, (int)pos.Y);
        }

        private static bool IsValid(int x, int y)
        {
            return x >= 0 && x < _nodeMap.GetLength(0) && y >= 0 && y < _nodeMap.GetLength(1);
        }

        private static void CreateNodeMap()
        {
            _nodeMap = new Node[_map.Size.X, _map.Size.Y];

            for (int i = 0; i < _map.Size.X; i++)
            {
                for (int j = 0; j < _map.Size.Y; j++)
                {
                    _nodeMap[i, j] = new Node(i, j);
                    if (_map.Tiles[i, j].Blocked) _nodeMap[i, j].visited = true;
                }
            }
        }

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

        public static List<Point> AStarPathfinding(Point start, Point goal)
        {
           ResetNodeMap();

            List<Node> openList = new List<Node>();
            HashSet<Node> closedList = new HashSet<Node>();

            Node startNode = _nodeMap[start.X, start.Y];
            startNode.visited = true;
            openList.Add(startNode);

            while (openList.Count > 0)
            {
                Node current = openList.OrderBy(node => node.x + node.y + Heuristic(node.x, node.y, goal.X, goal.Y)).First();

                if (current.x == goal.X && current.y == goal.Y)
                {
                    return RetracePath(goal.X, goal.Y);
                }

                openList.Remove(current);
                closedList.Add(current);

                for (int i = 0; i < row.Length; i++)
                {
                    int newX = current.x + row[i];
                    int newY = current.y + col[i];

                    if (IsValid(newX, newY) && !_nodeMap[newX, newY].visited && !_map.Tiles[newX, newY].Blocked)
                    {
                        Node neighbor = _nodeMap[newX, newY];
                        if (closedList.Contains(neighbor)) continue;

                        neighbor.parent = current;

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

        private static int Heuristic(int x, int y, int goalX, int goalY)
        {
            return Math.Abs(x - goalX) + Math.Abs(y - goalY);
        }

        private static List<Point> RetracePath(int goalX, int goalY)
        {
            Stack<Point> stack = new Stack<Point>();
            List<Point> result = new List<Point>();
            Node curr = _nodeMap[goalX, goalY];

            while (curr != null)
            {
                _map.Tiles[curr.x, curr.y].Path = true;
                stack.Push(new Point(curr.x, curr.y));
                curr = curr.parent;
            }

            while (stack.Count > 0) result.Add(stack.Pop());

            return result;
        }
    }
}
