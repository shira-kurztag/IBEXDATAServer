using IBEXDATA.Models;
using Microsoft.EntityFrameworkCore;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DB
{
    public class CommentDB : ICommentDB
    {
        private readonly dbContext _context;
        private static readonly Serilog.ILogger _logger = Log.ForContext<ProjectDB>(); // Create a logger instance

        public CommentDB(dbContext context)
        {
            _context = context;
        }
        public async Task<Comment> Add(Comment comment)
        {
            try
            {
                await _context.Comments.AddAsync(comment);
                await _context.SaveChangesAsync();

                if (comment != null)
                {
                    _logger.Information("Successfully added a new comment.");
                    return comment;
                }
                else
                {
                    _logger.Warning("comment was not added.");
                    return null;
                }
            }
            catch (Exception ex)
            {
                _logger.Error(ex, "Error in Add method of Add in CommentDB.");
                return null;
            }
        }
        public async Task<Comment> UpdateComment(Comment comment, long id)
        {
            try
            {
                var existingComment = await _context.Comments.FindAsync(id);
                if (existingComment != null)
                {
                    _context.Entry(existingComment).CurrentValues.SetValues(comment);
                    await _context.SaveChangesAsync();

                    return existingComment;
                }
                else
                {
                    return null;
                }
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<Comment> GetCommentById(long id)
        {
            return await _context.Comments.FindAsync(id);
        }



        public void DeleteCommentById(long Id)
        {
            Comment? comment = _context.Comments.Find(Id);
            if (comment != null)
            {
                _context.Comments.Remove(comment);
                _context.SaveChanges();
            }
        }

        public void DeletCommentById(long id)
        {
            throw new NotImplementedException();
        }
    }
}
