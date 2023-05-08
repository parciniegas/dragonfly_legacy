namespace Dragonfly.Twitter
{
    public interface ITwitter
    {
        void SendDirectMessage(string user, string message);
    }
}
