# improved-giggle

## Opis katalogów
# Views
**Okna i strony XAML**

Tu trafiają:
- MainWindow.xaml
- strony jak NotesPage.xaml, NpcPage.xaml, LocationsPage.xaml
- kontrolki UI

Tylko XAML + minimalny code-behind.

# ViewModels
**Logika UI (MVVM)**

Każdy widok ma swój ViewModel:
- NotesViewModel.cs
- NpcViewModel.cs
- SettingsViewModel.cs

Tu trafiają:
- komendy
- bindingi
- logika interfejsu

# Models
**modele domenowe**

To nie są encje EF.
To są modele, które opisują logikę gry/notatek:
- Note.cs
- Npc.cs
- Location.cs
- Encounter.cs

# Data
**baza danych i EF Core**
W środku:

*/Entities*
Encje EF Core:
- NoteEntity.cs
- NpcEntity.cs

*/Migrations*
Automatycznie generowane migracje.

*AppDbContext.cs*
Konfiguracja SQLite.

# Repositiories
**Dostep do danych**

Każdy typ danych ma repozytorium:
- NoteRepository.cs
- NpcRepository.cs

Repozytoria:
- pobierają dane z EF Core
- mapują encje → modele domenowe
- nie znają UI

# Services
**logika biznesowa**
Przykłady:

- NoteService.cs — logika notatek
- MarkdownService.cs — konwersja Markdown → HTML
- SearchService.cs — wyszukiwanie notatek
- ExportService.cs — eksport do PDF/HTML (jeśli dodasz)

# Markdown
**Obsługa Markdown**
Tu trafiają:
- parsery
- konwertery
- style HTML

# Converters
**konwertery XAML**
Np.:
- BoolToVisibilityConverter.cs
- StringToMarkdownHtmlConverter.cs

#Controls 
**własne kontrolki**
Np.:
- MarkdownViewer.xaml
- NoteCard.xaml

# Helpers
**narzędzia**
Np.:
- RelayCommand.cs
- FileHelper.cs
- DialogHelper.cs

# Resources
**style i zasoby**
Np.:
- Styles.xaml
- Colors.xaml
- Templates.xaml
