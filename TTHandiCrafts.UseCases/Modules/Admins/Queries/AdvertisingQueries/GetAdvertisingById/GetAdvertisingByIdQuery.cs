using MediatR;
using TTHandiCrafts.UseCases.Modules.Admins.Dtos;

namespace TTHandiCrafts.UseCases.Modules.Admins.Queries.AdvertisingQueries.GetAdvertisingById
{
    public class GetAdvertisingByIdQuery : IRequest<AdvertisingDto>
    {
        public int Id { get; }

        public GetAdvertisingByIdQuery(int id)
        {
            Id = id;
        }
    }
}