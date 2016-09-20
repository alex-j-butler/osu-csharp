using System;

using Newtonsoft.Json;
using OsuAPI.Common;

namespace OsuAPI.WebAPI
{

    /// <summary>
    /// Representation of an osu! user.
    /// </summary>
    public class User
    {

        /// <summary>
        /// User ID of the user.
        /// </summary>
        [JsonProperty("user_id")]
        public int ID;

        /// <summary>
        /// Username of the user.
        /// </summary>
        [JsonProperty("username")]
        public string Username;

        /// <summary>
        /// Number of 300 hits from all ranked and approved maps, by the user.
        /// </summary>
        [JsonProperty("count300")]
        public int Count300;

        /// <summary>
        /// Number of 100 hits from all ranked and approved maps, by the user.
        /// </summary>
        [JsonProperty("count100")]
        public int Count100;

        /// <summary>
        /// Number of 50 hits from all ranked and approved maps, by the user.
        /// </summary>
        [JsonProperty("count50")]
        public int Count50;

        /// <summary>
        /// Number of ranked and approved map plays.
        /// </summary>
        [JsonProperty("playcount")]
        public int PlayCount;

        /// <summary>
        /// Total ranked score of the user.
        /// </summary>
        [JsonProperty("ranked_score")]
        public long RankedScore;

        /// <summary>
        /// Total score of the user.
        /// </summary>
        [JsonProperty("total_score")]
        public long TotalScore;

        /// <summary>
        /// Rank of the user.
        /// </summary>
        [JsonProperty("pp_rank")]
        public int Rank;

        /// <summary>
        /// Level of the user.
        /// </summary>
        [JsonProperty("level")]
        public double Level;

        /// <summary>
        /// Performance Points of the user.
        /// </summary>
        [JsonProperty("pp_raw")]
        public double PerformancePoints;

        /// <summary>
        /// Accuracy of the user.
        /// </summary>
        [JsonProperty("accuracy")]
        public double Accuracy;

        /// <summary>
        /// Number of SS ranks achieved by the user.
        /// </summary>
        [JsonProperty("count_rank_ss")]
        public int CountSS;

        /// <summary>
        /// Number of S ranks achieved by the user.
        /// </summary>
        [JsonProperty("count_rank_s")]
        public int CountS;

        /// <summary>
        /// Number of A ranks achieved by the user.
        /// </summary>
        [JsonProperty("count_rank_a")]
        public int CountA;

        /// <summary>
        /// Country of the user.
        /// </summary>
        [JsonProperty("country")]
        public string Country;

        /// <summary>
        /// Country rank of the user.
        /// </summary>
        [JsonProperty("pp_country_rank")]
        public int CountryRank;

    }

}
