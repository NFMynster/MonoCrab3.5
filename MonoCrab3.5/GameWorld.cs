using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Diagnostics;

namespace MonoCrab3._5
{
    /// <summary>
    /// Enum over all Camera states for managing the different locations for the camera
    /// </summary>
    public enum GameState
    {
        Loading,
        MainMenu,
        Game,
        UI
    }
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class GameWorld : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        public float deltaTime;
        public Camera2D gameCamera;
        public GameState currentGameState;
        private KeyboardState enterKeyStateOld;
        private static GameWorld GM;    
        public Rectangle displayRectangle;
        private Texture2D background;
        public bool startGame = false;
        private Random rnd;
        //Amount of enemies to spawn
        public int enemies = 1;

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
            currentGameState = GameState.MainMenu;
            rnd = new Random(DateTime.Now.Millisecond);
            

        }
        /// <summary>
        /// Allows adding in new GameObjects at start
        /// </summary>
        private void AddGameObjects()
        {
            //Max the amount of possible enemies
            if (enemies >= 5)
            {
                enemies = 1;
            }

            IBuilder playerCrabBuilder = new PlayerCrabBuilder();
            Director director = new Director(playerCrabBuilder);
            Add(director.Construct(new Vector2(5500, 2400)));
            for (int i = 0; i < enemies; i++)
            {
                IBuilder crabBuilder = new CrabBuilder();
                director = new Director(crabBuilder);
                Add(director.Construct(new Vector2(rnd.Next(5250,5600), rnd.Next(1900,2300))));
            }
           
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
            background = Content.Load<Texture2D>("BackgroundMainMenu");
            
            //Initialise the update function in the UIManager
            UIManager.manager.LoadContent(this.Content);
            //UIManager.manager.SetUIScreen(0);
           
            
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
            KeyboardState enterKeyState = Keyboard.GetState();

            switch (currentGameState)
            {                
                case GameState.MainMenu:
                    if (enterKeyState.IsKeyDown(Keys.Enter) && enterKeyStateOld.IsKeyUp(Keys.Enter))
                    {
                        //RestartGame();
                        startGame = true;
                        currentGameState = GameState.Game;
                    }
                    break;
                case GameState.Game:
                    if (enterKeyState.IsKeyDown(Keys.Enter) && enterKeyStateOld.IsKeyUp(Keys.Enter))
                    {
                        currentGameState = GameState.MainMenu;
                    }
                    break;
                case GameState.UI:
                    if (enterKeyState.IsKeyDown(Keys.Enter) && enterKeyStateOld.IsKeyUp(Keys.Enter))
                    {
                        currentGameState = GameState.MainMenu;
                        
                        RestartGame();

                    }
                    break;
                default:
                    break;
            }
            enterKeyStateOld = enterKeyState;

            //Update our camera
            gameCamera.Update();
            ObjectPoolControl();
            CheckWin();
            base.Update(gameTime);
        }
        
        private void CheckWin()
        {
            float radius = 1840;

            foreach (GameObject t in GameWorld.gameWorld.crabList.ToList())
            {
                if (t.GetComponent("CCrab") != null)
                {
                    float dist = Vector2.Distance(t.Transform.position, new Vector2(5450,2100));
                    if (dist < radius)
                    {
                        Debug.Print("WITHIN RADIUS");
                    }
                    else
                    {
                        Debug.Print("OUT OF RADIUS");
                        if (t.GetComponent("CCrab") != null && t.GetComponent("CPlayer") != null && startGame)
                        {
                            startGame = false;
                            enemies++;
                            currentGameState = GameState.UI;
                            UIManager.manager.SetUIScreen(1);
                        }
                        else if ((t.GetComponent("CCrab") != null) && startGame)
                        {
                            startGame = false;
                            currentGameState = GameState.UI;
                            UIManager.manager.SetUIScreen(2);
                        }

                    }
                }

            }
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
            UIManager.manager.Draw(spriteBatch);
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
        /// <summary>
        /// Clear and restarts the game
        /// </summary>
        public void RestartGame()
        {
            GameObjects.Clear();
            baitlist.Clear();
            crabList.Clear();
            AddGameObjects();
            
        }
    }
}
