using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using WebApplication1.Models;

var builder = WebApplication.CreateBuilder(args);

// Добавляем контекст базы данных
builder.Services.AddDbContext<AgarContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Добавляем контроллеры
builder.Services.AddControllers();

// Добавляем Swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "Agar API", Version = "v1" });
});

var app = builder.Build();

// Включаем Swagger
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Agar API v1"));
}

// Обрабатываем запросы
app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();

// Определяем контроллер
[ApiController]
[Route("[controller]")]
public class AdController : ControllerBase
{
    private readonly AgarContext _context;

    public AdController(AgarContext context)
    {
        _context = context;
    }

    // Получаем среднюю стоимость всех реклам
    [HttpGet("average-cost")]
    public IActionResult GetAverageAdCost()
    {
        var averageCost = _context.AdOrders.Average(x => x.AdCost);
        return Ok(averageCost);
    }

    // Получаем список реклам, превышающих указанную величину стоимости
    [HttpGet("ads-above-cost/{cost}")]
    public IActionResult GetAdsAboveCost(decimal cost)
    {
        var ads = _context.AdOrders
            .Where(x => x.AdCost > cost)
            .Select(x => new
            {
                x.AdName,
                x.AdCost
            })
            .ToList();

        return Ok(ads);
    }

    // Получаем ведомость передач, пользующихся наибольшей популярностью
    [HttpGet("most-popular-ads")]
    public IActionResult GetMostPopularAds()
    {
        var popularAds = _context.AdOrders
            .GroupBy(x => x.AdCode)
            .Select(g => new
            {
                AdCode = g.Key,
                TotalDuration = g.Sum(x => x.DurationSeconds)
            })
            .OrderByDescending(x => x.TotalDuration)
            .ToList();

        return Ok(popularAds);
    }
}
