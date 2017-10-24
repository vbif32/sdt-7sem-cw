using System.Collections.Generic;

namespace Entities
{
    public class УчебныйПлан
    {
        public int Id { get; set; }
        public string Трудоемкость { get; set; }
        public string ТрудоемкостьГода { get; set; }
        public string ЛекцииВНеделю { get; set; }
        public string ЛабораторныеВНеделю { get; set; }
        public string ПрактическиеВНеделю { get; set; }
        public bool Экзамен { get; set; }
        public bool Зачет { get; set; }
        public КурсовоеПроектирование КурсовоеПроектирование { get; set; }

        public List<Предмет> Предметы { get; set; }
    }
}