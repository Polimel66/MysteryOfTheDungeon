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
                if (currentMouseState.X > 875 && currentMouseState.X < 1215 &&
                    currentMouseState.Y > 470 && currentMouseState.Y < 725)
                {
                    var column = (currentMouseState.X - 875) / 85;
                    var line = (currentMouseState.Y - 470) / 85;
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
                var cellNumber = updatedCell.Item1 * 4 + updatedCell.Item2;
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
