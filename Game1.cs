using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;
using Microsoft.Xna.Framework.Media;

namespace MysteryOfTheDungeon
{
    public class Game1 : Game
    {
        GraphicsDeviceManager graphics;

        SpriteBatch spriteBatch;
        private Interaction InteractionManager;
        private Map MapConstructor;
        private Sprite[,] Map;
        private Player SpritePlayer;
        private Bonfire SpriteBonfire;
        private Inventory SpriteInventory;
        private SpriteFont SpriteFont;
        private RenderTarget2D Target;
        private Effect LampEffectPlayer;
        private Song Music;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            graphics.PreferredBackBufferWidth = 1247;
            graphics.PreferredBackBufferHeight = 780;
            graphics.IsFullScreen = false;
            Target = new RenderTarget2D(GraphicsDevice, graphics.PreferredBackBufferWidth, graphics.PreferredBackBufferHeight);
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
                Content.Load<Texture2D>("MapTexture/Grit"), Content.Load<Texture2D>("MapTexture/Hat"), 
                Content.Load<Texture2D>("MapTexture/Shoes"), Content.Load<Texture2D>("MapTexture/ClosedDoor"),
                Content.Load<Texture2D>("MapTexture/BookOnTable"), Content.Load<Texture2D>("MapTexture/ClosedGoldenDoor"),
                Content.Load<Texture2D>("MapTexture/Boards"), Content.Load<Texture2D>("MapTexture/ClosedBlueDoor"),
                Content.Load<Texture2D>("MapTexture/PileOfStones"), Content.Load<Texture2D>("MapTexture/Scroll"),
                Content.Load<Texture2D>("MapTexture/Password"), Content.Load<Texture2D>("MapTexture/EnchantedExit"));
            Map = MapConstructor.GenerateMap();
            SpriteInventory = new Inventory(Content.Load<Texture2D>("Inventory/Inventory"), Content.Load<Texture2D>("Inventory/EmptyCell"),
                Content.Load<Texture2D>("Inventory/BackgroundForText"), Content.Load<Texture2D>("Inventory/MasterKey"), Content.Load<Texture2D>("Inventory/NailPuller"),
                Content.Load<Texture2D>("Inventory/ShovelInventory"), Content.Load<Texture2D>("Inventory/GoldenKeyInventory"), Content.Load<Texture2D>("Inventory/Dress"),
                Content.Load<Texture2D>("Inventory/HatInventory"), Content.Load<Texture2D>("Inventory/ShoesInventory"), Content.Load<Texture2D>("Inventory/EmptyOn"),
                Content.Load<Texture2D>("Inventory/MasterKeyOn"), Content.Load<Texture2D>("Inventory/NailPullerOn"), Content.Load<Texture2D>("Inventory/ShovelInventoryOn"),
                Content.Load<Texture2D>("Inventory/GoldenKeyInventoryOn"), Content.Load<Texture2D>("Inventory/DressOn"), Content.Load<Texture2D>("Inventory/HatInventoryOn"),
                Content.Load<Texture2D>("Inventory/ShoesInventoryOn"), Content.Load<Texture2D>("Inventory/FirstRelic"), Content.Load<Texture2D>("Inventory/FirstRelicOn"),
                Content.Load<Texture2D>("Inventory/SecondRelic"), Content.Load<Texture2D>("Inventory/SecondRelicOn"), Content.Load<Texture2D>("Inventory/BlueKey"),
                Content.Load<Texture2D>("Inventory/BlueKeyOn"), Content.Load<Texture2D>("Inventory/ThirdRelic"), Content.Load<Texture2D>("Inventory/ThirdRelicOn"),
                Content.Load<Texture2D>("Inventory/PasswordInventory"), Content.Load<Texture2D>("Inventory/PasswordInventoryOn"), Content.Load<Texture2D>("Inventory/FourthRelic"),
                Content.Load<Texture2D>("Inventory/FourthRelicOn"));
            SpriteFont = Content.Load<SpriteFont>("Font");
            InteractionManager = new Interaction(Content.Load<Texture2D>("MapTexture/Floor"), Content.Load<Texture2D>("MapTexture/EmptyBasket"), SpriteInventory.CellState,
                Content.Load<Texture2D>("MapTexture/DressedDummy"), Content.Load<Texture2D>("MapTexture/OpenedDoor"), Content.Load<Texture2D>("MapTexture/OpenedGoldenDoor"),
                Content.Load<Texture2D>("MapTexture/BrokenBoards"), Content.Load<Texture2D>("MapTexture/ExcavatedSand"), Content.Load<Texture2D>("MapTexture/OpenedBlueDoor"),
                Content.Load<Texture2D>("MapTexture/DugOutHeap"), Content.Load<Texture2D>("MapTexture/OpenedChest"), Content.Load<Texture2D>("MapTexture/PedestalWithFirstRelic"),
                Content.Load<Texture2D>("MapTexture/PedestalWithSecondRelic"), Content.Load<Texture2D>("MapTexture/PedestalWithThirdRelic"), Content.Load<Texture2D>("MapTexture/PedestalWithFourthRelic"));
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
            LampEffectPlayer = Content.Load<Effect>("Lamp");
            Music = Content.Load<Song>("Music");
            MediaPlayer.Play(Music);
            MediaPlayer.Volume = (float)0.05;
            MediaPlayer.IsRepeating = true;
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
            SpriteInventory.Update();
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.SetRenderTarget(Target);
            GraphicsDevice.Clear(Color.Black);

            spriteBatch.Begin();
            MapConstructor.Draw(spriteBatch, Map);
            SpriteBonfire.Draw(spriteBatch);
            SpritePlayer.Draw(spriteBatch);
            spriteBatch.End();

            GraphicsDevice.SetRenderTarget(null);
            GraphicsDevice.Clear(Color.Black);
            
            LampEffectPlayer.Parameters["position"].SetValue(new Vector2(SpritePlayer.PlayerGlowCoordinates.Item1,
                SpritePlayer.PlayerGlowCoordinates.Item2));
            spriteBatch.Begin(0, BlendState.AlphaBlend, null, null, null, LampEffectPlayer);
            spriteBatch.Draw(Target, Vector2.Zero, Color.White);
            spriteBatch.End();
            
            spriteBatch.Begin();
            SpriteInventory.Draw(spriteBatch, SpriteFont, InteractionManager.OutputText);
            spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}