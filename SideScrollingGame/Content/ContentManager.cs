using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace _SideScrollingGame.Content
{
    public class ContentManagers
    {
        // ? Variable
        public ContentManager contentManagerRoot;
        public ContentManager contentManager;

        // ? Function
        public Texture2D LoadTexture(string root, string path)
        {
            Texture2D tmpTexture2D = contentManager.Load<Texture2D>(root+"/"+path);
            return tmpTexture2D;
        }

        public (Texture2D, Rectangle[]) LoadSprite(string root, string path, int amount, int step)
        {
            Texture2D tmpTexture2D = contentManager.Load<Texture2D>(root+"/"+path);
            int widthStep = tmpTexture2D.Width / amount;
            Rectangle[] spriteRectangles = new Rectangle[amount];
            for (int i = 0; i < amount; i++)
            {
                spriteRectangles[i] = new Rectangle(widthStep * i, 0, step, tmpTexture2D.Height);
            }
            return (tmpTexture2D, spriteRectangles);
        }

        public (Texture2D, Rectangle[]) LoadSprite(string root, string path, int amount)
        {
            Texture2D tmpTexture2D = contentManager.Load<Texture2D>(root+"/"+path);
            int widthStep = tmpTexture2D.Width / amount;
            Rectangle[] spriteRectangles = new Rectangle[amount];
            for (int i = 0; i < amount; i++)
            {
                spriteRectangles[i] = new Rectangle(widthStep * i, 0, widthStep, tmpTexture2D.Height);
            }
            return (tmpTexture2D, spriteRectangles);
        }

        // ? Singleton
        private static ContentManagers instance;
        public static ContentManagers Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new ContentManagers();
                }
                return instance;
            }
        }

    }
}
