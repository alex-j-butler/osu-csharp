using System;
using System.Net;

using Newtonsoft.Json;
using OsuAPI.Common;

namespace OsuAPI.WebAPI
{

    enum RequestType
    {
        NONE,
        USERNAME,
        ID
    }

    /// <summary>
    /// Factory for querying beatmaps from the official osu! api.
    /// </summary>
    public class BeatmapRequest
    {

        private RequestType Type;


        /// <summary>
        /// MD5 hash of beatmap to search for.
        /// </summary>
        private string BeatmapHash;

        /// <summary>
        /// Username to search for.
        /// </summary>
        private string BeatmapUsername;

        /// <summary>
        /// User ID to search for.
        /// </summary>
        private int? BeatmapUserID;

        /// <summary>
        /// Search for beatmaps ranked since this day.
        /// </summary>
        private DateTime? BeatmapSince;

        /// <summary>
        /// Beatmap Set ID to search for.
        /// </summary>
        private int? BeatmapSetID;

        /// <summary>
        /// Beatmap ID to search for.
        /// </summary>
        private int? BeatmapID;

        /// <summary>
        /// Beatmap gamemode to search for.
        /// </summary>
        private GameMode? BeatmapGameMode;

        /// <summary>
        /// Whether to show beatmaps that have been converted from one gamemode to another (Standard to osu!mania, for example).
        /// </summary>
        private bool BeatmapConverted = false;

        /// <summary>
        /// Limit of beatmaps to retrieve.
        /// </summary>
        private int BeatmapLimit = 500;

        /// <summary>
        /// Set the beatmap hash for the beatmap request.
        /// </summary>
        /// <param name="hash">Beatmap hash</param>
        /// <returns>Beatmap request</returns>
        public BeatmapRequest Hash(string hash)
        {
            BeatmapHash = hash;

            return this;
        }

        /// <summary>
        /// Set the beatmap username for the beatmaap request.
        /// </summary>
        /// <param name="username"></param>
        /// <returns></returns>
        public BeatmapRequest Username(string username)
        {
            BeatmapUsername = username;
            Type = RequestType.USERNAME;

            return this;
        }

        public BeatmapRequest UserID(int userID)
        {
            BeatmapUserID = userID;
            Type = RequestType.ID;

            return this;
        }

        public BeatmapRequest SetID(int setID)
        {
            BeatmapSetID = setID;

            return this;
        }

        public BeatmapRequest ID(int ID)
        {
            BeatmapID = ID;

            return this;
        }

        public BeatmapRequest GameMode(GameMode gameMode)
        {
            BeatmapGameMode = gameMode;

            return this;
        }

        /// <summary>
        /// Query the osu API using the parameters set in the current BeatmapRequest object.
        /// 
        /// Returns all results, up to the limit set.
        /// </summary>
        /// <returns>Array of Beatmap objects containing search results</returns>
        public Beatmap[] Get()
        {
            using (WebClient client = (new WebClient()))
            {
                client.BaseAddress = RequestConfig.BaseURL;

                // Set the API key.
                client.QueryString.Add("k", RequestConfig.APIKey);

                // Set the beatmap hash, if a value has been set.
                if (BeatmapHash != null)
                {
                    client.QueryString.Add("h", BeatmapHash);
                }

                // Set the beatmap ID, if a value has been set.
                if (BeatmapID.HasValue)
                {
                    client.QueryString.Add("b", ((int)BeatmapID).ToString());
                }

                // Set the beatmap set ID, if a value has been set.
                if (BeatmapSetID.HasValue)
                {
                    client.QueryString.Add("s", ((int)BeatmapSetID).ToString());
                }

                // Set the beatmap since date, if a value has been set.
                if (BeatmapSince.HasValue)
                {
                    client.QueryString.Add("since", BeatmapSince.Value.ToString("yyyy-MM-dd HH:mm:ss"));
                }

                // Set the beatmap gamemode, if a value has been set.
                if (BeatmapGameMode.HasValue)
                {
                    client.QueryString.Add("m", ((int)BeatmapGameMode).ToString());
                }

                if (Type == RequestType.USERNAME)
                {
                    client.QueryString.Add("type", "string");
                    client.QueryString.Add("u", BeatmapUsername);
                }
                else if (Type == RequestType.ID)
                {
                    client.QueryString.Add("type", "u");
                    client.QueryString.Add("u", BeatmapUserID.ToString());
                }

                string json = client.DownloadString("get_beatmaps");

                return JsonConvert.DeserializeObject<Beatmap[]>(json);
            }
        }

        /// <summary>
        /// Query the osu API for the first beatmap returned from the search.
        /// 
        /// Beatmap returned may be null, if no beatmaps were returned.
        /// </summary>
        /// <returns>Beatmap object if beatmap was found, otherwise null</returns>
        public Beatmap GetFirst()
        {
            Beatmap[] beatmaps = Get();

            if (beatmaps.Length == 0)
            {
                return null;
            }
            else
            {
                return beatmaps[0];
            }
        }

        /// <summary>
        /// Create a new BeatmapRequest factory.
        /// </summary>
        /// <returns>new BeatmapRequest</returns>
        public static BeatmapRequest New()
        {
            return new BeatmapRequest();
        }

    }

}
