using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

//test1
namespace MysteryOfTheDungeon
{
    class Player
    {
        protected AnimationManager AnimationManager;
        protected Dictionary<string, Animations> AnimationsDictionary;
        protected Texture2D Texture;
        public Vector2 PositionValue;
        public Vector2 Position 
        { 
            get { return PositionValue; } 
            set { PositionValue = value; if (AnimationManager != null) AnimationManager.Position = PositionValue; } 
        }
        public float Speed;

        public Player(Texture2D texture)
        {
            Texture = texture;
        }

        public Player(Dictionary<string, Animations> animationsDictionary)
        {
            AnimationsDictionary = animationsDictionary;
            AnimationManager = new AnimationManager(AnimationsDictionary.First().Value);
        }

        public void Update(GameTime gameTime)
        {
            if (Keyboard.GetState().IsKeyDown(Keys.Left))
            {
                PositionValue.X -= Speed;
                AnimationManager.Play(AnimationsDictionary["GoLeft"]);
            }
            else if (Keyboard.GetState().IsKeyDown(Keys.Right))
            {
                PositionValue.X += Speed;
                AnimationManager.Play(AnimationsDictionary["GoRight"]);
            }
            else if (Keyboard.GetState().IsKeyDown(Keys.Up))
            {
                PositionValue.Y -= Speed;
                AnimationManager.Play(AnimationsDictionary["GoUp"]);
            }
            else if (Keyboard.GetState().IsKeyDown(Keys.Down))
            {
                PositionValue.Y += Speed;
                AnimationManager.Play(AnimationsDictionary["GoDown"]);
            }
            else
            {
                AnimationManager.Stop();
            }
            AnimationManager.Update(gameTime);
            Position = PositionValue;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            if (Texture != null)
                spriteBatch.Draw(Texture, Position, Color.White);
            else if (AnimationManager != null)
                AnimationManager.Draw(spriteBatch);
        }
    }
}
