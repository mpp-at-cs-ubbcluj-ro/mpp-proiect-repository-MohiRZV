using System;
using System.Collections.Generic;
using System.Text;

namespace LabMPP.domain
{
    [Serializable]
    public class Entity<ID>
    {
        public Entity(ID id)
        {
            this.id = id;
        }

        public ID id { get; set; }
    }
}
