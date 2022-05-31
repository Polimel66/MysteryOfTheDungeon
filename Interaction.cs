using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;

namespace MysteryOfTheDungeon
{

    class Interaction
    {
        private Dictionary<CellType, List<Action<SpriteMap, InventoryItems[]>>> InteractionDictionary;
        public string OutputText = "";
        public Dictionary<int, bool> CellState;
        private Texture2D FloorTexture;
        private Texture2D EmptyBasketTexture;
        private Texture2D DressedDummyTexture;
        private Texture2D OpenedDoorTexture;
        private Texture2D OpenedGoldenDoorTexture;
        private Texture2D BrokenBoardsTexture;
        private Texture2D ExcavatedSandTexture;
        private Texture2D OpenedBlueDoorTexture;
        private Texture2D DugOutHeapTexture;

        public Interaction(Texture2D floorTexture, Texture2D emptyBasketTexture, Dictionary<int, bool> cellState, Texture2D dressedDummyTexture,
            Texture2D openedDoorTexture, Texture2D openedGoldenDoorTexture, Texture2D brokenBoardsTexture, Texture2D excavatedSandTexture,
            Texture2D openedBlueDoorTexture, Texture2D dugOutHeapTexture)
        {
            FloorTexture = floorTexture;
            EmptyBasketTexture = emptyBasketTexture;
            InteractionDictionary = new Dictionary<CellType, List<Action<SpriteMap, InventoryItems[]>>>()
            { [CellType.TableWithBook] = new List<Action<SpriteMap, InventoryItems[]>> { PrintText },
                [CellType.BookTable] = new List<Action<SpriteMap, InventoryItems[]>> { PrintText },
                [CellType.BedsideTable] = new List<Action<SpriteMap, InventoryItems[]>> { PrintText },
                [CellType.Vase] = new List<Action<SpriteMap, InventoryItems[]>> { PrintText, FindItem },
                [CellType.Dummy] = new List<Action<SpriteMap, InventoryItems[]>> { PrintText, UseItem },
                [CellType.Ambry] = new List<Action<SpriteMap, InventoryItems[]>> { PrintText },
                [CellType.BrokenVase] = new List<Action<SpriteMap, InventoryItems[]>> { PrintText, FindItem },
                [CellType.Shovel] = new List<Action<SpriteMap, InventoryItems[]>> { PrintText, FindItem },
                [CellType.GoldenKey] = new List<Action<SpriteMap, InventoryItems[]>> { PrintText, FindItem },
                [CellType.Basket] = new List<Action<SpriteMap, InventoryItems[]>> { PrintText, FindItem },
                [CellType.Hat] = new List<Action<SpriteMap, InventoryItems[]>> { PrintText, FindItem },
                [CellType.Shoes] = new List<Action<SpriteMap, InventoryItems[]>> { PrintText, FindItem },
                [CellType.ClosedDoor] = new List<Action<SpriteMap, InventoryItems[]>> { PrintText, UseItem },
                [CellType.BookOnTable] = new List<Action<SpriteMap, InventoryItems[]>> { PrintText },
                [CellType.ClosedGoldenDoor] = new List<Action<SpriteMap, InventoryItems[]>> { PrintText, UseItem },
                [CellType.Boards] = new List<Action<SpriteMap, InventoryItems[]>> { PrintText, UseItem },
                [CellType.Grit] = new List<Action<SpriteMap, InventoryItems[]>> { UseItem },
                [CellType.SandWithBlueKey] = new List<Action<SpriteMap, InventoryItems[]>> { UseItem },
                [CellType.SandWithRelic] = new List<Action<SpriteMap, InventoryItems[]>> { UseItem },
                [CellType.ClosedBlueDoor] = new List<Action<SpriteMap, InventoryItems[]>> { PrintText, UseItem },
                [CellType.Scroll] = new List<Action<SpriteMap, InventoryItems[]>> { PrintText },
                [CellType.PileOfStones] = new List<Action<SpriteMap, InventoryItems[]>> { UseItem },
                [CellType.HeapWithRelic] = new List<Action<SpriteMap, InventoryItems[]>> { UseItem }
            };
            CellState = cellState;
            DressedDummyTexture = dressedDummyTexture;
            OpenedDoorTexture = openedDoorTexture;
            OpenedGoldenDoorTexture = openedGoldenDoorTexture;
            BrokenBoardsTexture = brokenBoardsTexture;
            ExcavatedSandTexture = excavatedSandTexture;
            OpenedBlueDoorTexture = openedBlueDoorTexture;
            DugOutHeapTexture = dugOutHeapTexture;
        }


        public void MakeInteraction(SpriteMap interactionCell, InventoryItems[] inventory)
        {
            if (InteractionDictionary.ContainsKey(interactionCell.Value))
            {
                foreach (var action in InteractionDictionary[interactionCell.Value])
                    action(interactionCell, inventory);
            }
        }

