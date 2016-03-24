using System;
using System.Diagnostics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace MonoCrab3._5
{
    /// <summary>
    /// Enum over all Camera states for managing the different locations for the camera
    /// </summary>
    public enum CameraZoomState
    {
        Loading,
        MainMenu,
        Game
    }
    public class Camera2D
    {
        public Matrix viewMatrix;
        private Vector2 position;
        private Vector2 halfViewSize;
        public Vector2 target;
        public Vector2 origin;
        private float cameraSpeed = 5;
        private int targetIndex = 0;
        private MouseState mouseState; //Mouse state
        private int scroll;
        KeyboardState keyStateOld;
        KeyboardState oldState1;
        private float crabZoom = 0.7f;
       
        public CameraZoomState currentCamState;

        public float zoom; // Camera Zoom
        
        public Camera2D(Rectangle clientRect)
        {
            currentCamState = CameraZoomState.Loading;
            origin = Vector2.Zero;
            position = new Vector2(4000, 2250);
            target = new Vector2(4000, 2250);
            //Camera zoom starts at 1 because we have a fixed loading screen
            zoom = 1f;

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

         
            //if (!GameWorld.gameWorld.startGame)
            //{
            //    foreach (GameObject go in GameWorld.gameWorld.GameObjects)
            //    {
            //        if (go.GetComponent("CIntroMenu") != null)
            //        {
            //           target = go.Transform.position;
            //        }
            //    }
            //}
            if (GameWorld.gameWorld.startGame)
            {
                for (int i = 0; i < GameWorld.gameWorld.CrabList.Count; i++)
                {
                    target = GameWorld.gameWorld.CrabList[targetIndex].Transform.position;
                }
            }
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
            
            Position = Vector2.Lerp(Position, target, cameraSpeed * GameWorld.gameWorld.deltaTime);
            //Check zoom
            mouseState = Mouse.GetState();
            if (mouseState.ScrollWheelValue > scroll)
            {
                zoom += 0.01f;
                scroll =  mouseState.ScrollWheelValue;
            }
            else if (mouseState.ScrollWheelValue < scroll)
            {
                zoom -= 0.01f;
                scroll = mouseState.ScrollWheelValue;
            }
            //Camera state
            //Check and see if the zoom is what should be
            switch (currentCamState)
            {
                case CameraZoomState.Loading:
                   if (Math.Round((decimal)GameWorld.gameWorld.gameCamera.zoom, 1) != 1.0m)
                   {
                        GameWorld.gameWorld.gameCamera.zoom = MathHelper.Lerp(GameWorld.gameWorld.gameCamera.zoom, 1f, 10f * GameWorld.gameWorld.deltaTime);
                   }                    
                    break;
                case CameraZoomState.MainMenu:
                    if (Math.Round((decimal)GameWorld.gameWorld.gameCamera.zoom, 2) != 0.16m)
                    {
                        GameWorld.gameWorld.gameCamera.zoom = MathHelper.Lerp(GameWorld.gameWorld.gameCamera.zoom, 0.165f, 10f * GameWorld.gameWorld.deltaTime);

                    }
                    break;
                case CameraZoomState.Game:
                    if (Math.Round((decimal)GameWorld.gameWorld.gameCamera.zoom, 2) != 0.7m)
                    {
                        GameWorld.gameWorld.gameCamera.zoom = MathHelper.Lerp(GameWorld.gameWorld.gameCamera.zoom, 0.7f, 1f * GameWorld.gameWorld.deltaTime);

                    }
                    break;
                default:
                    break;
            }


        }
        public void SetCameraState()
        {
           
            
        }
        private void UpdateViewMatrix()
        {
            //Clamp the value so we can't go below zero
            zoom = MathHelper.Clamp(zoom, 0.0f, 10.0f);
            viewMatrix = Matrix.CreateTranslation(new Vector3(-position.X, -position.Y, 0)) * 
                                         Matrix.CreateScale(new Vector3(zoom, zoom,1)) *
                                         Matrix.CreateTranslation(new Vector3(halfViewSize.X , halfViewSize.Y , 0));
            

            
        }
      


    }
}