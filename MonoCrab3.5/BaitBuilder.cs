using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace MonoCrab3._5
{
    enum BaitTypes
    {
        PosBait1,
        NegBait1,
        PosBait3,
        NegBait3,
        PosBait5,
        NegBait5
        
    }
    class BaitBuilder : IBuilder
    {
        private GameObject gameObject;
        private BaitTypes baitType;
        public BaitBuilder(BaitTypes baitType)
        {
            this.baitType = baitType;
        }
        public GameObject GetResult()
        {

            return gameObject;
        }
       
        public void BuildGameObject(Vector2 position)
        {
            gameObject = new GameObject(position);
            switch (baitType)
            {
                    case BaitTypes.PosBait1:
                    gameObject.AddComponent(new CSpriteRenderer(gameObject, "PosBait1", Color.White, 0.5f));
                    break;

                    case BaitTypes.PosBait3:
                    gameObject.AddComponent(new CSpriteRenderer(gameObject, "PosBait3", Color.White, 0.5f));
                    break;

                    case BaitTypes.PosBait5:
                    gameObject.AddComponent(new CSpriteRenderer(gameObject, "PosBait5", Color.White, 0.5f));
                    break;

                    case BaitTypes.NegBait1:
                    gameObject.AddComponent(new CSpriteRenderer(gameObject, "NegBait1", Color.White, 0.5f));
                    break;

                    case BaitTypes.NegBait3:
                    gameObject.AddComponent(new CSpriteRenderer(gameObject, "NegBait3", Color.White, 0.5f));
                    break;

                    case BaitTypes.NegBait5:
                    gameObject.AddComponent(new CSpriteRenderer(gameObject, "NegBait5", Color.White, 0.5f));
                    break;


            }
            gameObject.AddComponent(new CAnimator(gameObject));
            gameObject.AddComponent(new CCollider(gameObject, true, 8));
            gameObject.AddComponent(new CBait(gameObject, baitType));
        }

    }
}
