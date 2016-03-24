using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace MonoCrab3._5
{
    class Bait : Component, IUpdateable , ILoadable, IOnCollisionEnter, IDrawable
    {
        private float baitsize;

        public float Baitsize
        {
            get
            {
                return baitsize;
            }

            set
            {
                baitsize = value;
            }
        }

        public Bait(GameObject gameObject, float baitsize) : base(gameObject)
        {
            this.Baitsize = baitsize;
        }

        public void LoadContent(ContentManager content)
        {
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            throw new NotImplementedException();
        }

        public void Update()
        {
            throw new NotImplementedException();
        }

        public void OnCollisionEnter(CCollider other)
        {
            throw new NotImplementedException();
        }
    }
}
