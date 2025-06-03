using Microsoft.AspNetCore.Mvc;
using DebtPlannerAPI.Services;
using DebtPlannerAPI.Model;
using System.Collections.Generic;

namespace DebtPlannerAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PlanController : ControllerBase
    {
        private readonly AvalancheStrategyService _strategyService;
        public PlanController(AvalancheStrategyService strategyService) 
        {
            _strategyService = strategyService;
        }
        [HttpPost("generate")]
        public IActionResult GeneratePlan([FromBody] PlanRequestDto request)
        {
            var plan = _strategyService.GeneratePlan(request.User, request.Debts);
            return Ok(plan);
        }
    }
}
