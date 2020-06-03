using System.Collections.Generic;
using WebApi.Models;

namespace WebApi.Services
{
    internal interface IExampleService
    {
        List<Example> GetExamples();

        Example GetExample(int id);

        void CreateExample(Example example);

        void UpdateExample(int Id, Example example);

        void Delete(int id);
    }
}