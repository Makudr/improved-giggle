using improved_giggle.Data.Services;
using improved_giggle.Pages;
using improved_giggle.Windows;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using System;
using System.Linq;
using System.Threading.Tasks;
using Windows.Graphics;

namespace improved_giggle;

public sealed partial class MainWindow : Window
{
    public ActiveCampaignService ActiveCampaign { get; }

    private bool _initialized = false;

    public MainWindow()
    {
        InitializeComponent();

        AppWindow.Resize(new SizeInt32(1200, 800));

        ActiveCampaign = new ActiveCampaignService(App.Campaigns);

        Activated += MainWindow_Activated;
    }

    private async void MainWindow_Activated(object sender, WindowActivatedEventArgs e)
    {
        if (_initialized)
            return;

        _initialized = true;

        // 1. Załaduj kampanię domyślną
        await ActiveCampaign.InitializeAsync();

        if (ActiveCampaign.Current != null)
        {
            NavView.Header = $"Kampania: {ActiveCampaign.Current.Name}";
        }
        else if (ActiveCampaign.Current == null)
        {
            ShowNoCampaignDialog();
            return;
        }

        // 2. Załaduj listę kampanii do NavigationView
        await LoadCampaignsAsync();

        // 3. Przejdź do dashboardu
        RootFrame.Navigate(typeof(DashboardPage));
    }

    private async void ShowNoCampaignDialog()
    {
        var dialog = new ContentDialog
        {
            Title = "Brak kampanii",
            Content = "Nie znaleziono żadnej kampanii. Utwórz nową, aby rozpocząć pracę.",
            PrimaryButtonText = "Utwórz kampanię",
            CloseButtonText = "Zamknij",
            XamlRoot = this.Content.XamlRoot
        };

        var result = await dialog.ShowAsync();

        if (result == ContentDialogResult.Primary)
        {
            var window = new NewCampaignWindow();
            window.Activate();
        }
        else
        {
            Close();
        }
    }
    private async Task LoadCampaignsAsync()
    {
        var campaigns = await App.Campaigns.GetAllAsync();

        var parent = (NavigationViewItem)NavView.MenuItems
            .First(i => i is NavigationViewItem nvi && (string)nvi.Content == "Kampanie");

        parent.MenuItems.Clear();

        foreach (var c in campaigns)
        {
            parent.MenuItems.Add(new NavigationViewItem
            {
                Content = c.Name,
                Tag = $"campaign_{c.Id}",
                Icon = new SymbolIcon(Symbol.Document)
            });
        }

        parent.MenuItems.Add(new NavigationViewItemSeparator());

        parent.MenuItems.Add(new NavigationViewItem
        {
            Content = "Zarządzaj kampaniami",
            Tag = "manage_campaigns",
            Icon = new SymbolIcon(Symbol.Edit)
        });

        parent.MenuItems.Add(new NavigationViewItem
        {
            Content = "Nowa kampania",
            Tag = "new_campaign",
            Icon = new SymbolIcon(Symbol.Add)
        });
    }


    private async void NavView_SelectionChanged(NavigationView sender, NavigationViewSelectionChangedEventArgs args)
    {
        if (args.SelectedItem is not NavigationViewItem item)
            return;

        var tag = item.Tag?.ToString();

        if (tag == "dashboard")
        {
            RootFrame.Navigate(typeof(DashboardPage));
        }
        else if (tag == "manage_campaigns")
        {
            RootFrame.Navigate(typeof(ManageCampaignsPage));
        }
        else if (tag == "new_campaign")
        {
            var window = new NewCampaignWindow();
            window.Activate();
        }
        else if (tag?.StartsWith("campaign_") == true)
        {
            var id = int.Parse(tag.Replace("campaign_", ""));
            var campaign = await App.Campaigns.GetByIdAsync(id);

            ActiveCampaign.SetActive(campaign);

            NavView.Header = $"Kampania: {campaign.Name}";

            RootFrame.Navigate(typeof(DashboardPage), id);
        }

    }
}
