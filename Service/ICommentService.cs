using IBEXDATA.Models;

namespace Service
{
    public interface ICommentService
    {
      Task<Comment> Add(Comment comment);
      Task<Comment> GetCommentById(long id);
      Task<Comment> UpdateComment(Comment comment, long id);
        void DeleteCommentById(long id);
    }
}