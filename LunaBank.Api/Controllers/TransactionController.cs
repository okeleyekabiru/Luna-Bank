using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Lunabank.Data.Models;
using Lunabank.Data.Repos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace LunaBank.Api.Controllers
{
    [Authorize]
    [Route("api/transactions")]
    [ApiController]
    public class TransactionController : ControllerBase
    {
        private readonly ILogger<TransactionController> _logger;
        private readonly ITransactionRepo _transactionRepo;
        private readonly IMapper _mapper;

        public TransactionController(ILogger<TransactionController> logger, ITransactionRepo transactionRepo, IMapper mapper)
        {
            _logger = logger;
            _transactionRepo = transactionRepo;
            _mapper = mapper;
        }

        #region GetSpecificTranasction
        [HttpGet("{transactionId}")]
        public async Task<ActionResult<ResponseModel<TransactionDto>>> GetTransaction(Guid transactionId)
        {
            try
            {
                var transaction = await _transactionRepo.GetTransaction(transactionId);
                if (transaction == null)
                {
                    return NotFound("Transaction not found");
                }

                var response = new ResponseModel<TransactionDto>();
                var transactionDto = _mapper.Map<TransactionDto>(transaction);
                response.Status = "success";
                response.Data = transactionDto;
                return Ok(new {status = "success", data = transactionDto});
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        #endregion

    }
}