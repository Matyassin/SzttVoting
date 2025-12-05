using Services;
using ViewModel;

namespace View;

public partial class ListPollsView : ContentPage
{
    private readonly ListPollsViewModel _vm;

    public ListPollsView(UserServices userServices, PollServices pollServices)
    {
        InitializeComponent();
        _vm = new ListPollsViewModel(userServices, pollServices);
        BindingContext = _vm;
    }
}
