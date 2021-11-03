using Brainer.NetCore.Models;
using Brainer.NetCore.Models.Entities;
using Brainer.NetCore.Repository.interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Brainer.NetCore.Repository
{
    public class QuestionRepository : IQuestionRepository
    {
        private readonly AppDBContext _appDBContext;

        public QuestionRepository(AppDBContext appDBContext)
        {
            this._appDBContext = appDBContext;
        }

        public async Task<Question> AddQuestion(Question question)
        {
            var result = await _appDBContext.Questions.AddAsync(question);
            await _appDBContext.SaveChangesAsync();
            return result.Entity;
        }

        public async Task<bool> QuestionExists(Question question)
        {
            var result = await _appDBContext.Questions.FirstOrDefaultAsync(q => q.Text == question.Text && q.Categories.Equals(question.Categories));
            var exists = result == null ? true : false;
            return exists;
        }

        public Task DeleteQuestion(int id)
        {
            throw new NotImplementedException();
        }

        public Task<Question> GetQuestion(int id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Question>> GetQuestions()
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Question>> Search(string name)
        {
            throw new NotImplementedException();
        }

        public Task<Question> UpdateQuestion(Question user)
        {
            throw new NotImplementedException();
        }
    }
}
