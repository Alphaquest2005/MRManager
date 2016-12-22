using SystemInterfaces;

namespace RevolutionEntities.Process
{
    public class Agent : IUser
    {
        public Agent(string userId)
        {
            UserId = userId;
        }
        public string UserId { get; }
    }
}