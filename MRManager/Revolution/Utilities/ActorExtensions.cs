namespace Utilities
{
    public static class ActorExtensions
    {
        public static string GetSafeActorName(this string actorName)
        {
            return actorName.Replace(" ","");
        }
    }
}
