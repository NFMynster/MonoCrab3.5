using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;

namespace MonoCrab3._5
{
    interface IAnimateable
    {
        void OnAnimationDone(string animationName);
        void CreateAnimations();
    }
}
