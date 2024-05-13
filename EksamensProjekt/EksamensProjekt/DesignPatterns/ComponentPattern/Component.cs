using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EksamensProjekt.DesignPatterns.ComponentPattern
{
    public abstract class Component
    {
        public Component(Game game)
        {
            
        }

        public virtual void Initialize()
        {
            // Initialization logic goes here
        }

        public virtual void Update(GameTime gameTime)
        {
            // Update logic goes here
        }

        public virtual void Draw(GameTime gameTime)
        {
            // Draw logic goes here
        }
    }
}
