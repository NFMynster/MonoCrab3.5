using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace MonoCrab3._5
{
    class Animation
    {
        private float fps;
        private Vector2 offset;
        private Rectangle[] rectangles;
        private int animationFrames;
        public int AnimationFrames
        {
            get { return animationFrames; }
            set { animationFrames = value; }
        }
        public float Fps
        {
            get { return fps; }
            set { fps = value; }
        }

        public Vector2 Offset
        {
            get { return offset; }
            set { offset = value; }
        }

        public Rectangle[] Rectangles
        {
            get { return rectangles; }
            
        }

        

        public Animation(int frames, int yPos, int xStartFrame, int width, int height, float fps, Vector2 offset)
        {
            this.AnimationFrames = frames;
            rectangles = new Rectangle[frames];
            Offset = offset;
            this.fps = fps;
            for (int i = 0; i < frames; i++)
            {
                rectangles[i] = new Rectangle((i + xStartFrame)*width, yPos, width, height);
            }
        }
    }
}
