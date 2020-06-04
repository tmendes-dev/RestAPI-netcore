using System.Linq;
using WebApi.Data;
using WebApi.Models;

namespace WebApi.Repository
{
    public class SampleRepository : ISampleRepository
    {
        private ApplicationDbContext _context;

        public SampleRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public void Create(Sample example)
        {
            _context.Examples.Add(example);
            _context.SaveChanges();
        }

        public void Delete(Sample example)
        {
            _context.Examples.Remove(example);
            _context.SaveChanges();
        }

        public Sample Get(int id)
        {
            Sample result = _context.Examples.FirstOrDefault(x => x.ID == id);
            return result;
        }

        public IQueryable<Sample> GetAll(string sort)
        {
            IQueryable<Sample> result;

            switch (sort)
            {
                case "desc":
                    result = _context.Examples.OrderByDescending(e => e.Date);
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

        public IQueryable<Sample> Search(string word)
        {
            IQueryable<Sample> result = _context.Examples.Where(x => x.Name.Contains(word));
            return result;
        }

        public void Update(Sample example)
        {
            var obj = _context.Examples.Find(example.ID);
            obj.Name = example.Name;
            obj.Date = example.Date;
            obj.Available = example.Available;
            obj.Quantity = example.Quantity;
            _context.SaveChanges();
        }
    }
}