using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

using _SideScrollingGame.Content;
using _SideScrollingGame.Manager;

namespace _SideScrollingGame.Scenes
{
    public class BackgroundScene : GameScene
    {
        private Texture2D _sceneBackground;

        private string _rootFolderName = "BackgroundScene";
        public void LoadContent()
        {
            _sceneBackground = ContentManagers.Instance.LoadTexture(_rootFolderName, "Background");
        }

        public void UnloadContent()
        {
        }

        public void Update(GameTime gameTime)
        {
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(_sceneBackground, new Vector2(0, 0), Color.White);
        }
    }
}
