using Codefarts.Input;
using Codefarts.Input.MonoGameSources;
using Codefarts.MonoGame.SimpleMenuComponent;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace MonoGameExample;

public class SimpleMenuController : GameComponent
{
    public SimpleMenuComponent Menu { get; set; }
    private InputManager input;
    private BindingCallbacksManager callbacks;

    public SimpleMenuController(Game game, SimpleMenuComponent menu) : base(game)
    {
        this.Menu = menu;
        this.input = new InputManager();
        this.input.AddSource(new KeyboardSource());
        this.input.Bind("Select", "Keyboard", Keys.Enter.ToString());
        this.input.Bind("Next", "Keyboard", Keys.Down.ToString());
        this.input.Bind("Prev", "Keyboard", Keys.Up.ToString());

        this.callbacks = new BindingCallbacksManager(this.input);
        this.callbacks.Bind("Next", b =>
        {
            if (b.RelativeValue != -1)
            {
                return;
            }

            menu.SelectedIndex++;
        });

        this.callbacks.Bind("Prev", b =>
        {
            if (b.RelativeValue != -1)
            {
                return;
            }

            menu.SelectedIndex--;
        });

        this.callbacks.Bind("Select", b =>
        {
            if (b.RelativeValue != -1)
            {
                return;
            }

            switch (menu.SelectedIndex)
            {
                case 0:
                    menu.Enabled = false;
                    menu.Visible = false;
                    this.Enabled = false;

                    this.Game.Components.Add(new StandardInputMovement(game));
                    break;

                case 1:
                    menu.Enabled = false;
                    menu.Visible = false;
                    this.Enabled = false;

                    this.Game.Components.Add(new InputManagerMovement(game));
                    break;

                case 2:
                    game.Exit();
                    break;
            }
        });
    }

    public override void Update(GameTime gameTime)
    {
        this.input.Update(gameTime.TotalGameTime, gameTime.ElapsedGameTime);
    }
}