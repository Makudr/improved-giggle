using improved_giggle.Data.Entities;
using improved_giggle.Windows;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Controls.Primitives;
using Microsoft.UI.Xaml.Data;
using Microsoft.UI.Xaml.Input;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Navigation;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace improved_giggle.Pages
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class ManageCampaignsPage : Page
    {
        public ManageCampaignsPage()
        {
            InitializeComponent();
            LoadCampaigns();
        }

        private async void LoadCampaigns()
        {
            var campaigns = await App.Campaigns.GetAllAsync();
            CampaignList.ItemsSource = campaigns.OrderBy(c => c.Order).ToList();
        }

        private async void SetDefault_Click(object sender, RoutedEventArgs e)
        {
            var campaign = (sender as FrameworkElement).DataContext as CampaignEntity;

            await App.Campaigns.SetDefaultAsync(campaign.Id);

            LoadCampaigns();
        }

        private async void Edit_Click(object sender, RoutedEventArgs e)
        {
            var campaign = (sender as FrameworkElement).DataContext as CampaignEntity;

            //var dialog = new EditCampaignDialog(campaign);
            //await dialog.ShowAsync();

            LoadCampaigns();
        }

        private async void Delete_Click(object sender, RoutedEventArgs e)
        {
            var campaign = (sender as FrameworkElement).DataContext as CampaignEntity;

            var dialog = new ContentDialog
            {
                Title = "Usuń kampanię",
                Content = $"Czy na pewno chcesz usunąć kampanię „{campaign.Name}”? Wszystkie dane zostaną utracone.",
                PrimaryButtonText = "Usuń",
                CloseButtonText = "Anuluj",
                XamlRoot = this.XamlRoot
            };

            var result = await dialog.ShowAsync();

            if (result == ContentDialogResult.Primary)
            {
                await App.Campaigns.DeleteAsync(campaign.Id);
                LoadCampaigns();
            }
        }

        private async void MoveUp_Click(object sender, RoutedEventArgs e)
        {
            var campaign = (sender as FrameworkElement).DataContext as CampaignEntity;
            await App.Campaigns.MoveUpAsync(campaign.Id);
            LoadCampaigns();
        }

        private async void MoveDown_Click(object sender, RoutedEventArgs e)
        {
            var campaign = (sender as FrameworkElement).DataContext as CampaignEntity;
            await App.Campaigns.MoveDownAsync(campaign.Id);
            LoadCampaigns();
        }

        private void CreateCampaign_Click(object sender, RoutedEventArgs e)
        {
            var window = new NewCampaignWindow();
            window.Activate();
        }
    }

}
