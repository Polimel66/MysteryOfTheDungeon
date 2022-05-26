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
        private SpriteMap[,] _Inventory;
        private Texture2D BackgroundTexture;
        private Texture2D BackgroundForTextTexture;
        private Vector2 PositionBackground = new Vector2(30 * 28, 0);
        private Vector2 PositionOutputText = new Vector2(915, 110);
        private Vector2 PositionBackgroundForText = new Vector2(30 * 28 + 25, 60);
        public InventoryItems[] CurrentInventory;
        public Dictionary<int, bool> CellState;
        private MouseHandling Mouse;
        protected readonly int CellSize = 85;
        protected readonly int PaddingBetweenCells = 5;
        private Texture2D EmptyTexture;
        private Texture2D MasterKeyTexture;
        private Texture2D NailPullerTexture;
        private Texture2D ShovelTexture;
        private Texture2D GoldenKeyTexture;
        private Texture2D DressTexture;
        private Texture2D HatTexture;
        private Texture2D ShoesTexture;
        private Texture2D EmptyOnTexture;
        private Texture2D MasterKeyOnTexture;
        private Texture2D NailPullerOnTexture;
        private Texture2D ShovelOnTexture;
        private Texture2D GoldenKeyOnTexture;
        private Texture2D DressOnTexture;
        private Texture2D HatOnTexture;
        private Texture2D ShoesOnTexture;
        private Texture2D FirstRelicTexture;
        private Texture2D FirstRelicOnTexture;

        public Inventory(Texture2D inventoryTexture, Texture2D emptyTexture, Texture2D backgroundForTextTexture,
            Texture2D masterKeyTexture, Texture2D nailPullerTexture, Texture2D shovelTexture, Texture2D goldenKeyTexture,
            Texture2D dressTexture, Texture2D hatTexture, Texture2D shoesTexture, Texture2D emptyOnTexture,
            Texture2D masterKeyOnTexture, Texture2D nailPullerOnTexture, Texture2D shovelOnTexture,
            Texture2D goldenKeyOnTexture, Texture2D dressOnTexture, Texture2D hatOnTexture, Texture2D shoesOnTexture,
            Texture2D firstRelicTexture, Texture2D firstRelicOnTexture)
        {
            BackgroundTexture = inventoryTexture;
            BackgroundForTextTexture = backgroundForTextTexture;
            CurrentInventory = new InventoryItems[12];
            for(var i = 0; i < CurrentInventory.Length; i++)
                CurrentInventory[i] = InventoryItems.Empty;
            CellState = new Dictionary<int, bool>();
            for (var i = 0; i < CurrentInventory.Length; i++)
                CellState[i] = false;
            Mouse = new MouseHandling(CellState);
            EmptyTexture = emptyTexture;
            MasterKeyTexture = masterKeyTexture;
            NailPullerTexture = nailPullerTexture;
            ShovelTexture = shovelTexture;
            GoldenKeyTexture = goldenKeyTexture;
            DressTexture = dressTexture;
            HatTexture = hatTexture;
            ShoesTexture = shoesTexture;
            EmptyOnTexture = emptyOnTexture;
            MasterKeyOnTexture = masterKeyOnTexture;
            NailPullerOnTexture = nailPullerOnTexture;
            ShovelOnTexture = shovelOnTexture;
            GoldenKeyOnTexture = goldenKeyOnTexture;
            DressOnTexture = dressOnTexture;
            HatOnTexture = hatOnTexture;
            ShoesOnTexture = shoesOnTexture;
            FirstRelicTexture = firstRelicTexture;
            FirstRelicOnTexture = firstRelicOnTexture;
        }

        public SpriteMap[,] GenerateInventory()
        {
            var resultInventory = new SpriteMap[3, 4];
            var position = new Vector2(875, 470);
            Texture2D texture;
            var lineCounter = 0;
            var columnCounter = 0;
            for (int i = 0; i < CurrentInventory.Length; i++)
            {
                var inventoryCell = CurrentInventory[i];
                switch (inventoryCell)
                {
                    case InventoryItems.Empty:
                        if(!CellState[i])
                            texture = EmptyTexture;
                        else
                            texture = EmptyOnTexture;
                        break;
                    case InventoryItems.MasterKey:
                        if (!CellState[i])
                            texture = MasterKeyTexture;
                        else
                            texture = MasterKeyOnTexture;
                        break;
                    case InventoryItems.NailPuller:
                        if (!CellState[i])
                            texture = NailPullerTexture;
                        else
                            texture = NailPullerOnTexture;
                        break;
                    case InventoryItems.Shovel:
                        if (!CellState[i])
                            texture = ShovelTexture;
                        else
                            texture = ShovelOnTexture;
                        break;
                    case InventoryItems.GoldenKey:
                        if (!CellState[i])
                            texture = GoldenKeyTexture;
                        else
                            texture = GoldenKeyOnTexture;
                        break;
                    case InventoryItems.Dress:
                        if (!CellState[i])
                            texture = DressTexture;
                        else
                            texture = DressOnTexture;
                        break;
                    case InventoryItems.Hat:
                        if (!CellState[i])
                            texture = HatTexture;
                        else
                            texture = HatOnTexture;
                        break;
                    case InventoryItems.Shoes:
                        if (!CellState[i])
                            texture = ShoesTexture;
                        else
                            texture = ShoesOnTexture;
                        break;
                    case InventoryItems.FirstRelic:
                        if (!CellState[i])
                            texture = FirstRelicTexture;
                        else
                            texture = FirstRelicOnTexture;
                        break;
                    default:
                        texture = EmptyTexture;
                        break;
                }
                resultInventory[lineCounter, columnCounter] = new SpriteMap(texture, position);
                if ((i + 1) % 4 == 0)
                {
                    lineCounter += 1;
                    position.Y += CellSize;
                    position.X = 875; // высчитанная координата для отрисовки инвентаря
                    columnCounter = 0;
                }
                else
                {
                    position.X += CellSize;
                    columnCounter += 1;
                }   
            }
            return resultInventory;
        }

        public void Update()
        {
            Mouse.Update();
            _Inventory = GenerateInventory();
        }

        public void Draw(SpriteBatch spriteBatch, SpriteFont spriteFont, string outputText)
        {
            spriteBatch.Draw(BackgroundTexture, PositionBackground, Color.White);
            spriteBatch.Draw(BackgroundForTextTexture, PositionBackgroundForText, Color.White);
            foreach (var inventoryCell in _Inventory)
            {
                spriteBatch.Draw(inventoryCell.Texture, inventoryCell.Position, Color.White);
            }
            spriteBatch.DrawString(spriteFont, outputText, PositionOutputText, Color.White);
        }
    }
}
