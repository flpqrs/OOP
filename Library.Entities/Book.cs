namespace Library.Entities
{
    public class BookEntity : IEntity
    {
        public int Id { get; set; }
        public int LibraryId { get; set; }
        public string Title { get; set; }
        public string AuthorsFirstName { get; set; }
        public string AuthorsLastName { get; set; }
        public int Year { get; set; }


    }
}
