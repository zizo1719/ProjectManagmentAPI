namespace Project_Managment_API.Model
{
    public class Attachment
    {
        public int Id { get; set; }
        public string FileName { get; set; }
        public string FileType { get; set; }
        public long FileSize { get; set; }
        public int TaskId { get; set; }
        public Task Task { get; set; }
    }
}
