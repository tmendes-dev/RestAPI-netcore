using System.Collections.Generic;
using System.Linq;
using WebApi.Models;

namespace WebApi.Services
{
    public interface IExampleService
    {
        public IQueryable<Example> GetExamples(string sort);

        public Example GetExample(int id);

        public  void CreateExample(Example example);

        public void UpdateExample( Example example);

        public void Delete(int id);
    }
}