using LabMPP.domain;
using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;
namespace LabMPP.repository
{
    abstract class Repository<ID,E> : RepositoryInterface<ID,E> where E : Entity<ID>
    {
        public E add(E entity)
        {
            throw new NotImplementedException();
        }

        public E delete(ID id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<E> getAll()
        {
            throw new NotImplementedException();
        }

        protected abstract E extractEntity(DataSet resultSet);

        protected abstract String getEntityAsString(E entity);

        public E getOne(ID id)
        {
            throw new NotImplementedException();
        }

        public E update(E entity)
        {
            throw new NotImplementedException();
        }
    }
}
