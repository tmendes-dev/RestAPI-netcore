using System;
using System.Collections.Generic;
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

        public List<Example> GetExamples()
        {
            List<Example> result = _respository.GetExamples();

            return result;
        }

        public void UpdateExample(int id, Example example)
        {
            Example result = _respository.GetExample(id);

            if (result == null)
            {
                throw new ApplicationException("Example does not exists.");
            }

            _respository.UpdateExample(example);
        }
    }
}