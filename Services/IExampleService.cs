using System.Collections.Generic;
using System.Threading.Tasks;
using dotnetstarter.Resources;

namespace dotnetstarter.Services {
    public interface IExampleService {
        Task<IEnumerable<ExampleResource>> GetExamplesAsync();
        Task<ExampleResource> CreateExample(ExampleResource resource);
    }
}
