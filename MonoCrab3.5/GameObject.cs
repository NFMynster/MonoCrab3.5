using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
namespace MonoCrab3._5
{
    public class GameObject
    {
        private bool isLoaded = false;
        private List<Component> components;
        private CTransform transform;
       
        public CTransform Transform
        {
            get
            {
                return transform;
            }
            set
            {
                value = transform;
            }
        }

        public bool IsLoaded
        {
            get { return isLoaded; }
            set { isLoaded = value; }
        }


        public GameObject(Vector2 position)
        {
            
            components = new List<Component>();
            this.transform = new CTransform(this,position);
            AddComponent(transform);

        }

        public void AddComponent(Component component)
        {
            components.Add(component);
        }

        public Component GetComponent(string component)
        {
            foreach (Component co in components)
            {
                if (component == co.GetType().Name)
                {
                    return co;
                }
            }
            return null;
        }

        public void LoadContent(ContentManager content)
        {
            if (!IsLoaded)
            {
                foreach (Component component in components)
                {
                    if (component is ILoadable)
                    {
                        (component as ILoadable).LoadContent(content);
                    }
                }

            }

            IsLoaded = true;

        }
        public void Update()
        {
            foreach (Component component in components)
            {
                if (component is IUpdateable)
                {
                    (component as IUpdateable).Update();
                }
            }
        }
        public void Draw(SpriteBatch spriteBatch)
        {
            foreach (Component component in components)
            {
                if (component is IDrawable)
                {
                    (component as IDrawable).Draw(spriteBatch);

                }
            }
        }

       

        public void OnAnimationDone(string animationName)
        {
            foreach (Component component in components)
            {
                if (component is IAnimateable)
                {
                    (component as IAnimateable).OnAnimationDone(animationName);
                }

            }
        }
        public void OnCollisionEnter(CCollider other)
        {
            foreach (Component component in components)
            {
                if (component is IOnCollisionEnter)
                {
                    (component as IOnCollisionEnter).OnCollisionEnter(other);
                }

            }
        }
        public void OnCollisionStay(CCollider other)
        {
            foreach (Component component in components)
            {
                if (component is IOnCollisionStay)
                {
                    (component as IOnCollisionStay).OnCollisionStay(other);
                    
                }

            }
        }
        public void OnCollisionExit(CCollider other)
        {
            foreach (Component component in components)
            {
                if (component is IOnCollisionExit)
                {
                    (component as IOnCollisionExit).OnCollisionExit(other);
                }

            }
        }
        
    }
}
