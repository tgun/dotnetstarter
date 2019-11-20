using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using dotnetstarter.Model;
using dotnetstarter.Repositories;
using dotnetstarter.Resources;

namespace dotnetstarter.Services {
    /// <summary>
    /// A service holds the "Business Logic" of the application, to abstract it away from the incoming request layer.
    /// </summary>
    public class ExampleService : IExampleService {
        private readonly IExampleRepository _repository;
        private readonly IMapper _mapper;

        public ExampleService(IExampleRepository repo, IMapper mapper) {
            _repository = repo;
            _mapper = mapper;
        }

        public async Task<IEnumerable<ExampleResource>> GetExamplesAsync() {
            IEnumerable<ExampleModel> examples = await _repository.GetExamplesAsync();
            IEnumerable<ExampleResource> resources = _mapper.Map<IEnumerable<ExampleModel>, IEnumerable<ExampleResource>>(examples);

            return resources;
        }

        public async Task<ExampleResource> CreateExample(ExampleResource resource) {
            ExampleModel example = _mapper.Map<ExampleResource, ExampleModel>(resource);
            ExampleModel newExample = await _repository.CreateExample(example);
            ExampleResource newResource = _mapper.Map<ExampleModel, ExampleResource>(newExample);

            return newResource;
        }
    }
}
