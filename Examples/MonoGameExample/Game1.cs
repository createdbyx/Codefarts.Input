using Microsoft.Xna.Framework;
using Codefarts.MonoGame.SimpleMenuComponent;

namespace MonoGameExample;

public class Game1 : Game
{
    private GraphicsDeviceManager _graphics;

    public Game1()
    {
        this._graphics = new GraphicsDeviceManager(this);
        this.Content.RootDirectory = "Content";
        this.IsMouseVisible = true;
        this._graphics.PreferredBackBufferWidth = 1080; // set this value to the desired width of your window
        this._graphics.PreferredBackBufferHeight = 720; // set this value to the desired height of your window
        this._graphics.ApplyChanges();
    }

    protected override void Initialize()
    {
        var menu = new SimpleMenuComponent(this) { FontAsset = "CommonFont" };
        menu.Entries.Add(nameof(StandardInputMovement));
        menu.Entries.Add(nameof(InputManagerMovement));
        menu.Entries.Add("Quit");
        this.Components.Add(menu);

        var menuController = new SimpleMenuController(this, menu);
        this.Components.Add(menuController);

        base.Initialize();
    }

    protected override void Draw(GameTime gameTime)
    {
        this.GraphicsDevice.Clear(Color.CornflowerBlue);

        // TODO: Add your drawing code here

        base.Draw(gameTime);
    }
}