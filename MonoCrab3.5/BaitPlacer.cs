using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace MonoCrab3._5
{
    class BaitPlacer
    {
        
        private Vector2 currentTarget;
                private Random rnd;
        private float timerLimit = 3;
        private float timePassed = 0;
        private float minRange = 100;
        private float maxRange = 500;

        public BaitPlacer()
        {
            rnd = new Random();
            
        }
        public void Update()
        {
            if (timePassed > timerLimit)
            {
                for (int i = 0; i < GameWorld.gameWorld.CrabList.Count; i++)
                {
                    int random = rnd.Next(0, GameWorld.gameWorld.CrabList.Count);
                    currentTarget = GameWorld.gameWorld.CrabList[random].Transform.position;
                }
                    BaitTypes randomBait = (BaitTypes)rnd.Next(0, Enum.GetNames(typeof(BaitTypes)).Length);
                BaitPool.baitPoolInstance.Create(ChoosePosition(), randomBait);
                timePassed = 0;

            }
            timePassed += (float)GameWorld.gameWorld.deltaTime;

            
        }
        public Vector2 ChoosePosition()
        {

            int quadrant = rnd.Next(16);

            if (quadrant < 4)
            {
                //Top-Left Quadrant
                Vector2 topLeft = new Vector2((currentTarget.X - rnd.Next((int)minRange, (int)maxRange)), (currentTarget.Y - rnd.Next((int)minRange, (int)maxRange)));
                return topLeft;
            }

            else if (quadrant > 4 && quadrant < 8)
            {
                //Top-Right Quadrant
                Vector2 topRight = new Vector2((currentTarget.X + rnd.Next((int)minRange, (int)maxRange)), (currentTarget.Y - rnd.Next((int)minRange, (int)maxRange)));
                return topRight;
            }

            else if (quadrant > 8 && quadrant < 12)
            {
                //Lower-Right Quadrant
                Vector2 lowerRight = new Vector2((currentTarget.X + rnd.Next((int)minRange, (int)maxRange)), (currentTarget.Y + rnd.Next((int)minRange, (int)maxRange)));
                return lowerRight;
            }

            else if (quadrant > 12 && quadrant < 16)
            {
                //Lower-Left Quadrant
                Vector2 lowerLeft = new Vector2((currentTarget.X - rnd.Next((int)minRange, (int)maxRange)), (currentTarget.Y + rnd.Next((int)minRange, (int)maxRange)));
                return lowerLeft;
            }

            else

                return Vector2.Zero;
        }
    }
}
