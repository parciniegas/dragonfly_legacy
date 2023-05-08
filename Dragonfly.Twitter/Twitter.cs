using Dragonfly.Core.ArgumentGuard;
using Dragonfly.Core.Configuration;
using Tweetinvi;

namespace Dragonfly.Twitter
{
    public class Twitter : ITwitter
    {
        private readonly string _consumerKey;
        private readonly string _consumerSecret;
        private readonly string _accessToken;
        private readonly string _accessTokenSecret;
        //Auth.SetUserCredentials("CONSUMER_KEY", "CONSUMER_SECRET", "ACCESS_TOKEN", "ACCESS_TOKEN_SECRET");

        public Twitter(IConfigurator configurator)
        {
            Guard.Check(configurator, nameof(configurator));

            _consumerKey = configurator.GetKey("twitterConsumerKey");
            _consumerSecret = configurator.GetKey("twitterConsumerSecret");
            _accessToken = configurator.GetKey("twitterAccessToken");
            _accessTokenSecret = configurator.GetKey("twitterAccessTokenSecret");
        }

        public void SendDirectMessage(string user, string message)
        {
            Auth.SetUserCredentials(_consumerKey, _consumerSecret, _accessToken, _accessTokenSecret);
            Message.PublishMessage(message, user);
        }
    }
}
