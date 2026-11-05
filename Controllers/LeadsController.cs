using Microsoft.AspNetCore.Mvc;
using TrainingCenter.Api.DTOs;
using TrainingCenter.Api.Services.Interfaces;

namespace TrainingCenter.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class LeadsController : ControllerBase
{
    private readonly ILeadService _leadService;

    public LeadsController(ILeadService leadService) => _leadService = leadService;

    ///<summary>Форма Обратный звонок.</summary>
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Post([FromBody] LeadRequest request, CancellationToken ct)
    {
        await _leadService.SubmitAsync(request, ct);

        //Пользователь на сайте должен получить успешный ответ 200 OK, не замечая внутреннего сбоя.
        return Ok(new
        {
            success = true,
            message = $"{request.Name}, спасибо что выбрали нас! С Вами свяжутся в ближайшее время."
        });
    }
}