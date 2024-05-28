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
    public Vector2 Position { get; private set; }
    private Vector2 direction;
    private float speed;
    public int Damage { get; private set; }
    private Texture2D texture;
    public bool IsActive { get; set; }
    public float Radius { get; private set; }

    public Projectile(Vector2 position, Vector2 direction, float speed, int damage, Texture2D texture)
    {
        this.Position = position;
        this.direction = direction;
        this.speed = speed;
        this.Damage = damage;
        this.texture = texture;
        this.IsActive = true;
        this.Radius = texture.Width / 2; // Assuming the projectile is circular
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