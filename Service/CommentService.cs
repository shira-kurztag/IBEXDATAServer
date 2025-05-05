using DB;
using IBEXDATA.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service
{
    public class CommentService : ICommentService
    {
        ICommentDB _CommentDB;

        public CommentService(ICommentDB CommentDB)
        {
            _CommentDB = CommentDB;
        }

        public async Task<Comment> Add(Comment Comment)
        {
            return await _CommentDB.Add(Comment);
        }

    
        public async Task<Comment> GetCommentById(long id)
        {
            return await _CommentDB.GetCommentById(id);
        }

        public async Task<Comment> UpdateComment(Comment comment, long id)
        {
            return await _CommentDB.UpdateComment(comment, id);
        }

        public void DeleteCommentById(long Id)
        {
            _CommentDB.DeleteCommentById(Id);
        }


    }
}
