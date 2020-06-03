using System;
using System.Collections.Generic;
using System.Linq;
using WebApi.Data;
using WebApi.Models;

namespace WebApi.Repository
{
    public class ExampleRepository : IExampleRepository
    {
        private ApplicationDbContext _context;

        public ExampleRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public void CreateExample(Example example)
        {
            _context.Examples.Add(example);
            _context.SaveChanges();
        }

        public void Delete(Example example)
        {
            _context.Examples.Remove(example);
            _context.SaveChanges();
        }

        public Example GetExample(int id)
        {
            Example result = _context.Examples.FirstOrDefault(x => x.ID == id);
            return result;
        }

        public List<Example> GetExamples()
        {
            List<Example> result = _context.Examples.ToList();
            return result;
        }

        public void UpdateExample( Example example)
        {
            _context.Examples.Update(example);
            _context.SaveChanges();
        }
    }
}