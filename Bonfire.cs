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
        public bool fire = true;

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
            if(fire)
            {
            AnimationManager.Play(Animation);
                fire = false;
            }

            AnimationManager.Update(gameTime);
            //AnimationManager.Position = new Vector2(210, 570);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            //if (AnimationManager != null)
                AnimationManager.Draw(spriteBatch);
        }
    }
}
