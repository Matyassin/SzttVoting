using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Services;

namespace Tests
{
    public class userServicesTests
    {

        [Fact]
        public void DictionarryCalling()
        {
            var svc = new UserServices();
            svc.Load();

            Assert.NotNull(svc.ContainsEmail(""));
        }

        [Fact]
        public void AddUser_ContainsEmail()
        {
            var svc = new UserServices();
            svc.Load();

            svc.AddUser("user", "user@gmail.com", "pass123");

            Assert.True(svc.ContainsEmail("test@mail.hu"));
        }

        [Fact]
        public void SetLoggedInUser_And_Clearing()
        {
            var svc = new UserServices();
            svc.Load();

            svc.AddUser("user", "user@gmail.com", "pass123");
            svc.SetLoggedInUser("user@gmail.com");

            Assert.NotNull(svc.LoggedInUser);

            svc.ClearLoggedInUser();
            Assert.Null(svc.LoggedInUser);
        }

        [Fact]
        public void ValidateUser_WrongEmailOrPassword()
        {
            var svc = new UserServices();
            svc.Load();

            svc.AddUser("user", "valid@gmail.com", "goodpass");

            Assert.False(svc.ValidateUser("nonexistent@gmail.com", "goodpass"));
            Assert.False(svc.ValidateUser("valid@gmail.com", "badpass"));
        }

        [Fact]
        public void ValidateUser_Correct_Credentials()
        {
            var svc = new UserServices();
            svc.Load();

            svc.AddUser("user", "validate2@gmail.com", "mypassword");

            Assert.True(svc.ValidateUser("validate2@gmail.com", "mypassword"));
        }
    }
}
