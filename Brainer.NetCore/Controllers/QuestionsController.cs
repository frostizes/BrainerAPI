using Brainer.NetCore.Models.Entities;
using Brainer.NetCore.Repository.interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Brainer.NetCore.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class QuestionsController : ControllerBase
    {
        private readonly IQuestionRepository _questionRepository;

        public QuestionsController(IQuestionRepository questionRepository)
        {
            this._questionRepository = questionRepository;
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<Question>> GetQuestion(int id)
        {
            try
            {
                var result = await _questionRepository.GetQuestion(id);
                if (result == null)
                {
                    return NotFound();
                }
                else
                {
                    return result;
                }
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error");
            }
        }

        [HttpPost]
        public async Task<ActionResult> CreateQuestion(Question question)
        {
            if(question == null)
            {
                return BadRequest("no question");
            }
            bool questionAlreadyExists = await _questionRepository.QuestionExists(question);
            if (questionAlreadyExists)
            {
                return BadRequest("question already exists");
            }

            var createdQuestion = await _questionRepository.AddQuestion(question);
            return CreatedAtAction(nameof(GetQuestion),
                    new { id = createdQuestion.Id }, createdQuestion);
        }
    }
}
