package domain;

public class Account extends Entity<String>{
    private Long idUser;
    private String password;

    public Account(String username, Long idUser , String password) {
        super(username);
        this.idUser=idUser;
        this.password = password;
    }

    public Long getIdUser() {
        return idUser;
    }

    public String getPassword() {
        return password;
    }

    public String getUsername(){
        return super.getId();
    }

    @Override
    public String toString() {
        return getUsername();
    }
}
