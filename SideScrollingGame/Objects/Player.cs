using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

using _SideScrollingGame.Content;

namespace _SideScrollingGame.Objects
{
    public class Player : GameObject
    {
        private Texture2D Texture;
        public Vector2 Position;
        public Vector2 PrevPosition;
        public Vector2 Velocity;

        private Texture2D HitboxTexture;
        public Rectangle Hitbox;
        public Rectangle Footbox;
        public Rectangle Headbox;

        private float _playerMovementSpeed = 0.2f;
        private float _playerDeacceleration = 0.5f;
        private float _playerFallingSpeed = 20;
        private float _playerJumpSpeed = 15;
        public bool PlayerDirection = true;
        public bool _isPlayerOnGround = false;

        // ? Timer
        private float Timer = 0;
        private float Threshold;

        private string _rootFolderName = "PlayerSprite";
        public Player()
        {
            // ? 70, 134
            Position = new Vector2(1000, 897-134);
            PrevPosition = Position;
            Velocity = new Vector2();

            // ? Hitbox
            HitboxTexture = ContentManagers.Instance.LoadTexture(_rootFolderName, "hitbox");
            Hitbox = new Rectangle((int)Position.X, (int)Position.Y, HitboxTexture.Width, HitboxTexture.Height);
            Footbox = new Rectangle((int)Position.X, (int)Position.Y+Hitbox.Height, Hitbox.Width, 1);
            Headbox = new Rectangle((int)Position.X, (int)Position.Y-2, Hitbox.Width, 2);
        }

        public void LoadContent(ContentManager Content)
        {
        }

        public void UnloadContent()
        {
        }

        public void Update(GameTime gameTime)
        {
            PrevPosition = Position;
            float deltaTime = (float)gameTime.ElapsedGameTime.TotalSeconds;

            KeyboardState keyboard = Keyboard.GetState();

            if (keyboard.IsKeyDown(Keys.Space) && _isPlayerOnGround)
            {
                Velocity.Y = -_playerJumpSpeed;
                _isPlayerOnGround = false;
            }

            if (!_isPlayerOnGround)
            {
                Velocity.Y += _playerFallingSpeed * deltaTime;
            }

            if (keyboard.IsKeyDown(Keys.A) || keyboard.IsKeyDown(Keys.D))
            {
                if (keyboard.IsKeyDown(Keys.A))
                {
                    Velocity.X += -_playerMovementSpeed;
                    PlayerDirection = false;
                }
                else
                {
                    Velocity.X += _playerMovementSpeed;
                    PlayerDirection = true;
                }
            }
            else if (Velocity.X != 0)
            {
                Velocity.X += (System.Math.Sign(-Velocity.X)*_playerDeacceleration);
            }

            // ? Camera
            if (keyboard.IsKeyDown(Keys.Left))
                Singleton.Instance.offsetX -= 10;
            if (keyboard.IsKeyDown(Keys.Right))
                Singleton.Instance.offsetX += 10;
            if (keyboard.IsKeyDown(Keys.Up))
                Singleton.Instance.offsetY -= 10;
            if (keyboard.IsKeyDown(Keys.Down))
                Singleton.Instance.offsetY += 10;

            // ? Movement
            if (PlayerDirection)
            {
                Velocity.X = MathHelper.Clamp(Velocity.X, 0, 10);
            }
            else
            {
                Velocity.X = MathHelper.Clamp(Velocity.X, -10, 0);
            }

            if (_isPlayerOnGround)
            {
                Velocity.Y = MathHelper.Clamp(Velocity.Y, 0, 1f);
            }
            else
            {
                Velocity.Y = MathHelper.Clamp(Velocity.Y, -_playerJumpSpeed, _playerFallingSpeed);
            }

            Position += Velocity;
            Hitbox.X = (int)Position.X;
            Hitbox.Y = (int)Position.Y;
            Footbox.X = (int)Position.X;
            Footbox.Y = (int)Position.Y+Hitbox.Height;
            Headbox.X = (int)Position.X;
            Headbox.Y = (int)Position.Y-2;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            if (PlayerDirection)
            {
                spriteBatch.Draw(HitboxTexture, Position, Color.White);
            }
            else
            {
                spriteBatch.Draw(HitboxTexture, Position, null, Color.White, 0f, Vector2.Zero, 1f, SpriteEffects.FlipHorizontally, 0f);
            }
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
