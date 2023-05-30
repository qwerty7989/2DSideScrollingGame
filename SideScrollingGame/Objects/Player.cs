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

        private float _playerMovementSpeed = 0.2f;
        private float _playerDeacceleration = 0.5f;
        private float _playerFallingSpeed = 20;
        private float _playerJumpSpeed = 6;
        private bool _playerDirection = true;
        public bool _isPlayerFalling = true;

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

            // ? Animation
            HitboxTexture = ContentManagers.Instance.LoadTexture(_rootFolderName, "hitbox");
            Hitbox = new Rectangle((int)Position.X, (int)Position.Y, 70, 134);
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

            if (keyboard.IsKeyDown(Keys.Space))
            {
                Velocity.Y = -_playerJumpSpeed;
                _isPlayerFalling = true;
            }

            if (_isPlayerFalling)
            {
                Velocity.Y += _playerFallingSpeed * deltaTime;
            }

            if (keyboard.IsKeyDown(Keys.A) || keyboard.IsKeyDown(Keys.D))
            {
                if (keyboard.IsKeyDown(Keys.A))
                {
                    Velocity.X += -_playerMovementSpeed;
                    _playerDirection = false;
                }
                else
                {
                    Velocity.X += _playerMovementSpeed;
                    _playerDirection = true;
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
            if (_playerDirection)
            {
                Velocity.X = MathHelper.Clamp(Velocity.X, 0, 10);
            }
            else
            {
                Velocity.X = MathHelper.Clamp(Velocity.X, -10, 0);
            }

            Velocity.Y = MathHelper.Clamp(Velocity.Y, -_playerJumpSpeed, _playerFallingSpeed);
            Position += Velocity;
            Hitbox.X = (int)Position.X;
            Hitbox.Y = (int)Position.Y;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            if (_playerDirection)
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
