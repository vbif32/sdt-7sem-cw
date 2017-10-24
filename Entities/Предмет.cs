using System.Collections.Generic;

namespace Entities
{
    public class Предмет
    {
        public int Id { get; set; }
        public string Название { get; set; }
        public Кафедра Кафедра { get; set; }
        public string Специальность { get; set; }
        public ФормаОбучения ФормаОбучения { get; set; }
        public int Курс { get; set; }
        public int Семестр { get; set; }
        public int НедельВСем { get; set; }
        public int ЧислоГрупп { get; set; }
        public int ЧислоПодгрупп { get; set; }
        public int ГруппВПотоке { get; set; }
        public int Численность { get; set; }
        public УчебныйПлан УчебныйПлан { get; set; }
        public Нагрузка ПлановаяНагрузка { get; set; }

        public List<Предмет> Предметы { get; set; }
    }
}