using FinanceTracker.API.Interfaces;

namespace FinanceTracker.API.Data;

public class UnitOfWork(DataContext dataContext, IUserRepository userRepository, IMessageRepository messageRepository) : IUnitOfWork
{
    public IUserRepository UserRepository => userRepository;

    public IMessageRepository MessageRepository => messageRepository;

    public async Task<bool> Complete()
    {
        return await dataContext.SaveChangesAsync() > 0;
    }

    public bool HasChanges()
    {
        return dataContext.ChangeTracker.HasChanges();
    }
}
