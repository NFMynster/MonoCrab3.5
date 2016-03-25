using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;


namespace MonoCrab3._5
{
    class CSpriteRenderer : Component,ILoadable, IDrawable
    {
        public Rectangle Rectangle { get; set; }
        private Texture2D sprite;
        public Texture2D Sprite
        {
            get { return sprite; }
            set { sprite = value; }
        }
        private Vector2 offset;
        private Color color;
        //opacity by defualt is 100%
        public float opacity = 1f;
        private CAnimator animator;
        public Color drawColor
        {
            get { return color; }
            set { color = value; }
        }
       

        public Vector2 Offset
        {
            get
            {
                return offset;
            }
            set
            {
                offset = value;
            }
        }

        

        private string spriteName;
        private float layerDepth;

        public CSpriteRenderer(GameObject gameObject, string spriteName, Color drawColor, float layerDepth) : base (gameObject)
        {
            this.animator = (CAnimator)gameObject.GetComponent("CAnimator");
            this.drawColor = drawColor;
            this.spriteName = spriteName;
            this.layerDepth = layerDepth;
            
        }

        public void LoadContent(ContentManager content)
        {
            Sprite = content.Load<Texture2D>(spriteName);
            //If this gameobject does not have an animator, use a default rectangle.
            if (gameObject.GetComponent("CAnimator") == null)
            {
                this.Rectangle = new Rectangle(0, 0, Sprite.Width, Sprite.Height);

            }
            else
            {
                //rectangle is set via CAnimator
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {   
            //if our game object has an animator, draw the current animation frame at the correct spot.
            if (gameObject.GetComponent("CAnimator") != null)
            {
                spriteBatch.Draw(Sprite, gameObject.Transform.position + Offset, Rectangle, drawColor * opacity, gameObject.Transform.rotation,new Vector2(Rectangle.Width / 2,Rectangle.Height / 2), 1, SpriteEffects.None, layerDepth);

            }
            else
            {
                 spriteBatch.Draw(Sprite, gameObject.Transform.position + Offset, Rectangle, drawColor * opacity, gameObject.Transform.rotation,new Vector2(Sprite.Width / 2,Sprite.Height / 2), 1, SpriteEffects.None, layerDepth);

            }
            if (!gameObject.IsLoaded)
            {
                Debug.Print("DISPOSED");
            }
        }
    }
}
