using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

using _SideScrollingGame.Components;
using _SideScrollingGame.Content;
using _SideScrollingGame.Manager;

namespace _SideScrollingGame.Objects
{
    public class Player : GameObject
    {
        public Vector2 PrevPosition;

        private Texture2D HitboxTexture;
        public Rectangle Hitbox;
        public Rectangle Footbox;
        public Rectangle Rightbox;
        public Rectangle Leftbox;
        public Rectangle Headbox;

        private PlayerState _playerState;
        private bool _playerMoving;

        public bool Direction;

        // ? Timer
        private float Timer = 0;
        private float Threshold;

        public Player()
        {
            _rootFolderName = "PlayerSprite";

            Acceleration = 10f; Deacceleration = 8f; MaxSpeed = 5f;
            Gravity = 19f; JumpSpeed = 800f; MaxFallingSpeed = 600f;
            Direction = true;

            // ? 70, 134
            Position = new Vector2(0, 0); PrevPosition = Position;
            Velocity = new Vector2(0, 0);

            // ? Hitbox
            HitboxTexture = ContentManagers.Instance.LoadTexture(_rootFolderName, "player");
            Hitbox = new Rectangle((int)Position.X, (int)Position.Y, HitboxTexture.Width, HitboxTexture.Height);
            Footbox = new Rectangle((int)Position.X, (int)Position.Y+Hitbox.Height, Hitbox.Width, 1);
            Headbox = new Rectangle((int)Position.X, (int)Position.Y-2, Hitbox.Width, 1);
            Leftbox = new Rectangle((int)Position.X-1, (int)Position.Y, 1, Hitbox.Height);
            Rightbox = new Rectangle((int)Position.X+Hitbox.Width, (int)Position.Y, 1, Hitbox.Height);

            // ? State
            _playerState = PlayerState.Standing;
        }

        public override void LoadContent(ContentManager Content)
        {
        }

        public override void UnloadContent()
        {
        }

        public override void Update(GameTime gameTime)
        {
            // ? Follow
            Camera.Instance.Follow(Player.Instance.Hitbox, 960, 540, 1);
            // ! Non-Zoom
            //Camera.Instance.Follow(Player.Instance.Hitbox, 300, 500, 0, 2200, 50, 50);
            // ! Zoom
            //Camera.Instance.Follow(Player.Instance.Hitbox, 300, 100, 0, 2200, 50, 50, 2);

            PrevPosition = Position;
            InputManager.Instance.Update();
            PlayerInput(gameTime);
            Movement(gameTime);
            if (_playerState == PlayerState.Falling)
            {
                Falling(gameTime);
            }

            //if (Velocity.Y == 0 && Velocity.X == 0)
            //{
            //    _playerState = PlayerState.Standing;
            //}

            // ? Update
            Velocity.X = MoveSpeed;
            Velocity.Y = FallingSpeed;
            CheckCollision();
            Position += Velocity;
            Hitbox.X = (int)Position.X; Hitbox.Y = (int)Position.Y;
            Footbox.X = (int)Position.X; Footbox.Y = (int)Position.Y+Hitbox.Height;
            Headbox.X = (int)Position.X; Headbox.Y = (int)Position.Y-2;

            System.Console.WriteLine(_playerState);
        }

        public override void Falling(GameTime gameTime)
        {
            FallingSpeed += Gravity * (float)gameTime.ElapsedGameTime.TotalSeconds;
            FallingSpeed = MathHelper.Clamp(FallingSpeed, -JumpSpeed, MaxFallingSpeed);
        }

        public void PlayerInput(GameTime gameTime)
        {
            Input _input = InputManager.Instance.inputState;

            _playerMoving = false;
            if (_input == Input.Left)
            {
                MoveSpeed += -Acceleration * (float)gameTime.ElapsedGameTime.TotalSeconds;
                _playerMoving = true;
                Direction = false;
            }
            else if (_input == Input.Right)
            {
                MoveSpeed += Acceleration * (float)gameTime.ElapsedGameTime.TotalSeconds;
                _playerMoving = true;
                Direction = true;
            }

            if (_input == Input.Jump && _playerState != PlayerState.Falling)
            {
                _playerState = PlayerState.Falling;
                FallingSpeed = -JumpSpeed * (float)gameTime.ElapsedGameTime.TotalSeconds;
            }
        }

        public void Movement(GameTime gameTime)
        {
            if (!_playerMoving) MoveSpeed += (System.Math.Sign(-MoveSpeed)*Deacceleration*(float)gameTime.ElapsedGameTime.TotalSeconds);

            // ? Movement
            if (Direction) MoveSpeed = MathHelper.Clamp(MoveSpeed, 0, MaxSpeed);
            else MoveSpeed = MathHelper.Clamp(MoveSpeed, -MaxSpeed, 0);
        }

        public void CheckCollision()
        {

            // ? Check X axis
            foreach (Rectangle rect in Singleton.Instance.TileMapCollisionRects)
            {
                Rectangle testHitbox;
                if (Velocity.X > 0)
                {
                    for (int i = (int)Position.X+Hitbox.Width; i <= Position.X + Hitbox.Width + Velocity.X; i++)
                    {
                        testHitbox = new Rectangle((int)i, (int)Position.Y, 1, Hitbox.Height);
                        if (rect.Intersects(testHitbox))
                        {
                            Position.X = rect.X - Hitbox.Width;
                            Velocity.X = 0;
                            _playerState = PlayerState.Standing;
                        }
                    }
                }
                else if (Velocity.X <= 0)
                {
                    for (int i = (int)Position.X; i >= Position.X + (int)Velocity.X; i--)
                    {
                        testHitbox = new Rectangle((int)i, (int)Position.Y, 1, Hitbox.Height);
                        if (rect.Intersects(testHitbox))
                        {
                            Position.X = rect.X + rect.Width;
                            Velocity.X = 0;
                            _playerState = PlayerState.Standing;
                        }
                    }
                }
            }

            // ? Check Y axis // Fall
            foreach (Rectangle rect in Singleton.Instance.TileMapCollisionRects)
            {
                Rectangle testHitbox;
                if (Velocity.Y > 0)
                {
                    for (int i = (int)Position.Y+Hitbox.Height; i <= Position.Y + Hitbox.Height + Velocity.Y; i++)
                    {
                        testHitbox = new Rectangle((int)Position.X, (int)i, HitboxTexture.Width, 1);
                        if (rect.Intersects(testHitbox) && (i <= rect.Y))
                        {
                            Position.Y = rect.Y - Hitbox.Height;
                            FallingSpeed = 0;
                            Velocity.Y = 0;
                            _playerState = PlayerState.Standing;
                        }
                    }
                }
                else if (Velocity.Y <= 0)
                {
                    for (int i = (int)Position.Y; i >= Position.Y + (int)Velocity.Y; i--)
                    {
                        testHitbox = new Rectangle((int)Position.X, (int)i, HitboxTexture.Width, 1);
                        if (rect.Intersects(testHitbox) && (i >= rect.Y + rect.Height - 1))
                        {
                            Position.Y = rect.Y + rect.Height + 1;
                            FallingSpeed = 0;
                            Velocity.Y = 0;
                            _playerState = PlayerState.Standing;
                        }
                    }
                }
            }

            bool onGround = false;
            foreach (Rectangle rect in Singleton.Instance.TileMapCollisionRects)
            {
                if (rect.Intersects(Footbox))
                {
                    onGround = true;
                }
            }

            if (!onGround)
            {
                _playerState = PlayerState.Falling;
            }
        }

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
