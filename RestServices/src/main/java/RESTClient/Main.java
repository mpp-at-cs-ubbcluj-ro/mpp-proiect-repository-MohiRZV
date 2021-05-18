package RESTClient;

import domain.Artist;

public class Main {
    public static void main(String[] args) throws Exception {
        Client client = new Client();

        System.out.println("Search artist with ID 1");
        Artist artist = client.findOne(1L);
        System.out.println(artist);

        System.out.println("Display all artists");
        for(Artist artist1 : client.findAll())
            System.out.println(artist1);

        System.out.println("Add artist");
        Artist artist1 = client.save(new Artist(0L, "Klaus Meine","Rock"));
        System.out.println("Saved artist "+artist1);

        System.out.println("Update artist");
        Artist artist2 = client.update(new Artist(31L, "Klaus Meine","Metal"));
        System.out.println("Updated artist:"+artist2);

        System.out.println("Delete artist");
        client.delete(100L);
        System.out.println("Deleted artist with id 100");

        System.out.println("Display all artists");
        for(Artist artist4 : client.findAll())
            System.out.println(artist4);
    }
}
