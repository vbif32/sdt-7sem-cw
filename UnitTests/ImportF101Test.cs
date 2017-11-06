using System;
using Entities;
using ImportExport;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace UnitTests
{
    [TestClass]
    public class ImportF101Test
    {
        [TestMethod]
        public void TestF101Import_FirstRow()
        {
            var expected = new F101Entry(1, 2, 39, "ИКБО-01-02-03-04-05-13-14-15-16-17",
                "Объектно-ориентированное программирование(семестр 1)", 7, "1", "2", "2", true, false, false, true,
                "16", 5, 63, "253 (170)", 9);
            var path = @"D:\Download\Telegram Desktop\Ф101.xlsx";
            var actual = F101Import.LoadF101(path);
            if (!actual[0].Equals(expected))
                throw new Exception();
        }

        [TestMethod]
        public void TestF101CalculationImport_FirstRow()
        {
            var expected = new Нагрузка(16, 576,288,0,2,88.55f,0,506,0,0,0,0);
            var path = @"D:\Download\Telegram Desktop\Ф101.xlsx";
            var actual = F101Import.LoadCalculation(path);
            if (!actual[0].Equals(expected))
                throw new Exception();
        }
    }
}