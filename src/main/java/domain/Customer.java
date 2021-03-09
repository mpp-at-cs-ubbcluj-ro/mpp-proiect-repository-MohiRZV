package domain;

public class Customer extends Entity<Long>{
    private String name;
    private String address;

    public Customer(Long id, String name, String address) {
        super(id);
        this.name = name;
        this.address = address;
    }
}
