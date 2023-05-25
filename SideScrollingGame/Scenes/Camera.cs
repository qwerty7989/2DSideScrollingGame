using Microsoft.Xna.Framework;

namespace _SideScrollingGame.Scenes
{
    public class Camera
    {
        public Matrix Transform;

        public Matrix Follow(Rectangle target)
        {
            target.X = MathHelper.Clamp(target.X, (int)(Main.screenWidth / 4.0f), (int)((target.X*2)-(Main.screenHeight / 4.0f)));
            Vector3 translation = new Vector3(-target.X - target.Width/4.0f, -target.Y - target.Height/4.0f, 0);
            Vector3 offset = new Vector3(Main.screenWidth/4.0f, Main.screenHeight/4.0f, 0);

            Transform = Matrix.CreateTranslation(translation)*Matrix.CreateTranslation(offset)*Matrix.CreateScale(new Vector3(2.0f, 2.0f, 1));

            return Transform;
        }
    }
}