using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Food.Core.Securities.Entitys
{
    public class SecurityEntity
    {
        public string TokenType { get; set; }
        public string AccesToken { get; set; }
        public DateTime ExpireOn { get; set; }

    }
}
