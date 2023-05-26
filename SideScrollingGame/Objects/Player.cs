using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

using _SideScrollingGame.Content;
using System;

namespace _SideScrollingGame.Objects
{
    public class Player : GameObject
    {
        private Texture2D Texture;
        public Vector2 Position;
        public Vector2 Velocity;
        public Vector2 PrevPosition;

        public Rectangle Hitbox;
        public Rectangle FallingRect;

        public int _playerHealth = 3;
        private float _playerMovementSpeed = 3;

        private float _playerFallingSpeed = 3;
        public bool _isPlayerFalling = false;

        private bool _playerDirection = true;
        private bool _isPlayerAnimationDone = true;

        private float _attackDelay = 440f;
        private float _currentTime = 0;

        // ? Jumping
        private bool _playerJumping = false;

        // ? Animation
        private enum PlayerAnimationName
        {
            Idle,
            Run,
            Attack1,
            Attack2,
            Jump,
            GunIdle,
            GunRun
        }
        private PlayerAnimationName _currentPlayerAnimation;
        private PlayerAnimationName _playingPlayerAnimation;
        private Animation _idleAnimation;
        private Animation _runAnimation;
        private Animation _attack1Animation;
        private Animation _attack2Animation;

        // ? Guns, guns, guns
        private bool _unlockedGuns = true;
        private Animation _idleGunAnimation;
        private Animation _runGunAnimation;
        private Texture2D _gunIdle;
        private Texture2D _gunRun;

        // ? Timer
        private float Timer = 0;
        private float Threshold;

        private string _rootFolderName = "PlayerSprite";
        public Player()
        {
            Position = new Vector2();
            Velocity = new Vector2();
            PrevPosition = Position;

            Hitbox = new Rectangle((int)Position.X, (int)Position.Y, 26, 48);
            FallingRect = new Rectangle((int)Position.X, (int)Position.Y+48, 24, 1);

            // ? Animation
            _idleAnimation = new Animation(ContentManagers.Instance.LoadSprite(_rootFolderName, "Punk_idle", 4, 34), Position, 200);
            _runAnimation = new Animation(ContentManagers.Instance.LoadSprite(_rootFolderName, "Punk_run", 6, 34), Position, 110);
            _attack1Animation = new Animation(ContentManagers.Instance.LoadSprite(_rootFolderName, "Punk_attack1", 6, 34), Position, 60);
            _attack2Animation = new Animation(ContentManagers.Instance.LoadSprite(_rootFolderName, "Punk_attack2", 6, 34), Position, 60);

            // ? Gun
            _idleGunAnimation = new Animation(ContentManagers.Instance.LoadSprite(_rootFolderName, "Punk_idle_gun", 4, 34), Position, 200);
            _runGunAnimation = new Animation(ContentManagers.Instance.LoadSprite(_rootFolderName, "Punk_run_gun", 6, 34), Position, 110);
            _gunIdle = ContentManagers.Instance.LoadTexture(_rootFolderName, "Punk_gun_idle");
            _gunRun = ContentManagers.Instance.LoadTexture(_rootFolderName, "Punk_gun_run");
        }

        public void LoadContent(ContentManager Content)
        {
        }

        public void UnloadContent()
        {
        }

