using System.Collections.ObjectModel;
using Codefarts.Input.Models;

namespace Codefarts.Input;

public class BindingsCollection : ObservableCollection<BindingData>
{
    private InputManager inputManager;

    internal BindingsCollection(InputManager inputManager)
    {
        this.inputManager = inputManager;
    }

    protected override void InsertItem(int index, BindingData item)
    {
        if (!this.inputManager.InputSources.Any(x => x == item.InputSource))
        {
            throw new ArgumentException($"InputSource '{item.InputSource.Name}' not found.");
        }

        base.InsertItem(index, item);
    }
}