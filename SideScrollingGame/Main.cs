using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Content;

using _SideScrollingGame.Content;
using _SideScrollingGame.Manager;
using _SideScrollingGame.Objects;
using _SideScrollingGame.Scenes;

namespace _SideScrollingGame;

public class Main : Game
{
    private GraphicsDeviceManager _graphics;
    private SpriteBatch _spriteBatch;

    public static float screenWidth;
    public static float screenHeight;

    public static bool _playerStart = false;
    public static bool _isPlayerExit = false;

    private Camera Camera;
    private Matrix TransformMatrix;

    public Main()
    {
        _graphics = new GraphicsDeviceManager(this);
        Content.RootDirectory = "Content";
        ContentManagers.Instance.contentManagerRoot = Content;
        ContentManagers.Instance.contentManager = new ContentManager(Content.ServiceProvider, Content.RootDirectory);
        IsMouseVisible = true;
    }

    protected override void Initialize()
    {
        _graphics.PreferredBackBufferWidth = 1920;
        _graphics.PreferredBackBufferHeight = 1080;
        _graphics.IsFullScreen = true;
        _graphics.ApplyChanges();

        screenWidth = _graphics.PreferredBackBufferWidth;
        screenHeight = _graphics.PreferredBackBufferHeight;

        Camera = new Camera();

        base.Initialize();
    }

    protected override void LoadContent()
    {
        _spriteBatch = new SpriteBatch(GraphicsDevice);

        //SceneManager.Instance.LoadContent();

    }

    protected override void Update(GameTime gameTime)
    {
        if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape) || _isPlayerExit)
            Exit();

        SceneManager.Instance.Update(gameTime);
        TransformMatrix = Camera.Follow(Player.Instance.Hitbox);

        base.Update(gameTime);
    }

    protected override void Draw(GameTime gameTime)
    {
        GraphicsDevice.Clear(Color.Black);
        if (_playerStart)
            _spriteBatch.Begin(transformMatrix: TransformMatrix);
        else
            _spriteBatch.Begin();

        SceneManager.Instance.Draw(_spriteBatch);

        _spriteBatch.End();
        _graphics.BeginDraw();
        base.Draw(gameTime);
    }
}
