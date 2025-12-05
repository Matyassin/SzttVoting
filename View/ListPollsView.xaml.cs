using Services;
using ViewModel;

namespace View;

public partial class ListPollsView : ContentPage
{
    private readonly ListPollsViewModel _vm;

    public ListPollsView(PollServices pollServices)
    {
        InitializeComponent();
        _vm = new ListPollsViewModel(pollServices);
        BindingContext = _vm;
    }
}
