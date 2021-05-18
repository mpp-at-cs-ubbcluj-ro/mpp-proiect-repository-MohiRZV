package RESTClient;

import domain.Artist;
import org.springframework.web.client.RestTemplate;

import java.util.concurrent.Callable;

public class Client {
    String URL = "http://localhost:8080/artists";
    RestTemplate template = new RestTemplate();

    private <T> T execute(Callable<T> callable) throws Exception {
        return callable.call();
    }

    public Artist save(Artist artist) throws Exception {
        return execute(() -> template.postForObject(URL, artist, Artist.class));
    }

    public Artist findOne(Long id) throws Exception {
        return execute(() -> template.getForObject(URL + '/' + id.toString(), Artist.class));
    }

    public Artist[] findAll() throws Exception {
        return execute(() -> template.getForObject(URL, Artist[].class));
    }

    public Artist update(Artist artist) throws Exception {
        execute(() -> {
            template.put(URL, artist);
            return null;
        });
        return findOne(artist.getId());
    }

    public void delete(Long id) throws Exception {
        execute(() -> {
            template.delete(URL + '/' + id.toString());
            return  null;
        });
    }
}
