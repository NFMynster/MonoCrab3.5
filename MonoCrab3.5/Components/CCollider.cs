using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Runtime.Remoting.Messaging;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace MonoCrab3._5
{
    public class CCollider : Component, IDrawable, ILoadable, IUpdateable
    {
        private int animationframes;
        private CSpriteRenderer spriteRenderer;
        private Texture2D texture;
        private List<CCollider> otherColliders;
        private bool doCollisionCheck = true;
        private CAnimator animator;
        //Should the collider be per pixel?
        private bool pixelCollision;
        private Dictionary<string, Color[][]> pixels = new Dictionary<string, Color[][]>();
        private Color[] CurrentPixels
        {
            
            get
            {
                return pixels[animator.AnimationName][animator.CurrentIndex];
             }
        }

        public void SetDoCollisionCheck()
        {
            doCollisionCheck = true;
        }
        public CCollider(GameObject gameObject, bool pixelCollision, int frames) : base(gameObject)
        {
            spriteRenderer = (CSpriteRenderer)gameObject.GetComponent("CSpriteRenderer");

            otherColliders = new List<CCollider>();
            GameWorld.gameWorld.Colliders.Add(this);
            this.pixelCollision = pixelCollision;
            this.animationframes = frames;

        }
        /// <summary>
        /// Creates a collision box based on the sprite and an animation. Since we are using rotations on some objects, we hardcoded the collisionbox
        /// </summary>
        public Rectangle CollisionBox
        {
            get
            {
                 return new Rectangle
                    (
                    (int)(gameObject.Transform.position.X - spriteRenderer.Rectangle.Width /2 + spriteRenderer.Offset.X),
                    (int)(gameObject.Transform.position.Y - spriteRenderer.Rectangle.Height / 2 + spriteRenderer.Offset.Y),
                    spriteRenderer.Sprite.Width / animationframes,
                    spriteRenderer.Sprite.Height
                    );
            }
        }



        public void Draw(SpriteBatch spriteBatch)
        {
#if DEBUG
            Rectangle topLine = new Rectangle(CollisionBox.X , CollisionBox.Y, CollisionBox.Width, 1);
            Rectangle bottomLine = new Rectangle(CollisionBox.X, CollisionBox.Y + CollisionBox.Height, CollisionBox.Width, 1);
            Rectangle rightLine = new Rectangle(CollisionBox.X + CollisionBox.Width, CollisionBox.Y, 1, CollisionBox.Height);
            Rectangle leftLine = new Rectangle(CollisionBox.X, CollisionBox.Y, 1, CollisionBox.Height);

            spriteBatch.Draw(texture, topLine, null, Color.White, 0, Vector2.Zero, SpriteEffects.None, 1);
            spriteBatch.Draw(texture, bottomLine, null, Color.White, 0, Vector2.Zero, SpriteEffects.None, 1);
            spriteBatch.Draw(texture, rightLine, null, Color.White, 0, Vector2.Zero, SpriteEffects.None, 1);
            spriteBatch.Draw(texture, leftLine, null, Color.White, 0, Vector2.Zero, SpriteEffects.None, 1);
#endif 

        }

        public void LoadContent(ContentManager content)
        {

            animator = (CAnimator)gameObject.GetComponent("CAnimator");
            texture = content.Load<Texture2D>("CollisionTexture");
            if (pixelCollision)
            {
                CachePixels();
            }
            
        }

        public void Update()
        {
           
            CheckCollision();
        }

        private void CheckCollision()
        {
            if (doCollisionCheck)
            {
                foreach (CCollider other in GameWorld.gameWorld.Colliders)
                {
                    if (other != this)
                    {
                        if (!otherColliders.Contains(other))
                        {
                            if (CollisionBox.Intersects(other.CollisionBox) && CheckPixelCollision(other))
                            {
                                otherColliders.Add(other);
                                gameObject.OnCollisionEnter(other);
                                gameObject.OnCollisionStay(other);

                            }
                            else
                            {
                                if (CollisionBox.Intersects(other.CollisionBox))
                                {
                                    gameObject.OnCollisionStay(other);
                                }
                                else
                                {
                                    otherColliders.Remove(other);
                                    gameObject.OnCollisionExit(other);
                                }
                            }
                        }
                    }
                }
            }
        }

        private void CachePixels()
        {
            
                foreach (KeyValuePair<string, Animation> pair in animator.Animations)
                {
                    Animation animation = pair.Value;

                    Color[][] colors = new Color[animation.AnimationFrames][];
                    for (int i = 0; i < animation.AnimationFrames; i++)
                    {
                        colors[i] = new Color[animation.Rectangles[i].Width * animation.Rectangles[i].Height];
                        spriteRenderer.Sprite.GetData(0, animation.Rectangles[i], colors[i], 0, animation.Rectangles[i].Width * animation.Rectangles[i].Height);
                    }
                    pixels.Add(pair.Key, colors);

                }
            

        }




        private bool CheckPixelCollision(CCollider other)
        {
            if (pixelCollision)
            {
                // Find the bounds of the rectangle intersection
                int top = Math.Max(CollisionBox.Top, other.CollisionBox.Top);
                int bottom = Math.Min(CollisionBox.Bottom, other.CollisionBox.Bottom);
                int left = Math.Max(CollisionBox.Left, other.CollisionBox.Left);
                int right = Math.Min(CollisionBox.Right, other.CollisionBox.Right);

                for (int y = top; y < bottom; y++)
                {
                    for (int x = left; x < right; x++)
                    {
                        int firstIndex = (x - CollisionBox.Left) + (y - CollisionBox.Top) * CollisionBox.Width;
                        int secondIndex = (x - other.CollisionBox.Left) + (y - other.CollisionBox.Top) * other.CollisionBox.Width;
                        //Get the color of both pixels at this point
                        Color colorA = CurrentPixels[firstIndex];
                        Color colorB = other.CurrentPixels[secondIndex];
                        // If both pixels are not completely empty
                        if (colorA.A != 0 && colorB.A != 0)
                        {
                            //Intersect!
                            return true;
                        }
                    }
                }
                //
           }

            return false;
        }
    }

}