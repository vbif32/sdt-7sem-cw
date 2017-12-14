namespace Entities
{
    public class Предмет
    {
        public const string CollectionName = "subjects";

        public Предмет()
        {
            ПлановаяНагрузка = new Нагрузка();
        }
        

        public int Id { get; set; }
        public string Название { get; set; }
        public int Кафедра { get; set; }
        public string Специальность { get; set; }
        public ФормаОбучения ФормаОбучения { get; set; }
        public int Курс { get; set; }
        public int Семестр { get; set; }
        public int НедельВСем { get; set; }
        public string Поток { get; set; }
        public int ЧислоГрупп { get; set; }
        public int ЧислоПодгрупп { get; set; }
        public int ГруппВПотоке { get; set; } // Дублирует Число групп??
        public string Численность { get; set; }
        public float Трудоемкость { get; set; }
        public float ТрудоемкостьГода { get; set; }
        public string Лк { get; set; }
        public string Лаб { get; set; }
        public string Пр { get; set; }
        public bool Экзамен { get; set; }
        public bool Зачет { get; set; }
        public КурсовоеПроектирование КурсовоеПроектирование { get; set; }
        public Нагрузка ПлановаяНагрузка { get; set; }

        protected bool Equals(Предмет other)
        {
            return Id == other.Id && string.Equals(Название, other.Название) && Кафедра == other.Кафедра &&
                   string.Equals(Специальность, other.Специальность) && ФормаОбучения == other.ФормаОбучения &&
                   Курс == other.Курс && Семестр == other.Семестр && НедельВСем == other.НедельВСем &&
                   string.Equals(Поток, other.Поток) && ЧислоГрупп == other.ЧислоГрупп &&
                   ЧислоПодгрупп == other.ЧислоПодгрупп && ГруппВПотоке == other.ГруппВПотоке &&
                   string.Equals(Численность, other.Численность) && Трудоемкость.Equals(other.Трудоемкость) &&
                   ТрудоемкостьГода.Equals(other.ТрудоемкостьГода) && string.Equals(Лк, other.Лк) &&
                   string.Equals(Лаб, other.Лаб) && string.Equals(Пр, other.Пр) && Экзамен == other.Экзамен &&
                   Зачет == other.Зачет && КурсовоеПроектирование == other.КурсовоеПроектирование &&
                   Equals(ПлановаяНагрузка, other.ПлановаяНагрузка);
        }

        public override bool Equals(object obj)
        {
            if (obj is null) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != GetType()) return false;
            return Equals((Предмет) obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                var hashCode = Id;
                hashCode = (hashCode * 397) ^ (Название != null ? Название.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ Кафедра;
                hashCode = (hashCode * 397) ^ (Специальность != null ? Специальность.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (int) ФормаОбучения;
                hashCode = (hashCode * 397) ^ Курс;
                hashCode = (hashCode * 397) ^ Семестр;
                hashCode = (hashCode * 397) ^ НедельВСем;
                hashCode = (hashCode * 397) ^ (Поток != null ? Поток.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ ЧислоГрупп;
                hashCode = (hashCode * 397) ^ ЧислоПодгрупп;
                hashCode = (hashCode * 397) ^ ГруппВПотоке;
                hashCode = (hashCode * 397) ^ (Численность != null ? Численность.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ Трудоемкость.GetHashCode();
                hashCode = (hashCode * 397) ^ ТрудоемкостьГода.GetHashCode();
                hashCode = (hashCode * 397) ^ (Лк != null ? Лк.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (Лаб != null ? Лаб.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (Пр != null ? Пр.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ Экзамен.GetHashCode();
                hashCode = (hashCode * 397) ^ Зачет.GetHashCode();
                hashCode = (hashCode * 397) ^ (int) КурсовоеПроектирование;
                hashCode = (hashCode * 397) ^ (ПлановаяНагрузка != null ? ПлановаяНагрузка.GetHashCode() : 0);
                return hashCode;
            }
        }
    }
}