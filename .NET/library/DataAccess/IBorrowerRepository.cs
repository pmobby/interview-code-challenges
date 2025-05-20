using OneBeyondApi.Model;

namespace OneBeyondApi.DataAccess
{
    public interface IBorrowerRepository
    {
        public List<Borrower> GetBorrowers();

        public Guid AddBorrower(Borrower borrower);

        public List<BorrowersAndBooksOnLoan> GetBorrowersWithBooksOnLoan();

        public string ReturnLoanedBook(Book book);

        public string ReserveBook(ReserveBook book);
    }
}
