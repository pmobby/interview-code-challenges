namespace OneBeyondApi.Model
{
    public class ReserveBook
    {
        public string BookTitle { get; set; }
        public List<Borrower> Borrowers { get; set; }
        public DateTime BookAvailability { get; set; }
    }
}
