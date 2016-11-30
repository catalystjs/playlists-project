// Project Models
using beltexam3.ViewModels;

namespace beltexam3.Models
{
    public class UserWrapper
    {
        public Login Login { get; set; }
        public User User { get; set; }
        public UserWrapper(User user, Login login)
        {
            Login = login;
            User = user;
        }
    }

}