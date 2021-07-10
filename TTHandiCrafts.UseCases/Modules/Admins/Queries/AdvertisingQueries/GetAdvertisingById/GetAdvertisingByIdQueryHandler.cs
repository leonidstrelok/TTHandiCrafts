using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using TTHandiCrafts.Infrastructure.Interfaces.Interfaces;
using TTHandiCrafts.Models.Models.UserModels.Advertisings;
using TTHandiCrafts.UseCases.Modules.Admins.Dtos;

namespace TTHandiCrafts.UseCases.Modules.Admins.Queries.AdvertisingQueries.GetAdvertisingById
{
    public class GetAdvertisingByIdQueryHandler : IRequestHandler<GetAdvertisingByIdQuery, AdvertisingDto>
    {
        private readonly IApplicationDbContext dbContext;
        private readonly IMapper mapper;

        public GetAdvertisingByIdQueryHandler(IMapper mapper, IApplicationDbContext dbContext)
        {
            this.mapper = mapper;
            this.dbContext = dbContext;
        }

        public async Task<AdvertisingDto> Handle(GetAdvertisingByIdQuery request, CancellationToken cancellationToken)
        {
            var advertising = await dbContext.Set<Advertising>().FirstOrDefaultAsync(p => p.Id == request.Id);
            return mapper.Map<AdvertisingDto>(advertising);
        }
    }
}