        public void FindItem(SpriteMap interactionCell, InventoryItems[] inventory)
        {
            var interactionSubject = interactionCell.Value;
            switch (interactionSubject)
            {
                case CellType.Vase:
                    if (!inventory.Contains(InventoryItems.MasterKey))
                    {
                        AddToInventory(inventory, InventoryItems.MasterKey);
                    }
                    break;
                case CellType.Dummy:
                    if (!inventory.Contains(InventoryItems.FirstRelic))
                    {
                        AddToInventory(inventory, InventoryItems.FirstRelic);
                        interactionCell.Texture = DressedDummyTexture;
                        interactionCell.Value = CellType.DressedDummy;

                    }
                    break;
                case CellType.BrokenVase:
                    if (!inventory.Contains(InventoryItems.NailPuller))
                    {
                        AddToInventory(inventory, InventoryItems.NailPuller);
                    }
                    break;
                case CellType.Shovel:
                    if (!inventory.Contains(InventoryItems.Shovel))
                    {

                        AddToInventory(inventory, InventoryItems.Shovel);
                        interactionCell.Texture = FloorTexture;
                        interactionCell.Value = CellType.Floor;
                    }
                    break;
                case CellType.GoldenKey:
                    if (!inventory.Contains(InventoryItems.GoldenKey))
                    {

                        AddToInventory(inventory, InventoryItems.GoldenKey);
                        interactionCell.Texture = FloorTexture;
                        interactionCell.Value = CellType.Floor;
                    }
                    break;
                case CellType.Basket:
                    if (!inventory.Contains(InventoryItems.Dress))
                    {
                        AddToInventory(inventory, InventoryItems.Dress);
                        interactionCell.Texture = EmptyBasketTexture;
                        interactionCell.Value = CellType.EmptyBasket;
                    }
                    break;
                case CellType.Hat:
                    if (!inventory.Contains(InventoryItems.Hat))
                    {

                        AddToInventory(inventory, InventoryItems.Hat);
                        interactionCell.Texture = FloorTexture;
                        interactionCell.Value = CellType.Floor;
                    }
                    break;
                case CellType.Shoes:
                    if (!inventory.Contains(InventoryItems.Shoes))
                    {

                        AddToInventory(inventory, InventoryItems.Shoes);
                        interactionCell.Texture = FloorTexture;
                        interactionCell.Value = CellType.Floor;
                    }
                    break;
                case CellType.SandWithBlueKey:
                    if (!inventory.Contains(InventoryItems.BlueKey))
                    {
                        AddToInventory(inventory, InventoryItems.BlueKey);
                        interactionCell.Texture = ExcavatedSandTexture;
                        interactionCell.Value = CellType.ExcavatedSand;

                    }
                    break;
                case CellType.SandWithRelic:
                    if (!inventory.Contains(InventoryItems.SecondRelic))
                    {
                        AddToInventory(inventory, InventoryItems.SecondRelic);
                        interactionCell.Texture = ExcavatedSandTexture;
                        interactionCell.Value = CellType.ExcavatedSand;

                    }
                    break;
                case CellType.HeapWithRelic:
                    if (!inventory.Contains(InventoryItems.ThirdRelic))
                    {
                        AddToInventory(inventory, InventoryItems.ThirdRelic);
                        interactionCell.Texture = DugOutHeapTexture;
                        interactionCell.Value = CellType.DugOutHeap;

                    }
                    break;
                default:
                    break;
            }
        }

        public void AddToInventory(InventoryItems[] inventory, InventoryItems addedItem)
        {
            for (int i = 0; i < inventory.Length; i++)
            {
                if (inventory[i] == InventoryItems.Empty)
                {
                    inventory[i] = addedItem;
                    break;
                }
            }
        }
        
        public List<InventoryItems> FindSelectedСells(Dictionary<int, bool> cellState, InventoryItems[] inventory)
        {
            var result = new List<InventoryItems>();
            foreach(var cell in cellState.Keys)
            {
                if (cellState[cell])
                    result.Add(inventory[cell]);
            }
            return result;
        }

        public void ReplaceInventorySlots(Dictionary<int, bool> cellState, InventoryItems[] inventory)
        {
            for(var i = 0; i < cellState.Keys.Count; i++)
            {
                if(cellState[i])
                    inventory[i] = InventoryItems.Empty;
                cellState[i] = false;
            }
        }

