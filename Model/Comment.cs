namespace Project_Managment_API.Model
{
    public class Comment
    {
        public int Id { get; set; }
        public string Text { get; set; }
        public DateTime Date { get; set; }
        public int TaskId { get; set; }
        public Task Task { get; set; }
        
    }
}
