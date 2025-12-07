using CommunityToolkit.Mvvm.Messaging.Messages;

namespace ViewModel.Messages;

public class PollChangedMessage : ValueChangedMessage<string>
{
    public PollChangedMessage(string value) : base(value) { }
}