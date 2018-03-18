using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using LambdaLib;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CardGameFramework.Tests
{
    [TestClass]
    public class ConfigTest
    {
        private string TestContent { get; set; }

        [TestInitialize]
        public void Prepare()
        {
            Directory.SetCurrentDirectory(Path.GetTempPath());
            Remove();
            ConfigClass.Update();
            ConfigClass.Instance.Key1 = 1;
            ConfigClass.Save();
            TestContent = File.ReadAllText(ConfigClass.SavePath);
            Remove();
        }

        [TestMethod]
        public void Test()
        {
            ConfigClass.Update();
            File.WriteAllText(ConfigClass.FileName, TestContent);
            ConfigClass.Update();
            Assert.IsTrue(ConfigClass.Instance.Key1 == 1);
        }

        [TestCleanup]
        public void Finish()
        {
            Remove();
        }

        private void Remove()
        {
            if (File.Exists(ConfigClass.FileName))
            {
                File.Delete(ConfigClass.FileName);
            }
        }
    }

    [Config(FileName)]
    internal class ConfigClass : Config<ConfigClass>
    {
        public const string FileName = "test.json";
        public int Key1 { get; set; } = 0;
    }
}