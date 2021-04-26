package repository;

import domain.Artist;
import jdbcUtils.JdbcUtils;
import org.apache.logging.log4j.LogManager;
import org.apache.logging.log4j.Logger;
import org.hibernate.Session;
import org.hibernate.Transaction;

import java.sql.Connection;
import java.sql.PreparedStatement;
import java.sql.ResultSet;
import java.sql.SQLException;
import java.util.ArrayList;
import java.util.List;
import java.util.Properties;

public class ArtistRepoHibernate implements ArtistRepoInterface{
    private static final Logger logger= LogManager.getLogger();
    public ArtistRepoHibernate(Properties props) {
        logger.info("Initializing ArtistRepo with properties: {}",props);
    }

    @Override
    public Artist add(Artist entity) {
        logger.traceEntry("saving task {}",entity);
        try {
            Hibernater.initialize();
            try (Session session = Hibernater.sessionFactory.openSession()) {
                Transaction tx = null;
                try {
                    tx = session.beginTransaction();
                    Artist artist = new Artist(entity.getId(), entity.getName(), entity.getGenre());
                    session.save(artist);
                    tx.commit();
                } catch (RuntimeException ex) {
                    if (tx != null)
                        tx.rollback();
                }
            }
        }catch (Exception ex){
            logger.error(ex);
            System.err.println("Error DB"+ex);
        }finally {
            Hibernater.close();
            logger.traceExit();

        }
        return null;

    }

    @Override
    public Artist update(Artist entity) {
        logger.traceEntry("update task {}",entity);
        try{
            Hibernater.initialize();
            try(Session session = Hibernater.sessionFactory.openSession()){
                Transaction tx=null;
                try{
                    tx = session.beginTransaction();
                    Artist artist = session.load( Artist.class, entity.getId());
                    artist.setGenre(entity.getGenre());
                    artist.setName(entity.getName());
                    session.update(artist);
                    tx.commit();
                } catch(RuntimeException ex){
                    if (tx!=null)
                        tx.rollback();
                }
            }
        }catch (Exception ex){
            logger.error(ex);
            System.err.println("Error DB"+ex);
        }
        finally {
            Hibernater.close();
            logger.traceExit();
        }
        return null;
    }

    @Override
    public Iterable<Artist> findArtistByGenre(String genre) {
        logger.traceEntry();
        List<Artist> artists=new ArrayList<>();
        try{
            Hibernater.initialize();
            try(Session session = Hibernater.sessionFactory.openSession()) {
                Transaction tx = null;
                try {
                    tx = session.beginTransaction();
                    artists =
                            session.createQuery("from artist where genre="+genre, Artist.class).list();

                    System.out.println(artists.size() + "artists found:");
                    for (Artist m : artists) {
                        System.out.println(m.getName() + ' ' + m.getId());
                    }
                    tx.commit();
                } catch (RuntimeException ex) {
                    if (tx != null)
                        tx.rollback();
                }
            }
        }catch (Exception ex){
            logger.error(ex);
            System.err.println("Error DB"+ex);
        }
        finally {
            Hibernater.close();
            logger.traceExit();
        }
        return artists;
    }

    @Override
    public Artist getOne(Long id) {
        logger.traceEntry();
        Artist artist = null;
        try{
            Hibernater.initialize();
            try(Session session = Hibernater.sessionFactory.openSession()) {
                Transaction tx = null;
                try {
                    tx = session.beginTransaction();
                    artist =
                            session.createQuery("from artist where id="+id, Artist.class).getSingleResult();

                    System.out.println("artist found:"+artist);
                    tx.commit();
                } catch (RuntimeException ex) {
                    if (tx != null)
                        tx.rollback();
                }
            }
        }catch (Exception ex){
            logger.error(ex);
            System.err.println("Error DB"+ex);
        }
        finally {
            Hibernater.close();
            logger.traceExit();
        }
        return artist;
    }

    @Override
    public Iterable<Artist> getAll() {
        logger.traceEntry();
        List<Artist> artists=new ArrayList<>();
        try{
            Hibernater.initialize();
            try(Session session = Hibernater.sessionFactory.openSession()) {
                Transaction tx = null;
                try {
                    tx = session.beginTransaction();
                    artists =
                            session.createQuery("from artist", Artist.class).list();

                    System.out.println(artists.size() + "artists found:");
                    for (Artist m : artists) {
                        System.out.println(m.getName() + ' ' + m.getId());
                    }
                    tx.commit();
                } catch (RuntimeException ex) {
                    if (tx != null)
                        tx.rollback();
                }
            }
        }catch (Exception ex){
            logger.error(ex);
            System.err.println("Error DB"+ex);
        }
        finally {
            Hibernater.close();
            logger.traceExit();
        }
        return artists;
    }

    @Override
    public Artist delete(Long id) {
        logger.traceEntry();
        try{
            Hibernater.initialize();
            try(Session session = Hibernater.sessionFactory.openSession()) {
                Transaction tx = null;
                try {
                    tx = session.beginTransaction();

                    Artist crit = session.createQuery("from artist where id"+id, Artist.class)
                            .setMaxResults(1)
                            .uniqueResult();
                    System.err.println("Stergem artistul " + crit.getId());
                    session.delete(crit);
                    tx.commit();
                } catch (RuntimeException ex) {
                    if (tx != null)
                        tx.rollback();
                }
            }
        }catch (Exception ex){
            logger.error(ex);
            System.err.println("Error DB"+ex);
        }
        finally {
            Hibernater.close();
            logger.traceExit();
        }
        return null;
    }
}
