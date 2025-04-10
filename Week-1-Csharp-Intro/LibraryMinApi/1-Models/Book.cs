namespace Library.Models;

public class Book
{
    public Guid BookId { get; set; } = Guid.NewGuid();
    public string Title { get; set; } = string.Empty;
    public string Author { get; set; } = string.Empty;

    //flag if my book is available
    public bool IsAvailable { get; set; } = true;
}
