using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

using _SideScrollingGame.Content;

namespace _SideScrollingGame.Objects
{
    public class Player : GameObject
    {
        private Texture2D Texture;
        private Rectangle[] Rectangles;
        private Vector2 Position;

        // ? Animation
        private Animation[] _punchAnimation = new Animation[2];

        // ? Timer
        private float Timer = 0;
        private float Threshold;

        private string _rootFolderName = "PlayerSprite";
        public Player()
        {
            _punchAnimation[0] = new Animation(ContentManagers.Instance.LoadSprite(_rootFolderName, "Punk_attack1", 6), Position, 250);
        }

        public void LoadContent(ContentManager Content)
        {
        }

        public void UnloadContent()
        {
        }

        public void Update(GameTime gameTime)
        {
            _punchAnimation[0].Update(gameTime);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            _punchAnimation[0].Draw(spriteBatch);
        }
    }
}
