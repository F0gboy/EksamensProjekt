using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

public class Projectile
{
    //Leonard

    public Vector2 Position { get; private set; }
    private Vector2 direction;
    private float speed;
    private int damage;
    private Texture2D texture;
    public bool IsActive { get; set; }
    public float Radius { get; private set; }

    public Projectile(Vector2 position, Vector2 direction, float speed, int damage, Texture2D texture)
    {
        Position = position;
        this.direction = direction;
        this.speed = speed;
        this.damage = damage;
        this.texture = texture;
        IsActive = true;
        Radius = texture.Width; // Example, set the collision radius based on the texture size
    }

    public void Update(GameTime gameTime)
    {
        Position += direction * speed * (float)gameTime.ElapsedGameTime.TotalSeconds;
    }

    public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
    {
        spriteBatch.Draw(texture, Position, Color.White);
    }
}