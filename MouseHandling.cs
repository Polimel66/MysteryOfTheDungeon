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
    class MouseHandling
    {
        public Dictionary<int, bool> CellState;
        private readonly int LeftBorderInventory = 875;
        private readonly int RightBorderInventory = 1215;
        private readonly int TopBorderInventory = 470;
        private readonly int BottomBorderInventory = 725;
        private readonly int InventorySlotSize = 85;
        private readonly int CountCellsInRow = 4;
        MouseState LastMouseState = Mouse.GetState();
        public MouseHandling(Dictionary<int, bool> cellState)
        {
            CellState = cellState;
        }

        public (int,int) FindCellCoordinate()
        {
            MouseState currentMouseState = Mouse.GetState();
            if(currentMouseState.LeftButton == ButtonState.Pressed && LastMouseState.LeftButton == ButtonState.Released)
            {
                LastMouseState = currentMouseState;
                if (currentMouseState.X > LeftBorderInventory && currentMouseState.X < RightBorderInventory &&
                    currentMouseState.Y > TopBorderInventory && currentMouseState.Y < BottomBorderInventory)
                {
                    var column = (currentMouseState.X - LeftBorderInventory) / InventorySlotSize;
                    var line = (currentMouseState.Y - TopBorderInventory) / InventorySlotSize;
                    return (line, column);
                }
            }
            LastMouseState = currentMouseState;
            return (-1, -1);
        }

        public void UpdateCellState((int, int) updatedCell)
        {
            if (updatedCell.Item1 >= 0 && updatedCell.Item2 >= 0)
            {
                var cellNumber = updatedCell.Item1 * CountCellsInRow + updatedCell.Item2;
                CellState[cellNumber] = !CellState[cellNumber];
            }
        }

        public void Update()
        {
            var cellCoordinate = FindCellCoordinate();
            UpdateCellState(cellCoordinate);
        }
    }
}
