using LiteDB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    public class LiteDbModel
    {
        public void CreateModel() => new LiteDatabase(@"Data\MyData.db");
    }
}
