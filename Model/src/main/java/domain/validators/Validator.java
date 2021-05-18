package domain.validators;


import java.io.Serializable;

public interface Validator<T> extends Serializable {
    void validate(T entity) throws ValidationException;
}