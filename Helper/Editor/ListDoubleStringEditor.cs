using System.Windows.Data;
using HandyControl.Controls;
using MFAWPF.Helper.Converters;

namespace MFAWPF.Helper.Editor;

public class ListDoubleStringEditor: ListStringEditor
{
    protected override IValueConverter GetConverter(PropertyItem propertyItem) => new ListDoubleStringConverter();
}