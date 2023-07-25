using System.Diagnostics;
using System.Reflection.Emit;
using Discoteque.Business.IServices;
using Discoteque.Data.Models;

namespace Discoteque.Business.Services;

public class ArtistService : IArtistService
{
    private readonly List<Artist> artistsList = new List<Artist>();

    private int GenerateRandomId()
    {
        // Generate a random ID for the artist.
        Random randomNum = new();
        return randomNum.Next();
    }
    public async Task<Artist> CreateArtist(Artist artist) 
    {
        #region 
        /*
       Tarea #2
            Debes crear una lista de artistas, A partir del artista que llega
            Asignarle un ID al azar y devolver una lista ocn un solo elemento.
            List<Artist> artistsLlist, artists y Random() son los valores que utilizarás.
        */
        #endregion

        var artistsList = new List<Artist>();
        artist.Id = GenerateRandomId();
        artistsList.Add(artist);
        return artist;
        
    }

    public async Task<IEnumerable<Artist>> GetArtistsAsync()
    {    

        /*
            Tarea #2
            Debes crear una lista de artistas, y devolver 5 artistas de tu elección
            El ID de estos artistas debe ser al azar utilizando Random.
            List<Artist> artistsList es la clase raíz que debes devolver.
       */

        var artistsList = new List<Artist>();

        artistsList.Add(new Artist{
             Name = "Jerard",
             IsOnTour = true,
             Id = GenerateRandomId(),
             Label = "Label",

        });

        artistsList.Add(new Artist{
             Name = "David",
             IsOnTour = true,
             Id = GenerateRandomId(),
             Label = "Label",

        });

        artistsList.Add(new Artist{
             Name = "Juan",
             IsOnTour = true,
             Id = GenerateRandomId(),
             Label = "Label",

        });

        artistsList.Add(new Artist{
             Name = "Richard",
             IsOnTour = true,
             Id = GenerateRandomId(),
             Label = "Label",

        });

        artistsList.Add(new Artist{
             Name = "Jerard",
             IsOnTour = true,
             Id = GenerateRandomId(),
             Label = "Label",

        });

        return artistsList;
        
        // var artist = await _artistservice.GetArtist();
        //TODO: IMPLEMENT ME!
       //Bórrame y me implementas correctamente
        

  
    }

    public Task<Artist> GetById(int id)
    {
        throw new NotImplementedException();
    }

    public Task<Artist> UpdateArtist(Artist artist)
    {
        throw new NotImplementedException();
    }
}