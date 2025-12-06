using Model;
using Services;

namespace Tests;

public class PollCreationTest
{
    private PollServices _services = new PollServicesHelper();
    
    [Fact]
    public void TestCreatePoll()
    { }
}

public class PollServicesHelper : PollServices
{
    public void SetTestFile(string path)
    {
        base._fileName =  $"bin/Test/{path}+{DateTime.Now}";
    }
}