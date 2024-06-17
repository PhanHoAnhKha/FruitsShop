using WebFruit.Models;
using WebFruit.DTOs;

namespace WebFruit.Interfaces
{
    public interface INewRepository
    {
        Task<List<New>> GetNewsAsync();
        Task<New> GetNewsByIdAsync(int id);
        Task<bool> AddNewsAsync(NewDTO newsDto);
        Task<bool> UpdateNewsAsync(int id, NewDTO newsDto);
        Task<bool> DeleteNewsAsync(int id);
        Task<bool> AddComment(int newId, CommentDTO commentDTO);
        Task<New> GetNewWithComments(int id);
    }
}
