using Microsoft.Xna.Framework;

namespace _SideScrollingGame.Classes
{
    public class Camera
    {
        public Matrix Transform;

        public void Follow(Rectangle target)
        {
            target.X = MathHelper.Clamp(target.X, (int)(Singleton.Instance.widthScreen / 2.0f), (int)((target.X*2)-(Singleton.Instance.heightScreen / 2.0f)));
            Vector3 position = new Vector3(target.X + target.Width/2.0f, target.Y + target.Height/2.0f, 0);
            Vector3 offset = new Vector3(Singleton.Instance.offsetX, Singleton.Instance.offsetY, 0);

            Transform = Matrix.CreateTranslation(position)*Matrix.CreateTranslation(offset);
        }

        public void Follow(Rectangle target, float offsetX, float offsetY)
        {
            target.X = MathHelper.Clamp(target.X, (int)(Singleton.Instance.widthScreen / 2.0f), (int)((target.X*2)-(Singleton.Instance.heightScreen / 2.0f)));
            Vector3 position = new Vector3(target.X + target.Width/2.0f, target.Y + target.Height/2.0f, 0);
            Vector3 offset = new Vector3(offsetX, offsetY, 0);

            Transform = Matrix.CreateTranslation(position)*Matrix.CreateTranslation(offset);
        }

        public void Follow(Rectangle target, float offsetX, float offsetY, float limitLeft, float limitRight)
        {
            target.X = MathHelper.Clamp(target.X, (int)(limitLeft), (int)(limitRight));
            Vector3 position = new Vector3(target.X + target.Width/2.0f, target.Y + target.Height/2.0f, 0);
            Vector3 offset = new Vector3(offsetX, offsetY, 0);

            Transform = Matrix.CreateTranslation(position)*Matrix.CreateTranslation(offset);
        }

        public void Follow(Rectangle target, float offsetX, float offsetY, float limitLeft, float limitRight, float limitBottom, float limitTop)
        {
            target.X = MathHelper.Clamp(target.X, (int)(limitLeft), (int)(limitRight));
            target.Y = MathHelper.Clamp(target.Y, (int)(limitBottom), (int)(limitTop));
            Vector3 position = new Vector3(target.X + target.Width/2.0f, target.Y + target.Height/2.0f, 0);
            Vector3 offset = new Vector3(offsetX, offsetY, 0);

            Transform = Matrix.CreateTranslation(-position)*Matrix.CreateTranslation(offset);
        }

        public void Follow(Rectangle target, float offsetX, float offsetY, float limitLeft, float limitRight, float limitBottom, float limitTop, float zoomX, float zoomY)
        {
            target.X = MathHelper.Clamp(target.X, (int)(limitLeft), (int)(limitRight));
            target.Y = MathHelper.Clamp(target.Y, (int)(limitBottom), (int)(limitTop));
            Vector3 position = new Vector3(target.X + target.Width/2.0f, target.Y + target.Height/2.0f, 0);
            Vector3 offset = new Vector3(offsetX, offsetY, 0);

            Transform = Matrix.CreateTranslation(-position)*Matrix.CreateTranslation(offset)*Matrix.CreateScale(new Vector3(2f, 2f, 1f));
        }

        // ? Singleton
        private static Camera instance;
        public static Camera Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new Camera();
                }
                return instance;
            }
        }
    }
}