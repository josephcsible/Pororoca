using Avalonia.Controls;
using Avalonia.Controls.Templates;
using Pororoca.Desktop.ViewModels;

namespace Pororoca.Desktop;

public class ViewLocator : IDataTemplate
{
    public Control? Build(object? data)
    {
        string? name = data?.GetType()?.FullName?.Replace("ViewModel", "View");

        // Unrecognized value passed to the parameter of method. It's not possible to guarantee the availability of the target type.
        #pragma warning disable IL2057
        var type = name != null ? Type.GetType(name) : null;
        #pragma warning restore IL2057

        if (type != null)
        {
            return (Control)Activator.CreateInstance(type)!;
        }
        else
        {
            return new TextBlock { Text = "Not Found: " + name };
        }
    }

    public bool Match(object? data) =>
        data is ViewModelBase;
}