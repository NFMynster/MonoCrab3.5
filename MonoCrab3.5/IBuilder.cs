using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace MonoCrab3._5
{
    interface IBuilder
    {
        GameObject GetResult();

        void BuildGameObject(Vector2 position);
    }
}
