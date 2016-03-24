using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace MonoCrab3._5
{
    class Director
    {
        private IBuilder builder;
        public Director(IBuilder builder)
        {
            this.builder = builder;
        }

        public GameObject Construct(Vector2 position)
        {
            builder.BuildGameObject(position);
            return builder.GetResult();
        }
    }
}
