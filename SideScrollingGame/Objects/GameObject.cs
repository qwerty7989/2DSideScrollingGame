using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace _SideScrollingGame.Objects
{
    internal interface GameObject
    {
        public void LoadContent(ContentManager Content);
        public void UnloadContent();
        public void Update(GameTime gameTime);
        public void Draw(SpriteBatch spriteBatch);
    }
}
