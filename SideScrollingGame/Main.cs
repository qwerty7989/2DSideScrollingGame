using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Content;

using _SideScrollingGame.Content;
using _SideScrollingGame.Manager;
using _SideScrollingGame.Scenes;
using _SideScrollingGame.Objects;

namespace _SideScrollingGame;

public class Amnesia : Game
{
    private GraphicsDeviceManager _graphics;
    private SpriteBatch _spriteBatch;

    private Camera Camera;
    private Matrix TransformMatrix;

    public Amnesia()
    {
        _graphics = new GraphicsDeviceManager(this);
        Content.RootDirectory = "Content";
        ContentManagers.Instance.contentManagerRoot = Content;
        ContentManagers.Instance.contentManager = new ContentManager(Content.ServiceProvider, Content.RootDirectory);
    }

    protected override void Initialize()
    {
        _graphics.PreferredBackBufferWidth = Singleton.Instance.widthScreen;
        _graphics.PreferredBackBufferHeight = Singleton.Instance.heightScreen;
        _graphics.IsFullScreen = true;
        _graphics.ApplyChanges();

        Camera = new Camera();

        base.Initialize();
    }

    protected override void LoadContent()
    {
        _spriteBatch = new SpriteBatch(GraphicsDevice);

        SceneManager.Instance.LoadContent();

    }

    protected override void Update(GameTime gameTime)
    {
        if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape) || Singleton.Instance.isGameExit)
        {
            Exit();
        }

        SceneManager.Instance.Update(gameTime);
        TransformMatrix = Camera.Follow(Player.Instance.Hitbox);

        base.Update(gameTime);
    }

    protected override void Draw(GameTime gameTime)
    {
        GraphicsDevice.Clear(Color.White);
        if (Singleton.Instance.isGameStart)
            _spriteBatch.Begin(transformMatrix: TransformMatrix);
        else
            _spriteBatch.Begin();

        SceneManager.Instance.Draw(_spriteBatch);

        _spriteBatch.End();
        _graphics.BeginDraw();
        base.Draw(gameTime);
    }
}
