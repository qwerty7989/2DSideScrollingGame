using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

using _SideScrollingGame.Content;
using _SideScrollingGame.Manager;
using _SideScrollingGame.Objects;

namespace _SideScrollingGame.Scenes
{
    public class PlayScene : GameScene
    {
        private Texture2D _tile;
        private Texture2D _background;

        private string _rootFolderName = "PlayScene";
        public PlayScene()
        {
            Singleton.Instance.isGameStart = true;
        }

        public void LoadContent()
        {
            _tile = ContentManagers.Instance.LoadTexture(_rootFolderName, "tile");
            _background = ContentManagers.Instance.LoadTexture(_rootFolderName, "background");
        }

        public void UnloadContent()
        {
        }

        public void Update(GameTime gameTime)
        {
            //if (Keyboard.GetState().IsKeyDown(Keys.F))
            //    SceneManager.Instance.AddScene(SceneManager.SceneName.UIScene, 0);

            // ? Player
            Player.Instance.Update(gameTime);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            // ? Map - Background
            spriteBatch.Draw(_background, new Vector2(0, 0), Color.White);
            //spriteBatch.Draw(_tile, new Vector2(0, 897), Color.White);

            // ? Player
            Player.Instance.Draw(spriteBatch);
        }
    }
}