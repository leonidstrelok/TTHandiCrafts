using System.Collections.Generic;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using TTHandiCrafts.Infrastructure.Interfaces.Interfaces;
using TTHandiCrafts.Models.Models;
using TTHandiCrafts.Models.Models.UserModels.Advertisings;
using TTHandiCrafts.UseCases.Commons.Extensions;
using TTHandiCrafts.UseCases.Modules.Admins.Dtos;

namespace TTHandiCrafts.UseCases.Modules.Admins.Commands.AdvertisingCommands.CreateAdvertising
{
    public class CreateAdvertisingCommandHandler : IRequestHandler<CreateAdvertisingCommand, AdvertisingDto>
    {
        private readonly IApplicationDbContext dbContext;
        private readonly IMapper mapper;

        public CreateAdvertisingCommandHandler(IApplicationDbContext dbContext, IMapper mapper)
        {
            this.dbContext = dbContext;
            this.mapper = mapper;
        }

        public async Task<AdvertisingDto> Handle(CreateAdvertisingCommand request, CancellationToken cancellationToken)
        {
            var advertising = mapper.Map<Advertising>(request);

            await dbContext.AddAsync(advertising);
            await dbContext.SaveChangesAsync();

            await advertising.LoadDocumentAsync(request, advertising.Id, dbContext);

            return mapper.Map<AdvertisingDto>(advertising);
        }
    }
}