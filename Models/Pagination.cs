using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class Pagination<T>
    {
        public List<T> Products { get; set; }
        public int Total { get; set; }
        public int PageCount { get; set; }
        public int PageNum { get; set; }
    }
}
