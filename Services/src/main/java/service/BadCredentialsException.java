package service;

import java.io.Serializable;

public class BadCredentialsException extends Exception implements Serializable {
    public BadCredentialsException(String message) {
        super(message);
    }
}
