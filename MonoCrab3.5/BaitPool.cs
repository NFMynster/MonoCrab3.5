using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace MonoCrab3._5
{
    class BaitPool
    {
        private IBuilder baitBuilder;
        private Director baitDirector;
        private List<GameObject> inactiveBaits = new List<GameObject>();
        public List<GameObject> activeBaits = new List<GameObject>();
        private Random random = new Random();
        private static BaitPool baitPool;
        public static BaitPool baitPoolInstance
        {
            get
            {
                if (baitPool == null)
                {
                    baitPool = new BaitPool();
                }
                return baitPool;
            }
        }

        private GameObject gameObject;
        public GameObject Create(Vector2 position,BaitTypes baitType)
        {
            if (inactiveBaits.Count > 0)
            {
                gameObject = inactiveBaits[0];
                activeBaits.Add(gameObject);
                inactiveBaits.RemoveAt(0);
                return gameObject;
            }
            else
            {
                baitBuilder = new BaitBuilder(baitType);
                baitDirector = new Director(baitBuilder);
                gameObject = baitDirector.Construct(position);
                activeBaits.Add(gameObject);
                return gameObject;
            }
        }
    }
}
