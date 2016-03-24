using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace MonoCrab3._5
{
    class PlayerCrabBuilder : IBuilder
    {
        private GameObject gameObject;

        public GameObject GetResult()
        {
            return gameObject;
        }

        public void BuildGameObject(Vector2 position)
        {
            gameObject = new GameObject(position);
            gameObject.AddComponent(new CSpriteRenderer(gameObject, "crab", Color.LightGreen, 0.4f));
            gameObject.AddComponent(new CAnimator(gameObject));
            gameObject.AddComponent(new CCrab(gameObject));
            gameObject.AddComponent(new CCollider(gameObject, true, 6));            
            gameObject.AddComponent(new CPlayer(gameObject));
            


        }
    }
}
