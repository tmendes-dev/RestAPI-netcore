using System;
using System.Linq;
using WebApi.Models;
using WebApi.Repository;

namespace WebApi.Services
{
    public class ExampleService : IExampleService
    {
        private IExampleRepository _respository;

        public ExampleService(IExampleRepository respository)
        {
            _respository = respository;
        }

        public void CreateExample(Example example)
        {
            Example result = _respository.GetExample(example.ID);

            if (result != null)
            {
                throw new ApplicationException("Example Already Exisits.");
            }

            _respository.CreateExample(example);
        }

        public void Delete(int id)
        {
            Example result = _respository.GetExample(id);

            if (result == null)
            {
                throw new ApplicationException("Example does not exists.");
            }
            _respository.Delete(result);
        }

        public Example GetExample(int id)
        {
            Example result = _respository.GetExample(id);

            if (result == null)
            {
                throw new ApplicationException("Example does not exists.");
            }

            return result;
        }

        public IQueryable<Example> GetExamples(string sort)
        {
            IQueryable<Example> result = _respository.GetExamples(sort);

            return result;
        }

        public IQueryable<Example> Paging(int? pageNumber, int? pageSize)
        {
            IQueryable<Example> result = _respository.GetExamples("");

            int currentPageNumber = pageNumber ?? 1;
            int currentPageSize = pageSize ?? 5;

            return result.Skip((currentPageNumber - 1) * currentPageSize).Take(currentPageSize);
        }

        public void UpdateExample(Example example)
        {
            Example result = _respository.GetExample(example.ID);

            if (result == null)
            {
                throw new ApplicationException("Example does not exists.");
            }

            _respository.UpdateExample(example);
        }
    }
}