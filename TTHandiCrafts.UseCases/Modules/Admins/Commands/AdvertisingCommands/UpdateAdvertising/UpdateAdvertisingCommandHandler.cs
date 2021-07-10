using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using TTHandiCrafts.Infrastructure.Interfaces.Interfaces;
using TTHandiCrafts.Models.Models.UserModels.Advertisings;
using TTHandiCrafts.UseCases.Commons.Extensions;
using TTHandiCrafts.UseCases.Modules.Admins.Dtos;

namespace TTHandiCrafts.UseCases.Modules.Admins.Commands.AdvertisingCommands.UpdateAdvertising
{
    public class UpdateAdvertisingCommandHandler : IRequestHandler<UpdateAdvertisingCommand, AdvertisingDto>
    {
        private readonly IMapper mapper;
        private readonly IApplicationDbContext dbContext;

        public UpdateAdvertisingCommandHandler(IMapper mapper, IApplicationDbContext dbContext)
        {
            this.mapper = mapper;
            this.dbContext = dbContext;
        }

        public async Task<AdvertisingDto> Handle(UpdateAdvertisingCommand request, CancellationToken cancellationToken)
        {
            var advertising = await dbContext.Set<Advertising>().FirstOrDefaultAsync(p => p.Id == request.Id);
            mapper.Map(request, advertising);
            await dbContext.SaveChangesAsync();

            await advertising.LoadDocumentAsync(request, advertising.Id, dbContext);
            return mapper.Map<AdvertisingDto>(advertising);
        }
    }
}