using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace _SideScrollingGame.Objects
{
    public class GameObject
    {
        public Texture2D Texture;
        public Vector2 Position;
        public Vector2 Velocity;

        public float MoveSpeed;
        public float Acceleration;
        public float Deacceleration;
        public float MaxSpeed;
        public float JumpSpeed;
        public float FallingSpeed;
        public float MaxFallingSpeed;
        public float Gravity;

        public string _rootFolderName;

        public virtual void LoadContent(ContentManager Content) {}
        public virtual void UnloadContent() {}
        public virtual void Update(GameTime gameTime) {}
        public virtual void Draw(SpriteBatch spriteBatch) {}
        public virtual void Falling(GameTime gameTime) {}
    }
}
