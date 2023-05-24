using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using TiledSharp;

using _SideScrollingGame.Content;
using _SideScrollingGame.Objects;

namespace _SideScrollingGame.Scenes
{
    public class TilemapScene : GameScene
    {
        TmxMap _map;
        Texture2D _tileset;
        private int _tilesetTilesWidth;
        private int _tileWidth;
        private int _tileHeight;

        private List<Rectangle> _collisionRects;

        private string _rootFolderName = "TilemapScene";
        public void LoadContent()
        {
            _map = new TmxMap("Content/Map.tmx");
            _tileset = ContentManagers.Instance.LoadTexture(_rootFolderName, _map.Tilesets[0].Name.ToString());
            _tileWidth = _map.Tilesets[0].TileWidth;
            _tileHeight = _map.Tilesets[0].TileHeight;
            _tilesetTilesWidth = _tileset.Width / _tileWidth;

            _collisionRects = new List<Rectangle>();
            foreach (TmxObject tile in _map.ObjectGroups["Collisions"].Objects)
            {
                if (tile.Name == "")
                {
                    _collisionRects.Add(new Rectangle((int)tile.X, (int)tile.Y, (int)tile.Width, (int)tile.Height));
                }
            }
        }

        public void UnloadContent()
        {
        }

        public void Update(GameTime gameTime)
        {
            Vector2 initPos = Player.Instance.PrevPosition;

            // ? Check X axis
            foreach (Rectangle rect in _collisionRects)
            {
                if (rect.Intersects(Player.Instance.Hitbox) && !rect.Intersects(Player.Instance.FallingRect))
                {
                    Console.Write("HIT");
                    Player.Instance.Position.X = initPos.X;
                    Player.Instance.Velocity.X = initPos.X;
                    break;
                }
            }

            // ? Check Y axis
            foreach (Rectangle rect in _collisionRects)
            {
                Player.Instance._isPlayerFalling = true;
                if (rect.Intersects(Player.Instance.FallingRect))
                {
                    Player.Instance._isPlayerFalling = false;
                    Player.Instance.Position.Y = rect.Y - Player.Instance.Hitbox.Height;
                    Player.Instance.Velocity.Y = rect.Y - Player.Instance.Hitbox.Height;
                    break;
                }
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            for (int i = 0; i < _map.Layers.Count; i++)
            {
                for (int j = 0; j < _map.Layers[i].Tiles.Count; j++)
                {
                    int gid = _map.Layers[i].Tiles[j].Gid;
                    if (gid != 0)
                    {
                        int tileFrame = gid - 1;
                        int column = tileFrame % _tilesetTilesWidth;
                        int row = (int)Math.Floor((double)tileFrame / (double)_tilesetTilesWidth);
                        float x = (j % _map.Width) * _map.TileWidth;
                        float y = (float)Math.Floor(j / (double)_map.Width) * _map.TileHeight;
                        Rectangle tilesetRec = new Rectangle((_tileWidth) * column, (_tileHeight) * row, _tileWidth, _tileHeight);
                        spriteBatch.Draw(_tileset, new Rectangle((int)x, (int)y, _tileWidth, _tileHeight), tilesetRec, Color.White);
                    }
                }
            }
        }
    }
}