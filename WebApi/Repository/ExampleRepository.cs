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

        public IQueryable<Example> GetExamples(string sort)
        {
            IQueryable<Example> result ;

            switch (sort)
            {
                case "desc":
                    result = _context.Examples.OrderByDescending(e=> e.Date);
                    break;
                case "asc":
                    result = _context.Examples.OrderBy(e => e.Date);
                    break;
                default:
                    result = _context.Examples;
                    break;
            }

            return result;
        }

        public void UpdateExample( Example example)
        {
           var obj =  _context.Examples.Find(example.ID);
            obj.Name = example.Name;
            obj.Date = example.Date;
            obj.Available = example.Available;
            obj.Quantity = example.Quantity;
            _context.SaveChanges();
        }
    }
}