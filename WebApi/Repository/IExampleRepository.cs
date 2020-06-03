using System.Collections.Generic;
using WebApi.Models;

namespace WebApi.Repository
{
    internal interface IExampleRepository
    {
        Example GetExample(int id);

        List<Example> GetExamples();

        void CreateExample(Example example);

        void UpdateExample( Example example);

        void Delete(Example example);
    }
}