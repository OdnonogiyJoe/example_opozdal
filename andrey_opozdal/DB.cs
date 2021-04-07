using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace andrey_opozdal
{
    class DB
    {
        public static Entities entities;
        public static Entities GetDB()
        {
            if (entities == null)
                entities = new Entities();
            return entities;
        }
    }
}
