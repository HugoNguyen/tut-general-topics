
using Microsoft.AspNetCore.Mvc;
using RoomBookingApp.Core.Models;
using RoomBookingApp.Core.Processors;

namespace RoomBookingApp.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class RoomBookingController : ControllerBase
    {
        private IRoomBookingRequestProcessor _roomBookingRequestProcessor;

        public RoomBookingController(IRoomBookingRequestProcessor roomBookingRequestProcessor)
        {
            _roomBookingRequestProcessor = roomBookingRequestProcessor;
        }

        [HttpPost]
        public async Task<IActionResult> BookRoom(RoomBookingRequest request)
        {
            if (ModelState.IsValid)
            {
                var result = _roomBookingRequestProcessor.BookRoom(request);
                if(result.Flag == Core.Enums.BookingResultFlag.Success)
                {
                    return Ok(result);
                }

                ModelState.AddModelError(nameof(RoomBookingRequest.Date), "No rooms Available For Given Date");
            }

            return BadRequest(ModelState);
        }
    }
}
