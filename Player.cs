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
        protected AnimationManager AnimationManager;
        protected Dictionary<string, Animations> AnimationsDictionary;
        protected int TextureHight = 31;
        protected int TextureWidth = 20;
        public Vector2 PositionValue;
        public float Speed;
        protected List<CellType> CollisionTextures = new List<CellType>() { CellType.Bonfire, CellType.WallFront, CellType.WallTop,
            CellType.Pedestal, CellType.BedsideTable, CellType.TableWithBook, CellType.BedTop, CellType.BedBottom, CellType.Dummy, 
            CellType.Vase, CellType.ClosedChest, CellType.Bake, CellType.KitchenTable, CellType.Tabletop, CellType.Sink, CellType.Chair,
            CellType.DinnerTable, CellType.BrokenVase, CellType.Basket, CellType.Ambry, CellType.BookTable };

        public Player(Dictionary<string, Animations> animationsDictionary)
        {
            AnimationsDictionary = animationsDictionary;
            AnimationManager = new AnimationManager(AnimationsDictionary.First().Value);
            AnimationManager.Position = PositionValue;
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
