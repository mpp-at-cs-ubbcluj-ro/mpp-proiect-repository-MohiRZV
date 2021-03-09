package domain;

public class Ticket extends Entity<Long>{
    private Festival festival;
    private Double price;
    private Customer client;
    private Integer seats;

    public Ticket(Long id, Festival festival, Double price, Customer client, Integer seats) {
        super(id);
        this.festival = festival;
        this.price = price;
        this.client = client;
        this.seats = seats;
    }
}
