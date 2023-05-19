using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

using _SideScrollingGame.Content;

namespace _SideScrollingGame.Scenes
{
    public class IntroScene : GameScene
    {
        private Texture2D _sceneBackground;

        private string _rootFolderName = "IntroScene";
        public void LoadContent()
        {
            _sceneBackground = ContentManagers.Instance.LoadTexture(_rootFolderName, "IntroBackground");
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
