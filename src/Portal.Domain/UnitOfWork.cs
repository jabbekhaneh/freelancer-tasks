namespace Portal.Domain;

public interface UnitOfWork
{
    Task CommitAsync();
}
