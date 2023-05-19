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
