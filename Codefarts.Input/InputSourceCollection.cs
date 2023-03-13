using System.Collections.ObjectModel;
using Codefarts.Input.Interfaces;

namespace Codefarts.Input;

public class InputSourceCollection : ObservableCollection<IInputSource>
{
    private InputManager inputManager;

    internal InputSourceCollection(InputManager inputManager)
    {
        this.inputManager = inputManager;
    }

    protected override void InsertItem(int index, IInputSource item)
    {
        if (item == null)
        {
            throw new ArgumentNullException(nameof(item));
        }

        if (string.IsNullOrWhiteSpace(item.Name))
        {
            throw new ArgumentException("Input source name is null or missing.", nameof(item));
        }

        foreach (var inputSource in this)
        {
            if (inputSource.Name.Equals(item.Name, StringComparison.InvariantCulture))
            {
                throw new ArgumentException("Input source with same name already added.");
            }
        }

        base.InsertItem(index, item);
    }

    protected override void RemoveItem(int index)
    {
        var item = this[index];
        if (!this.inputManager.Bindings.Any(x => x.InputSource == item))
        {
            throw new ArgumentException($"Can not remove input source '{item.Name}' because there are bindings in the Bondings collection that reference it.");
        }

        base.RemoveItem(index);
    }

    /// <inheritdoc/>
    protected override void ClearItems()
    {
        foreach (var item in this)
        {
            if (!this.inputManager.Bindings.Any(x => x.InputSource == item))
            {
                throw new  ArgumentException($"Can not clear input sources because there are bindings in the Bindings collection that reference '{item.Name}'.");
            }
        }        
        
        base.ClearItems();
    }
}