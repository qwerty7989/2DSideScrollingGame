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
        Texture2D[] _tileset;
        private int[] _tilesetTilesWidth;
        private int[] _tileWidth;
        private int[] _tileHeight;

        public List<Rectangle> _collisionRects;

        private string _rootFolderName = "PlayScene";
        public void LoadContent()
        {
            _map = new TmxMap("Content/PlayScene/Map.tmx");
            _tileset = new Texture2D[_map.Tilesets.Count];
            _tilesetTilesWidth = new int[_map.Tilesets.Count];
            _tileWidth = new int[_map.Tilesets.Count];
            _tileHeight = new int[_map.Tilesets.Count];
            for (int i = 0;i < _map.Tilesets.Count; i++)
            {
                System.Console.WriteLine(_map.Tilesets[i].Name.ToString());
                _tileset[i] = ContentManagers.Instance.LoadTexture(_rootFolderName, _map.Tilesets[i].Name.ToString());
                _tileWidth[i] = _map.Tilesets[i].TileWidth;
                _tileHeight[i] = _map.Tilesets[i].TileHeight;
                _tilesetTilesWidth[i] = _tileset[i].Width / _tileWidth[i];
            }

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

            Singleton.Instance.TileMapCollisionRects = _collisionRects;
        }

        public void UnloadContent()
        {
        }

        public void Update(GameTime gameTime)
        {
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
                        int indexTileset = 0;
                        if (gid >= _map.Tilesets[1].FirstGid) indexTileset = 1;
                        else if (gid >= _map.Tilesets[2].FirstGid) indexTileset = 2;
                        int tileFrame = gid - 1;
                        int column = tileFrame % _tilesetTilesWidth[indexTileset];
                        int row = (int)Math.Floor((double)tileFrame / (double)_tilesetTilesWidth[indexTileset]);
                        float x = (j % _map.Width) * _map.TileWidth;
                        float y = (float)Math.Floor(j / (double)_map.Width) * _map.TileHeight;
                        Rectangle tilesetRec = new Rectangle((_tileWidth[indexTileset]) * column, (_tileHeight[indexTileset]) * row, _tileWidth[indexTileset], _tileHeight[indexTileset]);
                        spriteBatch.Draw(_tileset[indexTileset], new Rectangle((int)x, (int)y, _tileWidth[indexTileset], _tileHeight[indexTileset]), tilesetRec, Color.White);
                    }
                }
            }
        }
    }
}