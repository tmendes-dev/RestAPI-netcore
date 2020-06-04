using System.Collections.Generic;
using System.Linq;
using WebApi.Models;

namespace WebApi.Repository
{
    public  interface IExampleRepository
    {
        Example GetExample(int id);

        IQueryable<Example> GetExamples(string sort);

        void CreateExample(Example example);

        void UpdateExample(Example example);

        void Delete(Example example);
    }
}