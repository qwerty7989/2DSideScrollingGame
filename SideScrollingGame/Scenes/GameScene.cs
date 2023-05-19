using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace _SideScrollingGame.Scenes
{
    internal interface GameScene
    {
        public void LoadContent();
        public void UnloadContent();
        public void Update(GameTime gameTime);
        public void Draw(SpriteBatch spriteBatch);
    }
}