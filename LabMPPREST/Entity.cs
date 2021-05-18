using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LabMPPREST
{
    class Entity<ID>
    {
        public Entity(ID id)
        {
            this.id = id;
        }

        public ID id { get; set; }
    }
}
