using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MysteryOfTheDungeon
{
    class Inventory
    {
        private Texture2D InventoryTexture;
        private Vector2 Position = new Vector2(30 * 28, 0);

        public Inventory(Texture2D inventoryTexture)
        {
            InventoryTexture = inventoryTexture;
        }
        public void Update(GameTime gameTime)
        {
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(InventoryTexture, Position, Color.White);
        }
    }
}
