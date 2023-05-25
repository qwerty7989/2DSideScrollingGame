using Microsoft.Xna.Framework;

namespace _SideScrollingGame.Scenes
{
    public class Camera
    {
        public Matrix Transform;

        public Matrix Follow(Rectangle target)
        {
            target.X = MathHelper.Clamp(target.X, (int)Main.screenWidth / 2, (int)(1000-Main.screenHeight/ 2));
            //target.Y = (int)Main.screenHeight/2;
            Vector3 translation = new Vector3(-target.X - target.Width/2, -target.Y - target.Height/2, 0);

            Vector3 offset = new Vector3(Main.screenWidth/2, Main.screenHeight/ 2, 0);

            Transform = Matrix.CreateTranslation(translation)*Matrix.CreateTranslation(offset);

            return Transform;
        }
    }
}