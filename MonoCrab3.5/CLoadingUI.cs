using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace MonoCrab3._5
{
    public class CLoadingUI : Component, IUpdateable
    {
        private CSpriteRenderer spriteRenderer;
        private float timerLimit = 5f;
        private float timePassed = 0;
        public CLoadingUI(GameObject gameObject) : base(gameObject)
        {
            this.spriteRenderer = (CSpriteRenderer) gameObject.GetComponent("CSpriteRenderer");
        }
        public void Update()
        {
            if (timePassed > timerLimit && !GameWorld.gameWorld.startGame)
            {
                spriteRenderer.opacity = MathHelper.Lerp(spriteRenderer.opacity, 0, 5f * GameWorld.gameWorld.deltaTime);
                //just remove the object if it's faded out
                if (spriteRenderer.opacity <= 0.01)
                {
                    GameWorld.gameWorld.GameObjects.Remove(this.gameObject);
                }
            }
        }
    }
}
