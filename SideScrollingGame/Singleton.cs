using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework;

namespace _SideScrollingGame
{
    public class Singleton
    {
        // ? System-related
        public int widthScreen = 1920;
        public int heightScreen = 1080;

        public bool isGameExit = false;

        // ? PlayScene
        public bool isGameStart = false;

        // ? Camera
        public float offsetX = 960;
        public float offsetY = 540;

        // ? Singleton Stuff
        private static Singleton instance;
        public static Singleton Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new Singleton();
                }
                return instance;
            }
        }
    }
}