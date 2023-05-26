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
            IntroScene,
            BackgroundScene,
            TilemapScene,
            UIScene,
        }

        // ? Function
        public SceneManager()
        {
            _listGameScene = new GameScene[7,50];
            _indexList = new int[50];
            ChangeScene(SceneName.IntroScene, 1);
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
                case SceneName.BackgroundScene:
                    _listGameScene[layerLevel,_indexList[layerLevel]] = new BackgroundScene();
                    break;
                case SceneName.TilemapScene:
                    _listGameScene[layerLevel,_indexList[layerLevel]] = new TilemapScene();
                    break;
                case SceneName.UIScene:
                    _listGameScene[layerLevel,_indexList[layerLevel]] = new UIScene();
                    break;
            }
            _indexList[layerLevel]++;
            LoadContent(layerLevel, _indexList[layerLevel] - 1);
        }

        public void ChangeScene(SceneName sceneName, int layerLevel)
        {
            _listGameScene = new GameScene[7,50];
            _indexList = new int[50];
            AddScene(sceneName, layerLevel);
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

        // ? Default function
        public void LoadContent(int layerLevel, int index)
        {
            _listGameScene[layerLevel, index].LoadContent();
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
