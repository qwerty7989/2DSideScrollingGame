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
        public enum SceneName
        {
            // ? Each Scene files are referenced here.
            IntroScene
        }

        // ? Function
        public SceneManager()
        {
            _currentGameScene = new IntroScene();
        }

            // ! Load some scene and unload it.

            // ! Load regular scene


        public void LoadContent()
        {
            _currentGameScene.LoadContent();
        }

        public void UnloadContent()
        {
            _currentGameScene.UnloadContent();
        }

        public void Update(GameTime gameTime)
        {
            _currentGameScene.Update(gameTime);
        }

        public void Draw(SpriteBatch _spriteBatch)
        {
            _currentGameScene.Draw(_spriteBatch);
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
