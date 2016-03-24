using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace MonoCrab3._5
{
    class OpponentCrabBuilder : IBuilder
    {
        private GameObject gameObject;
        private Random rnd = new Random();
        private int selectRnd;
        public GameObject GetResult()
        {
            return gameObject;
        }

        public void BuildGameObject(Vector2 position)
        {
            selectRnd = rnd.Next(10);

            gameObject = new GameObject(position);
            if (selectRnd == 1)
            {
                gameObject.AddComponent(new CSpriteRenderer(gameObject, "testcrab", Color.Yellow, 1));
            }
            else if (selectRnd == 2)
            {
                gameObject.AddComponent(new CSpriteRenderer(gameObject, "testcrab", Color.Gold, 1));
            }
            else if (selectRnd == 3)
            {
                gameObject.AddComponent(new CSpriteRenderer(gameObject, "testcrab", Color.Lavender, 1));
            }
            else if (selectRnd == 4)
            {
                gameObject.AddComponent(new CSpriteRenderer(gameObject, "testcrab", Color.Blue, 1));
            }
            else if (selectRnd == 5)
            {
                gameObject.AddComponent(new CSpriteRenderer(gameObject, "testcrab", Color.Brown, 1));
            }
            else if (selectRnd == 6)
            {
                gameObject.AddComponent(new CSpriteRenderer(gameObject, "testcrab", Color.Black, 1));
            }
            else if (selectRnd == 7)
            {
                gameObject.AddComponent(new CSpriteRenderer(gameObject, "testcrab", Color.Purple, 1));
            }
            else if (selectRnd == 8)
            {
                gameObject.AddComponent(new CSpriteRenderer(gameObject, "testcrab", Color.Violet, 1));
            }
            else if (selectRnd == 9)
            {
                gameObject.AddComponent(new CSpriteRenderer(gameObject, "testcrab", Color.Silver, 1));
            }
            else if (selectRnd == 10)
            {
                gameObject.AddComponent(new CSpriteRenderer(gameObject, "testcrab", Color.Pink, 1));
            }

            gameObject.AddComponent(new CCrab(gameObject));
            gameObject.AddComponent(new CAnimator(gameObject));
            gameObject.AddComponent(new CCollider(gameObject, false,8));
        }
    }
}
