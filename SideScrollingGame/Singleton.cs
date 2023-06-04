using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework;
using System.Collections.Generic;

namespace _SideScrollingGame
{
    public class Singleton
    {
        // ? System-related
        public int widthScreen = 1920;
        public int heightScreen = 1080;
        public bool isGameExit = false;

        // ? Camera
        public float offsetX = 960;
        public float offsetY = 540;

        // ? Tilemap Collision
        public List<Rectangle> TileMapCollisionRects;

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