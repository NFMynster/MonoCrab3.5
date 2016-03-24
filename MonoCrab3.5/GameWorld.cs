using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;


namespace MonoCrab3._5
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class GameWorld : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        public float deltaTime;
        public Camera2D gameCamera;
        private static GameWorld GM;    
        public Rectangle displayRectangle;
        private Texture2D background;
        public bool startGame = false;
        public static GameWorld gameWorld
        {
            get
            {
                if (GM == null)
                {
                    GM = new GameWorld();
                }
                return GM;
            }
        }
        private static List<CCollider> colliders = new List<CCollider>();
        internal List<CCollider> Colliders
        {
            get { return colliders; }
            set { colliders = value; }
        }
        List<GameObject> gameObjects = new List<GameObject>();

        private List<GameObject> objectToAdd  = new List<GameObject>();
        internal List<GameObject> ObjectToAdd
        {
            get { return objectToAdd; }
            set { objectToAdd = value; }
        }

        internal List<GameObject> GameObjects
        {
            get { return gameObjects; }
            set { gameObjects = value; }
        }
        List<GameObject> crabList = new List<GameObject>();
        internal List<GameObject> CrabList
        {
            get { return crabList; }
            set { crabList = value; }
        }
        List<GameObject> baitlist = new List<GameObject>();

        
        internal List<GameObject> BaitList
        {
            get { return baitlist; }
            set { baitlist = value; }
        }

        /// <summary>
        /// Private constructor because of singleton patteren
        /// </summary>
        private GameWorld()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            graphics.PreferredBackBufferWidth = 1280;
            graphics.PreferredBackBufferHeight = 720;
            graphics.ApplyChanges();
            
            

        }
        /// <summary>
        /// Allows adding in new GameObjects at start
        /// </summary>
        private void AddGameObjects()
        {
            GameObject introMenu = new GameObject(new Vector2(4000,2250));
            introMenu.AddComponent(new CSpriteRenderer(introMenu, "welcomescreen", Color.White, 1f));
            introMenu.AddComponent(new CIntroMenu(introMenu));
            gameObjects.Add(introMenu);

            GameObject cursor = new GameObject(Vector2.Zero);
            cursor.AddComponent(new CSpriteRenderer(introMenu, "NegBait1", Color.White, 1f));
            cursor.AddComponent(new CCursor(cursor));
            Add(cursor);

            //crabDirector.Construct(new Vector2(5500, 2300));
            //crabDirector.Construct(new Vector2(5650, 2000));



        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            displayRectangle = GraphicsDevice.Viewport.Bounds;
            AddGameObjects();
           this.IsMouseVisible = true;
           
            //Initialize the camera
            gameCamera = new Camera2D(graphics.GraphicsDevice.Viewport.Bounds);
            
            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);
            //AddGameObjects();
            
            // TODO: use this.Content to load your game content here
            foreach (GameObject go in GameObjects.ToList())
            {
                if (go != null)
                {
                    go.LoadContent(this.Content);
                }
            }
            foreach (GameObject go in objectToAdd)
            {
                go.LoadContent(this.Content);
            }
            background = Content.Load<Texture2D>("Background");
            IBuilder crabBuilder = new CrabBuilder();
            Director crabDirector = new Director(crabBuilder);
            Add(crabDirector.Construct(new Vector2(5500,2100)));
            //Add(crabDirector.Construct(new Vector2(5500, 2400)));

        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// game-specific content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            deltaTime = (float)gameTime.ElapsedGameTime.TotalSeconds;
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            // TODO: Add your update logic here
            foreach (GameObject go in GameObjects.ToList())
            {
                go.Update();
            }
            KeyboardState keystate = Keyboard.GetState();
            if (keystate.IsKeyDown(Keys.N))
            {
                Random rnd = new Random();
                BaitTypes randomBait = (BaitTypes)rnd.Next(0, Enum.GetNames(typeof(BaitTypes)).Length);
                Add(BaitPool.baitPoolInstance.Create(new Vector2(5500, 2100), randomBait));

            }
            //Update our camera
            gameCamera.Update();
            ObjectPoolControl();
            base.Update(gameTime);
        }

        private void ObjectPoolControl()
        {
            foreach (var go in gameWorld.objectToAdd)
            {
                go.LoadContent(Content);
            }
            gameObjects.AddRange(objectToAdd);
            objectToAdd.Clear();
        }
        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            // TODO: Add your drawing code here
            //
            spriteBatch.Begin(SpriteSortMode.FrontToBack, BlendState.AlphaBlend, null, null, null, null, gameCamera.viewMatrix);

            foreach (GameObject go in GameObjects.ToList())
            {
                go.Draw(spriteBatch);
            }

            spriteBatch.Draw(background, Vector2.Zero, null, null, Vector2.Zero, 0, null, Color.White,
                SpriteEffects.None, 0f);

            spriteBatch.End();
            base.Draw(gameTime);
        }


        public void Add(GameObject go)
        {
            ObjectToAdd.Add(go);

            if (!go.IsLoaded)
            {
                go.LoadContent(Content);
            }
        }
    }
}
