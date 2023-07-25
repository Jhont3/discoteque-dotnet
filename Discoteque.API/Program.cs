using Microsoft.EntityFrameworkCore;
using Discoteque.Data;
using Discoteque.Business.IServices;
using Discoteque.Business.Services;
// using Discoteque.Data.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<DiscotequeContext>(
    opt => opt.UseInMemoryDatabase("Discoteque")
);

builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
builder.Services.AddScoped<IArtistsService, ArtistsService>();  // dependency injection, no need to use alwaus new

var app = builder.Build();
PopulateDB(app);

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();

async void PopulateDB(WebApplication app)
{
    using (var scope = app.Services.CreateScope())
    {
        var artistService = scope.ServiceProvider.GetRequiredService<IArtistsService>();
        // var albumService = scope.ServiceProvider.GetRequiredService<IAlbumService>();
                
        await artistService.CreateArtist(new Discoteque.Data.Models.Artist{
            Name = "Karol G",
            Label = "Universal",
            IsOnTour = true
        });

        await artistService.CreateArtist(new Discoteque.Data.Models.Artist{
            Name = "Juanes",
            Label = "SONY BMG",
            IsOnTour = true
        });
        
        // await albumService.CreateAlbum(new Discoteque.Data.Models.Album{
        //     Id = 1,
        //     Year = 2017,
        //     Name = "Unstopabble",
        //     ArtistId = 1,
        //     Genre = Discoteque.Data.Models.Genres.Urban
        // });
        
        // await albumService.CreateAlbum(new Discoteque.Data.Models.Album{
        //     Year = 2019,
        //     Name = "Ocean",
        //     ArtistId = 1,
        //     Genre = Discoteque.Data.Models.Genres.Urban
        // });
    }

}
