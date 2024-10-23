using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using WebApplication1.Models;

var builder = WebApplication.CreateBuilder(args);

// ��������� �������� ���� ������
builder.Services.AddDbContext<AgarContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// ��������� �����������
builder.Services.AddControllers();

// ��������� Swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "Agar API", Version = "v1" });
});

var app = builder.Build();

// �������� Swagger
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Agar API v1"));
}

// ������������ �������
app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();

// ���������� ����������
[ApiController]
[Route("[controller]")]
public class AdController : ControllerBase
{
    private readonly AgarContext _context;

    public AdController(AgarContext context)
    {
        _context = context;
    }

    // �������� ������� ��������� ���� ������
    [HttpGet("average-cost")]
    public IActionResult GetAverageAdCost()
    {
        var averageCost = _context.AdOrders.Average(x => x.AdCost);
        return Ok(averageCost);
    }

    // �������� ������ ������, ����������� ��������� �������� ���������
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

    // �������� ��������� �������, ������������ ���������� �������������
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
