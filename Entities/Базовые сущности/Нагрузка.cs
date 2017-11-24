namespace Entities
{
    public class Нагрузка
    {
        public const string CollectionName = "workload";

        public Нагрузка()
        {
        }


        public Нагрузка(float лекции, float лабораторные, float практические, float зачеты, float консультации,
            float экзамены, float нир, float курсовоеПроектирование, float вкр, float гэк, float гак,
            float рма, float рмп)
        {
            Лекции = лекции;
            Лабораторные = лабораторные;
            Практические = практические;
            Зачеты = зачеты;
            Консультации = консультации;
            Экзамены = экзамены;
            Нир = нир;
            КурсовоеПроектирование = курсовоеПроектирование;
            Вкр = вкр;
            Гэк = гэк;
            Гак = гак;
            Рма = рма;
            Рмп = рмп;
        }

        public int Id { get; set; }
        public float Лекции { get; set; }
        public float Лабораторные { get; set; }
        public float Практические { get; set; }
        public float Зачеты { get; set; }
        public float Консультации { get; set; }
        public float Экзамены { get; set; }
        public float Нир { get; set; }
        public float КурсовоеПроектирование { get; set; }
        public float Вкр { get; set; }
        public float Гэк { get; set; }
        public float Гак { get; set; }

        /// <summary>
        ///     Руководство магитрами аспирантами
        /// </summary>
        public float Рма { get; set; }

        /// <summary>
        ///     Руководство магистерскими программами
        /// </summary>
        public float Рмп { get; set; }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != GetType()) return false;
            return Equals((Нагрузка) obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                var hashCode = Id;
                hashCode = (hashCode * 397) ^ Лекции.GetHashCode();
                hashCode = (hashCode * 397) ^ Лабораторные.GetHashCode();
                hashCode = (hashCode * 397) ^ Практические.GetHashCode();
                hashCode = (hashCode * 397) ^ Зачеты.GetHashCode();
                hashCode = (hashCode * 397) ^ Консультации.GetHashCode();
                hashCode = (hashCode * 397) ^ Экзамены.GetHashCode();
                hashCode = (hashCode * 397) ^ Нир.GetHashCode();
                hashCode = (hashCode * 397) ^ КурсовоеПроектирование.GetHashCode();
                hashCode = (hashCode * 397) ^ Вкр.GetHashCode();
                hashCode = (hashCode * 397) ^ Гэк.GetHashCode();
                hashCode = (hashCode * 397) ^ Гак.GetHashCode();
                hashCode = (hashCode * 397) ^ Рма.GetHashCode();
                hashCode = (hashCode * 397) ^ Рмп.GetHashCode();
                return hashCode;
            }
        }

        public string ToStringDebug()
        {
            return
                $"{Лекции,3} {Лабораторные,3} {Практические,3} {Зачеты,3} {Консультации,3} {Экзамены,3} {Нир,3} {КурсовоеПроектирование,3} {Вкр,3} {Гэк,3} {Гак,3} {Рма,3}";
        }

        protected bool Equals(Нагрузка нагрузка)
        {
            return Id == нагрузка.Id && Лекции.Equals(нагрузка.Лекции) && Лабораторные.Equals(нагрузка.Лабораторные) &&
                   Практические.Equals(нагрузка.Практические) && Зачеты.Equals(нагрузка.Зачеты) &&
                   Консультации.Equals(нагрузка.Консультации) && Экзамены.Equals(нагрузка.Экзамены) &&
                   Нир.Equals(нагрузка.Нир) && КурсовоеПроектирование.Equals(нагрузка.КурсовоеПроектирование) &&
                   Вкр.Equals(нагрузка.Вкр) && Гэк.Equals(нагрузка.Гэк) && Гак.Equals(нагрузка.Гак) &&
                   Рма.Equals(нагрузка.Рма) && Рмп.Equals(нагрузка.Рмп);
        }
    }
}