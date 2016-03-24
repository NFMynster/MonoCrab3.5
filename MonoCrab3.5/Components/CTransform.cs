using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MonoCrab3._5
{
    public class CTransform : Component
    {
        public Vector2 position;
        public float rotation { get; set; }
        //set default speeds
        public float turnSpeed = 5;
        public float speed = 40;


        public CTransform(GameObject gameObject, Vector2 position) : base(gameObject)
        {
            this.position = position;
            
        }

        public void Translate(Vector2 translation)
        {
            position += translation;
        }
        public void LookAt(Vector2 target)
        {
            float XDistance = gameObject.Transform.position.X - target.X;
            float YDistance = gameObject.Transform.position.Y - target.Y;
            
            //Lerp the rotation smoothly
            gameObject.Transform.rotation = CurveAngle(gameObject.Transform.rotation, (float)Math.Atan2(YDistance, XDistance), turnSpeed * GameWorld.gameWorld.deltaTime);
            //gameObject.Transform.rotation = (float) Math.Atan2(YDistance, XDistance);
        }
        /// <summary>
        /// Credit due: https://gist.githubusercontent.com/AidanBurke/3107861/raw/9ec491b9c2a27c04ba7b851e96f798f5af3f719c/Game1.cs
        /// The previous system we had suffered from this https://www.youtube.com/watch?v=F26_R5Hwa4Y because we lerped the value
        /// </summary>
        /// <param name="from"></param>
        /// <param name="to"></param>
        /// <param name="step"></param>
        /// <returns></returns>
        private float CurveAngle(float from, float to, float step)
        {
            if (step == 0) return from;
            if (from == to || step == 1) return to;

            Vector2 fromVector = new Vector2((float)Math.Cos(from), (float)Math.Sin(from));
            Vector2 toVector = new Vector2((float)Math.Cos(to), (float)Math.Sin(to));

            Vector2 currentVector = Slerp(fromVector, toVector, step);

            return (float)Math.Atan2(currentVector.Y, currentVector.X);
        }
        /// <summary>
        /// Credit due: https://gist.githubusercontent.com/AidanBurke/3107861/raw/9ec491b9c2a27c04ba7b851e96f798f5af3f719c/Game1.cs
        /// The previous system we had suffered from this https://www.youtube.com/watch?v=F26_R5Hwa4Y because we lerped the value therefor we used this solution
        /// </summary>
        /// <param name="from"></param>
        /// <param name="to"></param>
        /// <param name="step"></param>
        /// <returns></returns>
        private Vector2 Slerp(Vector2 from, Vector2 to, float step)
        {
            if (step == 0) return from;
            if (from == to || step == 1) return to;

            double theta = Math.Acos(Vector2.Dot(from, to));
            if (theta == 0) return to;

            double sinTheta = Math.Sin(theta);
            return (float)(Math.Sin((1 - step) * theta) / sinTheta) * from + (float)(Math.Sin(step * theta) / sinTheta) * to;
        }
        public void MoveTo(Vector2 moveTo, bool lookAt)
        {
            if (lookAt)
            {
                gameObject.Transform.LookAt(moveTo);

            }
            gameObject.Transform.position += findDirection(moveTo) * GameWorld.gameWorld.deltaTime * speed;
        }
        private Vector2 findDirection(Vector2 target)
        {

            Vector2 direction = target - gameObject.Transform.position;
            direction.Normalize();
            return direction;

        }
    }
}
