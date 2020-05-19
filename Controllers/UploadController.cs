using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.IO;

namespace ExplorationUploadFile.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UploadController : ControllerBase
    {
        [HttpPost("Single")]
        [RequestSizeLimit(8000000)]
        public async Task<ActionResult> Single(IFormFile file)
        {
            string[] permittedExtensions = { ".csv" };
            var ext = Path.GetExtension(file.FileName).ToLowerInvariant();
            if (string.IsNullOrEmpty(ext) || !permittedExtensions.Contains(ext))
                return BadRequest("Only Allowed CSV File");

            var filePath = Path.Combine(@"c:\Project\testing2", file.FileName);
            //var filePath = Path.Combine("Upload", file.FileName);
            using (var stream = System.IO.File.Create(filePath))
            {
                await file.CopyToAsync(stream);
            }
            return Ok();
        }
    }
}