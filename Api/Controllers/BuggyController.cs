using Infrastructure.Data;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
	public class BuggyController : BaseApiController
	{
		private readonly StoreContext _context;

		public BuggyController(StoreContext _context)
		{
			this._context = _context;
		}

		[HttpGet("notfound")]
		public IActionResult GetNotFoundRequest()
		{
			var thing = _context.Products.Find(100);
			if (thing is null)
			{
				return NotFound();
			}

			return Ok();
		}

		[HttpGet("servererror")]
		public IActionResult GetServerError()
		{
			var thing = _context.Products.Find(100);
			var tts = thing.ToString();

			return Ok();
		}

		[HttpGet("badrequest")]
		public IActionResult GetBadRequest()
		{

			return BadRequest();
		}

		[HttpGet("badrequest/{id}")]
		public IActionResult GetBadRequest(int id)
		{
			return BadRequest();
		}
	}
}
