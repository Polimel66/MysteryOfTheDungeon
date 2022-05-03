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
        private Map MapConstructor;
        private SpriteMap[,] Map;
        private Player SpritePlayer;
        private Bonfire SpriteBonfire;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        }

        protected override void Initialize()
        {
            graphics.PreferredBackBufferWidth = 30 * 27 + 400;
            graphics.PreferredBackBufferHeight = 30 * 26;
            graphics.IsFullScreen = false;
            graphics.ApplyChanges();
            base.Initialize();
        }

        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);
            SpriteBonfire = new Bonfire(new Animations(Content.Load<Texture2D>("MapTexture/Burn"), 4));
            MapConstructor = new Map(Content.Load<Texture2D>("MapTexture/topOfWall"), Content.Load<Texture2D>("MapTexture/floor"), Content.Load<Texture2D>("MapTexture/frontOfWall"));
            Map = MapConstructor.GenerateMap();
            SpritePlayer = new Player(new Dictionary<string, Animations>()
            {
                { "GoRight", new Animations(Content.Load<Texture2D>("Player/GoRight"), 3) },
                { "GoLeft", new Animations(Content.Load<Texture2D>("Player/GoLeft"), 3) },
                { "GoUp", new Animations(Content.Load<Texture2D>("Player/GoUp"), 3) },
                { "GoDown", new Animations(Content.Load<Texture2D>("Player/GoDown"), 3) } })
            {
                PositionValue = new Vector2(60, 60),
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
            SpriteBonfire.Update(gameTime);
            SpritePlayer.Update(gameTime, Map);
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            spriteBatch.Begin(SpriteSortMode.Immediate, BlendState.AlphaBlend);
            MapConstructor.Draw(spriteBatch, Map);
            SpriteBonfire.Draw(spriteBatch);
            SpritePlayer.Draw(spriteBatch);
            spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}