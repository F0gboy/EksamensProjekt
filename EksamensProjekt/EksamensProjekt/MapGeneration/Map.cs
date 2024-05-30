using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;

namespace EksamensProjekt.MapGeneration
{
    public class Map
    {
        public readonly Point Size = new(13, 9);
        public Tile[,] Tiles { get; }
        public Point TileSize { get; }
        public Vector2 MapToScreen(int x, int y) => new(x * TileSize.X, y * TileSize.Y);
        public (int x, int y) ScreenToMap(int x, int y) => (x / TileSize.X, y / TileSize.Y);

        public SpriteBatch _spriteBatch;
        public Game game;
        public GameTime gameTime;

        public Map()
        {
            // Create the map
            Tiles = new Tile[Size.X, Size.Y];
            var texture = Globals.Content.Load<Texture2D>("iceTile2");
            TileSize = new(texture.Width, texture.Height);

            // Create the tiles
            for (int y = 0; y < Size.Y; y++)
            {
                for (int x = 0; x < Size.X; x++)
                {
                    Tiles[x, y] = new(game, texture, MapToScreen(x, y), x, y);
                }
            }
        }

        public void Update()
        {
            // Update the tiles
            for (int y = 0; y < Size.Y; y++)
            {

                for (int x = 0; x < Size.X; x++)
                {
                    Tiles[x, y].Update();
                }
            }
        }

        public void Draw()
        {
            // Draw the tiles
            for (int y = 0; y < Size.Y; y++)
            {

                for (int x = 0; x < Size.X; x++)
                {
                    Tiles[x, y].Draw(gameTime, _spriteBatch);
                }
            }
        }
    }
}
