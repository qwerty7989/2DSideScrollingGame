using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

using _SideScrollingGame.Scenes;

namespace _SideScrollingGame.Manager
{
    public class SceneManager
    {
        // ? Variable
        private GameScene[,] _listGameScene;
        private int[] _indexList;
        public enum SceneName
        {
            TestScene,
            IntroScene
        }

        // ? Function
        public SceneManager()
        {
            _listGameScene = new GameScene[7,50];
            _indexList = new int[50];
            AddScene(SceneName.IntroScene, 0);
        }

        public void AddScene(SceneName sceneName, int layerLevel)
        {
            switch (sceneName)
            {
                case SceneName.TestScene:
                    _listGameScene[layerLevel,_indexList[layerLevel]] = new TestScene();
                    break;
                case SceneName.IntroScene:
                    _listGameScene[layerLevel,_indexList[layerLevel]] = new IntroScene();
                    break;
            }
            _indexList[layerLevel]++;
            LoadContent();
        }

        public void ChangeScene(SceneName sceneName)
        {
            _listGameScene = new GameScene[7,50];
            _indexList = new int[50];
            AddScene(sceneName, 0);
            LoadContent();
        }

        // ? Default function
        public void LoadContent()
        {
            //_currentGameScene.LoadContent();
            foreach (GameScene scene in _listGameScene)
            {
                if (scene != null)
                    scene.LoadContent();
            }
        }

        public void UnloadContent()
        {
            foreach (GameScene scene in _listGameScene)
            {
                if (scene != null)
                    scene.UnloadContent();
            }
        }

        public void Update(GameTime gameTime)
        {
            //_currentGameScene.Update(gameTime);
            foreach (GameScene scene in _listGameScene)
            {
                if (scene != null)
                    scene.Update(gameTime);
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            //_currentGameScene.Draw(_spriteBatch);
            foreach (GameScene scene in _listGameScene)
            {
                if (scene != null)
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
