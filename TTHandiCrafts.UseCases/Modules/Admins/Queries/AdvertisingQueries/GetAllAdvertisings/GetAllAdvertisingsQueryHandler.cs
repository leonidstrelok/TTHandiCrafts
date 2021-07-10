using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using TTHandiCrafts.Infrastructure.Interfaces.Interfaces;
using TTHandiCrafts.Models.Models.UserModels.Advertisings;
using TTHandiCrafts.UseCases.Modules.Admins.Dtos;

namespace TTHandiCrafts.UseCases.Modules.Admins.Queries.AdvertisingQueries.GetAllAdvertisings
{
    public class GetAllAdvertisingsQueryHandler : IRequestHandler<GetAllAdvertisingsQuery, IEnumerable<AdvertisingDto>>
    {
        private readonly IApplicationDbContext dbContext;
        private readonly IMapper mapper;

        public GetAllAdvertisingsQueryHandler(IApplicationDbContext dbContext, IMapper mapper)
        {
            this.dbContext = dbContext;
            this.mapper = mapper;
        }

        public async Task<IEnumerable<AdvertisingDto>> Handle(GetAllAdvertisingsQuery request,
            CancellationToken cancellationToken)
        {
            return await Task.FromResult(dbContext.Set<Advertising>()
                .ProjectTo<AdvertisingDto>(mapper.ConfigurationProvider));
        }
    }
}