using SpotifyDashboard.Server.Models;
using SpotifyDashboard.Server.Services;

namespace SpotifyDashboard.Test;

public class SpotifyTests


{

    [Fact(DisplayName = "Ritorna i dati dell'utente")]
    public void GetUserData()
    {
        // Il metodo deve ritornae email e username dell'utente che recupera tramite l'access token
    }

    [Fact(DisplayName = "Ritorna l'artista preferito dall'utente")]
    public void ArtistaPreferito()
    {
        
    }

    [Fact(DisplayName = "Ritorna le 10 canzoni preferite dell'utente (le più ascoltate)")]
    public void Top10Canzoni()
    {

    }
    
}