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
        private Interaction InteractionManager;
        private Map MapConstructor;
        private SpriteMap[,] Map;
        private SpriteMap[,] Inventory;
        private Player SpritePlayer;
        private Bonfire SpriteBonfire;
        private Inventory SpriteInventory;
        private SpriteFont SpriteFont;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        }

        protected override void Initialize()
        {
            graphics.PreferredBackBufferWidth = 30 * 27 + 30 + 407;
            graphics.PreferredBackBufferHeight = 30 * 26;
            graphics.IsFullScreen = false;
            graphics.ApplyChanges();
            base.Initialize();
        }

        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);
            SpriteBonfire = new Bonfire(new Animations(Content.Load<Texture2D>("MapTexture/Burn"), 4));
            MapConstructor = new Map(Content.Load<Texture2D>("MapTexture/TopOfWall"), Content.Load<Texture2D>("MapTexture/Floor"),
                Content.Load<Texture2D>("MapTexture/FrontOfWall"), Content.Load<Texture2D>("MapTexture/Bonfire"),
                Content.Load<Texture2D>("MapTexture/Pedestal"), Content.Load<Texture2D>("MapTexture/BedsideTable"),
                Content.Load<Texture2D>("MapTexture/TableWithBook"), Content.Load<Texture2D>("MapTexture/BedTop"),
                Content.Load<Texture2D>("MapTexture/BedBottom"), Content.Load<Texture2D>("MapTexture/Dummy"),
                Content.Load<Texture2D>("MapTexture/Vase"), Content.Load<Texture2D>("MapTexture/ClosedChest"),
                Content.Load<Texture2D>("MapTexture/Bake"), Content.Load<Texture2D>("MapTexture/KitchenTable"),
                Content.Load<Texture2D>("MapTexture/Sink"), Content.Load<Texture2D>("MapTexture/Tabletop"),
                Content.Load<Texture2D>("MapTexture/Chair"), Content.Load<Texture2D>("MapTexture/DinnerTable"),
                Content.Load<Texture2D>("MapTexture/BrokenVase"), Content.Load<Texture2D>("MapTexture/Shovel"),
                Content.Load<Texture2D>("MapTexture/Basket"), Content.Load<Texture2D>("MapTexture/Ambry"),
                Content.Load<Texture2D>("MapTexture/BookTable"), Content.Load<Texture2D>("MapTexture/GoldenKey"),
                Content.Load<Texture2D>("MapTexture/Grit"));
            Map = MapConstructor.GenerateMap();
            SpriteInventory = new Inventory(Content.Load<Texture2D>("Inventory/Inventory"), Content.Load<Texture2D>("Inventory/EmptyCell"),
                Content.Load<Texture2D>("Inventory/BackgroundForText"), Content.Load<Texture2D>("Inventory/MasterKey"), Content.Load<Texture2D>("Inventory/NailPuller"));
            Inventory = SpriteInventory.GenerateInventory();
            SpriteFont = Content.Load<SpriteFont>("Font");
            InteractionManager = new Interaction();
            SpritePlayer = new Player(new Dictionary<string, Animations>()
            {
                { "GoRight", new Animations(Content.Load<Texture2D>("Player/GoRight"), 3) },
                { "GoLeft", new Animations(Content.Load<Texture2D>("Player/GoLeft"), 3) },
                { "GoUp", new Animations(Content.Load<Texture2D>("Player/GoUp"), 3) },
                { "GoDown", new Animations(Content.Load<Texture2D>("Player/GoDown"), 3) } }, InteractionManager, SpriteInventory)
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
            Inventory = SpriteInventory.GenerateInventory();
            SpritePlayer.Update(gameTime, Map);
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            spriteBatch.Begin(SpriteSortMode.Immediate, BlendState.AlphaBlend);
            MapConstructor.Draw(spriteBatch, Map);
            SpriteInventory.Draw(spriteBatch, SpriteFont, Inventory, InteractionManager.OutputText);
            //spriteBatch.DrawString(sf, "lalala\nghjkfghапрорпаАПАВЫ", new Vector2(890, 470), Color.Red);
            SpriteBonfire.Draw(spriteBatch);
            SpritePlayer.Draw(spriteBatch);
            spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}