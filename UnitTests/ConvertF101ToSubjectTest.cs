using System;
using System.Collections.Generic;
using System.Linq;
using Converting;
using Entities;
using ImportExport;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace UnitTests
{
    [TestClass]
    public class ConvertF101ToSubjectTest
    {
        [TestMethod]
        public void TestCalcLoad_FirstRow()
        {
            var ф101 = new F101Entry(1, 2, 39, "ИКБО-01-02-03-04-05-13-14-15-16-17",
                "Объектно-ориентированное программирование(семестр 1)", 7, "1", "2", "2", true, false, false, true,
                "16", 5, 63, "253 (170)", 9);
            var expected = new Нагрузка(16, 576, 288, 0, 2, 88.55f, 0, 506, 0, 0, 0, 0,0);
            Console.WriteLine(expected.ToStringDebug());

            var actual = Ф101ToПредмет.CalcLoad(ф101);
            Console.WriteLine(actual.ToStringDebug());
            if (!actual.Equals(expected))
                throw new Exception();
        }

        [TestMethod]
        public void TestCalcLoad_AllRows()
        {
            const string path = @"D:\Download\Telegram Desktop\Ф101.xlsx";
            var f101Entries = ExcelToF101.LoadF101(path);
            var calculations = ExcelToF101.LoadCalculation(path);
            var loads = f101Entries.Select(Ф101ToПредмет.CalcLoad).ToList();

            var bools = loads.Select((t, i) => t.Equals(calculations[i])).ToList();


            if (bools.Contains(false))
                throw new Exception();
        }
    }
}