using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PostgresTest.Model
{
    public class Payload
    {
        public int Id { get; set; }
        public DateTime DateTime { get; set; }
        public string Info { get; set; } = null!;
        public int Digit { get; set; }
    }
}
