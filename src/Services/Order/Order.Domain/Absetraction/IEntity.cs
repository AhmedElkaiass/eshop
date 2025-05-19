namespace Order.Domain.Absetraction;

public interface IEntity<T> : IEntity
{
    public T Id { get; set; }
}
