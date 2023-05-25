using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

using _SideScrollingGame.Content;
using _SideScrollingGame.Manager;

namespace _SideScrollingGame.Scenes
{
    public class UIScene : GameScene
    {
        private bool _isOpened = false;
        private float _currentTime = 0;
        private float _openDelay = 300f;

        private Texture2D _statusBar;

        private string _rootFolderName = "UIScene";
        public void LoadContent()
        {
            _statusBar = ContentManagers.Instance.LoadTexture("TestScene", "PlaySceneGameBackground");
        }

        public void UnloadContent()
        {
        }

        public void Update(GameTime gameTime)
        {
            _currentTime += (float)gameTime.ElapsedGameTime.TotalMilliseconds;

            if (_currentTime > _openDelay && Keyboard.GetState().IsKeyDown(Keys.F))
            {
                _currentTime = 0;
                _isOpened = !_isOpened;
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            if (_isOpened)
            {
                spriteBatch.Draw(_statusBar, new Vector2(0, 0), Color.White);
            }
        }
    }
}
