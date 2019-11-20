using System.Collections.Generic;
using System.Threading.Tasks;
using dotnetstarter.Model;

namespace dotnetstarter.Repositories {
    public interface IExampleRepository {
        Task<IEnumerable<ExampleModel>> GetExamplesAsync();
        Task<ExampleModel> CreateExample(ExampleModel resource);
    }
}
