using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

using _SideScrollingGame.Content;
using _SideScrollingGame.Manager;
using _SideScrollingGame.Objects;

namespace _SideScrollingGame.Scenes
{
    public class UIScene : GameScene
    {
        private bool _isOpened = true;
        private float _currentTime = 0;
        private float _openDelay = 300f;


        private Texture2D _statusBar;
        private Rectangle[] _statusBarRec;

        private string _rootFolderName = "UIScene";
        public void LoadContent()
        {
            _statusBarRec = new Rectangle[3];
            (_statusBar, _statusBarRec) = ContentManagers.Instance.LoadSprite(_rootFolderName, "Health", 3);
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
            float X = MathHelper.Clamp(Player.Instance.Hitbox.X, (int)(Main.screenWidth / 4.0f), (int)((Player.Instance.Hitbox.X*2)-(Main.screenHeight / 4.0f)));

            // ? Healthbar
            for (int i = 0; i < 3; i++)
            {
                if (Player.Instance._playerHealth > i)
                {
                    spriteBatch.Draw(_statusBar, new Vector2(X-450+(i*36), Player.Instance.Hitbox.Y-250), _statusBarRec[0], Color.White);
                }
                else
                {
                    spriteBatch.Draw(_statusBar, new Vector2(X-450+(i*36), Player.Instance.Hitbox.Y-250), _statusBarRec[2], Color.White);
                }
            }

            if (_isOpened)
            {
            }
        }
    }
}
