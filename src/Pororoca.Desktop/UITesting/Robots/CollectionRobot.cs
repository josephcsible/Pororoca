using Avalonia.Controls;
using Pororoca.Desktop.Controls;
using Pororoca.Desktop.Views;

namespace Pororoca.Desktop.UITesting.Robots;

public sealed class CollectionRobot : BaseNamedRobot
{
    public CollectionRobot(CollectionView rootView) : base(rootView) { }

    internal IconButton AddFolder => GetChildView<IconButton>("btAddFolder")!;
    internal IconButton AddHttpReq => GetChildView<IconButton>("btAddHttpReq")!;
    internal IconButton AddWebSocket => GetChildView<IconButton>("btAddWebSocket")!;
    internal IconButton AddEnvironment => GetChildView<IconButton>("btAddEnvironment")!;
    internal IconButton ImportEnvironment => GetChildView<IconButton>("btImportEnv")!;
    internal IconButton ExportCollection => GetChildView<IconButton>("btExportCollection")!;
    internal IconButton ExportAsPororocaCollection => GetChildView<IconButton>("btExportAsPororocaCollection")!;
    internal IconButton ExportAsPostmanCollection => GetChildView<IconButton>("btExportAsPostmanCollection")!;
    internal CheckBox IncludeSecretsWhenExporting => GetChildView<CheckBox>("cbIncludeSecretsWhenExporting")!;
}