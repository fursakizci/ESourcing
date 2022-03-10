using System.Collections.Generic;
using System.Threading.Tasks;
using ESourcing.Sourcing.Entities;

namespace ESourcing.Sourcing.Repositories.Interfaces
{
    public interface IBidRepository
    {
        Task<List<Bid>> GetBidsByAuctionId(string id);

        Task<Bid> GetWinnerBid(string id);

        Task SendBid(Bid bid);
    }
}