using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Net;
using Tourism.Repository.Contracts;
using Tourism.Repository.DTO;
using Tourism.Repository.Models;

namespace TourismAPI.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    [Authorize(Roles = "User,Admin")]
    public class SearchController : ControllerBase
    {
        private readonly IRepositoryWrapper _repository;
        private IMapper _mapper;
        private readonly ILogger<SearchController> _logger;
        public SearchController(IRepositoryWrapper repository, IMapper mapper, ILogger<SearchController> logger)
        {
            _repository = repository;
            _mapper = mapper;
            _logger = logger;
        }

        /// <summary>
        /// Action method to get all Branches from DB
        /// </summary>
        /// <returns></returns>
        [HttpGet("GetAllBranches")]
        public async Task<IActionResult> GetAllBranches()
        {
            try
            {
                _logger.LogInformation("Calling GetAllBranches to get Branches data from DB.");
                Task<IEnumerable<Branch>> branches = _repository.Search.GetAllBranches();

                //Check if Branches list has any data
                if (!branches.Result.Any())
                {
                    _logger.LogError($"GetAllBranches - no Branch data found");
                    return NotFound();
                }
                IEnumerable<BranchDto> BranchResult = _mapper.Map<IEnumerable<BranchDto>>(branches.Result);

                _logger.LogInformation($"All Branch data returned successfully");
                return Ok(BranchResult);
            }
            catch (Exception ex)
            {
                _logger.LogError($"GetAllBranches - Something went wrong inside GetAllBranches action: {ex}");
                return StatusCode(500, "GetAllBranches - Internal server error");
            }
        }


        [HttpGet("GetBranchByBranchId")]
        public async Task<IActionResult> GetBranchByBranchId(string branchId)
        {
            try
            {
                _logger.LogInformation("Calling GetAllBranchs to get Branchs data from DB.");
                Task<IEnumerable<Branch>> branches = _repository.Search.GetBranchByBranchId(branchId);

                //Check if Branches list has any data
                if (!branches.Result.Any())
                {
                    _logger.LogError($"GetBranchByBranchId - no branch data found");
                    return NotFound("GetBranchByBranchId - no branch data found");
                }
                IEnumerable<BranchDto> BranchResult = _mapper.Map<IEnumerable<BranchDto>>(branches.Result);

                _logger.LogInformation($"Branch data returned successfully");
                return Ok(BranchResult);
            }
            catch (Exception ex)
            {
                _logger.LogError($"GetBranchByBranchId - Something went wrong inside GetBranchByBranchId action: {ex}");
                return StatusCode(500, "GetBranchByBranchId - Internal server error");
            }
        }

        [HttpGet("GetBranchesByBranchName")]
        public async Task<IActionResult> GetBranchesByBranchName(string branchName)
        {
            try
            {
                _logger.LogInformation("Calling GetBranchesByBranchName to get Branches data from DB.");
                Task<IEnumerable<Branch>> branches = _repository.Search.GetBranchesByBranchName(branchName);

                //Check if Branches list has any data
                if (!branches.Result.Any())
                {
                    _logger.LogError($"GetBranchesByBranchName - no branch data found");
                    return NotFound("GetBranchesByBranchName - no branch data found");
                }
                IEnumerable<BranchDto> BranchResult = _mapper.Map<IEnumerable<BranchDto>>(branches.Result);

                _logger.LogInformation($"Branch data returned successfully");
                return Ok(BranchResult);
            }
            catch (Exception ex)
            {
                _logger.LogError($"GetBranchesByBranchName - Something went wrong inside GetBranchesByBranchName action: {ex}");
                return StatusCode(500, "GetBranchesByBranchName - Internal server error");
            }
        }

        [HttpGet("GetBranchesByPlace")]
        public async Task<IActionResult> GetBranchesByPlace(string placeName)
        {
            try
            {
                _logger.LogInformation("Calling GetBranchesByPlace to get Branches data from DB.");
                Task<IEnumerable<Branch>> branches = _repository.Search.GetBranchesByPlace(placeName);

                //Check if Branches list has any data
                if (!branches.Result.Any())
                {
                    _logger.LogError($"GetBranchesByPlace - no branch data found");
                    return NotFound("GetBranchesByPlace - no branch data found");
                }
                IEnumerable<BranchDto> BranchResult = _mapper.Map<IEnumerable<BranchDto>>(branches.Result);

                _logger.LogInformation($"Branch data returned successfully");
                return Ok(BranchResult);
            }
            catch (Exception ex)
            {
                _logger.LogError($"GetBranchesByPlace - Something went wrong inside GetBranchesByPlace action: {ex}");
                return StatusCode(500, "GetBranchesByPlace - Internal server error");
            }
        }
    }
}
