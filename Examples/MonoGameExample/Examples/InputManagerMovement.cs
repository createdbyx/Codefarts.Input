using System;
using System.Linq;
using Codefarts.Input;
using Codefarts.Input.Models;
using Codefarts.Input.MonoGameSources;
using Codefarts.MonoGame.SimpleMenuComponent;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MonoGameExample;

public class InputManagerMovement : DrawableGameComponent
{
    private Texture2D texture;
    private SpriteBatch spriteBatch;
    private Vector2 position = Vector2.One * 100;
    private float rotation = MathHelper.ToRadians(0);
    private float maxRotationSpeed = 300f;
    private float maxMovementSpeed = 400f;
    private InputManager inputManager;
    private BindingCallbacksManager callbacksManager;
    private float rotationDelta;
    private float speedDelta;

    public InputManagerMovement(Game game) : base(game)
    {
    }

    public override void Initialize()
    {
        this.spriteBatch = new SpriteBatch(this.Game.GraphicsDevice);
        inputManager = new InputManager();
        var kbSource = new KeyboardSource();
        var gpSource = new GamePadSource();

        inputManager.InputSources.Add(kbSource);
        inputManager.InputSources.Add(gpSource);
        inputManager.Bind("MoveForward", kbSource, "W");
        inputManager.Bind("MoveBackward", kbSource, "S");
        inputManager.Bind("MoveForward", gpSource, "RightTrigger");
        inputManager.Bind("MoveBackward", gpSource, "LeftTrigger");
        inputManager.Bind("TurnLeft", kbSource, "A");
        inputManager.Bind("TurnRight", kbSource, "D");
        inputManager.Bind("Turn", gpSource, "LeftThumbStickX");
        inputManager.Bind("Quit", kbSource, "Escape");
        inputManager.Bind("Quit", gpSource, "Back");

        callbacksManager = new BindingCallbacksManager(inputManager);
        callbacksManager.Bind("MoveForward", this.MoveForward);
        callbacksManager.Bind("MoveBackward", this.MoveBackward);
        callbacksManager.Bind("TurnLeft", this.TurnLeft);
        callbacksManager.Bind("TurnRight", this.TurnRight);
        callbacksManager.Bind("Turn", this.DoTurn);
        callbacksManager.BindButtonRelease("Quit", this.QuitGame);

        base.Initialize();
    }

    private void QuitGame(BindingData bindingData)
    {
        this.Exit();
    }

    private void MoveForward(BindingData bindingData)
    {
        this.speedDelta += -bindingData.RelativeValue;
    }

    private void MoveBackward(BindingData bindingData)
    {
        this.speedDelta += bindingData.RelativeValue;
    }

    private void DoTurn(BindingData bindingData)
    {
        this.rotationDelta += MathHelper.ToRadians(-bindingData.RelativeValue);
    }

    private void TurnLeft(BindingData bindingData)
    {
        this.rotationDelta += MathHelper.ToRadians(bindingData.RelativeValue);
    }

    private void TurnRight(BindingData bindingData)
    {
        this.rotationDelta += MathHelper.ToRadians(-bindingData.RelativeValue);
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
        this.spriteBatch.Draw(this.texture, this.position, sourceRectangle, Color.White, -this.rotation, half, Vector2.One, SpriteEffects.None, 0);
        this.spriteBatch.End();
    }

    public override void Update(GameTime gameTime)
    {
        inputManager.Update(gameTime.TotalGameTime, gameTime.ElapsedGameTime);

        var rotationAmmount = Math.Clamp(this.rotationDelta * this.maxRotationSpeed, -this.maxRotationSpeed, this.maxRotationSpeed);
        this.rotation += (float)(rotationAmmount * gameTime.ElapsedGameTime.TotalSeconds);

        var directionVector = new Vector2(MathF.Sin(this.rotation), MathF.Cos(this.rotation));
        directionVector.Normalize();

        var movementSpeed = Math.Clamp(this.speedDelta * this.maxMovementSpeed, -this.maxMovementSpeed, this.maxMovementSpeed);
        var distanceVector = directionVector * (float)(movementSpeed * gameTime.ElapsedGameTime.TotalSeconds);
        this.position += distanceVector;
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