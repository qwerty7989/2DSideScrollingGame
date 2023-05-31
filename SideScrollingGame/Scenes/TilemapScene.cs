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

        private string _rootFolderName = "PlayScene";
        public void LoadContent()
        {
            _map = new TmxMap("Content/PlayScene/PlayMap.tmx");
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
                if (tile.Name == "start")
                {
                    Player.Instance.Position.X = (float)tile.X;
                    Player.Instance.Position.Y = (float)tile.Y;
                }
            }
        }

        public void UnloadContent()
        {
        }

        public void Update(GameTime gameTime)
        {
            Vector2 initPos = Player.Instance.PrevPosition;
            Vector2 nextPos = Player.Instance.Position + Player.Instance.Velocity;

            Rectangle nextRec = new Rectangle((int)nextPos.X, (int)nextPos.Y, Player.Instance.Hitbox.Width, Player.Instance.Hitbox.Height);

            // ? Check X axis
            foreach (Rectangle rect in _collisionRects)
            {
                if (rect.Intersects(nextRec) && !(Player.Instance.Footbox.Y >= rect.Y && Player.Instance.Footbox.Y <= rect.Y+1) && !rect.Intersects(Player.Instance.Headbox))
                {
                    if (Player.Instance.Velocity.X > 0)
                    {
                        if (nextRec.X + nextRec.Width > rect.X && nextRec.X < rect.X)
                        {
                            Player.Instance.Position.X = rect.X - Player.Instance.Hitbox.Width;
                            Player.Instance.Velocity.X = 0;
                            break;
                        }
                    }
                    else if (Player.Instance.Velocity.X < 0)
                    {
                        if (nextRec.X < rect.X + rect.Width && nextRec.X + nextRec.Width > rect.X + rect.Width)
                        {
                            Player.Instance.Position.X = rect.X + rect.Width + 0.21f;
                            Player.Instance.Velocity.X = 0;
                            break;
                        }
                    }
                }
            }

            // ? Check Y axis
            bool onGround = false;
            foreach (Rectangle rect in _collisionRects)
            {
                if (rect.Intersects(Player.Instance.Footbox))
                {
                    onGround = true;
                }

                if (rect.Intersects(nextRec))
                {
                    if (Player.Instance.Velocity.Y < 0)
                    {
                        if (nextRec.Y < rect.Y + rect.Height && nextRec.Y + nextRec.Width > rect.Y + rect.Height)
                        {
                            //Player.Instance.Position.Y = rect.Y + rect.Height;
                            Player.Instance.Velocity.Y = 0;
                        }
                        else if (Player.Instance.Footbox.Y >= rect.Y && Player.Instance.Footbox.Y <= rect.Y)
                        {
                            Player.Instance.Position.Y = rect.Y - Player.Instance.Hitbox.Height;
                            Player.Instance._isPlayerOnGround = true;
                            break;
                        }
                    }
                    if (Player.Instance.Velocity.Y > 0)
                    {
                        if ((nextRec.X >= rect.X && nextRec.X + nextRec.Width <= rect.X + rect.Width) || rect.Intersects(Player.Instance.Footbox))
                        {
                            Player.Instance.Position.Y = rect.Y - Player.Instance.Hitbox.Height;
                            Player.Instance._isPlayerOnGround = true;
                            break;
                        }
                    }
                }
            }

            if (!onGround)
            {
                Player.Instance._isPlayerOnGround = false;
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