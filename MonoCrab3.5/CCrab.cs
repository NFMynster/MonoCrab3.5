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
    internal class CCrab : Component, ILoadable, IUpdateable, IAnimateable, IOnCollisionEnter
    {
        private CAnimator animator;
        
        private List<Vector2> targets = new List<Vector2>();
        private int speed = 40;
        private float turnSpeed = 3;
        private Vector2 closestTarget;
        private int energy = 100;
        public int Energy
        {
            get
            {
                return energy;
            }

            set
            {
                energy = value;
            }
        }
        public CCrab(GameObject gameObject) : base(gameObject)
        {
            this.animator = (CAnimator)gameObject.GetComponent("CAnimator");
            GameWorld.gameWorld.CrabList.Add(this.gameObject);
            gameObject.Transform.speed = speed;
            gameObject.Transform.turnSpeed = turnSpeed;
        }

        public void LoadContent(ContentManager content)
        {
            CreateAnimations();
        }

        /// <summary>
        /// Returns the closest bait
        /// </summary>
        /// <returns></returns>
        private Vector2 GetClosestBait()
        {
            float minDist = float.MaxValue;

            foreach (GameObject t in GameWorld.gameWorld.GameObjects.ToList())
            {
                if (t.GetComponent("CBait") != null)
                {
                    float dist = Vector2.Distance(t.Transform.position, gameObject.Transform.position);
                    if (dist < minDist)
                    {
                        closestTarget = t.Transform.position;
                        minDist = dist;
                    }
                }

            }
            return closestTarget;

        }

        public void Update()
        {
            //If the main menu is active, don't start the crabs
            if (GameWorld.gameWorld.startGame)
            {
                GetClosestBait();
                gameObject.Transform.MoveTo(closestTarget, true);
            }
        }

        public void OnAnimationDone(string animationName)
        {
            //animator.PlayAnimation("Walk");
        }

        public void CreateAnimations()
        {
            animator.CreateAnimation("Walk", new Animation(6, 0, 0, 256, 256, 10, Vector2.Zero));
            animator.PlayAnimation("Walk");
        }

       

        public void OnCollisionEnter(CCollider other)
        {
            
        }

       
    }
}
