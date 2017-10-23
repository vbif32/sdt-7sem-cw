using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    public class УчебныйПлан
    {

        public Guid Id { get; set; }
        public string ПолноеИмя => $"{Имя} {Отчество} {Фамилия}";
        public string Имя { get; set; }
        public string Отчество { get; set; }
        public string Фамилия { get; set; }
        public float Ставка { get; set; }

        public List<Предмет> Предметы { get; set; }
    }
}
