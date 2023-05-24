using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace _SideScrollingGame.Objects
{
    public class Animation
    {
        private Texture2D Texture;
        private Rectangle[] Rectangles;
        private int Amount;
        private Vector2 Position;

        private float Timer = 0;
        private float Threshold;

        private int Index = 0;
        private int PlayCount = 0;

        public Animation((Texture2D, Rectangle[]) sprite, Vector2 position, float threshold = 150)
        {
            Texture = sprite.Item1;
            Rectangles = sprite.Item2;
            Position = position;
            Amount = Rectangles.Length;
            Threshold = threshold;
        }

        public void LoadContent(ContentManager Content)
        {
        }

        public void UnloadContent()
        {
        }

        public bool IsDone()
        {
            if (PlayCount > 0)
            {
                PlayCount = 0;
                return true;
            }
            return false;
        }

        public bool IsDone(int index)
        {
            if (Index > index)
            {
                return true;
            }
            return false;
        }

        public void Update(GameTime gameTime)
        {
            if (Timer > Threshold)
            {
                Index += 1;

                if (Index == Amount)
                {
                    Index = 0;
                    PlayCount++;
                }

                Timer = 0;
            }
            else
            {
                Timer += (float)gameTime.ElapsedGameTime.TotalMilliseconds;
            }
        }

        public void Draw(SpriteBatch spriteBatch, Vector2 Position, bool _playerDirection)
        {
            if (_playerDirection)
            {
                spriteBatch.Draw(Texture, Position, Rectangles[Index], Color.White);
            }
            else
            {
                spriteBatch.Draw(Texture, Position, Rectangles[Index], Color.White, 0f, Vector2.Zero, 1f, SpriteEffects.FlipHorizontally, 0f);
            }
        }
    }
}
