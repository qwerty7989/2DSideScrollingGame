using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

using _SideScrollingGame.Content;
using _SideScrollingGame.Manager;

namespace _SideScrollingGame.Scenes
{
    public class CreditScene : GameScene
    {
        private Texture2D _sceneBackground;

        private string _rootFolderName = "IntroScene";
        public void LoadContent()
        {
            _sceneBackground = ContentManagers.Instance.LoadTexture(_rootFolderName, "");
        }

        public void UnloadContent()
        {
        }

        public void Update(GameTime gameTime)
        {
            if (Keyboard.GetState().IsKeyDown(Keys.F))
                SceneManager.Instance.AddScene(SceneManager.SceneName.PlayScene, 0);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(_sceneBackground, new Vector2(0, 0), Color.White);
        }
    }
}
