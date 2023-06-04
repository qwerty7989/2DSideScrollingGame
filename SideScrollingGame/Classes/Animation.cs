using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace _SideScrollingGame.Classes
{
    public class Animation
    {
        private Texture2D Texture;
        private Rectangle[] Rectangles;
        private Vector2 Position;

        private float Timer = 0;
        private float Threshold;

        private int Index = 0;
        private int Count = 0;
        private int Length;

        public Animation(Texture2D texture, Rectangle[] rectangles, Vector2 position, float threshold = 150)
        {
            Texture = texture;
            Rectangles = rectangles;
            Position = position;
            Length = Rectangles.Length;
            Threshold = threshold;
        }

        public void LoadContent(ContentManager Content)
        {
        }

        public void UnloadContent()
        {
        }

        public void Update(GameTime gameTime)
        {
            if (Timer > Threshold)
            {
                Index += 1;

                if (Index == Length)
                {
                    Index = 0;
                    Count++;
                }

                Timer = 0;
            }
            else Timer += (float)gameTime.ElapsedGameTime.TotalMilliseconds;
        }

        public void Draw(SpriteBatch spriteBatch, Vector2 Position, bool Direction)
        {
            if (Direction) spriteBatch.Draw(Texture, Position, Rectangles[Index], Color.White);
            else spriteBatch.Draw(Texture, Position, Rectangles[Index], Color.White, 0f, Vector2.Zero, 1f, SpriteEffects.FlipHorizontally, 0f);
        }
    }
}
