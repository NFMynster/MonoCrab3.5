using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MonoCrab3._5
{
    public abstract class Component
    {
        public GameObject gameObject { get; private set; }

        public Component(GameObject gameObject)
        {
            this.gameObject = gameObject;
        }

        public Component()
        {
            
        }

       
}
}

