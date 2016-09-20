using System;
using System.IO;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

using OsuAPI.Common;
using OsuAPI.WebAPI;

namespace OsuAPI.ReplayParser
{

    public class Replay
    {

        private ReadableByteFile File;

        /// <summary>
        /// The gamemode of the Osu replay.
        /// </summary>
        public GameMode GameMode { get; set; }

        /// <summary>
        /// The mods of the Osu replay.
        /// </summary>
        public Mods Mods { get; set; }

        /// <summary>
        /// The version of the game, at the time of replay creation.
        /// </summary>
        public int Version { get; set; }

        /// <summary>
        /// Beatmap MD5 hash.
        /// </summary>
        public string BeatmapMD5 { get; set; }

        /// <summary>
        /// Replay MD5 hash.
        /// </summary>
        public string ReplayMD5 { get; set; }

        /// <summary>
        /// The player name who played the Osu replay.
        /// </summary>
        public string PlayerName { get; set; }

        /// <summary>
        /// Number of 300s in the Osu replay.
        /// </summary>
        public short NumberOf300s { get; set; }

        /// <summary>
        /// Number of 100s in the Osu replay.
        /// </summary>
        public short NumberOf100s { get; set; }

        /// <summary>
        /// Number of 50s in the Osu replay.
        /// </summary>
        public short NumberOf50s { get; set; }

        /// <summary>
        /// Number of Gekis in the Osu replay.
        /// </summary>
        public short NumberOfGekis { get; set; }

        /// <summary>
        /// Number of Katus in the Osu replay.
        /// </summary>
        public short NumberOfKatus { get; set; }

        /// <summary>
        /// Number of misses in the Osu replay.
        /// </summary>
        public short NumberOfMisses { get; set; }

        /// <summary>
        /// Total score of the Osu replay.
        /// </summary>
        public int Score { get; set; }

        /// <summary>
        /// Whether the replay is perfect/full comboed.
        /// </summary>
        public bool FullCombo { get; set; }

        /// <summary>
        /// Highest combo of the Osu replay.
        /// </summary>
        public short HighestCombo { get; set; }

        public string LifeBarGraph { get; private set; }

        /// <summary>
        /// Timestamp of the Osu replay.
        /// </summary>
        public long Timestamp { get; set; }

        /// <summary>
        /// Accuracy of the Osu replay.
        /// </summary>
        public double Accuracy { get; set; }

        public int Seed;

        public List<ReplayFrame> ReplayFrames = new List<ReplayFrame>();

        /// <summary>
        /// Life frames making up the life graph shown on the osu! result screen.
        /// </summary>
        public List<LifeFrame> LifeFrames = new List<LifeFrame>();

        /// <summary>
        /// Create a new osu! Replay instance from the filepath given.
        /// </summary>
        /// <param name="filepath">filepath of the osu! replay</param>
        public Replay(string filepath)
        {
            File = new ReadableByteFile(filepath);
            Parse();
        }

        /// <summary>
        /// Create a new osu! replay instance from a byte array.
        /// </summary>
        /// <param name="file">byte array of the osu! replay</param>
        public Replay(byte[] file)
        {
            File = new ReadableByteFile(file);
            Parse();
        }

        /// <summary>
        /// Get the Beatmap played by the replay.
        /// 
        /// This action will query the osu! API, and will block until the request returns. Use GetBeatmapAsync for an asyncronous version.
        /// </summary>
        /// <returns>Beatmap played by the replay</returns>
        public Beatmap GetBeatmap()
        {
            return BeatmapRequest.New()
                .Hash(BeatmapMD5)
                .GetFirst();
        }

        /// <summary>
        /// Get the Beatmap played by the replay asyncronously.
        /// 
        /// This action will query the osu! API, but will not block.
        /// </summary>
        /// <returns>Async task returning the Beatmap played by the replay</returns>
        public async Task<Beatmap> GetBeatmapAsync()
        {
            return await Task.Run(() =>
            {
                return GetBeatmap();
            });
        }

        /// <summary>
        /// Get the User who played the replay.
        /// 
        /// This action will query the osu! API, and will block until the request returns. Use GetUserAsync for an asyncronous version.
        /// </summary>
        /// <returns>User who played the replay</returns>
        public User GetUser()
        {
            return UserRequest.New()
                .Username(PlayerName)
                .GetFirst();
        }

