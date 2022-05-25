using Home_Sales.Model;
using Home_Sales_BAL;
using Microsoft.AspNetCore.Mvc;

namespace Home_Sales.Controllers
{
    [Route("api/file")]
    public class FileController : Controller
    {
        private IHomeSalesDataHandler HomeSalesDataHandler;

        public FileController(IHomeSalesDataHandler homeSalesDataHandler)
        {
            HomeSalesDataHandler = homeSalesDataHandler;
        }

        [HttpPost("upload")]
        public async Task<IActionResult> UplocadFile([FromBody] IFormFile file)
        {
            try
            {
                await HomeSalesDataHandler.UploadHomeSalesFile(Request.Body);
                return Ok();
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }

        [HttpGet("getTopDistinctSchools/{month}/{year}")]
        public ActionResult<List<HomeSalesResponse>> GetTopDistinctSchools(int month, int year)
        {
            try
            {
                var data = HomeSalesDataHandler.GetTopDistinctSchools(month, year);
                return Ok(data);
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }

        [HttpGet("getAverageNumberOfDaysTookToSellProperty/{month}/{year}")]
        public ActionResult<int> GetAverageNumberOfDaysTookToSellProperty(string schoolName, int month, int year)
        {
            try
            {
                var avgNumberOfDays = HomeSalesDataHandler.GetAverageNumberOfDaysTookToSellProperty(schoolName, month, year);
                return Ok(avgNumberOfDays);
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }
    }
}