        public void Update(GameTime gameTime)
        {
            KeyboardState keyboard = Keyboard.GetState();

            if (_unlockedGuns)
            {
                _currentPlayerAnimation = PlayerAnimationName.GunIdle;
            }
            else
            {
                _currentPlayerAnimation = PlayerAnimationName.Idle;
            }

            _currentTime += (float)gameTime.ElapsedGameTime.TotalMilliseconds;

            if (_isPlayerFalling)
                Velocity.Y += _playerFallingSpeed;

            if (_currentTime > _attackDelay && keyboard.IsKeyDown(Keys.Z))
            {
                _currentTime = 0;
                Random rnd = new Random();
                if (_unlockedGuns)
                {
                    _currentPlayerAnimation = PlayerAnimationName.Idle;
                }
                else
                {
                    _currentPlayerAnimation = (rnd.Next(2) == 0) ? PlayerAnimationName.Attack1 : PlayerAnimationName.Attack2;
                }
                _playingPlayerAnimation = _currentPlayerAnimation;
                _isPlayerAnimationDone = false;
            }
            else if (_isPlayerAnimationDone && (keyboard.IsKeyDown(Keys.A) || keyboard.IsKeyDown(Keys.D)))
            {
                Velocity.X += (keyboard.IsKeyDown(Keys.A)) ? -_playerMovementSpeed : _playerMovementSpeed;
                _playerDirection = (keyboard.IsKeyDown(Keys.A)) ? false : true;
                if (_unlockedGuns)
                {
                    _currentPlayerAnimation = PlayerAnimationName.GunRun;
                }
                else
                {
                    _currentPlayerAnimation = PlayerAnimationName.Run;
                }
            }

            if (!_playerJumping && !_isPlayerFalling && keyboard.IsKeyDown(Keys.Space))
            {
                _currentTime = 0;
                _playerJumping = true;
            }

            if (_playerJumping)
            {
                Velocity.Y -= 20;
                if (_currentTime > 100f)
                {
                    _currentTime = 0;
                    _playerJumping = false;
                }
            }

            // ? Movement
            PrevPosition = Position;
            Position = Velocity;
            Hitbox.X = (int)Position.X;
            Hitbox.Y = (int)Position.Y;
            FallingRect.X = (int)Position.X;
            FallingRect.Y = (int)Position.Y+48;

            // ? If the animation doesn't end yet, finish the same animation.
            if (!_isPlayerAnimationDone)
                _currentPlayerAnimation = _playingPlayerAnimation;

            // ? Animaiton Frame Update
            switch (_currentPlayerAnimation)
            {
                case PlayerAnimationName.Idle:
                    _idleAnimation.Update(gameTime);
                    break;
                case PlayerAnimationName.Run:
                    _runAnimation.Update(gameTime);
                    break;
                case PlayerAnimationName.Attack1:
                    _attack1Animation.Update(gameTime);
                    _isPlayerAnimationDone = _attack1Animation.IsDone();
                    break;
                case PlayerAnimationName.Attack2:
                    _attack2Animation.Update(gameTime);
                    _isPlayerAnimationDone = _attack2Animation.IsDone();
                    break;
                case PlayerAnimationName.GunIdle:
                    _idleGunAnimation.Update(gameTime);
                    break;
                case PlayerAnimationName.GunRun:
                    _runGunAnimation.Update(gameTime);
                    break;
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            switch (_currentPlayerAnimation)
            {
                case PlayerAnimationName.Idle:
                    _idleAnimation.Draw(spriteBatch, Position, _playerDirection);
                    break;
                case PlayerAnimationName.Run:
                    _runAnimation.Draw(spriteBatch, Position, _playerDirection);
                    break;
                case PlayerAnimationName.Attack1:
                    _attack1Animation.Draw(spriteBatch, Position, _playerDirection);
                    break;
                case PlayerAnimationName.Attack2:
                    _attack2Animation.Draw(spriteBatch, Position, _playerDirection);
                    break;
                case PlayerAnimationName.GunIdle:
                    _idleGunAnimation.Draw(spriteBatch, Position, _playerDirection);
                    break;
                case PlayerAnimationName.GunRun:
                    _runGunAnimation.Draw(spriteBatch, Position, _playerDirection);
                    break;
            }

            if (_unlockedGuns)
            {
                if (_playerDirection)
                {
                    switch (_currentPlayerAnimation)
                    {
                        case PlayerAnimationName.GunRun:
                            spriteBatch.Draw(_gunRun, new Vector2(Position.X+5, Position.Y+22), Color.White);
                            break;
                        case PlayerAnimationName.GunIdle:
                            spriteBatch.Draw(_gunIdle, new Vector2(Position.X+1, Position.Y+27), Color.White);
                            break;
                    }
                }
                else
                {
                    switch (_currentPlayerAnimation)
                    {
                        case PlayerAnimationName.GunRun:
                            spriteBatch.Draw(_gunRun, new Vector2(Position.X+5, Position.Y+22), null, Color.White, 0f, Vector2.Zero, 1f, SpriteEffects.FlipHorizontally, 0f);
                            break;
                        case PlayerAnimationName.GunIdle:
                            spriteBatch.Draw(_gunIdle, new Vector2(Position.X+1, Position.Y+27), null, Color.White, 0f, Vector2.Zero, 1f, SpriteEffects.FlipHorizontally, 0f);
                            break;
                    }
                }

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
