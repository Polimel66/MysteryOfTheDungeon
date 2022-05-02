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
    enum CellType
    {
        WallTop = 0,
        Floor = 1,
        WallFront = 2,
        Bonfire = 3
    }

    class Map
    {
        private Texture2D WallTopTexture;
        private Texture2D FloorTexture;
        private Texture2D WallFrontTexture;
        protected int MapTextureSide = 30;

        public Map(Texture2D wallTopTexture, Texture2D floorTexture, Texture2D wallFrontTexture)
        {
            WallTopTexture = wallTopTexture;
            FloorTexture = floorTexture;
            WallFrontTexture = wallFrontTexture;
        }
        
        private readonly List<string> MapConstructor = new List<string>
        {
            "0222222200000000222222222220",
            "0111111100000000111111111110",
            "0111111122222222111111111110",
            "0111111111111111111111111110",
            "0111111101001000111111111110",
            "0111111101001000111111111110",
            "0000000001001000111111111110",
            "0020022221221000111111111110",
            "0012211111111000111111111110",
            "0011110000001000000000000000",
            "0000012200001000022202220200",
            "0022011102221222011101112100",
            "0011200001111111010101011100",
            "0011122001111111010101010000",
            "0010111221111111210121010000",
            "0012001111111111110111010000",
            "0011000001111111000000010000",
            "0000000001111111022222212220",
            "0222222201111111011111111110",
            "0111111100010000011111111110",
            "0111111100210000011111111110",
            "0111111122110000011111111110",
            "0111111111110000011111111110",
            "0111111100110000011111111110",
            "0111111100010000011111111110",
            "0000000000000000000000000000"
        };

        #region GenerateMap

        public SpriteMap[,] GenerateMap()
        {
            var resultMap = new SpriteMap[28, 26];
            var position = new Vector2(0, 0);
            Texture2D texture;
            CellType cellType;
            for (var y = 0; y < MapConstructor.Count; y++)
            {
                for (var x = 0; x < MapConstructor.ElementAt(y).Length; x++)
                {
                    var mapCell = MapConstructor.ElementAt(y)[x];
                    if (int.Parse(mapCell.ToString()) == 0)
                    {
                        texture = WallTopTexture;
                        cellType = CellType.WallTop;
                    }
                    else if (int.Parse(mapCell.ToString()) == 1)
                    {
                        texture = FloorTexture;
                        cellType = CellType.Floor;
                    }
                    else// if (int.Parse(mapCell.ToString()) == 2)
                    {
                        texture = WallFrontTexture;
                        cellType = CellType.WallFront;
                    }
                    //else
                    //{
                    //    texture = WallFrontTexture;
                    //    cellType = CellType.Bonfire;
                    //}
                    resultMap[x, y] = new SpriteMap(texture, position, cellType);
                    position.X += MapTextureSide;
                }
                position.X = 0;
                position.Y += MapTextureSide;
            }
            return resultMap;
        }

        #endregion

        public void Draw(SpriteBatch spriteBatch, SpriteMap[,] map)
        {
            foreach(var mapCell in map)
            {
                spriteBatch.Draw(mapCell.Texture, mapCell.Position, Color.White);
            }
        }
    }

    class Bonfire
    {
        protected AnimationManager AnimationManager;
        protected Animations Animation;
        public bool fire = true;

        public Bonfire(Animations animation)
        {
            Animation = animation;
            AnimationManager = new AnimationManager(Animation)
            {
                Position = new Vector2(360, 450)

            };
        }

        public void Update(GameTime gameTime)
        {
            //if(fire)
            //{
            AnimationManager.Play(Animation);
            //    fire = false;
            //}
            
            AnimationManager.Update(gameTime);
            //AnimationManager.Position = new Vector2(210, 570);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            if (AnimationManager != null)
                AnimationManager.Draw(spriteBatch);
        }
    }
}
