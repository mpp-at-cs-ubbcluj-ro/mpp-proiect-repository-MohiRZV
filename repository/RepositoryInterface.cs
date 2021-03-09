using System;
using System.Collections.Generic;
using System.Text;
using Lab2MPP.domain;

namespace Lab2MPP.repository
{
    interface RepositoryInterface<ID,E> where E:Entity<ID>
    {
        E getOne(ID id)
        {
            return null; 
        }

        IEnumerable<E> getAll()
        {
            return null;
        }

        E add(E entity)
        {
            return null;
        }

        E delete(ID id)
        {
            return null;
        }

        E update(E entity)
        {
            return null;
        }
    }
}
