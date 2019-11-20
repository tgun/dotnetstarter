using AutoMapper;
using dotnetstarter.Model;
using dotnetstarter.Resources;

namespace dotnetstarter.Mapping {
    public class ModelToResourceProfile : Profile {
        public ModelToResourceProfile() {
            CreateMap<ExampleModel, ExampleResource>();
            CreateMap<ExampleResource, ExampleModel>();
        }
    }
}
