using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;

namespace MysteryOfTheDungeon
{
    public class Game1 : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        Texture2D texture;
        private Player SpritePlayer;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        }

        protected override void Initialize()
        {
            base.Initialize();
        }

        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);
            SpritePlayer = new Player(new Dictionary<string, Animations>()
            {
                { "GoRight", new Animations(Content.Load<Texture2D>("Player/GoRight"), 3) },
                { "GoLeft", new Animations(Content.Load<Texture2D>("Player/GoLeft"), 3) },
                { "GoUp", new Animations(Content.Load<Texture2D>("Player/GoUp"), 3) },
                { "GoDown", new Animations(Content.Load<Texture2D>("Player/GoDown"), 3) } })
            {
                PositionValue = new Vector2(0, 0),
                Speed = 2f
            };

        }

        protected override void UnloadContent()
        {

        }

        protected override void Update(GameTime gameTime)
        {
            KeyboardState keyboardState = Keyboard.GetState();
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || keyboardState.IsKeyDown(Keys.Escape))
                Exit();

            SpritePlayer.Update(gameTime);
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            spriteBatch.Begin(SpriteSortMode.FrontToBack, BlendState.AlphaBlend);

            SpritePlayer.Draw(spriteBatch);

            spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}