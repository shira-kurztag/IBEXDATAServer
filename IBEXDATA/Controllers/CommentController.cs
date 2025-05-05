using AutoMapper;
using Common.DTO;
using Common;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Service;
using IBEXDATA.Models;

namespace Application.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CommentController : ControllerBase
    {
        private readonly ICommentService _CommentService;
        private readonly ILogger<CommentController> _logger;
        private readonly IMapper _mapper;


        public CommentController(ICommentService CommentService, ILogger<CommentController> logger, IMapper mapper)
        {
            _CommentService = CommentService;
            _logger = logger;
            _mapper = mapper;
        }

        [HttpPost]
        public async Task<IActionResult> Add([FromBody] Comment comment)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var commentnew = await _CommentService.Add(comment);

            if (commentnew != null)
            {
                _logger.LogInformation("Successfully added Comment: {CommentText}", comment.CommentText);
               return Ok(commentnew);
            }

            _logger.LogWarning("Failed to add the Comment: {CommentText}", comment.CommentText);
            return BadRequest($"The Comment {comment.CommentText} not successfully added");
        }


        [Route("GetComment/{id}")]
        [HttpGet]
        public async Task<ActionResult<Comment>> GetComment(long id)
        {
            var comment = await _CommentService.GetCommentById(id);
            if (comment == null)
            {
                return NotFound();
            }
            return Ok(comment);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateComment([FromRoute] long id, [FromBody] Comment comment)
        {
            try
            {
                var updatedComment = await _CommentService.UpdateComment(comment, id);
                return Ok(updatedComment);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }


        [HttpDelete("{Id}")]
        public IActionResult DeleteComment(long Id)
        {
            _CommentService.DeleteCommentById(Id);
            return NoContent();
        }
    }
}
