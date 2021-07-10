using System.Collections.Generic;
using MediatR;
using TTHandiCrafts.UseCases.Modules.Admins.Dtos;

namespace TTHandiCrafts.UseCases.Modules.Admins.Queries.AdvertisingQueries.GetAllAdvertisings
{
    public class GetAllAdvertisingsQuery : IRequest<IEnumerable<AdvertisingDto>>
    {
    }
}