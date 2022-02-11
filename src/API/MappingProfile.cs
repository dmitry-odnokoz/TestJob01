using AutoMapper;

using TestJob01.ApplicationCore.Entities;
using TestJob01.ApplicationCore.Entities.TransferAggregate;

namespace TestJob01.API {
    public class MappingProfile: Profile {
        public MappingProfile() {
            CreateMap<Warehouse, v1.WarehouseEndpoints.WarehouseDto>();
            CreateMap<Product, v1.ProductEndpoints.ProductDto>();
            CreateMap<Remainder, v1.RemainderEndpoints.RemainderDto>()
                .ForMember(t => t.Quantity, o => o.MapFrom(f => f.Quantity.Value))
                ;
            CreateMap<Transfer, v1.TransferEndpoints.TransferHeadDto>();
            CreateMap<Transfer, v1.TransferEndpoints.TransferDto>();
        }
    }
}
