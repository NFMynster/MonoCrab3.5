using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
namespace MonoCrab3._5
{
    class CPlayer : Component, IUpdateable, ILoadable
    {
        private Vector2 origin;
        private GameObject go;
        private float minRange = 100;
        private float maxRange = 500;
        private Random rnd;
        private float timerLimit = 2;
        private float timePassed = 0;
        private MouseState mStateOld;
        

        public CPlayer(GameObject gameObject) : base(gameObject)
        {
            this.go = gameObject;
            origin = gameObject.Transform.position;
            rnd = new Random(DateTime.Now.Millisecond);

        }
        public void Update()
        {

            
            //Check if 5 seconds has elapsed
            if (timePassed > timerLimit && GameWorld.gameWorld.startGame)
            {

                MouseState mState = Mouse.GetState();              
                
                
              
                Vector2 worldPosition = Vector2.Transform(new Vector2(mState.X, mState.Y), Matrix.Invert(GameWorld.gameWorld.gameCamera.viewMatrix));
                if (mState.LeftButton == ButtonState.Pressed && mStateOld.LeftButton == ButtonState.Released)
                {
                    
                    BaitTypes randomBait = (BaitTypes)rnd.Next(0, Enum.GetNames(typeof(BaitTypes)).Length - 1);
                    GameWorld.gameWorld.Add(BaitPool.baitPoolInstance.Create(worldPosition, randomBait));

                    //Reset your counter
                    timePassed = 0;

                }
                mStateOld = mState;

                
            }

            timePassed += (float)GameWorld.gameWorld.deltaTime;
            //Update the enemy creation timer
        }
        public static Vector2 MousePositionCamera(Camera2D camera)
        {
            Vector2 mousePosition;
            mousePosition.X = Mouse.GetState().X;
            mousePosition.Y = Mouse.GetState().Y;

            //Adjust for resolutions like 800 x 600 that are letter boxed on the Y:
            //mousePosition.Y -= Resolution.VirtualViewportY;

            Vector2 screenPosition = Vector2.Transform(mousePosition, Matrix.Invert(camera.viewMatrix));
            Vector2 worldPosition = Vector2.Transform(screenPosition, Matrix.Invert(camera.viewMatrix));

            return worldPosition;
        }

        public Vector2 ChoosePosition()
        {

            int quadrant = rnd.Next(16);

            if (quadrant < 4)
            {
                //Top-Left Quadrant
                Vector2 topLeft = new Vector2((origin.X - rnd.Next((int)minRange, (int)maxRange)), (origin.Y - rnd.Next((int)minRange, (int)maxRange)));
                return topLeft;
            }

            else if (quadrant > 4 && quadrant < 8)
            {
                //Top-Right Quadrant
                Vector2 topRight = new Vector2((origin.X + rnd.Next((int)minRange, (int)maxRange)), (origin.Y - rnd.Next((int)minRange, (int)maxRange)));
                return topRight;
            }

            else if (quadrant > 8 && quadrant < 12)
            {
                //Lower-Right Quadrant
                Vector2 lowerRight = new Vector2((origin.X + rnd.Next((int)minRange, (int)maxRange)), (origin.Y + rnd.Next((int)minRange, (int)maxRange)));
                return lowerRight;
            }

            else if (quadrant > 12 && quadrant < 16)
            {
                //Lower-Left Quadrant
                Vector2 lowerLeft = new Vector2((origin.X - rnd.Next((int)minRange, (int)maxRange)), (origin.Y + rnd.Next((int)minRange, (int)maxRange)));
                return lowerLeft;
            }

            else

                return Vector2.Zero;
        }


        public void LoadContent(ContentManager content)
        {

        }
    }
}