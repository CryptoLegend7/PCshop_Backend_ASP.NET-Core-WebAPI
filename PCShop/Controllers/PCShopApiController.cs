using Microsoft.AspNetCore.Mvc;
using PCShop.Services;
using PCShop.Models.Request;


namespace PCShop.Controllers
{
    // API Controller handle 
    [ApiController]
    [Route("[controller]")]
    public class PCShopApiController : ControllerBase
    {
        [HttpGet(Name = "GetPC")]
        public JsonResult Get(string keyword = "",int page = 0, int limit = 0)
        {
            try
            {
                return new JsonResult(new
                {
                    status = 200,
                    msg = "Success",
                    data = DBAdapter.Instance.GetPCByParam(keyword, page, limit),
                    totalSize = DBAdapter.Instance.GetTotalPCCount(keyword)
                }); ;
            }
            catch (Exception ex)
            {
                // Handle the exception here
                Console.WriteLine("An error occurred: " + ex.Message);
                return new JsonResult(new
                {
                    status = 400,
                    msg = "Failed",
                    data = "",
                    totalSize = 0
                }); ;
            }
        }
        [HttpPost(Name = "UpdatePC")]
        public JsonResult Set(EditRequest request)
        {
            try
            {
                DBAdapter.Instance.UpdatePCByParam(request).ToString();
                return new JsonResult(new
                {
                    status = 200,
                    msg = "Success"
                }); ;
            }
            catch (Exception ex)
            {
                // Handle the exception here
                Console.WriteLine("An error occurred: " + ex.Message);
                return new JsonResult(new
                {
                    status = 400,
                    msg = "Failed"
                }); ;
            }
        }
    }
}
