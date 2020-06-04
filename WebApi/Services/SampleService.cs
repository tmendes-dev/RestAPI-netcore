using System;
using System.Linq;
using WebApi.Models;
using WebApi.Repository;

namespace WebApi.Services
{
    public class SampleService : ISampleService
    {
        private ISampleRepository _respository;

        public SampleService(ISampleRepository respository)
        {
            _respository = respository;
        }

        public void Create(Sample example)
        {
            Sample result = _respository.Get(example.ID);

            if (result != null)
            {
                throw new ApplicationException("Example Already Exisits.");
            }

            _respository.Create(example);
        }

        public void Delete(int id)
        {
            Sample result = _respository.Get(id);

            if (result == null)
            {
                throw new ApplicationException("Example does not exists.");
            }
            _respository.Delete(result);
        }

        public Sample Get(int id)
        {
            Sample result = _respository.Get(id);

            if (result == null)
            {
                throw new ApplicationException("Example does not exists.");
            }

            return result;
        }

        public IQueryable<Sample> GetAll(string sort)
        {
            IQueryable<Sample> result = _respository.GetAll(sort);

            return result;
        }

        public IQueryable<Sample> Paging(int? pageNumber, int? pageSize)
        {
            IQueryable<Sample> result = _respository.GetAll("");

            int currentPageNumber = pageNumber ?? 1;
            int currentPageSize = pageSize ?? 5;

            return result.Skip((currentPageNumber - 1) * currentPageSize).Take(currentPageSize);
        }

        public IQueryable<Sample> Search(string word)
        {

            IQueryable<Sample> result = _respository.Search(word);

            return result;
        }

        public void Update(Sample example)
        {
            Sample result = _respository.Get(example.ID);

            if (result == null)
            {
                throw new ApplicationException("Example does not exists.");
            }

            _respository.Update(example);
        }
    }
}