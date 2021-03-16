using System;
using System.Collections.Generic;
using System.Text;
using Lab2MPP.domain;
using Microsoft.Data.Sqlite;

namespace Lab2MPP.repository
{
    interface RepositoryInterface<ID,E> where E:Entity<ID>
    {
        E getOne(ID id);

        IEnumerable<E> getAll();

        E add(E entity);

        E delete(ID id);

        E update(E entity);
    }
}
