package domain.validators;

import java.io.Serializable;

public class ValidationException extends Exception implements Serializable {
    public ValidationException(String message) {
        super(message);
    }
}
