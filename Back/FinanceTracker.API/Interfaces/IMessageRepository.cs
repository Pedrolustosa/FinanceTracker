using FinanceTracker.API.DTOs;
using FinanceTracker.API.Entities;
using FinanceTracker.API.Helpers;

namespace FinanceTracker.API.Interfaces;

public interface IMessageRepository
{
    void AddMessage(Message message);
    void DeleteMessage(Message message);
    Task<Message?> GetMessage(int id);
    Task<PagedList<MessageDto>> GetMessagesForUser(MessageParams messageParams);
    Task<IEnumerable<MessageDto>> GetMessageThread(string currentUsername, string recipientUsername);
    Task<bool> SaveAllAsync();
}