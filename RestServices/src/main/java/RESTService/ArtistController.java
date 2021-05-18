package RESTService;

import domain.Artist;
import org.springframework.http.HttpStatus;
import org.springframework.http.ResponseEntity;
import org.springframework.web.bind.annotation.*;
import repository.ArtistRepo;

import java.io.IOException;
import java.util.List;
import java.util.Properties;
import java.util.stream.StreamSupport;

@RestController
@RequestMapping("/artists")
public class ArtistController {
    private ArtistRepo repository = null;
    public ArtistController(){
        Properties clientProps=new Properties();
        try {
            clientProps.load(ArtistRepo.class.getResourceAsStream("/db.properties"));
            System.out.println("Client properties set. ");
            clientProps.list(System.out);
        } catch (IOException e) {
            System.err.println("Cannot find db.properties "+e);
            return;
        }
        repository = new ArtistRepo(clientProps);
    }

    @RequestMapping(method = RequestMethod.POST)
    public Artist save(@RequestBody Artist artist){
        repository.add(artist);
        return artist;
    }

    @RequestMapping(value="/{id}", method = RequestMethod.GET)
    public ResponseEntity<?> findOne(@PathVariable Long id){
        Artist artist = repository.getOne(id);
        if(artist==null)return new ResponseEntity<String>("Artist not found", HttpStatus.NOT_FOUND);
        else return new ResponseEntity<Artist>(artist, HttpStatus.OK);
    }

    @RequestMapping(method = RequestMethod.GET)
    public Artist[] findAll(){
        int size = (int) StreamSupport.stream(repository.getAll().spliterator(), false).count();
        Artist[] result = new Artist[size];
        result = ((List<Artist>) repository.getAll()).toArray(result);
        return result;
    }

    @RequestMapping(method = RequestMethod.PUT)
    public Artist update(@RequestBody Artist artist){
        return repository.update(artist);
    }

    @RequestMapping(value = "/{id}", method = RequestMethod.DELETE)
    public ResponseEntity<?> delete(@PathVariable Long id){
        Artist artist = repository.delete(id);
        if(artist==null)return new ResponseEntity<String>("Artist not found", HttpStatus.NOT_FOUND);
        else return new ResponseEntity<Artist>(artist, HttpStatus.OK);

    }

}
