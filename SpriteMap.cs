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
    class SpriteMap
    {
        public readonly Texture2D Texture;
        public CellType Value;
        public Vector2 Position;
        public SpriteMap(Texture2D texture, Vector2 position, CellType value)
        {
            Texture = texture;
            Position = position;
            Value = value;
        }

        public SpriteMap(Texture2D texture, Vector2 position)
        {
            Texture = texture;
            Position = position;
        }
    }
}
