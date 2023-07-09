using Project_Managment_API.Model;
using System.Collections.Generic;

namespace Project_Managment_API.Interfaces
{
    public interface ICommentRepository
    {
        IEnumerable<Comment> GetAllComments();
        Comment GetCommentById(string id);
        void AddComment(Comment comment);
        void UpdateComment(Comment comment);
        void DeleteComment(Comment comment);
        bool SaveChanges();
    }
}