        /// <summary>
        /// Get the User who played the replay asyncronously.
        /// 
        /// This action will query the osu! API, but will not block.
        /// </summary>
        /// <returns>Async task returning the User played by the replay</returns>
        public async Task<User> GetUserAsync()
        {
            return await Task.Run(() =>
            {
                return GetUser();
            });
        }

        /// <summary>
        /// Save the osu! Replay to the specified file path.
        /// </summary>
        /// <param name="filepath">file path to save osu! Replay to</param>
        public void Save(string filepath)
        {
            WritableByteFile WriteFile = new WritableByteFile(filepath);

            // Write replay.
            WriteFile.WriteByte((Byte)GameMode);
            WriteFile.WriteInt(Version);
            WriteFile.WriteString(BeatmapMD5);
            WriteFile.WriteString(PlayerName);
            WriteFile.WriteString(ReplayMD5);
            WriteFile.WriteShort(NumberOf300s);
            WriteFile.WriteShort(NumberOf100s);
            WriteFile.WriteShort(NumberOf50s);
            WriteFile.WriteShort(NumberOfGekis);
            WriteFile.WriteShort(NumberOfKatus);
            WriteFile.WriteShort(NumberOfMisses);
            WriteFile.WriteInt(Score);
            WriteFile.WriteShort(HighestCombo);
            WriteFile.WriteBool(FullCombo);
            WriteFile.WriteInt((int)Mods);

            // Write the life frames.
            StringBuilder sb = new StringBuilder();
            
            for (int i = 0; i < LifeFrames.Count; i++)
            {
                sb.AppendFormat("{0}|{1},", LifeFrames[i].Time, LifeFrames[i].LifePercent);
            }

            WriteFile.WriteString(sb.ToString());


            WriteFile.WriteLong(Timestamp);

            if (ReplayFrames.Count == 0)
            {
                WriteFile.WriteInt(0);
            }
            else
            {
                sb.Clear();

                for (int i = 0; i < ReplayFrames.Count; i++)
                {
                    sb.AppendFormat("{0}|{1}|{2}|{3},", ReplayFrames[i].TimeDiff.ToString(), ReplayFrames[i].X.ToString(), ReplayFrames[i].Y.ToString(), (int)ReplayFrames[i].Keys);
                }
                sb.AppendFormat("{0}|{1}|{2}|{3},", -12345, 0, 0, Seed);

                byte[] rawBytes = Encoding.ASCII.GetBytes(sb.ToString());

                using (MemoryStream ms = new MemoryStream())
                {
                    ms.Write(rawBytes, 0, rawBytes.Length);
                    MemoryStream codedStream = SevenZip.LZMACoder.Compress(ms);
                    byte[] rawBytesCompressed = new byte[codedStream.Length];
                    codedStream.Read(rawBytesCompressed, 0, rawBytesCompressed.Length);

                    WriteFile.WriteInt(rawBytesCompressed.Length);
                    WriteFile.WriteBytes(rawBytesCompressed);
                }
            }

            // Online ID, not exactly sure what this is.
            Byte[] footer = { 0xDD, 0xDD, 0xDD, 0xDD, 0xDD, 0xDD, 0xDD, 0xDD };
            WriteFile.WriteBytes(footer);
        }

