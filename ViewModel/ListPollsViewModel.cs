using Model;
using Services;
using System.Collections.ObjectModel;

namespace ViewModel;

public class ListPollsViewModel : BaseViewModel
{
    public ObservableCollection<PollData> Polls { get; private set; }
    public PollServices PollServices { get; private set; }

    public ListPollsViewModel(PollServices pollServices)
    {
        PollServices = pollServices;
        Polls = new ObservableCollection<PollData>(PollServices.Polls.Values.ToList());
    }
}
