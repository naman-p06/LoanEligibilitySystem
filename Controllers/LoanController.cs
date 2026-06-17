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

    // POST /api/loan/apply
    [HttpPost("apply")]
    public async Task<IActionResult> Apply([FromBody] LoanApplicationRequestDto request)
    {
        var result = await _service.ApplyAsync(request);
        return CreatedAtAction(nameof(GetById), new { id = 1 }, result);
    }

    // GET /api/loan
    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var result = await _service.GetAllAsync();
        return Ok(result);
    }

    // GET /api/loan/{id}
    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var result = await _service.GetByIdAsync(id);
        if (result == null)
            return NotFound(new { message = $"Application with ID {id} not found." });
        return Ok(result);
    }
}