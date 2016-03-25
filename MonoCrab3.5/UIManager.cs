using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
namespace MonoCrab3._5
{
    class UIManager
    {
        string[] UIContent = new string[] { "Loading", "MonoCrabWin", "MonoCrabLose" };
        Texture2D[] UIScreens;
        private Texture2D currentUIScreen;
        private static UIManager Manager;
        public bool shouldDraw = false;
        private bool fadeOut = false;
        private KeyboardState oldState1;
        private float opacity = 1f;
        public static UIManager manager
        {
            get
            {
                if (Manager == null)
                {
                    Manager = new UIManager();
                }
                return Manager;
            }
        }
        public UIManager()
        {
            UIScreens = new Texture2D[UIContent.Length];

        }
        /// <summary>
        /// Draws a picture in the center of the camera, used for UI
        /// </summary>
        /// <param name="spriteBatch"></param>
        public void Draw(SpriteBatch spriteBatch)
        {
            if (shouldDraw)
            {
                Vector2 centerScreen = new Vector2(GameWorld.gameWorld.gameCamera.Position.X - currentUIScreen.Width / 2, GameWorld.gameWorld.gameCamera.Position.Y - currentUIScreen.Height / 2);

                spriteBatch.Draw(currentUIScreen, centerScreen, null, Color.White * opacity, 0, Vector2.Zero, 1, SpriteEffects.None, 1f);

                KeyboardState NewKey1State = Keyboard.GetState();
                //Hardcodes the controls for all panels. 
                //TODO more control
                if (NewKey1State.IsKeyDown(Keys.Enter) && oldState1.IsKeyUp(Keys.Enter) && !GameWorld.gameWorld.startGame)
                {
                    fadeOut = true;
                    
                }
                oldState1 = NewKey1State;

                if (fadeOut)
                {
                    //If we reached the lerp?
                    if (Math.Round((decimal)opacity, 2) != 0.0m)
                    {
                        opacity = MathHelper.Lerp(opacity, 0, 2f * GameWorld.gameWorld.deltaTime);
                    }
                    else
                    {
                        shouldDraw = false;
                        fadeOut = false;

                    }
                }
            }
        }
        public void LoadContent(ContentManager content)
        {
            for (int i = 0; i < UIContent.Length; i++)
            {
                UIScreens[i] = content.Load<Texture2D>(UIContent[i]);
            }
        }
        public void SetUIScreen(int index)
        {
            currentUIScreen = UIScreens[index];

            opacity = 1f;
            fadeOut = false;
            shouldDraw = true;
        }

    }
}
