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
        Bonfire = 3,
        Pedestal = 4,
        BedsideTable = 5,
        TableWithBook = 6,
        BedTop = 7,
        BedBottom = 8,
        Dummy = 9,
        Vase = 'v'// = 'v'
    }

    class Map
    {
        private Texture2D WallTopTexture;
        private Texture2D FloorTexture;
        private Texture2D WallFrontTexture;
        private Texture2D BonfireTexture;
        private Texture2D PedestalTexture;
        private Texture2D BedsideTableTexture;
        private Texture2D TableWithBookTexture;
        private Texture2D BedTopTexture;
        private Texture2D BedBottomTexture;
        private Texture2D DummyTexture;
        private Texture2D VaseTexture;
        protected int MapTextureSide = 30;

        public Map(Texture2D wallTopTexture, Texture2D floorTexture, Texture2D wallFrontTexture, Texture2D bonfireTexture,
            Texture2D pedestalTexture, Texture2D bedsideTableTexture, Texture2D tableWithBookTexture, Texture2D bedTopTexture,
            Texture2D bedBottomTexture, Texture2D dummyTexture, Texture2D vaseTexture)
        {
            WallTopTexture = wallTopTexture;
            FloorTexture = floorTexture;
            WallFrontTexture = wallFrontTexture;
            BonfireTexture = bonfireTexture;
            PedestalTexture = pedestalTexture;
            BedsideTableTexture = bedsideTableTexture;
            TableWithBookTexture = tableWithBookTexture;
            BedTopTexture = bedTopTexture;
            BedBottomTexture = bedBottomTexture;
            DummyTexture = dummyTexture;
            VaseTexture = vaseTexture;
        }
        
        private readonly List<string> MapConstructor = new List<string>
        {
            "0222222200000000222222222220",
            "0115751900000000111111111110",
            "0111811122222222111111111110",
            "0111111111111111111111111110",
            "0161111101001000111111111110",
            "011111v101001000111111111110",
            "0000000001001000111111111110",
            "0020022221221000111111111110",
            "0012211111111000111111111110",
            "0011110000001000000000000000",
            "0000012200001000022202220200",
            "0022011102221222011101112100",
            "0011200001111111010101011100",
            "0011122001111111010101010000",
            "0010111221114111210121010000",
            "0012001111143411110111010000",
            "0011000001114111000000010000",
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
                    if ("0123456789".Contains(mapCell))
                    {
                        var mapCellValue = int.Parse(mapCell.ToString();
                        if (mapCellValue == 0)
                        {
                            texture = WallTopTexture;
                            cellType = CellType.WallTop;
                        }
                        else if (mapCellValue == 1)
                        {
                            texture = FloorTexture;
                            cellType = CellType.Floor;
                        }
                        else if (mapCellValue == 2)
                        {
                            texture = WallFrontTexture;
                            cellType = CellType.WallFront;
                        }
                        else if (mapCellValue == 3)
                        {
                            texture = BonfireTexture;
                            cellType = CellType.Pedestal;
                        }
                        else if (mapCellValue == 4)
                        {
                            texture = PedestalTexture;
                            cellType = CellType.Bonfire;
                        }
                        else if (mapCellValue == 5)
                        {
                            texture = BedsideTableTexture;
                            cellType = CellType.BedsideTable;
                        }
                        else if (mapCellValue == 6)
                        {
                            texture = TableWithBookTexture;
                            cellType = CellType.TableWithBook;
                        }
                        else if (mapCellValue == 7)
                        {
                            texture = BedTopTexture;
                            cellType = CellType.BedTop;
                        }
                        else if (mapCellValue == 8)
                        {
                            texture = BedBottomTexture;
                            cellType = CellType.BedBottom;
                        }
                        else //if (mapCellValue == 9)
                        {
                            texture = DummyTexture;
                            cellType = CellType.Dummy;
                        }
                    }
                    else //if (mapCell.Equals('v'))
                    {
                        texture = VaseTexture;
                        cellType = CellType.Vase;
                    }
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

    /*class Bonfire
    {
        protected AnimationManager AnimationManager;
        protected Animations Animation;

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

            AnimationManager.Play(Animation);  
            AnimationManager.Update(gameTime);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            AnimationManager.Draw(spriteBatch);
        }
    }*/
}
