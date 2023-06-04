using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Content;

using _SideScrollingGame.Components;
using _SideScrollingGame.Content;
using _SideScrollingGame.Manager;
using _SideScrollingGame.Objects;
using _SideScrollingGame.Scenes;

namespace _SideScrollingGame;

public class Amnesia : Game
{
    private GraphicsDeviceManager _graphics;
    private SpriteBatch _spriteBatch;

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

        base.Update(gameTime);
    }

    protected override void Draw(GameTime gameTime)
    {
        GraphicsDevice.Clear(Color.White);
        _spriteBatch.Begin(samplerState: SamplerState.PointClamp, transformMatrix: Camera.Instance.Transform);

        SceneManager.Instance.Draw(_spriteBatch);

        _spriteBatch.End();
        _graphics.BeginDraw();
        base.Draw(gameTime);
    }
}
