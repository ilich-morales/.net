using api.multitracks.com.Dto;
using api.multitracks.com.Extensions;
using api.multitracks.com.Models;
using DataAccess;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.ObjectModel;
using System.Data;

namespace api.multitracks.com.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SongController : ControllerBase
    {
        private readonly SQL sql;
        public SongController(IConfiguration conf)
        {
            string cnn = conf["ConnectionStrings:admin"];
            sql = new SQL(true, cnn);
        }

        [HttpGet("List/{pageSize:int}/{pageIndex:int}/{artistID:int}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ServiceResult<PaginatedList<SongDto>>))]
        public async Task<IActionResult> GetSongs([FromRoute] int pageSize, [FromRoute] int pageIndex, [FromRoute] int artistID)
        {
            try
            {
                sql.Parameters.Add("@artistID", artistID);
                DataTable dt = sql.ExecuteStoredProcedureDT("GetSongs");
                //Ideally we must implement a mapper like AutoMapper or Mapster
                //but for testing purposes, this would be enougth.
                PaginatedList<SongDto> songList = (from d in dt.AsEnumerable()
                                                   select new SongDto
                                                   {
                                                       songID = d.Field<int>("songID"),
                                                       dateCreation = d.Field<DateTime>("dateCreation"),
                                                       albumID = d.Field<int>("albumID"),
                                                       artistID = d.Field<int>("artistID"),
                                                       title = d.Field<string>("title"),
                                                       bpm = d.Field<int>("albumID"),
                                                       timeSignature = d.Field<string>("timeSignature"),
                                                       multitracks = d.Field<bool>("multitracks"),
                                                       customMix = d.Field<bool>("customMix"),
                                                       chart = d.Field<bool>("chart"),
                                                       rehearsalMix = d.Field<bool>("rehearsalMix"),
                                                       patches = d.Field<bool>("patches"),
                                                       songSpecificPatches = d.Field<bool>("songSpecificPatches"),
                                                       proPresenter = d.Field<bool>("proPresenter")
                                                   }).ToPaginatedList(pageSize, pageIndex);
                return Ok(ServiceResult.Success(songList));
            }
            catch (Exception ex)
            {
                Console.WriteLine($"{ex.Message} {ex.InnerException?.Message}");
                return StatusCode(StatusCodes.Status500InternalServerError, ServiceResult.Failed(ErrorResult.InternalServerError));
            }
            
        }
    }
}
