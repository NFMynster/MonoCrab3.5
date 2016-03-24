using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;

namespace MonoCrab3._5
{
    class CAnimator : Component,IUpdateable
    {
        private CSpriteRenderer spriteRenderer;
        private int currentIndex;
        public int CurrentIndex
        {
            get { return currentIndex; }
            set { currentIndex = value; }
        }
        private float timeElapsed;
        private float fps;
        public Rectangle[] rectangles;
        //The amount of pictures in your picture strip
        private string animationName;
        public string AnimationName
        {
            get { return animationName; }
            set { animationName = value; }
        }

        private Dictionary<string, Animation> animations;

        public Dictionary<string, Animation> Animations
        {
            get
            {
                return animations;
            }

            set
            {
                animations = value;
            }
        }

       


        public CAnimator(GameObject gameObject) : base (gameObject)
        {
            fps = 5;
            animations = new Dictionary<string, Animation>();
            this.spriteRenderer = (CSpriteRenderer)gameObject.GetComponent("CSpriteRenderer");
        }

        public void Update()
        {
            timeElapsed += GameWorld.gameWorld.deltaTime;
            CurrentIndex = (int)(timeElapsed*fps);
            //restarts the animation
            if (CurrentIndex > rectangles.Length - 1)
            {
                gameObject.OnAnimationDone(AnimationName);
                timeElapsed = 0;
                CurrentIndex = 0;
            }
            spriteRenderer.Rectangle = rectangles[CurrentIndex];

        }

        public void CreateAnimation(string name, Animation animation)
        {
            animations.Add(name,animation);
        }

        public void PlayAnimation(string animationName)
        {
            //Checks if it's a new animation
            if (this.AnimationName != animationName)
            {
                this.rectangles = animations[animationName].Rectangles;
                this.spriteRenderer.Rectangle = rectangles[0];
                this.spriteRenderer.Offset = animations[animationName].Offset; 
                this.AnimationName = animationName;
                this.fps = animations[animationName].Fps;

                timeElapsed = 0;
                CurrentIndex = 0;
            }
            
        }

        
    }
}
