namespace Lpsgrupo.WebApi.Presentation.Models
{
    public class TaskItem
    {
        public string? Id { get; set; } = Guid.NewGuid().ToString();
        public string? Title { get; set; }
        public string? Description { get; set; }
        public bool IsCompleted { get; set; }
    }
}
