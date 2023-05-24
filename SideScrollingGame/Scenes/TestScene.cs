using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework;

using _SideScrollingGame.Content;
using _SideScrollingGame.Manager;
using _SideScrollingGame.Objects;

namespace _SideScrollingGame.Scenes
{
    public class TestScene : GameScene
    {
        private bool _isClicked = false;

        private string _rootFolderName = "TestScene";
        public TestScene()
        {

        }

        public void LoadContent()
        {
            SceneManager.Instance.AddScene(SceneManager.SceneName.TilemapScene, 2);
        }

        public void UnloadContent()
        {
        }

        public void Update(GameTime gameTime)
        {
            if (Keyboard.GetState().IsKeyDown(Keys.F) && !_isClicked)
            {
                SceneManager.Instance.AddScene(SceneManager.SceneName.BackgroundScene, 0);
                _isClicked = true;
            }
            Player.Instance.Update(gameTime);
        }

        public void Draw(SpriteBatch spriteBatch)
        {;
            Player.Instance.Draw(spriteBatch);
        }
    }
}
