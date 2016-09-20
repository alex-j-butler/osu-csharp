using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using OsuAPI.Common;
using OsuAPI.ReplayParser;

namespace OsuAPI.ReplayParser.Tests
{

    [TestClass]
    public class ReplayTest
    {

        [TestMethod]
        public void ParseReplay_Passcode()
        {
            // Load the osu! replay called 'Passcode'.
            Replay replay = new Replay(Properties.Resources.Passcode);

            GameMode Gamemode = GameMode.Standard;
            Mods Mods = Mods.DoubleTime;
            int Version = 20160821;
            string BeatmapMD5 = "48984f4dd668eb32914a6a0696a159ea";
            string ReplayMD5 = "9659d98cb8c12551a8fd869f5df028d9";
            string PlayerName = "ranndom";
            short Count300 = 229;
            short Count100 = 5;
            short Count50 = 0;
            short CountMisses = 2;
            short CountGekis = 46;
            short CountKatus = 4;
            int Score = 1303683;
            bool FullCombo = false;
            short HighestCombo = 256;
            string LifebarGraph = "2063|1,4218|1,6354|1,8490|1,10651|1,12988|1,15346|1,17479|1,19846|1,22202|1,24366|1,26703|1,29061|1,31216|1,33561|1,35917|1,38071|1,40419|1,42775|1,44922|1,47060|1,49204|1,51345|1,54133|1,56489|1,58626|0.99,60988|1,63160|1,65275|0.89,67416|0.99,69570|1,71918|1,74061|0.83,76419|0.9,78533|1,81130|1,83489|0.97,85632|1,87340|1,";
            long Timestamp = 636092398192394688;
            double Accuracy = 97.74;

            Assert.AreEqual(Gamemode, replay.GameMode);
            Assert.AreEqual(Mods, replay.Mods);
            Assert.AreEqual(Version, replay.Version);
            Assert.AreEqual(BeatmapMD5, replay.BeatmapMD5);
            Assert.AreEqual(ReplayMD5, replay.ReplayMD5);
            Assert.AreEqual(PlayerName, replay.PlayerName);
            Assert.AreEqual(Count300, replay.NumberOf300s);
            Assert.AreEqual(Count100, replay.NumberOf100s);
            Assert.AreEqual(Count50, replay.NumberOf50s);
            Assert.AreEqual(CountMisses, replay.NumberOfMisses);
            Assert.AreEqual(CountGekis, replay.NumberOfGekis);
            Assert.AreEqual(CountKatus, replay.NumberOfKatus);
            Assert.AreEqual(Score, replay.Score);
            Assert.AreEqual(FullCombo, replay.FullCombo);
            Assert.AreEqual(HighestCombo, replay.HighestCombo);
            Assert.AreEqual(LifebarGraph, replay.LifeBarGraph);
            Assert.AreEqual(Timestamp, replay.Timestamp);
            Assert.AreEqual(Accuracy, replay.Accuracy);
        }

        [TestMethod]
        public void ParseReplay_FallingInLove()
        {
            // Load the osu! replay called 'FallingInLove'.
            Replay replay = new Replay(Properties.Resources.FallingInLove);

            GameMode Gamemode = GameMode.Standard;
            Mods Mods = Mods.None;
            int Version = 20160821;
            string BeatmapMD5 = "fee70288ea38d6e3a33e6923405e3df2";
            string ReplayMD5 = "dbc7dcfb64ff93a6ab2acbbd9c2bfc98";
            string PlayerName = "ranndom";
            short Count300 = 115;
            short Count100 = 3;
            short Count50 = 0;
            short CountMisses = 0;
            short CountGekis = 23;
            short CountKatus = 3;
            int Score = 560174;
            bool FullCombo = true;
            short HighestCombo = 192;
            string LifebarGraph = "7782|1,10021|1,12260|1,14496|1,16738|1,18979|1,21135|0.99,23157|1,25244|1,27470|1,29706|1,31962|1,34201|1,36439|1,38679|1,40916|1,43155|1,45396|1,47625|1,49857|1,52110|1,";
            long Timestamp = 635726294399080564;
            double Accuracy = 98.31;

            Assert.AreEqual(Gamemode, replay.GameMode);
            Assert.AreEqual(Mods, replay.Mods);
            Assert.AreEqual(Version, replay.Version);
            Assert.AreEqual(BeatmapMD5, replay.BeatmapMD5);
            Assert.AreEqual(ReplayMD5, replay.ReplayMD5);
            Assert.AreEqual(PlayerName, replay.PlayerName);
            Assert.AreEqual(Count300, replay.NumberOf300s);
            Assert.AreEqual(Count100, replay.NumberOf100s);
            Assert.AreEqual(Count50, replay.NumberOf50s);
            Assert.AreEqual(CountMisses, replay.NumberOfMisses);
            Assert.AreEqual(CountGekis, replay.NumberOfGekis);
            Assert.AreEqual(CountKatus, replay.NumberOfKatus);
            Assert.AreEqual(Score, replay.Score);
            Assert.AreEqual(FullCombo, replay.FullCombo);
            Assert.AreEqual(HighestCombo, replay.HighestCombo);
            Assert.AreEqual(LifebarGraph, replay.LifeBarGraph);
            Assert.AreEqual(Timestamp, replay.Timestamp);
            Assert.AreEqual(Accuracy, replay.Accuracy);
        }

        [TestMethod]
        public void CalculateAccuracy()
        {
            int Count300 = 322;
            int Count100 = 0;
            int Count50 = 0;
            int CountMisses = 1;
            double ExpectedAcc = 99.69;

            Assert.AreEqual(ExpectedAcc, Replay.CalculateAccuracy(Count300, Count100, Count50, CountMisses));
        }

        [TestMethod]
        public void CalculateAccuracyAt100Percent()
        {
            int Count300 = 400;
            int Count100 = 0;
            int Count50 = 0;
            int CountMisses = 0;
            double ExpectedAcc = 100.00;

            Assert.AreEqual(ExpectedAcc, Replay.CalculateAccuracy(Count300, Count100, Count50, CountMisses));
        }

    }

}
