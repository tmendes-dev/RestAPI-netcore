using System.Collections.Generic;
using WebApi.Models;

namespace WebApi.Services
{
    public interface IExampleService
    {
        public List<Example> GetExamples();

        public Example GetExample(int id);

        public  void CreateExample(Example example);

        public void UpdateExample( Example example);

        public void Delete(int id);
    }
}