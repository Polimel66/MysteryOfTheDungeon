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
        Vase = 'v',
        ClosedChest = 'c',
        Bake = 'b',
        KitchenTable = 'k',
        Sink = 's',
        Tabletop = 't',
        Chair = 'C',
        DinnerTable = 'd',
        BrokenVase = 'B',
        Shovel = 'S',
        Basket = 'K',
        Ambry = 'a',
        BookTable = 'T',
        GoldenKey = 'G',
        Grit = 'g',
        SandWithRelic = 'f',
        SandWithBlueKey = 'F',
        EmptyBasket = 'e',
        Hat = 'h',
        Shoes = 'H',
        DressedDummy = 'D',
        OpenedDoor = 'o',
        ClosedDoor = 'O',
        BookOnTable = 'l',
        ClosedGoldenDoor = 'N',
        OpenedGoldenDoor = 'n',
        Boards = 'L',
        BrokenBoards = 'p',
        ClosedBlueDoor = 'R',
        OpenedBlueDoor = 'r',
        ExcavatedSand = 'E',
        PileOfStones = 'P',
        Scroll = 'm',
        DugOutHeap = 'M',
        HeapWithRelic = 'x',
        Password = 'u'
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
        private Texture2D BrokenVaseTexture;
        private Texture2D ClosedChestTexture;
        private Texture2D BakeTexture;
        private Texture2D KitchenTableTexture;
        private Texture2D SinkTexture;
        private Texture2D TabletopTexture;
        private Texture2D ChairTexture;
        private Texture2D DinnerTableTexture;
        private Texture2D ShovelTexture;
        private Texture2D BasketTexture;
        private Texture2D AmbryTexture;
        private Texture2D BookTableTexture;
        private Texture2D GoldenKeyTexture;
        private Texture2D GritTexture;
        private Texture2D HatTexture;
        private Texture2D ShoesTexture;
        private Texture2D ClosedDoorTexture;
        private Texture2D BookOnTableTexture;
        private Texture2D ClosedGoldenDoorTexture;
        private Texture2D BoardsTexture;
        private Texture2D ClosedBlueDoorTexture;
        private Texture2D PileOfStonesTexture;
        private Texture2D ScrollTexture;
        private Texture2D PasswordTexture;

        protected readonly int MapTextureSide = 30;

        public Map(Texture2D wallTopTexture, Texture2D floorTexture, Texture2D wallFrontTexture, Texture2D bonfireTexture,
            Texture2D pedestalTexture, Texture2D bedsideTableTexture, Texture2D tableWithBookTexture, Texture2D bedTopTexture,
            Texture2D bedBottomTexture, Texture2D dummyTexture, Texture2D vaseTexture, Texture2D closedChestTexture, Texture2D bakeTexture,
            Texture2D kitchenTableTexture, Texture2D sinkTexture, Texture2D tabletopTexture, Texture2D chairTexture, Texture2D dinnerTableTexture,
            Texture2D brokenvaseTexture, Texture2D shovelTexture, Texture2D basketTexture, Texture2D ambryTexture, Texture2D bookTableTexture,
            Texture2D goldenKeyTexture, Texture2D gritTexture, Texture2D hatTexture, Texture2D shoesTexture, Texture2D closedDoorTexture,
            Texture2D bookOnTableTexture, Texture2D closedGoldenDoorTexture, Texture2D boardsTexture, Texture2D closedBlueDoorTexture,
            Texture2D pileOfStonesTexture, Texture2D scrollTexture, Texture2D passwordTexture)
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
            BrokenVaseTexture = brokenvaseTexture;
            ClosedChestTexture = closedChestTexture;
            BakeTexture = bakeTexture;
            KitchenTableTexture = kitchenTableTexture;
            SinkTexture = sinkTexture;
            TabletopTexture = tabletopTexture;
            ChairTexture = chairTexture;
            DinnerTableTexture = dinnerTableTexture;
            ShovelTexture = shovelTexture;
            BasketTexture = basketTexture;
            AmbryTexture = ambryTexture;
            BookTableTexture = bookTableTexture;
            GoldenKeyTexture = goldenKeyTexture;
            GritTexture = gritTexture;
            HatTexture = hatTexture;
            ShoesTexture = shoesTexture;
            ClosedDoorTexture = closedDoorTexture;
            BookOnTableTexture = bookOnTableTexture;
            ClosedGoldenDoorTexture = closedGoldenDoorTexture;
            BoardsTexture = boardsTexture;
            ClosedBlueDoorTexture = closedBlueDoorTexture;
            PileOfStonesTexture = pileOfStonesTexture;
            ScrollTexture = scrollTexture;
            PasswordTexture = passwordTexture;
        }
        
        private readonly List<string> MapConstructor = new List<string>
        {
            "0222222200000000222222222220",
            "0115751900000000aa11K1551c10",
            "0111811122222222111111111110",
            "01111111O111111N11111111T110",
            "01611111010010001111111111B0",
            "011111v1010010001111111111b0",
            "0000000001001000111CCCCC11s0",
            "0020022221221000111ddddd11t0",
            "00G2211111111000S111111111k0",
            "0011110000001000000000000000",
            "0000012200001000022202220200",
            "0022011u02221222011101112h00",
            "00112000011111l1010101011100",
            "0011122001111111010101010000",
            "0010111221114111210121010000",
            "0012001111143411110111010000",
            "00110000011141110000000R0000",
            "0000000001111111022222212220",
            "02222222011111110PP11P1111H0",
            "0ggggggg0001000001P1PP111110",
            "0ggggFgg0021000001111111PPP0",
            "0ggggggg221100000111111PP110",
            "0gggggggL1110000011111111110",
            "0gfggggg00110000011PP11111P0",
            "0ggggggg000100000P1P1111mPx0",
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
                        var mapCellValue = int.Parse(mapCell.ToString());
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
                    else if (mapCell.Equals('v'))
                    {
                        texture = VaseTexture;
                        cellType = CellType.Vase;
                    }
                    else if(mapCell.Equals('b'))
                    {
                        texture = BakeTexture;
                        cellType = CellType.Bake;
                    }
                    else if (mapCell.Equals('k'))
                    {
                        texture = KitchenTableTexture;
                        cellType = CellType.KitchenTable;
                    }
                    else if (mapCell.Equals('t'))
                    {
                        texture = TabletopTexture;
                        cellType = CellType.Tabletop;
                    }
                    else if (mapCell.Equals('c'))
                    {
                        texture = ClosedChestTexture;
                        cellType = CellType.ClosedChest;
                    }
                    else if (mapCell.Equals('C'))
                    {
                        texture = ChairTexture;
                        cellType = CellType.Chair;
                    }
                    else if (mapCell.Equals('d'))
                    {
                        texture = DinnerTableTexture;
                        cellType = CellType.DinnerTable;
                    }
                    else if (mapCell.Equals('B'))
                    {
                        texture = BrokenVaseTexture;
                        cellType = CellType.BrokenVase;
                    }
                    else if (mapCell.Equals('S'))
                    {
                        texture = ShovelTexture;
                        cellType = CellType.Shovel;
                    }
                    else if (mapCell.Equals('K'))
                    {
                        texture = BasketTexture;
                        cellType = CellType.Basket;
                    }
                    else if (mapCell.Equals('a'))
                    {
                        texture = AmbryTexture;
                        cellType = CellType.Ambry;
                    }
                    else if (mapCell.Equals('T'))
                    {
                        texture = BookTableTexture;
                        cellType = CellType.BookTable;
                    }
                    else if (mapCell.Equals('G'))
                    {
                        texture = GoldenKeyTexture;
                        cellType = CellType.GoldenKey;
                    }
                    else if (mapCell.Equals('g'))
                    {
                        texture = GritTexture;
                        cellType = CellType.Grit;
                    }
                    else if (mapCell.Equals('h'))
                    {
                        texture = HatTexture;
                        cellType = CellType.Hat;
                    }
                    else if (mapCell.Equals('H'))
                    {
                        texture = ShoesTexture;
                        cellType = CellType.Shoes;
                    }
                    else if (mapCell.Equals('O'))
                    {
                        texture = ClosedDoorTexture;
                        cellType = CellType.ClosedDoor;
                    }
                    else if (mapCell.Equals('l'))
                    {
                        texture = BookOnTableTexture;
                        cellType = CellType.BookOnTable;
                    }
                    else if (mapCell.Equals('N'))
                    {
                        texture = ClosedGoldenDoorTexture;
                        cellType = CellType.ClosedGoldenDoor;
                    }
                    else if (mapCell.Equals('L'))
                    {
                        texture = BoardsTexture;
                        cellType = CellType.Boards;
                    }
                    else if (mapCell.Equals('R'))
                    {
                        texture = ClosedBlueDoorTexture;
                        cellType = CellType.ClosedBlueDoor;
                    }
                    else if (mapCell.Equals('f'))
                    {
                        texture = GritTexture;
                        cellType = CellType.SandWithRelic;
                    }
                    else if (mapCell.Equals('F'))
                    {
                        texture = GritTexture;
                        cellType = CellType.SandWithBlueKey;
                    }
                    else if (mapCell.Equals('P'))
                    {
                        texture = PileOfStonesTexture;
                        cellType = CellType.PileOfStones;
                    }
                    else if (mapCell.Equals('m'))
                    {
                        texture = ScrollTexture;
                        cellType = CellType.Scroll;
                    }
                    else if (mapCell.Equals('x'))
                    {
                        texture = PileOfStonesTexture;
                        cellType = CellType.HeapWithRelic;
                    }
                    else if (mapCell.Equals('u'))
                    {
                        texture = PasswordTexture;
                        cellType = CellType.Password;
                    }
                    else
                    {
                        texture = SinkTexture;
                        cellType = CellType.Sink;
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
}
