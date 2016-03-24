using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using System.Diagnostics;
namespace MonoCrab3._5
{
    public class CLoadingUI : Component, IUpdateable
    {
        private CSpriteRenderer spriteRenderer;
        private float timerLimit = 1.5f;
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
                //While the opacity is lerping, zoom in the camera as well so we are close to out game objects     
                GameWorld.gameWorld.gameCamera.currentCamState = CameraZoomState.MainMenu;    
                //GameWorld.gameWorld.gameCamera.zoom = MathHelper.Lerp(GameWorld.gameWorld.gameCamera.zoom, 0.165f, 10f * GameWorld.gameWorld.deltaTime);
                ////Since we are lerping floats, we need to check the rounded up differences, so we can disable our lerp.
                //if (Math.Round((decimal)GameWorld.gameWorld.gameCamera.zoom, 2) == 0.16m)
                //{
                //    GameWorld.gameWorld.GameObjects.Remove(this.gameObject);
                //}                

            }
            timePassed += (float)GameWorld.gameWorld.deltaTime;

        }
    }
}
