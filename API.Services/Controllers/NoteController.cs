using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication;
using Microsoft.EntityFrameworkCore;
using Todo.API.Models;
using Todo.API.Data.Entities;
using Todo.API.Utilities;
using Newtonsoft.Json;

namespace Todo.API.Controllers
{
    //[Authorize]
    [ApiController]
    [Route("api/Note")]
    public class NoteController : ControllerBase
    {
        private readonly IRepositoryWrapper _repository;

        public NoteController(IRepositoryWrapper repository)
        {
            _repository = repository;
        }

        [HttpGet]
        public async Task<IActionResult> GetNoteList([FromQuery] QueryParameters queryParameters)
        {
            var model = await _repository.Notes.GetNoteList(queryParameters);
            return model == null ? NotFound() : Ok(model);
        }

        [HttpGet]
        [Route("{NoteId}")]
        public async Task<IActionResult> GetNote(int NoteId)
        {
            var model = await _repository.Notes.GetNoteById(NoteId);
            return model == null ? NotFound() : Ok(model);
        }

        [HttpPost]
        public async Task<IActionResult> CreateNote([FromBody] NoteModel noteModel)
        {
            if (!(ModelState.IsValid))
            {
                return BadRequest("Invalid");
            }
            else
            {
                try
                {
                    await _repository.Notes.CreateNewNote(noteModel);
                }
                catch (Exception e)
                {
                    return BadRequest(e);
                }

                return Ok();
            }
        }

        [HttpPut]
        [Route("{NoteId}")]
        public async Task<IActionResult> UpdateNote(int NoteId, [FromBody] NoteModel noteModel)
        {
            if (!(ModelState.IsValid))
            {
                return BadRequest("Invalid");
            }
            else
            {
                // Ensure NoteId, OwnerId, StatusId is valid
                if (await _repository.Notes.IsValidId(NoteId) &&
                    await _repository.Users.IsValidId(noteModel.OwnerId) &&
                    await _repository.Users.IsValidId(noteModel.StatusId))
                {
                    var model = await _repository.Notes.UpdateNoteById(NoteId, noteModel);
                    return Ok(model);
                }
                else
                {
                    return NotFound();
                }
            }
        }

        [HttpDelete]
        [Route("{NoteId}")]
        public async Task<IActionResult> DeleteNote(int NoteId)
        {
            if (await _repository.Notes.IsValidId(NoteId))
            {
                try
                {
                    await _repository.Notes.DeleteNoteById(NoteId);
                }
                catch (Exception e)
                {
                    return BadRequest(e);
                }
                return Ok();
            }
            else
            {
                return NotFound();
            }
        }

        [HttpGet]
        [Route("{NoteId}/Collaborators")]
        public async Task<IActionResult> GetNoteCollaboratorList(int NoteId)
        {
            if (await _repository.Notes.IsValidId(NoteId))
            {
                var model = await _repository.Collaborators.GetCollaboratorListByNoteId(NoteId);
                return Ok(model);
            }
            else
            {
                return NotFound();
            }
        }


    }
}