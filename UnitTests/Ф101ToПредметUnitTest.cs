using Converting;
using Entities;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace UnitTests
{
    [TestClass]
    public class Ф101ToПредметUnitTest
    {
        [TestMethod]
        private void CalcLoadTest()
        {
            var ф101 = new Ф101
            {
                Курс = 1,
                Семестр = 2,
                НомерПотока = 39,
                ИмяПотока = "ИКБО-01-02-03-04-05-13-14-15-16-17",
                Дисциплина = "Объектно-ориентированное программирование (семестр 1)",
                Кафедра = 7,
                ЛекцииВНеделю = 1,
                ЛабораторныеВНеделю = 2,
                ПрактическиеВНеделю = 3,
                Экзамен = true,
                Зачет = false,
                Кп = false,
                Кр = true,
                НедельТо = 16,
                Трудоемкость = 5,
                ТрудоемкостьГода = 63,
                Численность = "253 (170)",
                ЧислоГрупп = 9
            };
            var expected = new Нагрузка
            {
                Лекции = 16,
                Лабораторные = 576,
                Практические = 288,
                Зачеты = 0,
                Консультации = 2,
                Экзамены = 88.55f,
                КурсовоеПроектирование = 0,
                ПрактикиНир = 506,
                Вкр = 0,
                ГэкГак = 0,
                ДопЗащ = 0,
                Рма = 0,
                Рмп = 0
            };

            var actual = Ф101ToПредмет.CalcLoad(ф101);
            Assert.AreEqual(expected, actual);
        }
    }
}