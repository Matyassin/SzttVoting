namespace ViewModel
{
    internal interface ICredentialsValidator
    {
        public bool IsEmailValid(string email);
        public bool IsPasswordValid(string password);
    }
}
