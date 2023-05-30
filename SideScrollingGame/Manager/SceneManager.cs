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
            IntroScene,
            CreditScene,
            PlayScene,
            TilemapScene,
            UIScene
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
                case SceneName.IntroScene:
                    _listGameScene[layerLevel,_indexList[layerLevel]] = new IntroScene();
                    break;
                case SceneName.CreditScene:
                    _listGameScene[layerLevel,_indexList[layerLevel]] = new CreditScene();
                    break;
                case SceneName.PlayScene:
                    _listGameScene[layerLevel,_indexList[layerLevel]] = new PlayScene();
                    break;
                case SceneName.TilemapScene:
                    _listGameScene[layerLevel,_indexList[layerLevel]] = new TilemapScene();
                    break;
            }
            _indexList[layerLevel]++;
            LoadContent();
        }

        public void ChangeScene(SceneName sceneName, int layerLevel)
        {
            _listGameScene = new GameScene[7,50];
            _indexList = new int[50];
            AddScene(sceneName, layerLevel);
            LoadContent();
        }

        // ? Default function
        public void LoadContent()
        {
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
            foreach (GameScene scene in _listGameScene)
            {
                if (scene != null)
                    scene.Update(gameTime);
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
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
