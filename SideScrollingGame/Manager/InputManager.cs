using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

using _SideScrollingGame.Components;

namespace _SideScrollingGame.Manager
{
    public class InputManager
    {
        private KeyboardState _keyState;
        private KeyboardState _lastKeyState;
        private Keys _lastKey;

        public Input inputState;

        public InputManager()
        {
            inputState = Input.None;
        }

        public void Update()
        {
            _keyState = Keyboard.GetState();
            if (_keyState.IsKeyUp(_lastKey) && _lastKey != Keys.None)
            {
                inputState = Input.None;
            }
            MappingKeyboardState(Keys.Left, Input.Left);
            MappingKeyboardState(Keys.Right, Input.Right);
            MappingKeyboardState(Keys.Up, Input.Up);
            MappingKeyboardState(Keys.Down, Input.Down);
            MappingKeyboardState(Keys.Z, Input.Attack);
            MappingKeyboardState(Keys.C, Input.Skill);
            MappingKeyboardState(Keys.X, Input.Dash);
            MappingKeyboardState(Keys.Space, Input.Jump);
            MappingKeyboardState(Keys.Enter, Input.Enter);

            _lastKeyState = _keyState;
        }

        public void MappingKeyboardState(Keys key, Input input)
        {
            if (_keyState.IsKeyDown(key))
            {
                inputState = input;
                _lastKey = key;
            }

        }

        // ? Singleton
        private static InputManager instance;
        public static InputManager Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new InputManager();
                }
                return instance;
            }
        }

    }
}
