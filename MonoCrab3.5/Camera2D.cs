using System;
using System.Diagnostics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace MonoCrab3._5
{

    public class Camera2D
    {
        public Matrix viewMatrix;
        private Vector2 position;
        private Vector2 halfViewSize;
        public Vector2 target;

        private float cameraSpeed = .5f;
        private int targetIndex = 0;
        private MouseState mouseState; //Mouse state
        private int scroll;
        KeyboardState keyStateOld;
        KeyboardState oldState1;




        public GameState currentCamState;

        public float zoom; // Camera Zoom

        public Camera2D(Rectangle clientRect)
        {

            position = new Vector2(3000, 1700);
            // target = new Vector2(4000, 2250);
            //Start zoomed out
            zoom = 0.3f;
            //currentCamState = CameraZoomState.MainMenu;

            halfViewSize = new Vector2(clientRect.Width * 0.5f, clientRect.Height * 0.5f);
            UpdateViewMatrix();

        }

        public Vector2 Position
        {
            get
            {
                return position;
            }

            set
            {
                position = value;
                UpdateViewMatrix();
            }
        }

        public void Update()
        {

            KeyboardState NewKeyState = Keyboard.GetState();

            if (NewKeyState.IsKeyDown(Keys.Q) && keyStateOld.IsKeyUp(Keys.Q))
            {
                if (targetIndex < GameWorld.gameWorld.CrabList.Count && targetIndex > 0)
                {
                    targetIndex--;
                }
                else
                {
                    //Minus one because of 0-index
                    targetIndex = GameWorld.gameWorld.CrabList.Count - 1;


                }
            }
            keyStateOld = NewKeyState;

            KeyboardState NewKey1State = Keyboard.GetState();

            if (NewKey1State.IsKeyDown(Keys.E) && oldState1.IsKeyUp(Keys.E))
            {
                //Minus one because of 0-index
                if (targetIndex < GameWorld.gameWorld.CrabList.Count - 1)
                {
                    targetIndex++;

                }
                else
                {
                    targetIndex = 0;
                }

            }
            oldState1 = NewKey1State;

            //Check zoom
            mouseState = Mouse.GetState();
            if (mouseState.ScrollWheelValue > scroll)
            {
                zoom += 0.1f;
                scroll = mouseState.ScrollWheelValue;
            }
            else if (mouseState.ScrollWheelValue < scroll)
            {
                zoom -= 0.1f;
                scroll = mouseState.ScrollWheelValue;
            }
            //Camera state
            //Check and see if the zoom is what should be and lerps to the correct value. The zoom should always be 1 if there is a UI element present (GameState.UI)
            //
            switch (GameWorld.gameWorld.currentGameState)
            {
                case GameState.Loading:
                    if (Math.Round((decimal)GameWorld.gameWorld.gameCamera.zoom, 1) != 1.0m)
                    {
                        GameWorld.gameWorld.gameCamera.zoom = MathHelper.Lerp(GameWorld.gameWorld.gameCamera.zoom, 1f, 10f * GameWorld.gameWorld.deltaTime);
                    }
                    break;

                case GameState.MainMenu:
                    //Set our camera at a specific place
                    target = new Vector2(3000, 1700);

                    if (Math.Round((decimal)GameWorld.gameWorld.gameCamera.zoom, 2) != 0.22m)
                    {
                        GameWorld.gameWorld.gameCamera.zoom = MathHelper.Lerp(GameWorld.gameWorld.gameCamera.zoom, 0.22f, .5f * GameWorld.gameWorld.deltaTime);
                    }
                    break;

                case GameState.Game:
                    if (GameWorld.gameWorld.startGame)
                    {
                        //If the game is running, update our camera logic to follow the crabs as well
                        for (int i = 0; i < GameWorld.gameWorld.CrabList.Count; i++)
                        {
                            target = GameWorld.gameWorld.CrabList[targetIndex].Transform.position;
                        }
                    }
                    if (Math.Round((decimal)GameWorld.gameWorld.gameCamera.zoom, 2) != 0.7m)
                    {
                        GameWorld.gameWorld.gameCamera.zoom = MathHelper.Lerp(GameWorld.gameWorld.gameCamera.zoom, 0.7f, .3f * GameWorld.gameWorld.deltaTime);

                    }
                    break;

                case GameState.UI:
                    GameWorld.gameWorld.gameCamera.zoom = MathHelper.Lerp(GameWorld.gameWorld.gameCamera.zoom, 1, 5f * GameWorld.gameWorld.deltaTime);
                    break;
            }
            
            Position = Vector2.Lerp(Position, target, cameraSpeed * GameWorld.gameWorld.deltaTime);


        }

        private void UpdateViewMatrix()
        {
            //Clamp the value so we can't go below zero
            zoom = MathHelper.Clamp(zoom, 0.0f, 10.0f);
            viewMatrix = Matrix.CreateTranslation(new Vector3(-position.X, -position.Y, 0)) *
                                         Matrix.CreateScale(new Vector3(zoom, zoom, 1)) *
                                         Matrix.CreateTranslation(new Vector3(halfViewSize.X, halfViewSize.Y, 0));



        }



    }
}