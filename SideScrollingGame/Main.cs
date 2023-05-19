using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Content;

using _SideScrollingGame.Content;
using _SideScrollingGame.Manager;

namespace _SideScrollingGame;

public class Main : Game
{
    private GraphicsDeviceManager _graphics;
    private SpriteBatch _spriteBatch;

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
        _graphics.PreferredBackBufferWidth = 1600;
        _graphics.PreferredBackBufferHeight = 900;
        _graphics.IsFullScreen = false;
        _graphics.ApplyChanges();

        base.Initialize();
    }

    protected override void LoadContent()
    {
        _spriteBatch = new SpriteBatch(GraphicsDevice);

        SceneManager.Instance.LoadContent();

    }

    protected override void Update(GameTime gameTime)
    {
        if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
            Exit();

        SceneManager.Instance.Update(gameTime);

        base.Update(gameTime);
    }

    protected override void Draw(GameTime gameTime)
    {
        GraphicsDevice.Clear(Color.Black);
        _spriteBatch.Begin();

        SceneManager.Instance.Draw(_spriteBatch);

        _spriteBatch.End();
        _graphics.BeginDraw();
        base.Draw(gameTime);
    }
}
