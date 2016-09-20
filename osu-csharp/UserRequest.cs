using System;
using System.Net;

using Newtonsoft.Json;
using OsuAPI.Common;

namespace OsuAPI.WebAPI
{

    /// <summary>
    /// Factory for querying users from the official osu! API.
    /// </summary>
    public class UserRequest
    {

        private RequestType Type;

        private string UserUsername;

        private int? UserID;

        /// <summary>
        /// User gamemode to search for.
        /// </summary>
        private GameMode? UserGameMode;

        /// <summary>
        /// Set the user username for the user request.
        /// </summary>
        /// <param name="username"></param>
        /// <returns></returns>
        public UserRequest Username(string username)
        {
            UserUsername = username;
            Type = RequestType.USERNAME;

            return this;
        }

        public UserRequest ID(int ID)
        {
            UserID = ID;
            Type = RequestType.ID;

            return this;
        }

        public UserRequest GameMode(GameMode gameMode)
        {
            UserGameMode = gameMode;

            return this;
        }

        /// <summary>
        /// Query the osu API using the parameters set in the current UserRequest object.
        /// 
        /// Returns all results, up to the limit set.
        /// </summary>
        /// <returns>Array of User objects containing search results</returns>
        public User[] Get()
        {
            using (WebClient client = (new WebClient()))
            {
                client.BaseAddress = RequestConfig.BaseURL;

                // Set the API key.
                client.QueryString.Add("k", RequestConfig.APIKey);

                // Set the beatmap gamemode, if a value has been set.
                if (UserGameMode.HasValue)
                {
                    client.QueryString.Add("m", ((int)UserGameMode).ToString());
                }

                if (Type == RequestType.USERNAME)
                {
                    client.QueryString.Add("type", "string");
                    client.QueryString.Add("u", UserUsername);
                }
                else if (Type == RequestType.ID)
                {
                    client.QueryString.Add("type", "u");
                    client.QueryString.Add("u", UserID.ToString());
                }

                string json = client.DownloadString("get_user");

                return JsonConvert.DeserializeObject<User[]>(json);
            }
        }

        /// <summary>
        /// Query the osu API for the first user returned from the search.
        /// 
        /// User returned may be null, if no users were returned.
        /// </summary>
        /// <returns>User object if user was found, otherwise null</returns>
        public User GetFirst()
        {
            User[] users = Get();

            if (users.Length == 0)
            {
                return null;
            }
            else
            {
                return users[0];
            }
        }

        /// <summary>
        /// Create a new UserRequest factory.
        /// </summary>
        /// <returns>new UserRequest</returns>
        public static UserRequest New()
        {
            return new UserRequest();
        }

    }

}
