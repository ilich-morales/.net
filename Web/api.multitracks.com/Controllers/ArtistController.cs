using api.multitracks.com.Dto;
using api.multitracks.com.Models;
using DataAccess;
using Microsoft.AspNetCore.Mvc;
using System.Data;

namespace api.multitracks.com.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ArtistController : ControllerBase
    {
        private readonly SQL sql;
        public ArtistController(IConfiguration conf)
        {
            string cnn = conf["ConnectionStrings:admin"];
            sql = new SQL(true, cnn);
        }

        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ServiceResult<IList<ArtistFullDto>>))]
        [HttpGet("Search/{prefixText}")]
        public async Task<IActionResult> SearchAuthors([FromRoute] string prefixText)
        {
            try
            {
                if (string.IsNullOrEmpty(prefixText))
                    return BadRequest(ServiceResult.Failed(ErrorResult.BadRequest));

                sql.Parameters.Clear();
                sql.Parameters.Add("@Name", prefixText);
                DataTable dt = sql.ExecuteStoredProcedureDT("SearchArtistByName", false);
                IList<ArtistFullDto> artistList = (from row in dt.AsEnumerable()
                                               select new ArtistFullDto
                                               {
                                                   artistID = row.Field<int>("artistID"),
                                                   dateCreation = row.Field<DateTime>("dateCreation"),
                                                   title = row.Field<string>("title"),
                                                   biography = row.Field<string>("biography"),
                                                   imageURL = row.Field<string>("imageURL"),
                                                   heroURL = row.Field<string>("heroURL")
                                               }).ToList();
                return Ok(ServiceResult.Success(artistList));
            }
            catch (Exception ex)
            {
                Console.WriteLine($"{ex.Message} {ex.InnerException?.Message}");
                return StatusCode(StatusCodes.Status500InternalServerError, ServiceResult.Failed(ErrorResult.InternalServerError));
            }            
        }

        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ServiceResult<ArtistDto>))]
        [HttpPost("Add")]
        public async Task<IActionResult> AddArtists([FromBody] ArtistDto artist)
        {
            try
            {
                sql.OpenConnection();
                sql.BeginTransaction();
                
                sql.Parameters.Add("@title", artist.title);
                sql.Parameters.Add("@biography", artist.biography);
                sql.Parameters.Add("@imageURL", artist.imageURL);
                sql.Parameters.Add("@heroURL", artist.heroURL);

                sql.Execute("INSERT INTO [dbo].[Artist] ([dateCreation], [title], [biography], [imageURL], [heroURL]) VALUES(GETDATE(), @title, @biography, @imageURL, @heroURL)");

                sql.Commit();
                sql.CloseConnection();
                return Ok(ServiceResult.Success(artist));
            }
            catch (Exception ex)
            {
                sql.Rollback();
                Console.WriteLine($"{ex.Message} {ex.InnerException?.Message}");
                return StatusCode(StatusCodes.Status500InternalServerError, ServiceResult.Failed(ErrorResult.InternalServerError));                
            }
        }
    }
}
