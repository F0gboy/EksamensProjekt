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
            //Set tile texture based on if it is a path or not
            texture = Path ? Globals.Content.Load<Texture2D>("watertile2") : Globals.Content.Load<Texture2D>("iceTile2");
        }
    }
}
