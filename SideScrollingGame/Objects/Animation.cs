using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace _SideScrollingGame.Objects
{
    public class Animation : GameObject
    {
        private Texture2D Texture;
        private Rectangle[] Rectangles;
        private int Amount;
        private Vector2 Position;

        private float Timer = 0;
        private float Threshold;

        private int Index = 0;

        public Animation((Texture2D, Rectangle[]) sprite, Vector2 position, float threshold)
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

        public void Update(GameTime gameTime)
        {
            if (Timer > Threshold)
            {
                Index += 1;

                if (Index == Amount)
                {
                    Index = 0;
                }

                Timer = 0;
            }
            else
            {
                Timer += (float)gameTime.ElapsedGameTime.TotalMilliseconds;
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(Texture, Position, Rectangles[Index], Color.White);
        }
    }
}
