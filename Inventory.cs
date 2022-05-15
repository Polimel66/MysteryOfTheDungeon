using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MysteryOfTheDungeon
{
    enum InventoryItems
    {
        Empty,
        MasterKey,
        Dress,
        Shoes,
        Shovel,
        NailPuller,
        GoldenKey,
        BlueKey
        // + 4 реликвии
    }

    class Inventory
    {
        private Texture2D BackgroundTexture;
        private Vector2 PositionBackground = new Vector2(30 * 28, 0);
        public InventoryItems[] CurrentInventory;
        protected readonly int CellSize = 85;
        protected readonly int PaddingBetweenCells = 5;
        private Texture2D EmptyTexture;

        public Inventory(Texture2D inventoryTexture, Texture2D emptyTexture)
        {
            BackgroundTexture = inventoryTexture;
            CurrentInventory = new InventoryItems[12];
            for(var i = 0; i < CurrentInventory.Length; i++)
                CurrentInventory[i] = InventoryItems.Empty;
            EmptyTexture = emptyTexture;
        }

        public SpriteMap[,] GenerateInventory()
        {
            var resultInventory = new SpriteMap[3, 4];
            var position = new Vector2(866, 470);
            Texture2D texture;
            var lineCounter = 0;
            var columnCounter = 0;
            for (int i = 0; i < CurrentInventory.Length; i++)
            {
                var inventoryCell = CurrentInventory[i];
                switch (inventoryCell)
                {
                    case InventoryItems.Empty:
                        texture = EmptyTexture;
                        break;
                    default:
                        texture = EmptyTexture;
                        break;
                }
                resultInventory[lineCounter, columnCounter] = new SpriteMap(texture, position);
                if ((i + 1) % 4 == 0)
                {
                    lineCounter += 1;
                    position.Y += CellSize + PaddingBetweenCells;
                    position.X = 866;
                    columnCounter = 0;
                }
                else
                {
                    position.X += CellSize + PaddingBetweenCells;
                    columnCounter += 1;
                }
                    
            }
            return resultInventory;
        }

        public void Update(GameTime gameTime)
        {
        }

        public void Draw(SpriteBatch spriteBatch, SpriteMap[,] inventory)
        {
            spriteBatch.Draw(BackgroundTexture, PositionBackground, Color.White);
            foreach (var inventoryCell in inventory)
            {
                spriteBatch.Draw(inventoryCell.Texture, inventoryCell.Position, Color.White);
            }
        }
    }
}
