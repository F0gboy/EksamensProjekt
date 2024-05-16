using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EksamensProjekt.DesignPatterns.ComponentPattern;
using Microsoft.Xna.Framework;

namespace EksamensProjekt.MapGeneration
{
    public class Tile : SpriteRenderer
    {
        public bool Blocked { get; set; }
        public bool Path { get; set; }

        private readonly int _mapX;
        private readonly int _mapY;

        public Tile(Game game, Texture2D texture, Vector2 position, int mapX, int mapY) : base(game, texture, position)
        {
            _mapX = mapX;
            _mapY = mapY;
            
        }

        public void Update()
        {
            if (Rectangle.Contains(InputManager.MouseRectangle))
            {
                if (InputManager.MouseClicked)
                {
                    Blocked = !Blocked;
                }

                if (InputManager.MouseRightClicked)
                {
                   // Pathfinder.AStarPathfinding(_mapX, _mapY);
                }
            }

            //Color = Path ? Microsoft.Xna.Framework.Color.Green : Microsoft.Xna.Framework.Color.White;
            Color = Blocked ? Microsoft.Xna.Framework.Color.Red : Color;

            texture = Path ? Globals.Content.Load<Texture2D>("icemap") : Globals.Content.Load<Texture2D>("iceTile");
             
        }
    }
}
