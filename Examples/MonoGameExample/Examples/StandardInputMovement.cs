using System;
using System.Linq;
using Codefarts.MonoGame.SimpleMenuComponent;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace MonoGameExample;

public class StandardInputMovement : DrawableGameComponent
{
    private Texture2D texture;
    private SpriteBatch spriteBatch;
    private Vector2 position = Vector2.One * 100;
    private float rotation = MathHelper.ToRadians(0);
    private float rotationSpeed = 0.25f;
    private float movementSpeed = 0.5f;

    public StandardInputMovement(Game game) : base(game)
    {
    }

    public override void Initialize()
    {
        this.spriteBatch = new SpriteBatch(this.Game.GraphicsDevice);
        base.Initialize();
    }

    protected override void LoadContent()
    {
        this.texture = this.Game.Content.Load<Texture2D>("Plane");
        base.LoadContent();
    }

    public override void Draw(GameTime gameTime)
    {
        var sourceRectangle = new Rectangle(0, 0, this.texture.Width, this.texture.Height);
        var half = new Vector2(this.texture.Width / 2, this.texture.Height / 2);

        this.spriteBatch.Begin();
        this.spriteBatch.Draw(this.texture, this.position, sourceRectangle, Color.White, -this.rotation, half, Vector2.One,
                              SpriteEffects.None, 0);
        this.spriteBatch.End();
    }

    public override void Update(GameTime gameTime)
    {
        var keyboardState = Keyboard.GetState();

        float rotationDelta = MathHelper.ToRadians(this.rotationSpeed) * gameTime.ElapsedGameTime.Milliseconds;
        this.rotation -= (keyboardState.IsKeyDown(Keys.D) ? rotationDelta : 0);
        this.rotation += (keyboardState.IsKeyDown(Keys.A) ? rotationDelta : 0);

        var directionVector = new Vector2(MathF.Sin(this.rotation), MathF.Cos(this.rotation));
        directionVector.Normalize();
        var directionDeta = (directionVector * this.movementSpeed) * gameTime.ElapsedGameTime.Milliseconds;
        this.position = Vector2.Subtract(this.position, (keyboardState.IsKeyDown(Keys.W) ? directionDeta : Vector2.Zero));
        this.position = Vector2.Add(this.position, (keyboardState.IsKeyDown(Keys.S) ? directionDeta : Vector2.Zero));

        if (keyboardState.IsKeyDown(Keys.Escape))
        {
            this.Exit();
        }
    }

    private void Exit()
    {
        this.Game.Components.Remove(this);
        var menu = Game.Components.OfType<SimpleMenuComponent>().FirstOrDefault();
        var controller = Game.Components.OfType<SimpleMenuController>().FirstOrDefault();
        controller.Enabled = true;
        menu.Enabled = true;
        menu.Visible = true;
    }
}