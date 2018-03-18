using System;
using System.Diagnostics;
using System.Net;
using LambdaLib;
using Microsoft.VisualBasic.Logging;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CardGameFramework.Tests
{
    [TestClass]
    public class I18NTest
    {
        [TestMethod]
        public void TestI18N()
        {
            var testdata = new WebClient().DownloadString(
                @"https://raw.githubusercontent.com/Cyl18/CardSharp/i18n/CardSharp.I18N/Lang/zh_CN/normal.json");
            Localization.Load(testdata);
        }
    }
}