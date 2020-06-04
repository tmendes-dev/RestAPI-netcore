using System.Linq;
using WebApi.Models;

namespace WebApi.Repository
{
    public interface ISampleRepository
    {
        Sample Get(int id);

        IQueryable<Sample> GetAll(string sort);

        void Create(Sample example);

        void Update(Sample example);

        void Delete(Sample example);

        IQueryable<Sample> Search(string word);
    }
}