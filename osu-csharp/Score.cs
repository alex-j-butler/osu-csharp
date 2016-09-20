using System;

using Newtonsoft.Json;
using OsuAPI.Common;

namespace OsuAPI.WebAPI
{

    /// <summary>
    /// Representation of an osu! Score.
    /// </summary>
    public class Score
    {

        [JsonProperty("score")]
        public int ObtainedScore;

        [JsonProperty("beatmap_id")]
        public int BeatmapID;

        /// <summary>
        /// Username of the user.
        /// </summary>
        [JsonProperty("username")]
        public string Username;

        [JsonProperty("user_id")]
        public int UserID;

        [JsonProperty("count300")]
        public int Count300;

        [JsonProperty("count100")]
        public int Count100;

        [JsonProperty("count50")]
        public int Count50;

        [JsonProperty("countmiss")]
        public int CountMiss;

        [JsonProperty("countkatu")]
        public int CountKatu;

        [JsonProperty("countgeki")]
        public int CountGeki;

        [JsonProperty("maxcombo")]
        public int MaxCombo;

        [JsonProperty("enabled_mods")]
        public Mods Mods;

        [JsonProperty("date")]
        public DateTime Date;

        [JsonProperty("rank")]
        public string Rank;

        [JsonProperty("pp")]
        public float PerformancePoints;

        [JsonProperty("perfect")]
        private int Perfect;

        public bool FullCombo
        {
            get
            {
                return Convert.ToBoolean(Perfect);
            }
        }

        public Beatmap Beatmap
        {
            get
            {
                return BeatmapRequest.New()
                    .ID(BeatmapID)
                    .GetFirst();
            }
        }

    }

}
