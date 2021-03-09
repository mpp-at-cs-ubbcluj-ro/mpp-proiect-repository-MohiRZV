package repository;

import domain.Entity;

public interface RepositoryInterface<ID,E extends Entity<ID>> {


    default E getOne(ID id){
        return null;
    }

    default Iterable<E> getAll(){
        return null;
    }

    default E add(E entity){
        return null;
    }

    default E delete(ID id){
        return null;
    }

    default E update(E entity){
        return null;
    }


}
