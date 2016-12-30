using System.Diagnostics.Contracts;
using SystemInterfaces;


namespace RevolutionEntities.Process
{
    public class User: IUser
    {
        private string Username { get; }
        private string Password { get; }

        public IPerson Person { get; }

        public User(IPerson person, string password, string userId)
        {
            Contract.Requires(person != null);
            Username = person.Name;
            Password = password;
            UserId = userId;
            Person = person;
        }
        
        public string UserId { get; }

    }
}
