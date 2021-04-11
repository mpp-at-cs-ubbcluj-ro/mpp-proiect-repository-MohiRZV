package domain.validators;


public interface Validator<T> extends Serializable{
    void validate(T entity) throws ValidationException;
}