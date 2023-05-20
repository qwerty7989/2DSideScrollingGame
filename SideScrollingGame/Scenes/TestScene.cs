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
        private Texture2D _sceneBackground;
        private Player _player;

        private string _rootFolderName = "TestScene";
        public void LoadContent()
        {
            _sceneBackground = ContentManagers.Instance.LoadTexture(_rootFolderName, "PlaySceneGameBackground");
            _player = new Player();
        }

        public void UnloadContent()
        {
        }

        public void Update(GameTime gameTime)
        {
            if (Keyboard.GetState().IsKeyDown(Keys.G))
                SceneManager.Instance.ChangeScene(SceneManager.SceneName.IntroScene);
            _player.Update(gameTime);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(_sceneBackground, new Vector2(0, 0), Color.White);
            _player.Draw(spriteBatch);
        }
    }
}
