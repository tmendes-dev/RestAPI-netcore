using System.Collections.Generic;
using System.Linq;
using WebApi.Models;

namespace WebApi.Services
{
    public interface ISampleService
    {
        public IQueryable<Sample> GetAll(string sort);

        public Sample Get(int id);

        public  void Create(Sample example);

        public void Update( Sample example);

        public void Delete(int id);
        IQueryable<Sample> Paging(int? pageNumber, int? pageSize);
        IQueryable<Sample> Search(string word);
    }
}