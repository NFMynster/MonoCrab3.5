using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace MonoCrab3._5
{
    class CBait : Component, ILoadable, IOnCollisionEnter, IAnimateable
    {
        private CAnimator animator;
        private CSpriteRenderer spriteRenderer;
        private int energy;
        private BaitTypes baitType;
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

        public CBait(GameObject gameObject, BaitTypes baitType) : base(gameObject)
        {
            this.Energy = energy;
            this.animator = (CAnimator)gameObject.GetComponent("CAnimator");
            this.baitType = baitType;
            CreateAnimations();



        }

        public void LoadContent(ContentManager content)
        {
           //CreateAnimations();
        }

        public void CreateAnimations()
        { 
            if(baitType == BaitTypes.PosBait5 || baitType == BaitTypes.NegBait5)
            { 
            animator.CreateAnimation("large", new Animation(8, 0, 0, 90, 90, 8, Vector2.Zero));
            animator.PlayAnimation("large");
            }

            if (baitType == BaitTypes.PosBait3 || baitType == BaitTypes.NegBait3)
            {
                animator.CreateAnimation("medium", new Animation(8, 0, 0, 75, 75, 8, Vector2.Zero));
                animator.PlayAnimation("medium");
            }

            if (baitType == BaitTypes.PosBait1 || baitType == BaitTypes.NegBait1)
            {
                animator.CreateAnimation("mini", new Animation(8, 0, 0, 60, 60, 8, Vector2.Zero));
                animator.PlayAnimation("mini");
            }
        }


        public void OnCollisionEnter(CCollider other)
        {
            if (other.gameObject.GetComponent("CCrab") != null)
            {
                CCrab tCrab = (CCrab)other.gameObject.GetComponent("CCrab");
                tCrab.Energy += Energy;
            }
            if (other.gameObject.GetComponent("CCrab") != null)
            {
                GameWorld.gameWorld.BaitList.Remove(this.gameObject);
                GameWorld.gameWorld.GameObjects.Remove(this.gameObject);
            }
        }

        

        public void OnAnimationDone(string animationName)
        {
            //animator.PlayAnimation("Spin");
        }
    }
}
