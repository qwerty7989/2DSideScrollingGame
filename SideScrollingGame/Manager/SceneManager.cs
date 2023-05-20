using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

using _SideScrollingGame.Scenes;

namespace _SideScrollingGame.Manager
{
    public class SceneManager
    {
        // ? Variable
        private GameScene _currentGameScene;
        private GameScene[] _listGameScene;
        public enum SceneName
        {
            // ? Each Scene files are referenced here.
            TestScene,
            IntroScene
        }

        // ? Function
        public SceneManager()
        {
            //_currentGameScene = new IntroScene();
            _listGameScene = new GameScene[2];
        }

        public void AddScene(SceneName sceneName)
        {
        }

        public void ChangeScene(SceneName sceneName)
        {
            switch (sceneName)
            {
                case SceneName.IntroScene:
                    _currentGameScene = new IntroScene();
                    break;
                case SceneName.TestScene:
                    _currentGameScene = new TestScene();
                    break;
            }
            LoadContent();
        }

        public void LoadContent()
        {
            //_currentGameScene.LoadContent();
            foreach (GameScene scene in _listGameScene)
            {
                scene.LoadContent();
            }
        }

        public void UnloadContent()
        {
            _currentGameScene.UnloadContent();
        }

        public void Update(GameTime gameTime)
        {
            //_currentGameScene.Update(gameTime);
            foreach (GameScene scene in _listGameScene)
            {
                scene.Update(gameTime);
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            //_currentGameScene.Draw(_spriteBatch);
            foreach (GameScene scene in _listGameScene)
            {
                scene.Draw(spriteBatch);
            }
        }

        // ? Singleton
        private static SceneManager instance;
        public static SceneManager Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new SceneManager();
                }
                return instance;
            }
        }

    }
}
