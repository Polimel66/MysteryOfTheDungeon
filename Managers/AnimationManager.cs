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
    class AnimationManager
    {
        private float Timer;
        protected Animations Animation;
        public Vector2 Position { get; set; }
        public AnimationManager(Animations animation)
        {
            Animation = animation;
        }

        public void Play(Animations animation)
        {
            if (Animation != animation)
            {
                Animation = animation;
                Animation.CurrentFrame = 0;
                Timer = 0;
            }
            return;
        }

        public void Stop()
        {
            Animation.CurrentFrame = 0;
            Timer = 0;
        }

        public void Update(GameTime gameTime)
        {
            Timer += (float)gameTime.ElapsedGameTime.TotalSeconds;
            if (Timer > Animation.FrameSpeed)
            {
                Animation.CurrentFrame += 1;
                Timer = 0;
                if (Animation.CurrentFrame >= Animation.FrameCount)
                    Animation.CurrentFrame = 0;
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(Animation.PlayerTexture, Position, new Rectangle(Animation.FrameWidth * Animation.CurrentFrame, 0, Animation.FrameWidth, Animation.FrameHeight), Color.White);
        }
    }
}
