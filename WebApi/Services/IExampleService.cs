using System.Collections.Generic;
using WebApi.Models;

namespace WebApi.Services
{
    public interface IExampleService
    {
        List<Example> GetExamples();

        Example GetExample(int id);

        void CreateExample(Example example);

        void UpdateExample( Example example);

        void Delete(int id);
    }
}