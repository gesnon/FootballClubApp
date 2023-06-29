
using FootballClubApp.Application.Interfaces;
using FootballClubApp.Application.Models.FanClub;
using FootballClubApp.Application.Models.FootballClub;
using FootballClubApp.Application.Models.FootballFan;
using FootballClubApp.Application.Models.FootballPlayer;
using FootballClubApp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace WpfApp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly IDataService dataService;
        private readonly IFootballClubService footballClubService;
        private readonly IFootballFanService footballFanService;
        private readonly IFootballPlayerService footballPlayerService;
        private readonly IFanClubService fanClubService;
        private CollectionViewSource footballClubViewSource;
        private CollectionViewSource footballFanViewSource;
        private CollectionViewSource footballPlayerViewSource;
        private CollectionViewSource fanClubViewSource;
        private CollectionViewSource clubPlayersViewSource;
        private CollectionViewSource playerDTOViewSource;
        private CollectionViewSource freePlayerViewSource;
        private CollectionViewSource unfollowedClubsPlayerViewSource;
        private CollectionViewSource followedClubsPlayerViewSource;

        int param = 0;
        int param2 = 0;
        public MainWindow(IDataService dataService, IFanClubService fanClubService, IFootballClubService footballClubService, IFootballFanService footballFanService, IFootballPlayerService footballPlayerService)
        {
            this.dataService = dataService;
            this.footballClubService = footballClubService;
            this.footballFanService = footballFanService;
            this.footballPlayerService = footballPlayerService;
            this.fanClubService = fanClubService;

            InitializeComponent();
            
        }

        private async void closeAllBorders()
        {
            fansBorderGrid.Visibility = Visibility.Hidden;
            clubsBorderGrid.Visibility = Visibility.Hidden;
            playersBorderGrid.Visibility = Visibility.Hidden;
            createFanBorderGrid.Visibility = Visibility.Hidden;
            createClubBorderGrid.Visibility = Visibility.Hidden;
            createPlayerBorderGrid.Visibility = Visibility.Hidden;
            fanInformationmodel.Visibility = Visibility.Hidden;
            clubInformationmodel.Visibility = Visibility.Hidden;
            playerInformationmodel.Visibility = Visibility.Hidden;
            dropDownPlayers.Visibility = Visibility.Hidden;
            invitePlayerAgreeForm.Visibility = Visibility.Hidden;
            LeaveClubAgreeForm.Visibility = Visibility.Hidden;
        }

        private async void FanButton_Click(object sender, RoutedEventArgs e)
        {

            closeAllBorders();
            fansBorderGrid.Visibility = Visibility.Visible;
            footballFanViewSource =
                   (CollectionViewSource)FindResource(nameof(footballFanViewSource));


            // bind to the source
            footballFanViewSource.Source = await footballFanService.GetAllAsync();
        }
        private async void PlayerButton_Click(object sender, RoutedEventArgs e)
        {
            closeAllBorders();
            playersBorderGrid.Visibility = Visibility.Visible;

            footballPlayerViewSource =
                   (CollectionViewSource)FindResource(nameof(footballPlayerViewSource));

            footballPlayerViewSource.Source = await footballPlayerService.GetAllAsync();
        }

        private async void ClubButton_Click(object sender, RoutedEventArgs e)
        {
            closeAllBorders();
            clubsBorderGrid.Visibility = Visibility.Visible;
            footballClubViewSource =
                   (CollectionViewSource)FindResource(nameof(footballClubViewSource));
           

            // bind to the source
            footballClubViewSource.Source = await footballClubService.GetAllAsync();
        }
        private async void FillDataButton_Click(object sender, RoutedEventArgs e)
        {
            await dataService.FillData();
            
        }       

        private async void RemoveDataButton_Click(object sender, RoutedEventArgs e)
        {
            await dataService.RemoveData();
        }
        
        private async void OpenCreateNewFanFormButton_Click(object sender, RoutedEventArgs e)
        {
            closeAllBorders();
            createFanBorderGrid.Visibility = Visibility.Visible;
            
        }
        private async void OpenCreateNewClubFormButton_Click(object sender, RoutedEventArgs e)
        {
            closeAllBorders();
            createClubBorderGrid.Visibility = Visibility.Visible;
        }
        private async void OpenCreateNewPlayerFormButton_Click(object sender, RoutedEventArgs e)
        {
            closeAllBorders();
            createPlayerBorderGrid.Visibility = Visibility.Visible;        }


        private async void CreateClubButton_Click(object sender, RoutedEventArgs e)
        {
            FootballClubDTO dto = new FootballClubDTO { Name = ClubNameInputBox.Text, City=ClubCityInputBox.Text };
            await footballClubService.CteateAsync(dto);
            clubsBorderGrid.Visibility = Visibility.Visible;
            createClubBorderGrid.Visibility = Visibility.Hidden;
        }
        private async void CreateFanButton_Click(object sender, RoutedEventArgs e)
        {
            FootballFanDTO dto = new FootballFanDTO { Name= FullNameInputBox.Text };
            await footballFanService.CteateAsync(dto);
            fansBorderGrid.Visibility = Visibility.Visible;
            createFanBorderGrid.Visibility = Visibility.Hidden;
        }
        private async void CreatePlayerButton_Click(object sender, RoutedEventArgs e)
        {
            FootballPlayerDTO dto = new FootballPlayerDTO { Name = PlayerNameInputBox.Text, Birth= DateTime.Parse(PlayerBirthInputBox.Text), SNILS=PlayerSNILSInputBox.Text };
            await footballPlayerService.CteateAsync(dto);
            playersBorderGrid.Visibility = Visibility.Visible;
            createPlayerBorderGrid.Visibility = Visibility.Hidden;
        }
        
        private async void OpenDeleteFanFormButton_Click(object sender, RoutedEventArgs e)
        {
            DeleteFanAgreeForm.Visibility = Visibility.Visible;
        }
        private async void DeleteFanAgreeButton_Click(object sender, RoutedEventArgs e)
        {

            await footballFanService.RemoveAsync(param);

            DeleteFanAgreeForm.Visibility = Visibility.Hidden;
            fanInformationmodel.Visibility = Visibility.Hidden;
            FanButton_Click(sender, e);

        }
        private async void DeleteFanDisAgreeButton_Click(object sender, RoutedEventArgs e)
        {

            DeleteFanAgreeForm.Visibility = Visibility.Hidden;
        }

        private async void OpenDeleteClubFormButton_Click(object sender, RoutedEventArgs e)
        {
            DeleteClubAgreeForm.Visibility = Visibility.Visible;
        }
        private async void DeleteClubAgreeButton_Click(object sender, RoutedEventArgs e)
        {

            await footballClubService.RemoveAsync(param);

            DeleteClubAgreeForm.Visibility = Visibility.Hidden;
            clubInformationmodel.Visibility = Visibility.Hidden;

            freePlayerViewSource =
                   (CollectionViewSource)FindResource(nameof(freePlayerViewSource));
            freePlayerViewSource.Source = await footballPlayerService.GetFreePlayersAsync();

            ClubButton_Click(sender, e);

        }
        private async void DeleteClubDisAgreeButton_Click(object sender, RoutedEventArgs e)
        {

            dropDownPlayers.Visibility = Visibility.Hidden;
        }

        private async void OpenDeletePlayerFormButton_Click(object sender, RoutedEventArgs e)
        {
            DeletePlayerAgreeForm.Visibility = Visibility.Visible;
        }
        private async void DeletePlayerAgreeButton_Click(object sender, RoutedEventArgs e)
        {

            await footballPlayerService.RemoveAsync(param);

            DeletePlayerAgreeForm.Visibility = Visibility.Hidden;
            playerInformationmodel.Visibility = Visibility.Hidden;
            PlayerButton_Click(sender, e);

        }

        private async void DeletePlayerDisAgreeButton_Click(object sender, RoutedEventArgs e)
        {

            DeletePlayerAgreeForm.Visibility = Visibility.Hidden;

        }


        private async void InvitePlayerButton_Click(object sender, RoutedEventArgs e)
        {
            dropDownPlayers.Visibility = Visibility.Visible;
            invitePlayerAgreeForm.Visibility = Visibility.Visible;
        }
        private async void InvitePlayerAgreeButton_Click(object sender, RoutedEventArgs e)
        {

            await footballClubService.AddPlayerToClubAsync(param2, param);

            dropDownPlayers.Visibility = Visibility.Hidden;
            invitePlayerAgreeForm.Visibility = Visibility.Hidden;
            clubPlayersViewSource =
                   (CollectionViewSource)FindResource(nameof(clubPlayersViewSource));
            FootballClubGetDTO club = await footballClubService.GetAsync(param);
            clubPlayersViewSource.Source = club.FootballPlayers;

            freePlayerViewSource =
                   (CollectionViewSource)FindResource(nameof(freePlayerViewSource));
            freePlayerViewSource.Source = await footballPlayerService.GetFreePlayersAsync();

        }
        private async void InvitePlayerDisAgreeButton_Click(object sender, RoutedEventArgs e)
        {
            dropDownPlayers.Visibility = Visibility.Hidden;
            invitePlayerAgreeForm.Visibility=Visibility.Hidden;

        }


        private async void FollowClubButton_Click(object sender, RoutedEventArgs e)
        {
            dropDownClubs.Visibility = Visibility.Visible;
            followClubAgreeForm.Visibility = Visibility.Visible;
        }
        private async void followClubAgreeButton_Click(object sender, RoutedEventArgs e)
        {
            FanClubDTO dto = new FanClubDTO { ClubId = param2, FanId = param };
            await fanClubService.CreateAsync(dto);

            fanClubViewSource =
                               (CollectionViewSource)FindResource(nameof(fanClubViewSource));

            fanClubViewSource.Source = await footballClubService.GetFansClubAsync(param);

            freePlayerViewSource =
                   (CollectionViewSource)FindResource(nameof(freePlayerViewSource));

            //unfollowedClubsPlayerViewSource =
            //        (CollectionViewSource)FindResource(nameof(unfollowedClubsPlayerViewSource));

            unfollowedClubsPlayerViewSource.Source = await footballClubService.GetUnfollowedClubs(param);

            followedClubsPlayerViewSource =
                    (CollectionViewSource)FindResource(nameof(followedClubsPlayerViewSource));

            followedClubsPlayerViewSource.Source = await footballClubService.GetFansClubAsync(param);
            dropDownClubs.Visibility = Visibility.Hidden;
            followClubAgreeForm.Visibility = Visibility.Hidden;

        }
        private async void followClubDisAgreeButton_Click(object sender, RoutedEventArgs e)
        {
            dropDownClubs.Visibility = Visibility.Hidden;
            followClubAgreeForm.Visibility = Visibility.Hidden;

        }


        private async void unFollowClubButton_Click(object sender, RoutedEventArgs e)
        {
            dropDownClubs2.Visibility = Visibility.Visible;
            unfollowClubAgreeForm.Visibility = Visibility.Visible;
        }
        private async void unfollowClubAgreeButton_Click(object sender, RoutedEventArgs e)
        {
            
            await fanClubService.DeleteAsync(param, param2);

            fanClubViewSource =
            (CollectionViewSource)FindResource(nameof(fanClubViewSource));

            fanClubViewSource.Source = await footballClubService.GetFansClubAsync(param);

            dropDownClubs2.Visibility = Visibility.Hidden;
            unfollowClubAgreeForm.Visibility = Visibility.Hidden;

            followedClubsPlayerViewSource =
                   (CollectionViewSource)FindResource(nameof(followedClubsPlayerViewSource));

            List<FootballClubGetDTO> clubs = await footballClubService.GetFansClubAsync(param);
            followedClubsPlayerViewSource.Source = clubs;

        }
        private async void unfollowClubDisAgreeButton_Click(object sender, RoutedEventArgs e)
        {
            dropDownClubs.Visibility = Visibility.Hidden;
            followClubAgreeForm.Visibility = Visibility.Hidden;

        }

        private async void OpenLeaveClubFormButton_Click(object sender, RoutedEventArgs e)
        {

            LeaveClubAgreeForm.Visibility = Visibility.Visible;

        }
        private async void LeaveClubDisAgreeButton_Click(object sender, RoutedEventArgs e)
        {

            LeaveClubAgreeForm.Visibility = Visibility.Hidden;

        }
        private async void LeaveClubAgreeButton_Click(object sender, RoutedEventArgs e)
        {
            
            await footballPlayerService.LeavClub(param);

            LeaveClubAgreeForm.Visibility = Visibility.Hidden;
            playerClubNameOutput.Text = "";

        }

       


        private async void openFanModel(object sender, MouseButtonEventArgs e)
        {
            closeAllBorders();

            fanInformationmodel.Visibility= Visibility.Visible;

            var row = ItemsControl.ContainerFromElement((DataGrid)sender,
                                        e.OriginalSource as DependencyObject) as DataGridRow;

            if (row == null) return;

            int Id = ((FootballFanGetDTO)row.DataContext).Id;
             
            FootballFanGetDTO fan = await footballFanService.GetAsync(Id);
            
            fanNameOutput.Text= fan.Name;

            param = fan.Id;

            fanClubViewSource =
                   (CollectionViewSource)FindResource(nameof(fanClubViewSource));

            fanClubViewSource.Source= await footballClubService.GetFansClubAsync(Id);

            freePlayerViewSource =
                   (CollectionViewSource)FindResource(nameof(freePlayerViewSource));

            unfollowedClubsPlayerViewSource =
                    (CollectionViewSource)FindResource(nameof(unfollowedClubsPlayerViewSource));

            unfollowedClubsPlayerViewSource.Source = await footballClubService.GetUnfollowedClubs(param);

            followedClubsPlayerViewSource =
                    (CollectionViewSource)FindResource(nameof(followedClubsPlayerViewSource));

            followedClubsPlayerViewSource.Source = await footballClubService.GetFansClubAsync(Id);
            
        }
        private async void openClubModel(object sender, MouseButtonEventArgs e)
        {
            closeAllBorders();

            clubInformationmodel.Visibility = Visibility.Visible;

            var row = ItemsControl.ContainerFromElement((DataGrid)sender,
                                        e.OriginalSource as DependencyObject) as DataGridRow;

            if (row == null) return;
            int Id = ((FootballClubGetDTO)row.DataContext).Id;
            
            FootballClubGetDTO club = await footballClubService.GetAsync(Id);

            clubNameOutput.Text = club.Name;

            param = club.Id;

            clubPlayersViewSource =
                   (CollectionViewSource)FindResource(nameof(clubPlayersViewSource));

            clubPlayersViewSource.Source = club.FootballPlayers;

            freePlayerViewSource =
                   (CollectionViewSource)FindResource(nameof(freePlayerViewSource));
            freePlayerViewSource.Source = await footballPlayerService.GetFreePlayersAsync();
        }

        private async void openClubModel2(object sender, MouseButtonEventArgs e)
        {
            closeAllBorders();
            footballFanViewSource.Source = await footballClubService.GetFansClubAsync(param);
            clubInformationmodel.Visibility = Visibility.Visible;

            var row = ItemsControl.ContainerFromElement((DataGrid)sender,
                                        e.OriginalSource as DependencyObject) as DataGridRow;

            if (row == null) return;
            int? Id = ((FootballClubPreviewDTO)row.DataContext).Id;
            if (Id == null) { return; }
            FootballClubGetDTO club = await footballClubService.GetAsync(Id.Value);

            clubNameOutput.Text = club.Name;

            param = club.Id;

            clubPlayersViewSource =
                   (CollectionViewSource)FindResource(nameof(clubPlayersViewSource));

            clubPlayersViewSource.Source = club.FootballPlayers;

            freePlayerViewSource =
                   (CollectionViewSource)FindResource(nameof(freePlayerViewSource));
            freePlayerViewSource.Source = await footballPlayerService.GetFreePlayersAsync();
        }
        private async void peopleComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (e.AddedItems.Count != 0)
            {
                param2 = ((FootballPlayer)e.AddedItems[0]).Id;
            }
            List<FootballPlayer> players = await footballPlayerService.GetFreePlayersAsync();

            peopleComboBox.ItemsSource = players;
            

        }

        private async void clubComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (e.AddedItems.Count != 0)
            {
                param2 = ((FootballClubGetDTO)e.AddedItems[0]).Id;
            }
            List<FootballClubGetDTO> clubs = await footballClubService.GetUnfollowedClubs(param);

            clubComboBox.ItemsSource = clubs;

        }

        private async void club2ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (e.AddedItems.Count != 0)
            {
                param2 = ((FootballClubGetDTO)e.AddedItems[0]).Id;
            }
            List<FootballClubGetDTO> clubs = await footballClubService.GetUnfollowedClubs(param);

            clubComboBox.ItemsSource = clubs;

        }
        private async void openPlayerModel(object sender, MouseButtonEventArgs e)
        {
            closeAllBorders();

            playerInformationmodel.Visibility = Visibility.Visible;

            var row = ItemsControl.ContainerFromElement((DataGrid)sender,
                                        e.OriginalSource as DependencyObject) as DataGridRow;

            if (row == null) return;
            int Id = ((FootballPlayerGetDTO)row.DataContext).Id;

            FootballPlayerGetDTO player = await footballPlayerService.GetAsync(Id);

            playerNameOutput.Text = player.Name;

            playerClubNameOutput.Text = player.Club;

            param = player.Id;

            playerDTOViewSource =
                   (CollectionViewSource)FindResource(nameof(playerDTOViewSource));

            playerDTOViewSource.Source = player.Name;
        }

        
    }
}
