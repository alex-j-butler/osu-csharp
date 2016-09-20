using System;
using System.Net;

using Newtonsoft.Json;
using OsuAPI.Common;

namespace OsuAPI.WebAPI
{

    /// <summary>
    /// Factory for querying user scores from the official osu! API.
    /// </summary>
    public class UserScoreRequest
    {

        private RequestType Type;

        private string ScoreUsername;

        private int? ScoreUserID;

        /// <summary>
        /// User gamemode to search for.
        /// </summary>
        private GameMode? ScoreGameMode;

        private int? ScoreLimit;

        /// <summary>
        /// Set the username for the user score request.
        /// </summary>
        /// <param name="username"></param>
        /// <returns></returns>
        public UserScoreRequest Username(string username)
        {
            ScoreUsername = username;
            Type = RequestType.USERNAME;

            return this;
        }

        public UserScoreRequest ID(int ID)
        {
            ScoreUserID = ID;
            Type = RequestType.ID;

            return this;
        }

        public UserScoreRequest GameMode(GameMode gameMode)
        {
            ScoreGameMode = gameMode;

            return this;
        }

        public UserScoreRequest Limit(int limit)
        {
            ScoreLimit = limit;

            return this;
        }

        /// <summary>
        /// Query the osu API using the parameters set in the current UserScoreRequest object.
        /// 
        /// Returns all results, up to the limit set.
        /// </summary>
        /// <returns>Array of Score objects containing search results</returns>
        public Score[] Get()
        {
            using (WebClient client = (new WebClient()))
            {
                client.BaseAddress = RequestConfig.BaseURL;

                // Set the API key.
                client.QueryString.Add("k", RequestConfig.APIKey);

                // Set the gamemode, if a value has been set.
                if (ScoreGameMode.HasValue)
                {
                    client.QueryString.Add("m", ((int)ScoreGameMode).ToString());
                }

                if (ScoreLimit.HasValue)
                {
                    client.QueryString.Add("limit", ScoreLimit.ToString());
                }

                if (Type == RequestType.USERNAME)
                {
                    client.QueryString.Add("type", "string");
                    client.QueryString.Add("u", ScoreUsername);
                }
                else if (Type == RequestType.ID)
                {
                    client.QueryString.Add("type", "u");
                    client.QueryString.Add("u", ScoreUserID.ToString());
                }

                string json = client.DownloadString("get_user_best");

                return JsonConvert.DeserializeObject<Score[]>(json);
            }
        }

        /// <summary>
        /// Query the osu API for the first user returned from the search.
        /// 
        /// User returned may be null, if no users were returned.
        /// </summary>
        /// <returns>User object if user was found, otherwise null</returns>
        public Score GetFirst()
        {
            Score[] scores = Get();

            if (scores.Length == 0)
            {
                return null;
            }
            else
            {
                return scores[0];
            }
        }

        /// <summary>
        /// Create a new UserScoreRequest factory.
        /// </summary>
        /// <returns>new UserScoreRequest</returns>
        public static UserScoreRequest New()
        {
            return new UserScoreRequest();
        }

    }

}
