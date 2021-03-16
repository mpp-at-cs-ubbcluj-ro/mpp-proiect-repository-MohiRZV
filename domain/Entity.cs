using System;
using System.Collections.Generic;
using System.Text;

namespace Lab2MPP.domain
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
