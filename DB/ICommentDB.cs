using IBEXDATA.Models;

namespace DB
{
    public interface ICommentDB
    {
        Task<Comment> Add(Comment comment);
        Task<Comment> GetCommentById(long id);
        Task<Comment> UpdateComment(Comment comment, long id);
        void DeletCommentById(long id);
        void DeleteCommentById(long id);
    }
}