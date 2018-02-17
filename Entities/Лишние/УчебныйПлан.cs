using System.Collections.Generic;

namespace Entities
{
    public class УчебныйПлан
    {
        public int Id { get; set; }


        public List<Subject> Предметы { get; set; }
    }
}