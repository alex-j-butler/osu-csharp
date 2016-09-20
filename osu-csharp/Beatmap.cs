using System;
using System.ComponentModel;

using Newtonsoft.Json;
using OsuAPI.Common;

namespace OsuAPI.WebAPI
{

    /// <summary>
    /// Representation of an osu! Beatmap.
    /// </summary>
    public class Beatmap
    {

        [JsonProperty("approved")]
        public BeatmapStatus Status;

        [JsonProperty("mode")]
        public GameMode GameMode;

        [JsonProperty("approved_date")]
        public DateTime? RankedTime;

        [JsonProperty("last_update")]
        public DateTime LastUpdate;

        [JsonProperty("artist")]
        public string Artist;

        [JsonProperty("creator")]
        public string Creator;

        [JsonProperty("difficultyrating")]
        public double Stars;

        [JsonProperty("diff_size")]
        public double CircleSize;

        [JsonProperty("diff_overall")]
        public double OverallDifficulty;

        [JsonProperty("diff_approach")]
        public double ApproachRate;

        [JsonProperty("diff_drain")]
        public double HealthDrain;

        [JsonProperty("hit_length")]
        public int DrainTimeSeconds;

        [JsonProperty("total_length")]
        public int TotalTimeSeconds;

        [JsonProperty("source")]
        public string Source;

        [JsonProperty("genre_id")]
        public BeatmapGenre Genre;

        [JsonProperty("language_id")]
        public BeatmapLanguage Language;

        [JsonProperty("tags")]
        public string Tags;

        [JsonProperty("favourite_count")]
        public int FavouriteCount;

        [JsonProperty("playcount")]
        public int PlayCount;

        [JsonProperty("passcount")]
        public int PassCount;

        [DefaultValue(0)]
        [JsonProperty("max_combo", DefaultValueHandling = DefaultValueHandling.Populate, NullValueHandling = NullValueHandling.Ignore)]
        public int MaxCombo;

        [JsonProperty("title")]
        public string Title;

        [JsonProperty("version")]
        public string DifficultyName;

        [JsonProperty("file_md5")]
        public string MD5;

        [JsonProperty("beatmap_id")]
        public int ID;

        [JsonProperty("beatmapset_id")]
        public int SetID;

        [JsonProperty("bpm")]
        public double BeatsPerMinute;

        public virtual double BPM
        {
            get
            {
                return BeatsPerMinute;
            }
        }

    }

}
