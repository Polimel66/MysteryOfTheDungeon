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
    public class Animations
    {
        public Texture2D PlayerTexture { get; private set; }
        public int CurrentFrame { get; set; }
        public int FrameCount { get; private set; }
        public int FrameHeight { get { return PlayerTexture.Height; } }
        public int FrameWidth { get { return PlayerTexture.Width / FrameCount; } }
        public float FrameSpeed { get; set; }
        public bool isLooping { get; set; }
        public Animations(Texture2D texture, int frameCount)
        {
            PlayerTexture = texture;
            FrameCount = frameCount;
            isLooping = true;
            FrameSpeed = 0.2f;
        }
    }
}
