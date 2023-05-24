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
        private Vector2 Position;
        private Vector2 Velocity;

        public Rectangle Hitbox;
        public Rectangle FallingRect;

        private float _playerMovementSpeed = 4;
        private float _playerFallingSpeed = 4;
        private bool _isPlayerFalling = false;
        private bool _playerDirection = true;
        private bool _isPlayerAnimationDone = true;

        // ? Animation
        private enum PlayerAnimationName
        {
            Idle,
            Run,
            Attack1,
            Attack2
        }
        private PlayerAnimationName _currentPlayerAnimation;
        private PlayerAnimationName _playingPlayerAnimation;
        private Animation _idleAnimation;
        private Animation _runAnimation;
        private Animation _runAttackAnimation;
        private Animation _attackAnimation;

        // ? Timer
        private float Timer = 0;
        private float Threshold;

        private string _rootFolderName = "PlayerSprite";
        public Player()
        {
            Position = new Vector2();
            Velocity = new Vector2();

            Hitbox = new Rectangle((int)Position.X, (int)Position.Y, 26, 48);
            FallingRect = new Rectangle((int)Position.X, (int)Position.Y+48, 26, 1);

            // ? Animation
            _idleAnimation = new Animation(ContentManagers.Instance.LoadSprite(_rootFolderName, "Punk_idle", 4, 48), Position);
            _runAnimation = new Animation(ContentManagers.Instance.LoadSprite(_rootFolderName, "Punk_run", 6, 48), Position, 100);
            _attackAnimation = new Animation(ContentManagers.Instance.LoadSprite(_rootFolderName, "Punk_attack1", 6, 48), Position, 30);
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

            _currentPlayerAnimation = PlayerAnimationName.Idle;

            if (_isPlayerFalling)
                Velocity.Y += _playerFallingSpeed;

            if (keyboard.IsKeyDown(Keys.Z))
            {
                _currentPlayerAnimation = PlayerAnimationName.Attack1;
                _playingPlayerAnimation = _currentPlayerAnimation;
                _isPlayerAnimationDone = false;
            }
            else if (_isPlayerAnimationDone && (keyboard.IsKeyDown(Keys.A) || keyboard.IsKeyDown(Keys.D)))
            {
                Velocity.X += (keyboard.IsKeyDown(Keys.A)) ? -_playerMovementSpeed : _playerMovementSpeed;
                _playerDirection = (keyboard.IsKeyDown(Keys.A)) ? false : true;
                _currentPlayerAnimation = PlayerAnimationName.Run;
            }

            // ? Movement
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
                    _attackAnimation.Update(gameTime);
                    _isPlayerAnimationDone = _attackAnimation.IsDone();
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
                    _attackAnimation.Draw(spriteBatch, Position, _playerDirection);
                    break;
            }
        }
    }
}