        public void UseItem(SpriteMap interactionCell, InventoryItems[] inventory)
        {
            var interactionSubject = interactionCell.Value;
            var selectedСells = FindSelectedСells(CellState, inventory);
            switch (interactionSubject)
            {
                case CellType.Dummy:
                    if(selectedСells.Count == 3 && selectedСells.Contains(InventoryItems.Hat)
                        && selectedСells.Contains(InventoryItems.Dress)
                        && selectedСells.Contains(InventoryItems.Shoes))
                    {
                        ReplaceInventorySlots(CellState, inventory);
                        FindItem(interactionCell, inventory);
                        OutputText = "Реликвия! Я приближаюсь к выходу...";
                    }
                    break;
                case CellType.ClosedDoor:
                    if (selectedСells.Count == 1 && selectedСells.Contains(InventoryItems.MasterKey))
                    {
                        ReplaceInventorySlots(CellState, inventory);
                        interactionCell.Texture = OpenedDoorTexture;
                        interactionCell.Value = CellType.OpenedDoor;
                        OutputText = "Открыл! Надеюсь еще не поздно\n" +
                            "помочь этой бедняге...";
                    }
                    break;
                case CellType.ClosedGoldenDoor:
                    if (selectedСells.Count == 1 && selectedСells.Contains(InventoryItems.GoldenKey))
                    {
                        ReplaceInventorySlots(CellState, inventory);
                        interactionCell.Texture = OpenedGoldenDoorTexture;
                        interactionCell.Value = CellType.OpenedGoldenDoor;
                        OutputText = "Ключ подошел! Отлично,\n" +
                            "двигаемся дальше.";
                    }
                    break;
                case CellType.Boards:
                    if (selectedСells.Count == 1 && selectedСells.Contains(InventoryItems.NailPuller))
                    {
                        ReplaceInventorySlots(CellState, inventory);
                        interactionCell.Texture = BrokenBoardsTexture;
                        interactionCell.Value = CellType.BrokenBoards;
                        OutputText = "Фух, надеюсь маг не очень\n" +
                            "будет злиться из-за поломки.";
                    }
                    break;
                case CellType.Grit:
                    if (selectedСells.Count == 1 && selectedСells.Contains(InventoryItems.Shovel))
                    {
                        interactionCell.Texture = ExcavatedSandTexture;
                        interactionCell.Value = CellType.ExcavatedSand;
                    }
                    break;
                case CellType.SandWithBlueKey:
                    if (selectedСells.Count == 1 && selectedСells.Contains(InventoryItems.Shovel))
                    {
                        FindItem(interactionCell, inventory);
                        OutputText = "Синий ключ! Я могу проникнуть\n" +
                            "в последнюю комнату.";
                    }
                    break;
                case CellType.SandWithRelic:
                    if (selectedСells.Count == 1 && selectedСells.Contains(InventoryItems.Shovel))
                    {
                        FindItem(interactionCell, inventory);
                        OutputText = "Реликвия! Еще немного\n" +
                            "и я выберусь отсюда, и помогу\n" +
                            "этой бедной девушке.";
                    }
                    break;
                case CellType.ClosedBlueDoor:
                    if (selectedСells.Count == 1 && selectedСells.Contains(InventoryItems.BlueKey))
                    {
                        ReplaceInventorySlots(CellState, inventory);
                        interactionCell.Texture = OpenedBlueDoorTexture;
                        interactionCell.Value = CellType.OpenedBlueDoor;
                        OutputText = "Еще одна дверь открыта.";
                    }
                    break;
                case CellType.PileOfStones:
                    if (selectedСells.Count == 1 && selectedСells.Contains(InventoryItems.Shovel))
                    {
                        interactionCell.Texture = DugOutHeapTexture;
                        interactionCell.Value = CellType.DugOutHeap;
                    }
                    break;
                case CellType.HeapWithRelic:
                    if (selectedСells.Count == 1 && selectedСells.Contains(InventoryItems.Shovel))
                    {
                        FindItem(interactionCell, inventory);
                        OutputText = "Еще одна реликвия!\n" +
                            "Держись Джулия, я скоро спасу\n" +
                            "тебя.";
                    }
                    break;
                default:
                    break;
            }
        }

