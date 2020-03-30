using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using AutoMapper;
using Lunabank.Data.Entities;
using Lunabank.Data.Helper;
using Lunabank.Data.Models;
using Lunabank.Data.Repos;
using Lunabank.Data.ResourceParameters;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.VisualStudio.Web.CodeGeneration.Contracts.Messaging;

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
        private readonly IAccounRepo _accounRepo;

        public TransactionController(ILogger<TransactionController> logger, ITransactionRepo transactionRepo, IMapper mapper, IAccounRepo accounRepo)
        {
            _logger = logger;
            _transactionRepo = transactionRepo;
            _mapper = mapper;
            _accounRepo = accounRepo;
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
            catch (Exception ex)
            {
                var error = ex.Message;
                _logger.LogError(error);
                return StatusCode(500, new { Error = "Internal Server Error" });
            }
        }

        #endregion

        #region TransactionHistory

        [HttpGet("{accountNumber}/history", Name = "AccountHistory")]
        public async Task<ActionResult<TransactionDto>> TransactionHistory([FromQuery]TransactionHistoryParameters parameters, string accountNumber)
        {
            try
            {
                var account = await _accounRepo.GetAccount(accountNumber);
                if (account == null)
                {
                    return NotFound(new {Message = "Account not found"});
                }
                var transactionHistory = await _transactionRepo.TransactionHistory(parameters, accountNumber);
                var previousPageLink = transactionHistory.HasPrevious ? CreateTransactionResourceUri(parameters, ResourceUriType.PreviousPage) : null;
                var nextPageLink = transactionHistory.HasNext ? CreateTransactionResourceUri(parameters, ResourceUriType.NextPage) : null;
                var paginationMetadata = new
                {
                    totalCount = transactionHistory.TotalCount,
                    pageSize = transactionHistory.PageSize,
                    currentPage = transactionHistory.CurrentPage,
                    totalPages = transactionHistory.TotalPages,
                    previousPageLink,
                    nextPageLink
                };

                Response.Headers.Add("X-Pagination",
                    JsonSerializer.Serialize(paginationMetadata));
                return Ok(_mapper.Map<IEnumerable<TransactionDto>>(transactionHistory));
            }
            catch (Exception ex)
            {
                var error = ex.Message;
                _logger.LogError(error);
                return StatusCode(500, new { Error = "Internal Server Error" });
            }
           
        }

        #endregion

        private string CreateTransactionResourceUri(
            TransactionHistoryParameters parameters,
            ResourceUriType type)
        {
            switch (type)
            {
                case ResourceUriType.PreviousPage:
                    return Url.Link("AccountHistory",
                        new
                        {
                            pageNumber = parameters.PageNumber - 1,
                            pageSize = parameters.PageSize,
                            fromDate = parameters.FromDate
                        });
                case ResourceUriType.NextPage:
                    return Url.Link("AccountHistory",
                        new
                        {
                            pageNumber = parameters.PageNumber + 1,
                            pageSize = parameters.PageSize,
                            fromDate = parameters.FromDate
                        });

                default:
                    return Url.Link("AccountHistory",
                        new
                        {
                            pageNumber = parameters.PageNumber,
                            pageSize = parameters.PageSize,
                            fromDate = parameters.FromDate
                        });
            }

        }

    }
}