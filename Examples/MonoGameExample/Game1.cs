using System.Windows.Forms.VisualStyles;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace MonoGameExample;

public class Game1 : Game
{
    private GraphicsDeviceManager _graphics;

    public Game1()
    {
        _graphics = new GraphicsDeviceManager(this);
        Content.RootDirectory = "Content";
        IsMouseVisible = true;
        this.Components.Add(new InputManagerMovement(this));
        _graphics.PreferredBackBufferWidth = 1080;  // set this value to the desired width of your window
        _graphics.PreferredBackBufferHeight = 720;   // set this value to the desired height of your window
        _graphics.ApplyChanges();
    }
 
    protected override void Draw(GameTime gameTime)
    {
        GraphicsDevice.Clear(Color.CornflowerBlue);

        // TODO: Add your drawing code here

        base.Draw(gameTime);
    }
}