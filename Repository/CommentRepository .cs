using Project_Management_API.Data;
using Project_Managment_API.Interfaces;
using Project_Managment_API.Model;
using System.Collections.Generic;
using System.Linq;

namespace Project_Managment_API.Repositories
{
    public class CommentRepository : ICommentRepository
    {
        private readonly ProjectManagementDbContext _context;

        public CommentRepository(ProjectManagementDbContext context)
        {
            _context = context;
        }

        public IEnumerable<Comment> GetAllComments()
        {
            return _context.Comments.ToList();
        }

        public Comment GetCommentById(string id)
        {
            return _context.Comments.FirstOrDefault(c => c.Id == id);
        }

        public void AddComment(Comment comment)
        {
            _context.Comments.Add(comment);
        }

        public void UpdateComment(Comment comment)
        {
            _context.Comments.Update(comment);
        }

        public void DeleteComment(Comment comment)
        {
            _context.Comments.Remove(comment);
        }

        public bool SaveChanges()
        {
            return (_context.SaveChanges() >= 0);
        }
    }
}
