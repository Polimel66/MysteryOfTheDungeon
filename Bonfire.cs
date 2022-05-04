using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MysteryOfTheDungeon
{
    class Bonfire
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
    }
}
