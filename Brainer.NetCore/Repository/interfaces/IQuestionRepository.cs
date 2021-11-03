using Brainer.NetCore.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Brainer.NetCore.Repository.interfaces
{
    public interface IQuestionRepository
    {
        Task<IEnumerable<Question>> Search(string name);
        Task<IEnumerable<Question>> GetQuestions();
        Task<Question> GetQuestion(int id);
        Task<Question> AddQuestion(Question question);
        Task<Question> UpdateQuestion(Question question);
        Task<bool> QuestionExists(Question question);
        Task DeleteQuestion(int id);
    }
}
