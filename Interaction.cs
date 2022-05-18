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
        private Dictionary<CellType, List<Action<CellType, Inventory>>> InteractionDictionary;
        public string OutputText = "";


        public Interaction()
        {
            InteractionDictionary = new Dictionary<CellType, List<Action<CellType, Inventory>>>()
            { [CellType.TableWithBook] = new List<Action<CellType, Inventory>> { PrintText }, 
                [CellType.BookTable] = new List<Action<CellType, Inventory>> { PrintText }, 
                [CellType.BedsideTable] = new List<Action<CellType, Inventory>> { PrintText },
                [CellType.Vase] = new List<Action<CellType, Inventory>> { PrintText, FindItem },
                [CellType.Dummy] = new List<Action<CellType, Inventory>> { PrintText, UseItem },
                [CellType.Ambry] = new List<Action<CellType, Inventory>> { PrintText },
                [CellType.BrokenVase] = new List<Action<CellType, Inventory>> { PrintText, FindItem }
            };
        }


        public void MakeInteraction(CellType interactionSubject, Inventory inventory)
        {
            if (InteractionDictionary.ContainsKey(interactionSubject))
            {
                foreach (var action in InteractionDictionary[interactionSubject])
                    action(interactionSubject, inventory);
            }
        }

        public void FindItem(CellType interactionSubject, Inventory inventory)
        {
            switch (interactionSubject)
            {
                case CellType.Vase:
                    if (!inventory.CurrentInventory.Contains(InventoryItems.MasterKey))
                    {
                        AddToInventory(inventory, InventoryItems.MasterKey);
                    }
                    break;
                case CellType.Dummy:
                    if (!inventory.CurrentInventory.Contains(InventoryItems.FirstRelic))
                    {
                        AddToInventory(inventory, InventoryItems.FirstRelic);
                    }
                    break;
                case CellType.BrokenVase:
                    if (!inventory.CurrentInventory.Contains(InventoryItems.NailPuller))
                    {
                        AddToInventory(inventory, InventoryItems.NailPuller);
                    }
                    break;
                default:
                    break;
            }
        }

        public void AddToInventory(Inventory inventory, InventoryItems addedItem)
        {
            for (int i = 0; i < inventory.CurrentInventory.Length; i++)
            {
                if (inventory.CurrentInventory[i] == InventoryItems.Empty)
                {
                    inventory.CurrentInventory[i] = addedItem;
                    break;
                }
            }
        }

        public void UseItem(CellType interactionSubject, Inventory inventory)
        {
            switch (interactionSubject)
            {
                case CellType.Dummy:
                    if(inventory.CurrentInventory.Contains(InventoryItems.Hat)
                        && inventory.CurrentInventory.Contains(InventoryItems.Dress)
                        && inventory.CurrentInventory.Contains(InventoryItems.Shoes))
                    {
                        FindItem(interactionSubject, inventory);
                    }
                    break;
                default:
                    break;
            }
        }

        public void PrintText(CellType interactionSubject, Inventory inventory)
        {
            switch (interactionSubject)
            {
                case CellType.TableWithBook:
                    OutputText = "Идет тридцать пятая неделя моего\n" +
                        "заточения. Я уже не надеюсь на\n" +
                        "спасение. Злой маг похитил меня\n" +
                        "и оставил существовать в этом\n" +
                        "подземелье. Я могу только писать\n" +
                        "в книгах, чтобы не сойти с ума...\n" +
                        "Но я не сижу на месте, а ищу вы-\n" +
                        "ход отсюда. Я смастерила отмычку\n" +
                        "и спрятала ее в вазу.";
                    break;
                case CellType.BookTable:
                    OutputText = "Я редко выбираюсь на\n" +
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
                    if (!inventory.CurrentInventory.Contains(InventoryItems.MasterKey))
                        OutputText = "Я нашел отмычку! Попробую\n" +
                            "открыть с помощью нее дверь.";
                    break;
                case CellType.Dummy:
                    if (!(inventory.CurrentInventory.Contains(InventoryItems.Hat))
                        && !inventory.CurrentInventory.Contains(InventoryItems.Dress)
                        && !inventory.CurrentInventory.Contains(InventoryItems.Shoes))
                        OutputText = "Странный манекен. Думаю\n" +
                            "с ним можно будет что-то сделать.";
                    break;
                case CellType.Ambry:
                    OutputText = "Я ничего не нашел. Нужно поискать\n" +
                        "в другом месте.";
                    break;
                case CellType.BrokenVase:
                    if (!inventory.CurrentInventory.Contains(InventoryItems.NailPuller))
                        OutputText = "Я нашел гвоздодер! Думаю\n" +
                            "Он может пригодиться в этом ужасном\n" +
                            "месте.";
                    break;
                default:
                    OutputText = "Я не могу с этим взаимодействовать.";
                    break; 
            }
            
        }
    }
}
