using MudBlazor;

namespace Orcan.Web;

public static class Configuration
{
    public const string HttpClientName = "orcan";
    public static string BackendUrl { get; set; } = "http://localhost:5194";

    public static MudTheme Theme = new MudTheme()
  {
      PaletteLight = new PaletteLight() // Substituir por PaletteLight ou PaletteDark, conforme necessário  
      {
          Primary = "#388e3c",            // Cor primária  
          Secondary = "#66bb6a",          // Cor secundária  
          Background = "#f5f5f5",         // Cor de fundo  
          AppbarBackground = "#2e7d32",   // Cor do fundo do AppBar  
          DrawerBackground = "#a5d6a7",   // Cor do fundo do Drawer  
          Surface = "#ffffff",            // Cor da superfície  
          TextPrimary = "#1b5e20",        // Cor do texto primário  
          ActionDefault = "#43a047"       // Cor de ações  
      }
  };
}

