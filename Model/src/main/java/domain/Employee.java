package domain;

import java.io.Serializable;

public class Employee extends Entity<Long> implements Serializable {
    private String name;
    public Employee(Long aLong,String name) {
        super(aLong);
        this.name=name;

    }

    public String getName() {
        return name;
    }

    public void setName(String name) {
        this.name = name;
    }
}

