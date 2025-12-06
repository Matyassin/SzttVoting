using Model;
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
    
    private void OnOptionCheckedChanged(object sender, CheckedChangedEventArgs e)
    {
        if (!e.Value)
            return;

        var radioButton = sender as RadioButton;
        var selectedOption = radioButton?.Value as OptionData;
        
        if (BindingContext is ListPollsViewModel _vm && selectedOption != null)
        {
            _vm.SelectOptionCommand.Execute(selectedOption);
        }
    }

    private void Button_OnClicked(object? sender, EventArgs e)
    {
        if (_vm.CanModifyVote)
        {
            Navigation.PushAsync(new ModifyPollView(_vm.UserService, _vm.PollService, _vm.SelectedPoll));
        }
    }
}
