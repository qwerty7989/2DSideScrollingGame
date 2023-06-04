using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

using _SideScrollingGame.Classes;
using _SideScrollingGame.Content;

namespace _SideScrollingGame.Objects
{
    public class Player : GameObject
    {
        public Vector2 PrevPosition;

        private Texture2D HitboxTexture;
        public Rectangle Hitbox;
        public Rectangle Footbox;
        public Rectangle Headbox;

        public bool Direction;

        // ? Timer
        private float Timer = 0;
        private float Threshold;

        public Player()
        {
            _rootFolderName = "PlayerSprite";

            MoveSpeed = 0.2f; Deacceleration = 0.5f; MaxSpeed = 10f;
            FallingSpeed = 32f; JumpSpeed = 18f;
            Direction = true;

            // ? 70, 134
            Position = new Vector2(1000, 897-134); PrevPosition = Position;
            Velocity = new Vector2();

            // ? Hitbox
            HitboxTexture = ContentManagers.Instance.LoadTexture(_rootFolderName, "hitbox");
            Hitbox = new Rectangle((int)Position.X, (int)Position.Y, HitboxTexture.Width, HitboxTexture.Height);
            Footbox = new Rectangle((int)Position.X, (int)Position.Y+Hitbox.Height, Hitbox.Width, 1);
            Headbox = new Rectangle((int)Position.X, (int)Position.Y-2, Hitbox.Width, 2);
        }

        public override void LoadContent(ContentManager Content)
        {
        }

        public override void UnloadContent()
        {
        }

        public override void Update(GameTime gameTime)
        {
            // ! Non-Zoom
            //Camera.Instance.Follow(Player.Instance.Hitbox, 300, 500, 0, 2200, 50, 50);
            // ! Zoom
            Camera.Instance.Follow(Player.Instance.Hitbox, 300, 100, 0, 2200, 50, 50, 2);

            PrevPosition = Position;
            KeyboardState keyboard = Keyboard.GetState();

            if (keyboard.IsKeyDown(Keys.Space) && Velocity.Y == 0)
            {
                Velocity.Y = -JumpSpeed;
                Falling(gameTime);
            }

            Movement(keyboard);

            Position += Velocity;
            Hitbox.X = (int)Position.X;
            Hitbox.Y = (int)Position.Y;
            Footbox.X = (int)Position.X;
            Footbox.Y = (int)Position.Y+Hitbox.Height;
            Headbox.X = (int)Position.X;
            Headbox.Y = (int)Position.Y-2;
        }

        public override void Falling(GameTime gameTime)
        {
            Velocity.Y += FallingSpeed * (float)gameTime.ElapsedGameTime.TotalSeconds;
            Velocity.Y = MathHelper.Clamp(Velocity.Y, -JumpSpeed, FallingSpeed);
        }

        public void Movement(KeyboardState keyboard)
        {
            if (keyboard.IsKeyDown(Keys.A) || keyboard.IsKeyDown(Keys.D))
            {
                if (keyboard.IsKeyDown(Keys.A))
                {
                    Velocity.X += -MoveSpeed;
                    Direction = false;
                }
                else // ? Go right
                {
                    Velocity.X += MoveSpeed;
                    Direction = true;
                }
            }
            else if (Velocity.X != 0) Velocity.X += (System.Math.Sign(-Velocity.X)*Deacceleration);

            // ? Movement
            if (Direction) Velocity.X = MathHelper.Clamp(Velocity.X, 0, MaxSpeed);
            else Velocity.X = MathHelper.Clamp(Velocity.X, -MaxSpeed, 0);
        }

        //public bool CheckCollision()
        //{

        //    // ? Check X axis
        //    foreach (Rectangle rect in _collisionRects)
        //    {

        //    }

        //    // ? Check Y axis
        //    foreach (Rectangle rect in _collisionRects)
        //    {

        //    }

        //    return false;
        //}

        public void Debugger(KeyboardState keyboard)
        {
            // ? Camera
            if (keyboard.IsKeyDown(Keys.Left))
                Singleton.Instance.offsetX -= 10;
            if (keyboard.IsKeyDown(Keys.Right))
                Singleton.Instance.offsetX += 10;
            if (keyboard.IsKeyDown(Keys.Up))
                Singleton.Instance.offsetY -= 10;
            if (keyboard.IsKeyDown(Keys.Down))
                Singleton.Instance.offsetY += 10;
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            if (Direction) spriteBatch.Draw(HitboxTexture, Position, Color.White);
            else spriteBatch.Draw(HitboxTexture, Position, null, Color.White, 0f, Vector2.Zero, 1f, SpriteEffects.FlipHorizontally, 0f);
        }

        // ? Singleton
        private static Player instance;
        public static Player Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new Player();
                }
                return instance;
            }
        }
    }
}
