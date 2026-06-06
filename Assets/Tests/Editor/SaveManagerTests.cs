using NUnit.Framework;
using Assets.Scripts.SaveLoad;

namespace Assets.Tests.Editor
{
    public class SaveManagerTests
    {
        [SetUp]
        public void Setup()
        {
            SaveManager.DeleteSave();
        }

        [TearDown]
        public void TearDown()
        {
            SaveManager.DeleteSave();
        }

        [Test]
        public void Load_WhenNoSaveExists_ReturnsNull()
        {
            SaveData data = SaveManager.Load();

            Assert.IsNull(data);
        }

        [Test]
        public void SaveAndLoad_PreservesBasicFields()
        {
            SaveData original = new SaveData(4, 3123123, new[] { 2, 4, 6, 8 }, 7, 15);

            SaveManager.Save(original);

            SaveData loaded = SaveManager.Load();

            Assert.IsNotNull(loaded);

            Assert.AreEqual(original.seed, loaded.seed);
            Assert.AreEqual(original.difficultyLevel, loaded.difficultyLevel);
            Assert.AreEqual(original.matchCount, loaded.matchCount);
            Assert.AreEqual(original.turnCount, loaded.turnCount);
        }

        [Test]
        public void SaveAndLoad_PreservesSolvedIds()
        {
            SaveData original = new SaveData(1, 24214, new[] { 1, 3, 5, 7, 9 }, 7, 15);

            SaveManager.Save(original);

            SaveData loaded = SaveManager.Load();

            CollectionAssert.AreEqual(
                original.solvedIDs,
                loaded.solvedIDs);
        }

        [Test]
        public void DeleteSave_RemovesStoredData()
        {
            SaveManager.Save(new SaveData(1, 242, new int[0], 0, 0));

            SaveManager.DeleteSave();

            SaveData loaded = SaveManager.Load();

            Assert.IsNull(loaded);
        }

        [Test]
        public void Save_OverwriteExistingSave_UsesLatestData()
        {
            SaveManager.Save(new SaveData(1, 222, new int[0], 0, 0));

            SaveManager.Save(new SaveData(1, 222, new int[0], 0, 0));

            SaveData loaded = SaveManager.Load();

            Assert.AreEqual(222, loaded.seed);
        }
    }
}