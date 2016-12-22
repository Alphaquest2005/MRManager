using SystemInterfaces;


namespace RevolutionEntities.Process
{
    public class User: IUser
    {
        private string UserName { get; }
        private string Password { get; }

        public IPerson Person { get; }

        public User(IPerson person, string password, string userId)
        {
            UserName = person.ComputedProperties().Name;
            Password = password;
            UserId = userId;
            Person = person;
        }
        
        public string UserId { get; }

    }
}
