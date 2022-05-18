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
        Hat,
        Dress,
        Shoes,
        Shovel,
        NailPuller,
        GoldenKey,
        BlueKey,
        FirstRelic
        // + 4 реликвии
    }

    class Inventory
    {
        private Texture2D BackgroundTexture;
        private Texture2D BackgroundForTextTexture;
        private Vector2 PositionBackground = new Vector2(30 * 28, 0);
        public InventoryItems[] CurrentInventory;
        protected readonly int CellSize = 85;
        protected readonly int PaddingBetweenCells = 5;
        private Texture2D EmptyTexture;
        private Texture2D MasterKeyTexture;
        private Texture2D NailPullerTexture;

        public Inventory(Texture2D inventoryTexture, Texture2D emptyTexture, Texture2D backgroundForTextTexture,
            Texture2D masterKeyTexture, Texture2D nailPullerTexture)
        {
            BackgroundTexture = inventoryTexture;
            BackgroundForTextTexture = backgroundForTextTexture;
            CurrentInventory = new InventoryItems[12];
            for(var i = 0; i < CurrentInventory.Length; i++)
                CurrentInventory[i] = InventoryItems.Empty;
            EmptyTexture = emptyTexture;
            MasterKeyTexture = masterKeyTexture;
            NailPullerTexture = nailPullerTexture;
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
                    case InventoryItems.MasterKey:
                        texture = MasterKeyTexture;
                        break;
                    case InventoryItems.NailPuller:
                        texture = NailPullerTexture;
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

        public void Draw(SpriteBatch spriteBatch, SpriteFont spriteFont, SpriteMap[,] inventory, string outputText)
        {
            spriteBatch.Draw(BackgroundTexture, PositionBackground, Color.White);
            spriteBatch.Draw(BackgroundForTextTexture, new Vector2(30 * 28 + 25, 60), Color.White);
            foreach (var inventoryCell in inventory)
            {
                spriteBatch.Draw(inventoryCell.Texture, inventoryCell.Position, Color.White);
            }
            spriteBatch.DrawString(spriteFont, outputText, new Vector2(915, 110), Color.White);
        }
    }
}
