using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

using _SideScrollingGame.Content;
using _SideScrollingGame.Manager;

namespace _SideScrollingGame.Scenes
{
    public class IntroScene : GameScene
    {
        private Texture2D[] _sceneBackground;

        private int _selIndex = 0;

        // ? Timer
        private float Timer = 0;
        private float Threshold;
        private float _selDelay = 150f;
        private float _currentTime = 0;

        private string _rootFolderName = "IntroScene";
        public void LoadContent()
        {
            _sceneBackground = new Texture2D[3];
            for (int i = 1; i <= 3; i++)
            {
                _sceneBackground[i-1] = ContentManagers.Instance.LoadTexture(_rootFolderName, "Introduction"+i);

            }
        }

        public void UnloadContent()
        {
        }

        public void Update(GameTime gameTime)
        {
            _currentTime += (float)gameTime.ElapsedGameTime.TotalMilliseconds;

            if (_currentTime > _selDelay && Keyboard.GetState().IsKeyDown(Keys.Up))
            {
                _currentTime = 0;
                if (_selIndex == 0) _selIndex = 0;
                else _selIndex -= 1;
            }
            else if (_currentTime > _selDelay && Keyboard.GetState().IsKeyDown(Keys.Down))
            {
                _currentTime = 0;
                if (_selIndex == 2) _selIndex = 2;
                else _selIndex += 1;
            }

            if (Keyboard.GetState().IsKeyDown(Keys.Z))
            {
                if (_selIndex == 0)
                {
                    SceneManager.Instance.ChangeScene(SceneManager.SceneName.TestScene, 1);
                }
                else if (_selIndex == 1)
                {

                }
                else if (_selIndex == 2)
                {
                    Main._isPlayerExit = true;
                }
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(_sceneBackground[_selIndex], new Vector2(0, 0), Color.White);
        }
    }
}
