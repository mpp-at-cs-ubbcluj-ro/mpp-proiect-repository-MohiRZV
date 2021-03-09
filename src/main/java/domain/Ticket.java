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

    public Festival getFestival() {
        return festival;
    }

    public void setFestival(Festival festival) {
        this.festival = festival;
    }

    public Double getPrice() {
        return price;
    }

    public void setPrice(Double price) {
        this.price = price;
    }

    public Customer getClient() {
        return client;
    }

    public void setClient(Customer client) {
        this.client = client;
    }

    public Integer getSeats() {
        return seats;
    }

    public void setSeats(Integer seats) {
        this.seats = seats;
    }
}
