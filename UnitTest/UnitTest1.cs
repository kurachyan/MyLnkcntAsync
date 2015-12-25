using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using LRSkipAsync;
using LnkcntAsync;

namespace UnitTest
{
    [TestClass]
    public class LnkcntAsync_UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {
            CS_LnkcntAsync lnkcnt = new CS_LnkcntAsync();

            #region 対象：評価対象なし
            lnkcnt.Clear();
            lnkcnt.Wbuf = @"This is a Pen.";
            lnkcnt.Exec();

            Assert.AreEqual(0, lnkcnt.Lnkcnt, @"[This is a Pen.] : Nest = 0");
            #endregion
        }

        [TestMethod]
        public void TestMethod2()
        {
            CS_LnkcntAsync lnkcnt = new CS_LnkcntAsync();

            #region 対象：評価対象あり（”｛”）
            lnkcnt.Clear();
            lnkcnt.Wbuf = @"This is a Pen. {";
            lnkcnt.Exec();

            Assert.AreEqual(1, lnkcnt.Lnkcnt, @"[This is a Pen. {] : Nest = 1");
            #endregion

            #region 対象：評価対象あり（”｝”）
            lnkcnt.Wbuf = @"This is a Pen. }";
            lnkcnt.Exec();

            Assert.AreEqual(0, lnkcnt.Lnkcnt, @"[This is a Pen. }] : Nest = 0");
            #endregion
        }
    }

    [TestClass]
    public class Lnkcnt_UnitTest2
    {
        [TestMethod]
        public void TestMethod3()
        {
            CS_LnkcntAsync lnkcnt = new CS_LnkcntAsync();
            String[] Keyword = {
                @"main() {",
                @"  test();",
                @"}"
            };

            #region 対象：評価対象なし
            lnkcnt.Clear();
            lnkcnt.Wbuf = Keyword[0];
            lnkcnt.Exec();
            Assert.AreEqual(1, lnkcnt.Lnkcnt, @"[main() {] : Nest = 1");

            lnkcnt.Wbuf = Keyword[1];
            lnkcnt.Exec();
            Assert.AreEqual(1, lnkcnt.Lnkcnt, @"[  test();] : Nest = 1");

            lnkcnt.Wbuf = Keyword[2];
            lnkcnt.Exec();
            Assert.AreEqual(0, lnkcnt.Lnkcnt, @"[}] : Nest = 0");
            #endregion
        }

        [TestMethod]
        public void TestMethod4()
        {
            CS_LnkcntAsync lnkcnt = new CS_LnkcntAsync();
            String[] Keyword = {
                @"main() {",            // 1
                @"  test1();",          // 2
                @"  if(true) {",        // 3
                @"      test2();",      // 4
                @"  }",                 // 5
                @"}"                    // 6
            };

            #region 対象：評価対象なし
            lnkcnt.Clear();
            lnkcnt.Wbuf = Keyword[0];
            lnkcnt.Exec();
            Assert.AreEqual(1, lnkcnt.Lnkcnt, @"[main() {] : Nest = 1");

            lnkcnt.Wbuf = Keyword[1];
            lnkcnt.Exec();
            Assert.AreEqual(1, lnkcnt.Lnkcnt, @"[   test1();] : Nest = 1");

            lnkcnt.Wbuf = Keyword[2];
            lnkcnt.Exec();
            Assert.AreEqual(2, lnkcnt.Lnkcnt, @"[   if(true){] : Nest = 2");

            lnkcnt.Wbuf = Keyword[3];
            lnkcnt.Exec();
            Assert.AreEqual(2, lnkcnt.Lnkcnt, @"[       test2();] : Nest = 2");


            lnkcnt.Wbuf = Keyword[4];
            lnkcnt.Exec();
            Assert.AreEqual(1, lnkcnt.Lnkcnt, @"[   }] : Nest = 1");

            lnkcnt.Wbuf = Keyword[5];
            lnkcnt.Exec();
            Assert.AreEqual(0, lnkcnt.Lnkcnt, @"[}] : Nest = 0");
            #endregion
        }
    }

}
