using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace MonoCrab3._5
{
    class CIntroMenu : Component, IUpdateable
    {
        private CSpriteRenderer spriteRenderer;
        public CIntroMenu(GameObject gameObject) : base(gameObject)
        {
            this.spriteRenderer = (CSpriteRenderer) gameObject.GetComponent("CSpriteRenderer");
        }

        public void Update()
        {
            KeyboardState NewKeyState = Keyboard.GetState();

            if (NewKeyState.IsKeyDown(Keys.Enter) && !GameWorld.gameWorld.startGame)
            {
                //GameWorld.gameWorld.gameCamera.shouldLerp = true;
                GameWorld.gameWorld.gameCamera.currentCamState = CameraZoomState.Game;
                GameWorld.gameWorld.startGame = true;
                
            }
            else if (GameWorld.gameWorld.startGame)
            {
                spriteRenderer.opacity = MathHelper.Lerp(spriteRenderer.opacity, 0, 5f*GameWorld.gameWorld.deltaTime);
                //just remove the object if it's faded out
                if (spriteRenderer.opacity <= 0.01)
                {
                    GameWorld.gameWorld.GameObjects.Remove(this.gameObject);
                }
            }
            
        }
    }
}
