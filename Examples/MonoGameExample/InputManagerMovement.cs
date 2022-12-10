﻿using System;
using System.Diagnostics;
using Codefarts.Input;
using Codefarts.Input.Models;
using Codefarts.Input.MonoGameSources;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MonoGameExample;

public class InputManagerMovement : DrawableGameComponent
{
    private Texture2D texture;
    private SpriteBatch spriteBatch;
    private Vector2 position = Vector2.One * 100;
    private float rotation = MathHelper.ToRadians(0);
    private float rotationSpeed = 0.05f;
    private float movementSpeed = 0.1f;
    private InputManager inputManager;
    private BindingCallbacksManager callbacksManager;
    private float rotationDelta;
    private float speed;

    public InputManagerMovement(Game game) : base(game)
    {
    }

    public override void Initialize()
    {
        this.spriteBatch = new SpriteBatch(this.Game.GraphicsDevice);
        inputManager = new InputManager();
        var inputSource = new KeyboardSource();

        inputManager.AddSource(inputSource);
        inputManager.Bind("MoveForward", "Keyboard", "W");
        inputManager.Bind("MoveBackward", "Keyboard", "S");
        inputManager.Bind("TurnLeft", "Keyboard", "A");
        inputManager.Bind("TurnRight", "Keyboard", "D");
        inputManager.Bind("Quit", "Keyboard", "Q");                         

        callbacksManager = new BindingCallbacksManager(inputManager);
        callbacksManager.Bind("MoveForward", this.MoveForward);
        callbacksManager.Bind("MoveBackward", this.MoveBackward);
        callbacksManager.Bind("TurnLeft", this.TurnLeft);
        callbacksManager.Bind("TurnRight", this.TurnRight);
        callbacksManager.Bind("Quit", this.QuitGame);

        base.Initialize();
    }

    private void QuitGame(BindingData bindingData)
    {
        if (bindingData.RelativeValue == -1)
        {
            this.Game.Exit();
        }
    }

    private void MoveForward(BindingData bindingData)
    {
        var directionVector = new Vector2(MathF.Sin(this.rotation), MathF.Cos(this.rotation));
        directionVector.Normalize();
        this.speed = bindingData.Value * this.movementSpeed;
        var directionDeta = (directionVector * this.speed) * bindingData.ElapsedTime.Milliseconds;
        this.position = Vector2.Subtract(this.position, directionDeta);
    }

    private void MoveBackward(BindingData bindingData)
    {
        var directionVector = new Vector2(MathF.Sin(this.rotation), MathF.Cos(this.rotation));
        directionVector.Normalize();
        this.speed = bindingData.Value * this.movementSpeed;
        var directionDeta = (directionVector * this.speed) * bindingData.ElapsedTime.Milliseconds;
        this.position = Vector2.Add(this.position, directionDeta);
    }

    private void TurnLeft(BindingData bindingData)
    {
        var speed = bindingData.Value * this.rotationSpeed;
        this.rotationDelta = MathHelper.ToRadians(speed) * bindingData.ElapsedTime.Milliseconds;
        this.rotation += this.rotationDelta;
    }

    private void TurnRight(BindingData bindingData)
    {
        var speed = bindingData.Value * this.rotationSpeed;
        this.rotationDelta = MathHelper.ToRadians(speed) * bindingData.ElapsedTime.Milliseconds;
        this.rotation -= rotationDelta;
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
        inputManager.Update(gameTime.TotalGameTime, gameTime.ElapsedGameTime);
    }
}