﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RJB.HttpExtinction.HttpRequests.RequestHelpers;
using RJB.Model.Model.Robots;
using RJF.MobileApp.Pages.Layouts;
using RJF.MobileApp.ViewModel;
using RJF.MobileApp.ViewModel.Robots;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace RJF.MobileApp.Pages.Robots
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Robots : ContentPage
    {
        private readonly RobotsViewModel _viewModel;

        public Robots()
        {
            InitializeComponent();

            var robots = HttpRobotService.GetRobotsByOfCompany(CurrentUser.CurrentUserModel.UserId);

            _viewModel = new RobotsViewModel
            {
                RobotsModel = new ObservableCollection<Robot>(robots) 
            };

            BindingContext = _viewModel;
        }

        async void OnItemSelected(object sender, SelectedItemChangedEventArgs args)
        {
            var item = args.SelectedItem as Robot;
            if (item == null)
                return;

            await Navigation.PushAsync(new RobotDetails(new RobotDetailsViewModel(item)));
            
            ItemsListView.SelectedItem = null;
        }

        async void AddRobot_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new AddRobot());
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            if (_viewModel.RobotsModel.Count == 0)
                _viewModel.LoadItemsCommand.Execute(null);
        }

        private async void AddRobotModel_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new AddRobotModel());
        }

        private async void LogOff_OnClicked(object sender, EventArgs e)
        {
            CurrentUser.CurrentUserModel = null;
            await Navigation.PopAsync();
        }

        private async void AddSpecialization_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new AddSpecializationPage());
        }
    }
}
