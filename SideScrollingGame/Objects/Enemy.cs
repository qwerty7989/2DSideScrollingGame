using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

using _SideScrollingGame.Content;
using System;

namespace _SideScrollingGame.Objects
{
    public class Enemy : GameObject
    {
        private Texture2D Texture;
        public Vector2 Position;
        public Vector2 Velocity;

        public Rectangle Hitbox;
        public Rectangle FallingRect;

        private string _rootFolderName = "EnemySprite";
        public Enemy()
        {
        }

        public void LoadContent(ContentManager Content)
        {
        }

        public void UnloadContent()
        {
        }

        public void Update(GameTime gameTime)
        {
        }

        public void Draw(SpriteBatch spriteBatch)
        {
        }

        // ? Singleton
        private static Enemy instance;
        public static Enemy Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new Enemy();
                }
                return instance;
            }
        }
    }
}