        public void PrintText(SpriteMap interactionCell, InventoryItems[] inventory)
        {
            var interactionSubject = interactionCell.Value;
            switch (interactionSubject)
            {
                case CellType.TableWithBook:
                    OutputText = "-Идет тридцать пятая неделя моего\n" +
                        "заточения. Я уже не надеюсь на\n" +
                        "спасение. Злой маг похитил меня\n" +
                        "и оставил существовать в этом\n" +
                        "подземелье. Я могу только писать\n" +
                        "в книгах, чтобы не сойти с ума...\n" +
                        "Но я не сижу на месте, а ищу вы-\n" +
                        "ход отсюда. Я смастерила отмычку\n" +
                        "и спрятала ее в вазу.\n" +
                        "P.S. Забыла назвать свое имя\n" +
                        "Джулия.";
                    break;
                case CellType.BookTable:
                    OutputText = "-Я редко выбираюсь на\n" +
                        "кухню. Боюсь, что маг меня пой-\n" +
                        "мает. Но иногда, голод берет\n" +
                        "свое. Я съедаю крошки и объедки,\n" +
                        "которые оставляет мой похити-\n" +
                        "тель. Не знаю сколько еще\n" +
                        "смогу продержаться...";
                    break;
                case CellType.BedsideTable:
                    OutputText = "Я ничего не нашел. Нужно поискать\n" +
                        "в другом месте.";
                    break;
                case CellType.Vase:
                    if (!inventory.Contains(InventoryItems.MasterKey))
                        OutputText = "Я нашел отмычку! Попробую\n" +
                            "открыть с помощью нее дверь.";
                    break;
                case CellType.Dummy:
                    if (!(inventory.Contains(InventoryItems.Hat))
                        && !inventory.Contains(InventoryItems.Dress)
                        && !inventory.Contains(InventoryItems.Shoes))
                        OutputText = "Странный манекен. Думаю\n" +
                            "с ним можно будет что-то сделать.";
                    break;
                case CellType.Ambry:
                    OutputText = "Я ничего не нашел. Нужно поискать\n" +
                        "в другом месте.";
                    break;
                case CellType.BrokenVase:
                    if (!inventory.Contains(InventoryItems.NailPuller))
                        OutputText = "Я нашел гвоздодер! Думаю\n" +
                            "он может пригодиться в этом ужасном\n" +
                            "месте.";
                    break;
                case CellType.Shovel:
                    if (!inventory.Contains(InventoryItems.Shovel))
                        OutputText = "Я нашел лопату! Думаю\n" +
                            "она может пригодиться в этой тем-\n" +
                            "нице.";
                    break;
                case CellType.GoldenKey:
                    if (!inventory.Contains(InventoryItems.GoldenKey))
                        OutputText = "Я нашел золотой ключ!\n" +
                            "Вероятно я могу открыть какую-то\n" +
                            "дверь.";
                    break;
                case CellType.Basket:
                    if (!inventory.Contains(InventoryItems.Dress))
                        OutputText = "Хмм... Платье.\n" +
                            "Возьму его, вдруг пригодится!";
                    break;
                case CellType.Hat:
                    if (!inventory.Contains(InventoryItems.Hat))
                        OutputText = "Еще один предмет одежды...\n" +
                            "Шляпка наверняка нужна. Заберу ее.";
                    break;
                case CellType.Shoes:
                    if (!inventory.Contains(InventoryItems.Shoes))
                        OutputText = "Туфли?! Может всю одежду,\n" +
                            "которую я нашел можно куда-то надеть?\n" +
                            "Надо попытаться ее применить.";
                    break;
                case CellType.ClosedDoor:
                    if (!inventory.Contains(InventoryItems.MasterKey))
                        OutputText = "Заперто.";
                    break;
                case CellType.BookOnTable:
                    OutputText = "-Я изучала всю темницу пока\n" +
                        "маг спал. Вчера нашла старые\n" +
                        "свитки в ящике. Там сказано, что\n" +
                        "в этом месте есть лишь один выход.\n" +
                        "Он открывается с помощью 4 релик-\n" +
                        "вий. Я в отчаянии, не знаю смогу\n" +
                        "ли их найти.";
                    break;
                case CellType.ClosedGoldenDoor:
                    if (!inventory.Contains(InventoryItems.GoldenKey))
                        OutputText = "Заперто. Надо найти ключ.";
                    break;
                case CellType.ClosedBlueDoor:
                    if (!inventory.Contains(InventoryItems.BlueKey))
                        OutputText = "Закрыто.";
                    break;
                case CellType.Boards:
                    if (!inventory.Contains(InventoryItems.NailPuller))
                        OutputText = "Проход забит досками.\n" +
                            "Но я думаю, что его можно чем-то\n" +
                            "проломить.";
                    break;
                case CellType.Scroll:
                    OutputText = "-Я нашла лишь одну релик\n" +
                        "вию. Где спрятаны остальные -\n" +
                        "представить не могу. Я больше\n" +
                        "не буду пытаться выбраться\n" +
                        "отсюда. Но однажды, здесь\n" +
                        "окажется новая жертва. Поэтому\n" +
                        "я спрячу этот предмет так, чтобы\n" +
                        "в будущем он помог бедняге. Ком-\n" +
                        "ната, в которой лежит эта записка,\n" +
                        "используется для хранения\n" +
                        "куч камней. Именно в одну из\n" +
                        "этих куч я и... спрячу реликвию.";
                    break;
                default:
                    OutputText = "Я не могу с этим взаимодействовать.";
                    break; 
            }
            
        }
    }
}
