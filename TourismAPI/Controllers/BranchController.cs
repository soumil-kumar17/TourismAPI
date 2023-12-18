using Tourism.Repository.Contracts;
using Tourism.Repository.DTO;
using Tourism.Repository.Models;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

namespace TourismAPI.Controllers;

[ApiController]
[Route("api/v1/[controller]")]
//[Authorize(Roles = "Admin")]
public class BranchController : ControllerBase
{
    private readonly IRepositoryWrapper _repository;
    private IMapper _mapper;
    private readonly ILogger<BranchController> _logger;
    public BranchController(IRepositoryWrapper repository, IMapper mapper, ILogger<BranchController> logger)
    {
        _repository = repository;
        _mapper = mapper;
        _logger = logger;
    }


    /// <summary>
    /// Action method to create new Branch
    /// </summary>
    /// <param name="branch"></param>
    /// <returns></returns>
    [HttpPost("CreateBranch")]
    public async Task<IActionResult> CreateBranch([FromBody] CreateBranchDto branch)
    {
        try
        {
            if (branch is null)
            {
                _logger.LogError("CreateBranch - Branch object sent from client is null.");
                return BadRequest("CreateBranch - Branch object is null");
            }
            //Checks whether the passed object has all the required fields
            if (!ModelState.IsValid)
            {
                _logger.LogError("CreateBranch - " + ModelState + "Invalid Branch object sent from client.");
                //return BadRequest("CreateBranch -Invalid model object");
                return UnprocessableEntity(ModelState);
            }

            //Mapping CreateBranchDto to Branch object
            Branch objBranch = _mapper.Map<Branch>(branch);
            objBranch.CreatedDate = DateTime.Now;
            objBranch.UpdatedBy = objBranch.UpdatedBy;
            objBranch.UpdatedDate = DateTime.Now;

            _logger.LogInformation("Calling CreateBranch method to create new Branch.");
            Task<bool> flag = _repository.Branch.CreateBranch(objBranch);
            //Checks if flag is true
            if (flag.Result)
            {
                _logger.LogInformation($"Branch created successfully");
                return Ok();
            }
            else
            {
                _logger.LogError($"Something went wrong inside CreateBranch action.");
                return StatusCode(500, "Internal server error");
            }
        }
        catch (Exception ex)
        {
            _logger.LogError($"Something went wrong inside CreateBranch action: {ex}");
            return StatusCode(500, "Internal server error");
        }
    }

    /// <summary>
    ///  Action method to update Branchs to DB
    /// </summary>
    /// <param name="branch"></param>
    /// <returns></returns>
    [HttpPut("UpdateBranch")]
    public async Task<IActionResult> UpdateBranch([FromBody] UpdateBranchDto branch)
    {

        try
        {   // check if Branch object is null
            if (branch is null)
            {
                _logger.LogError("UpdateBranch - empty branch object sent from client.");
                return BadRequest("UpdateBranch - Branch object is null");
            }
            //Checks whether the passed object has all the required fields
            if (!ModelState.IsValid)
            {
                _logger.LogError("UpdateBranch - Invalid Branch object sent from client.");
                //return BadRequest("UpdateBranch -Invalid model object");
                return UnprocessableEntity(ModelState);
            }

            //Mapping UpdateBranchDto to Branch object
            Branch objBranch = _mapper.Map<Branch>(branch);
            objBranch.UpdatedDate = DateTime.Now;

            _logger.LogInformation("Calling UpdateBranch method to update existing Branch.");
            bool flag = await _repository.Branch.UpdateBranch(objBranch);
            //Checks if flag is true
            if (flag)
            {
                _logger.LogInformation($"Branch updated successfully with id: {branch.BranchName}");
                return Ok();
            }
            else
            {
                _logger.LogError($"Something went wrong inside UpdateBranch action.");
                return StatusCode(500, "UpdateBranch - Internal server error");
            }
        }
        catch (Exception ex)
        {
            _logger.LogError($"Something went wrong inside UpdateBranch action: {ex}");
            return StatusCode(500, "UpdateBranch - Internal server error");
        }
    }


    /// <summary>
    ///  Action method to delete Branch from DB
    /// </summary>
    /// <param name="branchID"></param>
    /// <returns></returns>
    //[HttpDelete("DeleteBranch")]
    //public async Task<IActionResult> DeleteBranch(string branchID)
    //{
    //    bool result;
    //    try
    //    {
    //        _logger.LogInformation("Calling DeleteBranch to delete Branch.");
    //       result = await _repository.Branch.DeleteBranch(branchID);
    //        //Check if result is true
    //        if (result)
    //        {
    //            _logger.LogInformation($"Branch deleted successfully with id: {branchID}");
    //            return Ok();
    //        }
    //        else
    //        {
    //            _logger.LogError($"Something went wrong inside DeleteBranch action.");
    //            return StatusCode(500, "DeleteBranch - Internal server error");
    //        }
    //    }
    //    catch (Exception ex)
    //    {
    //        _logger.LogError($"Something went wrong inside DeleteBranch action: {ex}");
    //        return StatusCode(500, "DeleteBranch - Internal server error");
    //    }
    //}

}
