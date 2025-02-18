﻿using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using ESourcing.Sourcing.Entities;
using ESourcing.Sourcing.Repositories.Interfaces;

namespace ESourcing.Sourcing.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class BidController : Controller
    {

        private readonly IBidRepository _bidRepository;

        public BidController(IBidRepository bidRepository)
        {
            _bidRepository = bidRepository;
        }

        [HttpPost]
        [ProducesResponseType((int) HttpStatusCode.OK)]
        public async Task<ActionResult> SendBid([FromBody] Bid bid)
        {
            await _bidRepository.SendBid(bid);

            return Ok();
        }
        
        [HttpGet("GetBitByAuctionId")]
        [ProducesResponseType(typeof(IEnumerable<Bid>),(int) HttpStatusCode.OK)]
        public async Task<ActionResult> GetBitByAuctionId(string id)
        {
            IEnumerable<Bid> bids = await _bidRepository.GetBidsByAuctionId(id);

            return Ok(bids);
        }
        
        [HttpGet("GetWinnerBid")]
        [ProducesResponseType(typeof(Bid),(int) HttpStatusCode.OK)]
        public async Task<ActionResult> GetWinnerBid(string id)
        {
            Bid bid = await _bidRepository.GetWinnerBid(id);

            return Ok(bid);
        }
    }
}
