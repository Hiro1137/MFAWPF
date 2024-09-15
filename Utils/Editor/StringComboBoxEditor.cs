using System.Windows;
using System.Windows.Data;
using HandyControl.Controls;
using MFAWPF.Utils.Converters;

namespace MFAWPF.Utils.Editor;

public class StringComboBoxEditor : PropertyEditorBase
{
    // 重写编辑器的控件类型
    public override FrameworkElement CreateElement(PropertyItem propertyItem)
    {
        var comboBox = new System.Windows.Controls.ComboBox
        {
            IsEditable = false,
            IsSynchronizedWithCurrentItem = true,
            SelectedItem = propertyItem.Value
        };

        // 动态设置 ItemsSource，根据字段名称定制选项
        comboBox.ItemsSource = GetItemsSource(propertyItem);

        comboBox.SelectionChanged += (_, _) => { propertyItem.Value = comboBox.SelectedItem; };

        return comboBox;
    }

    // 实现抽象方法，返回 ComboBox.SelectedItemProperty 作为绑定的 DependencyProperty
    public override DependencyProperty GetDependencyProperty() => System.Windows.Controls.ComboBox.SelectedItemProperty;


    // 根据属性的字段名称，设置不同的ItemsSource
    private IEnumerable<string> GetItemsSource(PropertyItem propertyItem)
    {
        // 根据字段名称，动态返回选项
        if (propertyItem.PropertyName == "recognition")
        {
            return new List<string>
            {
                "",
                "DirectHit",
                "TemplateMatch",
                "FeatureMatch",
                "ColorMatch",
                "OCR",
                "NeuralNetworkClassify",
                "NeuralNetworkDetect",
                "Custom"
            };
        }

        if (propertyItem.PropertyName == "action")
        {
            return new List<string>
            {
                "",
                "DoNothing",
                "Click",
                "Swipe",
                "Key",
                "Text",
                "StartApp",
                "StopApp",
                "StopTask",
                "Custom"
            };
        }

        if (propertyItem.PropertyName == "order_by")
        {
            return new List<string>
            {
                "",
                "Horizontal",
                "Vertical",
                "Score",
                "Random",
                "Area",
                "Length"
            };
        }

        if (propertyItem.PropertyName == "detector")
        {
            return new List<string>
            {
                "",
                "SIFT",
                "KAZE",
                "AKAZE",
                "BRISK",
                "ORB"
            };
        }

        // 如果没有匹配的字段名称，返回空选项
        return new List<string> { "" };
    }

    // 重写GetConverter来实现IValueConverter
    protected override IValueConverter GetConverter(PropertyItem propertyItem) => new NullStringConverter();
}