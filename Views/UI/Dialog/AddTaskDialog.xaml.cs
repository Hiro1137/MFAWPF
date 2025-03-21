using System.Collections.ObjectModel;
using System.Windows;
using HandyControl.Data;
using MFAWPF.Extensions;
using MFAWPF.Helper;
using MFAWPF.ViewModels;
using MFAWPF.ViewModels.UI;
using MFAWPF.ViewModels.UI.Dialog;
using MFAWPF.Views.UI;
using DragItemViewModel = MFAWPF.ViewModels.Tool.DragItemViewModel;

namespace MFAWPF.Views.UI.Dialog;

public partial class AddTaskDialog
{

    public AddTaskDialogViewModel?
        Data;

    public DragItemViewModel? OutputContent
    {
        get;
        set;
    }

    private readonly ObservableCollection<DragItemViewModel> _source = new();

    public AddTaskDialog(IList<DragItemViewModel>? dragItemViewModels)
    {
        InitializeComponent();
        Data = DataContext as AddTaskDialogViewModel;
        _source.AddRange(dragItemViewModels);
        if (Data != null)
        {
            Data.DataList.Clear();
            Data.DataList.AddRange(_source);
        }
    }

    private void Add(object sender, RoutedEventArgs e)
    {
        DialogResult = true;
        OutputContent = ListBoxDemo.SelectedValue as DragItemViewModel;
        Close();
    }

    protected override void OnClosed(EventArgs e)
    {
        Instances.RootViewModel.SetIdle(true);
        base.OnClosed(e);
    }

    private void SearchBar_OnSearchStarted(object sender, FunctionEventArgs<string> e)
    {
        string key = e.Info;

        if (string.IsNullOrEmpty(key))
        {
            if (Data != null)
            {
                Data.DataList.Clear();
                Data.DataList.AddRange(_source);
            }
        }
        else
        {
            key = key.ToLower();
            Data?.DataList.Clear();
            foreach (DragItemViewModel item in _source)
            {
                string name = item.Name.ToLower();
                if (name.Contains(key))
                    Data?.DataList.Add(item);
            }
        }
    }
}