        /// <summary>
        /// Parse the replay file using a ByteFile.
        /// </summary>
        private void Parse()
        {
            GameMode = (GameMode)File.ReadByte();
            Version = File.ReadInt();
            BeatmapMD5 = File.ReadString();
            PlayerName = File.ReadString();
            ReplayMD5 = File.ReadString();
            NumberOf300s = File.ReadShort();
            NumberOf100s = File.ReadShort();
            NumberOf50s = File.ReadShort();
            NumberOfGekis = File.ReadShort();
            NumberOfKatus = File.ReadShort();
            NumberOfMisses = File.ReadShort();
            Score = File.ReadInt();
            HighestCombo = File.ReadShort();
            FullCombo = Convert.ToBoolean(File.ReadByte());
            Mods = (Mods)File.ReadInt();
            LifeBarGraph = File.ReadString();
            Timestamp = File.ReadLong();
            Accuracy = CalculateAccuracy(NumberOf300s, NumberOf100s, NumberOf50s, NumberOfMisses);

            ParseLifeGraph();

            int length = File.ReadInt();
            byte[] compressed = File.ReadBytes(length);
            MemoryStream compressedMS = new MemoryStream(compressed);

            using (MemoryStream codedStream = SevenZip.LZMACoder.Decompress(compressedMS))
            using (StreamReader sr = new StreamReader(codedStream))
            {
                int LastTime = 0;

                Random random = new Random();

                foreach (string frame in sr.ReadToEnd().Split(','))
                {
                    if (string.IsNullOrEmpty(frame))
                    {
                        continue;
                    }

                    string[] split = frame.Split('|');
                    if (split.Length < 4)
                    {
                        continue;
                    }

                    if (split[0] == "-12345")
                    {
                        Seed = int.Parse(split[3]);
                        continue;
                    }

                    int keys = int.Parse(split[3]);

                    ReplayFrames.Add(new ReplayFrame()
                    {
                        TimeDiff = int.Parse(split[0]),
                        Time = int.Parse(split[0]) + LastTime,
                        // X = (keys != 0) ? float.Parse(split[1]) : random.Next(0, 512),
                        // Y = (keys != 0) ? float.Parse(split[2]) : random.Next(0, 384),
                        X = float.Parse(split[1]),
                        Y = float.Parse(split[2]),
                        Keys = (Keys)Enum.Parse(typeof(Keys), split[3])
                    });
                    LastTime = ReplayFrames[ReplayFrames.Count - 1].Time;
                }
            }

            // Dispose of the ByteFile reader.
            File.Dispose();
        }

        /// <summary>
        /// Parse the life graph, separating it into LifeFrames.
        /// </summary>
        private void ParseLifeGraph()
        {
            foreach (string frame in LifeBarGraph.Split(','))
            {
                if (string.IsNullOrEmpty(frame))
                {
                    continue;
                }

                string[] split = frame.Split('|');
                if (split.Length < 2)
                {
                    continue;
                }

                LifeFrames.Add(new LifeFrame()
                {
                    Time = int.Parse(split[0]),
                    LifePercent = float.Parse(split[1])
                });
            }
        }

        /// <summary>
        /// Calculate the Osu accuracy from the number of 300s, 100s, 50s and misses.
        /// </summary>
        /// <param name="num300s">Number of 300s obtained in the song.</param>
        /// <param name="num100s">Number of 100s obtained in the song.</param>
        /// <param name="num50s">Number of 50s obtained in the song.</param>
        /// <param name="numMisses">Number of misses obtained in the song.</param>
        /// <returns>Accuracy as a percentage.</returns>
        public static double CalculateAccuracy(int num300s, int num100s, int num50s, int numMisses)
        {
            double totalPointsOfHits = (num50s * 50 + num100s * 100 + num300s * 300);
            double totalNumberOfHits = (numMisses + num50s + num100s + num300s);

            double accuracy = totalPointsOfHits / (totalNumberOfHits * 300) * 100;

            return Math.Round(accuracy, 2);
        }

        /// <summary>
        /// TODO
        /// </summary>
        /// <param name="replay"></param>
        /// <returns></returns>
        public static string CalculateMD5(Replay replay)
        {
            string HashString = String.Format("{0}p{1}o{2}o{3}t{4}a{5}r{6}e{7}y{8}o{9}u{10}{11}{12}",
                replay.NumberOf300s + replay.NumberOf100s, replay.NumberOf50s, replay.NumberOfGekis, replay.NumberOfKatus,
                replay.NumberOfMisses, replay.BeatmapMD5, replay.HighestCombo, !replay.FullCombo ? "True" : "False",
                replay.PlayerName, replay.Score, "A", (int)replay.Mods, "True");

            System.Security.Cryptography.MD5 md5 = System.Security.Cryptography.MD5.Create();
            byte[] inputBytes = System.Text.Encoding.ASCII.GetBytes(HashString);
            byte[] hash = md5.ComputeHash(inputBytes);

            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < hash.Length; i++)
            {
                sb.Append(hash[i].ToString("x2"));
            }

            return sb.ToString();
        }

    }

}
