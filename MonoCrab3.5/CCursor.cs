using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework;

namespace MonoCrab3._5
{
    public class CCursor : Component, IUpdateable
    {
        public CCursor(GameObject gameObject) : base (gameObject)
        {

        }
        public void Update()
        {
            
            if (GameWorld.gameWorld.startGame)
            {
                Vector2 targetWorldPosition = GameWorld.gameWorld.gameCamera.GetScreenPosition(new Vector2(Mouse.GetState().X, Mouse.GetState().Y));
                gameObject.Transform.position = Vector2.Lerp(gameObject.Transform.position, targetWorldPosition, 0.3f * GameWorld.gameWorld.deltaTime);
            }
        }
    }
}
