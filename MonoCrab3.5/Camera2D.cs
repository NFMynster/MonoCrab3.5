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
        public Vector2 origin;
        private float cameraSpeed = 5;
        private int targetIndex = 0;
        private KeyboardState keyState;
        private MouseState mouseState; //Mouse state
        private int scroll;
        KeyboardState oldState;
        KeyboardState oldState1;
        private float crabZoom = 0.7f;
        public bool shouldLerp = false;
        
        public float zoom; // Camera Zoom
        
        public Camera2D(Rectangle clientRect)
        {
            origin = Vector2.Zero;
            zoom = 0.165f;
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
           

            if (shouldLerp && zoom <= crabZoom)
            {
                GameWorld.gameWorld.gameCamera.zoom = MathHelper.Lerp(GameWorld.gameWorld.gameCamera.zoom, 0.7f, 0.2f * GameWorld.gameWorld.deltaTime);
            }
           
            if (!GameWorld.gameWorld.startGame)
            {
                foreach (GameObject go in GameWorld.gameWorld.GameObjects)
                {
                    if (go.GetComponent("CIntroMenu") != null)
                    {
                        target = go.Transform.position;
                    }
                }
            }
            else if (GameWorld.gameWorld.startGame)
            {
                for (int i = 0; i < GameWorld.gameWorld.CrabList.Count; i++)
                {
                    target = GameWorld.gameWorld.CrabList[targetIndex].Transform.position;
                }
            }
            keyState = Keyboard.GetState();


            KeyboardState NewKeyState = Keyboard.GetState();

            if (NewKeyState.IsKeyDown(Keys.Q) && oldState.IsKeyUp(Keys.Q))
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
            oldState = NewKeyState;


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
            //Vector2 targetWorldPosition = GameWorld.gameWorld.gameCamera.GetWorldPosition(new Vector2(Mouse.GetState().X, Mouse.GetState().Y));

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




        }
        private void UpdateViewMatrix()
        {
            //Clamp the value so we can't go below zero
            zoom = MathHelper.Clamp(zoom, 0.0f, 10.0f);
            viewMatrix = Matrix.CreateTranslation(new Vector3(-position.X, -position.Y, 0)) * 
                                         Matrix.CreateScale(new Vector3(zoom, zoom,1)) *
                                         Matrix.CreateTranslation(new Vector3(halfViewSize.X , halfViewSize.Y , 0));
            

            //viewMatrix = Matrix.CreateTranslation(halfViewSize.X - position.X, halfViewSize.Y - position.Y, 0.0f);
        }
        public Vector2 GetScreenPosition(Vector2 worldPosition)
        {
            return worldPosition - position;
        }

        public Vector2 GetWorldPosition(Vector2 screenPosition)
        {
            return screenPosition + position;
        }


    }
}