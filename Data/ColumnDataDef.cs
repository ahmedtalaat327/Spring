using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spring.Data
{
    //this is exceptional datatype for only columns headers type ex: varchar ,...
    public class ColumnDataDef
    {
        public int Id {  get; set; }
        public string Name { get; set; }
    }
}
