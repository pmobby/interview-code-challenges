using Microsoft.EntityFrameworkCore;
using OneBeyondApi.Model;

namespace OneBeyondApi.DataAccess
{
    public class BorrowerRepository : IBorrowerRepository
    {
        public BorrowerRepository()
        {
        }
        public List<Borrower> GetBorrowers()
        {
            using (var context = new LibraryContext())
            {
                var list = context.Borrowers
                    .ToList();
                return list;
            }
        }

        public Guid AddBorrower(Borrower borrower)
        {
            using (var context = new LibraryContext())
            {
                context.Borrowers.Add(borrower);
                context.SaveChanges();
                return borrower.Id;
            }
        }

        public List<BorrowersAndBooksOnLoan> GetBorrowersWithBooksOnLoan()
        {
            using (var context = new LibraryContext())
            {
                var borrowersWithBooksOnLoan = context.Catalogue
                                                 .Where(s => s.LoanEndDate > DateTime.Now)
                                                 .Include(s => s.Book)
                                                 .Include(s => s.OnLoanTo)
                                                 .Select(s => new BorrowersAndBooksOnLoan
                                                 {
                                                     Name = s.OnLoanTo.Name,
                                                     EmailAddress = s.OnLoanTo.EmailAddress,
                                                     BookTitle = s.Book.Name
                                                 }).ToList();
                return borrowersWithBooksOnLoan;

            }
        }

        public string ReturnLoanedBook(Book book)
        {
            using (var context = new LibraryContext())
            {
                var loanedBook = context.Catalogue
                                        .Include(s => s.Book)
                                        .Where(s => s.OnLoanTo != null && s.Book.Name == book.Name)
                                        .FirstOrDefault();

                if (loanedBook == null) return "No record for book";

                if (DateTime.Now > loanedBook.LoanEndDate)
                {
                    loanedBook.OnLoanTo.Fine.Amount = 5.99m;
                    return $"Due date exceeded, you are fined";
                }

                loanedBook.Book = book;
                loanedBook.LoanEndDate = null;
                loanedBook.OnLoanTo = null;

                context.Catalogue.Update(loanedBook);
                context.SaveChanges();
                return "book returned";

            }
        }

        public string ReserveBook(ReserveBook bookToReserve)
        {
            var book = new Book
            {
                Name = bookToReserve.BookTitle
            };

            var bookStock = new BookStock
            {
                LoanEndDate = DateTime.Now.Date.AddDays(3)
            };

            if (bookToReserve.BookAvailability > bookStock.LoanEndDate)
            {
                using (var context = new LibraryContext())
                {
                    bookStock.Book = book;

                    foreach (var borrower in bookToReserve.Borrowers)
                    {
                        bookStock.OnLoanTo = borrower;
                    }
                    context.Catalogue.Update(bookStock);
                    context.SaveChanges();
                    return "book will be available";
                }
            }
            else
            {
                return "book won't be available for that date";
            }

        }
    }
}
