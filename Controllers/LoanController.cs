using Microsoft.AspNetCore.Mvc;
using LoanEligibilitySystem.DTOs;
using LoanEligibilitySystem.Services;

namespace LoanEligibilitySystem.Controllers;

[ApiController]
[Route("api/loan")]
public class LoanController : ControllerBase
{
    private readonly ILoanService _service;

    public LoanController(ILoanService service)
    {
        _service = service;
    }

    [HttpPost("apply")]
    public async Task<IActionResult> Apply([FromBody] LoanApplicationRequestDto request)
    {
        var result = await _service.ApplyAsync(request);
        return CreatedAtAction(nameof(GetById), new { id = 1 }, result);
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var result = await _service.GetAllAsync();
        return Ok(result);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var result = await _service.GetByIdAsync(id);
        if (result == null)
            return NotFound(new ErrorResponseDto
            {
                StatusCode = 404,
                Message = $"Application with ID {id} not found."
            });
        return Ok(result);
    }

    [HttpGet("search")]
    public async Task<IActionResult> Search([FromQuery] string applicationNo)
    {
        if (string.IsNullOrWhiteSpace(applicationNo))
            return BadRequest(new ErrorResponseDto
            {
                StatusCode = 400,
                Message = "Application number is required."
            });

        var result = await _service.GetByApplicationNumberAsync(applicationNo);
        if (result == null)
            return NotFound(new ErrorResponseDto
            {
                StatusCode = 404,
                Message = $"No application found with number {applicationNo}."
            });

        return Ok(result);
    }

    [HttpGet("dashboard")]
    public async Task<IActionResult> Dashboard()
    {
        var result = await _service.GetDashboardAsync();
        return Ok(result);
    }
}