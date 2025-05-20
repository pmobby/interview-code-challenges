using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OneBeyondApi.DataAccess;
using OneBeyondApi.Model;

namespace OneBeyondApi.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class OnLoanController : ControllerBase
    {
        private readonly ILogger<BorrowerController> _logger;
        private readonly IBorrowerRepository _borrowerRepository;

        public OnLoanController(ILogger<BorrowerController> logger, IBorrowerRepository borrowerRepository)
        {
            _logger = logger;
            _borrowerRepository = borrowerRepository;
        }

        [HttpGet]
        [Route("GetBorrowersWithBooksOnLoan")]
        public IList<BorrowersAndBooksOnLoan> Get()
        {
            return _borrowerRepository.GetBorrowersWithBooksOnLoan();
        }

        [HttpPost]
        [Route("ReturnLoanedBooks")]
        public string Post(Book book)
        {
            return _borrowerRepository.ReturnLoanedBook(book);
        }

        [HttpPost]
        [Route("ReserveBook")]
        public string Post(ReserveBook reserveBook)
        {
            return _borrowerRepository.ReserveBook(reserveBook);
        }
    }
}
