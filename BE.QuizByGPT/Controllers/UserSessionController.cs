using AutoMapper;
using BE.QuizByGPT.BLL.Models;
using BE.QuizByGPT.DAL.Interfaces;
using BE.QuizByGPT.Interfaces;
using BE.QuizByGPT.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BE.QuizByGPT.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserSessionController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IUserSessionService _userSessionService;

        public UserSessionController(IUserSessionService userSessionService, IMapper mapper)
        {
            _userSessionService = userSessionService;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            if (Guid.TryParse(Request.Cookies["UserSessionId"], out var userSessionId))
            {
                var userSession = await _userSessionService.Get(userSessionId);

                Response.Cookies.Append("UserSessionId", userSession.Id.ToString("D"));
                Response.Cookies.Append("UserSessionName", userSession.Name);

                return Ok(_mapper.Map<UserSessionModelGetDto>(userSession));
            }

            return BadRequest("UserSessionId invalid.");
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(Guid id)
        {
            var userSession = await _userSessionService.Get(id);

            Response.Cookies.Append("UserSessionId", userSession.Id.ToString("D"));
            Response.Cookies.Append("UserSessionName", userSession.Name);

            return Ok(userSession);
        }

        [HttpPost]
        public async Task<IActionResult> Create(UserSessionModelGetDto userSessionGetDto)
        {
            var createdUserSession = await _userSessionService.Create(_mapper.Map<UserSessionModel>(userSessionGetDto));

            Response.Cookies.Append("UserSessionId", createdUserSession.Id.ToString("D"));
            Response.Cookies.Append("UserSessionName", createdUserSession.Name);

            return CreatedAtAction(nameof(Get), new { id = createdUserSession.Id }, _mapper.Map<UserSessionModelGetDto>(createdUserSession));
        }
    }
}
