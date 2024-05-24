using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EksamensProjekt.State_Pattern
{
  public interface I_State_Menu
    {
       public void Update(Menu menu,GameTime gameTime);
        public void Draw(Menu menu,SpriteBatch spriteBatch);
    }
}
