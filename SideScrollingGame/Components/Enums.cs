using Microsoft.Xna.Framework;

namespace _SideScrollingGame.Components
{
    public enum Input
    {
        Left,
        Right,
        Up,
        Down,
        Jump,
        Dash,
        Attack,
        Skill,
        Enter,
        None
    }

    public enum PlayerState
    {
        Standing,
        Falling,
    }
}