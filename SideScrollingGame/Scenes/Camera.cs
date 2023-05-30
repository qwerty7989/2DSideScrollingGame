using Microsoft.Xna.Framework;

namespace _SideScrollingGame.Scenes
{
    public class Camera
    {
        public Matrix Transform;

        public Matrix Follow(Rectangle target)
        {
            target.X = MathHelper.Clamp(target.X, (int)(Singleton.Instance.widthScreen / 2.0f), (int)((target.X*2)-(Singleton.Instance.heightScreen / 2.0f)));
            Vector3 translation = new Vector3(-target.X - target.Width/2.0f, -target.Y - target.Height/2.0f, 0);
            Vector3 offset = new Vector3(Singleton.Instance.offsetX, Singleton.Instance.offsetY, 0);

            Transform = Matrix.CreateTranslation(translation)*Matrix.CreateTranslation(offset);

            return Transform;
        }
    }
}