using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace MysteryOfTheDungeon
{
    class Player
    {
        protected Interaction InteractionManager;
        protected Inventory Inventory;
        protected AnimationManager AnimationManager;
        protected Dictionary<string, Animations> AnimationsDictionary;
        protected int TextureHight = 31;
        protected int TextureWidth = 20;
        protected int MapTextureSide = 30;
        public Vector2 PositionValue;
        public float Speed;
        protected List<CellType> CollisionTextures = new List<CellType>() { CellType.Bonfire, CellType.WallFront, CellType.WallTop,
            CellType.Pedestal, CellType.BedsideTable, CellType.TableWithBook, CellType.BedTop, CellType.BedBottom, CellType.Dummy, 
            CellType.Vase, CellType.ClosedChest, CellType.Bake, CellType.KitchenTable, CellType.Tabletop, CellType.Sink, CellType.Chair,
            CellType.DinnerTable, CellType.BrokenVase, CellType.Basket, CellType.Ambry, CellType.BookTable, CellType.EmptyBasket, CellType.DressedDummy };

        public Player(Dictionary<string, Animations> animationsDictionary, Interaction interactionManager, Inventory inventory)
        {
            AnimationsDictionary = animationsDictionary;
            AnimationManager = new AnimationManager(AnimationsDictionary.First().Value);
            AnimationManager.Position = PositionValue;
            Inventory = inventory;
            InteractionManager = interactionManager;
        }

        #region Move

        public void Move(SpriteMap[,] Map)
        {
            float dx = 0;
            float dy = 0;
            if (Keyboard.GetState().IsKeyDown(Keys.Left))
            {
                dx = -Speed;
                AnimationManager.Play(AnimationsDictionary["GoLeft"]);
            }
            else if (Keyboard.GetState().IsKeyDown(Keys.Right))
            {
                dx = Speed;
                AnimationManager.Play(AnimationsDictionary["GoRight"]);
            }
            else if (Keyboard.GetState().IsKeyDown(Keys.Up))
            {
                dy = -Speed;
                AnimationManager.Play(AnimationsDictionary["GoUp"]);
            }
            else if (Keyboard.GetState().IsKeyDown(Keys.Down))
            {
                dy = Speed;
                AnimationManager.Play(AnimationsDictionary["GoDown"]);
            }
            else
            {
                AnimationManager.Stop();
            }
            if (Keyboard.GetState().IsKeyDown(Keys.Space))
            {
                var playerCellPositionX = (int)((PositionValue.X + (TextureWidth / 2)) / MapTextureSide + 1);
                var playerCellPositionY = (int)((PositionValue.Y + TextureWidth) / MapTextureSide + 1);

                var interactionCells = GenerateInteractionCells(playerCellPositionX, playerCellPositionY, Map);
                foreach (var cell in interactionCells)
                {
                    InteractionManager.MakeInteraction(cell, Inventory.CurrentInventory);
                }
            }
           foreach (var mapCell in Map)
            {
                if (IsTouchingLeft(mapCell, dx) && Keyboard.GetState().IsKeyDown(Keys.Left))
                {
                    dx = 0;
                    AnimationManager.Stop();
                }
                else if (IsTouchingRight(mapCell, dx) && Keyboard.GetState().IsKeyDown(Keys.Right))
                {
                    dx = 0;
                    AnimationManager.Stop();
                }
                else if (IsTouchingTopOrBottom(mapCell, dy) && (Keyboard.GetState().IsKeyDown(Keys.Up) || Keyboard.GetState().IsKeyDown(Keys.Down)))
                {
                    dy = 0;
                    AnimationManager.Stop();
                }
            }
            PositionValue.X += dx;
            PositionValue.Y += dy;
        }

        public List<SpriteMap> GenerateInteractionCells(int positionX, int positionY, SpriteMap[,] map)
        {
            /*
            var shift = new List<int>() { 1, -1 };
            var result = new List<(int, int)>() { (positionX - 1, positionY - 1) };
            for (int i = 0; i < 2; i++)
            {
                if (i == 0)
                {
                    foreach (var j in shift)
                    {
                        // -1 в каждой координате тк отсчёт в массиве с 0
                        if (positionX + j - 1 > 0 && positionX + j - 1 < 27)
                            result.Add(((positionX + j) - 1, positionY - 1));
                    }
                }
                else
                {
                    foreach (var j in shift)
                    {
                        if (positionY + j - 1 > 0 && positionY + j - 1 < 28)
                            result.Add((positionX - 1, (positionY + j) - 1));
                    }
                }*/
                //генерация такая, потому что нужно проверять не выходит ли индекс массива за пределы
                var shift = new List<int>() { 1, -1 };
                var result = new List<SpriteMap>() { map[positionX - 1, positionY - 1] };
                for(int i = 0; i < 2; i++)
                {
                    if (i == 0)
                    {
                        foreach (var j in shift)
                        {
                            // -1 в каждой координате тк отсчёт в массиве с 0
                            if (positionX + j - 1 > 0 && positionX + j - 1 < 27)
                                result.Add(map[(positionX + j) - 1, positionY - 1]);
                        }
                    }
                    else
                    {
                        foreach (var j in shift)
                        {
                            if (positionY + j - 1 > 0 && positionY + j - 1 < 28)
                                result.Add(map[positionX - 1, (positionY + j) - 1]);
                        }
                    }
            }
            return result;
        }

        #endregion

        #region Collisions

        protected bool IsTouchingLeft(SpriteMap spriteMap, float dx)
        {
            var mapTextureSide = spriteMap.Texture.Width;
            // проверка левого нижнего угла текстуры персонажа
            var playerX = PositionValue.X + dx;
            var playerY = PositionValue.Y + TextureHight;
            return playerX <= spriteMap.Position.X + mapTextureSide && playerX >= spriteMap.Position.X && playerY
                >= spriteMap.Position.Y && playerY <= spriteMap.Position.Y + mapTextureSide && CollisionTextures.Contains(spriteMap.Value);
        }

        protected bool IsTouchingRight(SpriteMap spriteMap, float dx)
        {
            var mapTextureSide = spriteMap.Texture.Width;
            // проверка правого нижнего угла текстуры персонажа
            var playerX = PositionValue.X + TextureWidth + dx;
            var playerY = PositionValue.Y + TextureHight;
            return playerX >= spriteMap.Position.X && playerX <= spriteMap.Position.X + mapTextureSide && playerY
                >= spriteMap.Position.Y && playerY <= spriteMap.Position.Y + mapTextureSide && CollisionTextures.Contains(spriteMap.Value);
        }

        protected bool IsTouchingTopOrBottom(SpriteMap spriteMap, float dy)
        {
            var mapTextureSide = spriteMap.Texture.Width;
            // проверка и левого, и правого нижних углов текстуры
            var playerLeftX = PositionValue.X;
            var playerRightX = PositionValue.X + TextureWidth;
            var playerY = PositionValue.Y + TextureHight + dy;
            return ((playerLeftX > spriteMap.Position.X && playerLeftX < spriteMap.Position.X + mapTextureSide) ||
                (playerRightX > spriteMap.Position.X && playerRightX < spriteMap.Position.X + mapTextureSide)) &&
                playerY >= spriteMap.Position.Y && playerY <= spriteMap.Position.Y + mapTextureSide && CollisionTextures.Contains(spriteMap.Value);
        }

        #endregion

        public void Update(GameTime gameTime, SpriteMap[,] Map)
        {
            Move(Map);
            
            AnimationManager.Update(gameTime);
            AnimationManager.Position = PositionValue;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
                AnimationManager.Draw(spriteBatch);
        }
    }
}
