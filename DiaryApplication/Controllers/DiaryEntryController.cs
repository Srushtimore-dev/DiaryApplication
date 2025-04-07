using DiaryApplication.Data;
using DiaryApplication.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DiaryApplication.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class DiaryEntryController : ControllerBase
	{
		private readonly ApplicationDBContext _context;

        public DiaryEntryController(ApplicationDBContext context)
        {
			_context = context;
        }

		[HttpGet("All")]
		public  async Task<ActionResult<IEnumerable<DiaryEntry>>> GetDiaryEntries()
		{
			return await _context.DiaryEntries.ToListAsync();
		}

		[HttpGet("{id}")]
		public async  Task<ActionResult<DiaryEntry>> GetDiaryEntry(int id)
		{
			var diaryEntry = await _context.DiaryEntries.FindAsync(id);

			if (diaryEntry == null)
			{
				return NotFound();
			}

			return diaryEntry;
		}

		[HttpPost("add")]
		public  async Task<ActionResult<DiaryEntry>> PostDiaryEntry(DiaryEntry diaryEntry)
		{
			  
			_context.DiaryEntries.Add(diaryEntry);
			await _context.SaveChangesAsync();
			return CreatedAtAction("GetDiaryEntry", new { id = diaryEntry.Id }, diaryEntry);


		}

		[HttpPut("update/{id}")]
		public async Task<IActionResult> PutDairyEntry(int id, DiaryEntry diaryEntry)
		{
			if (id != diaryEntry.Id)
			{
				return BadRequest();
			}

			_context.Entry(diaryEntry).State = EntityState.Modified;

			try
			{
				await _context.SaveChangesAsync();
			}
			catch (DbUpdateConcurrencyException)
			{
				if (!DiaryEntryExists(id))
				{
					return NotFound();
				}
				else
				{
					throw;
				}
			}
			return NoContent();

		}

		private bool DiaryEntryExists(int id)
		{
			return _context.DiaryEntries.Any(e => e.Id == id);
		}

		[HttpGet("delete/{id}")]
		public async Task<IActionResult> DeleteDiaryEntry(int id)
		{
			var diaryEntry = await _context.DiaryEntries.FindAsync(id);
			if (diaryEntry == null)
			{
				return NotFound();
			}

			_context.DiaryEntries.Remove(diaryEntry);
			await _context.SaveChangesAsync();

			return NoContent();
		}



	
	}
}
