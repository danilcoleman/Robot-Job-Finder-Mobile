﻿using System;
using RJB.HttpExtinction.HttpRequests.RequestHelpers;
using RJB.Model.Model.Leases;
using RJF.MobileApp.Pages.Layouts;
using RJF.MobileApp.ViewModel;
using RJF.MobileApp.ViewModel.Leases;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace RJF.MobileApp.Pages.Leases
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Leases : ContentPage
    {
        private readonly LeasesViewModel _viewModel;

        public Leases()
        {
            InitializeComponent();
            _viewModel = new LeasesViewModel();
            BindingContext = _viewModel;
        }

        private async void OnItemSelected(object sender, SelectedItemChangedEventArgs args)
        {
            var item = args.SelectedItem as Lease;
            if (item == null)
                return;

            await Navigation.PushAsync(new LeaseDetails());

            // Manually deselect item
            ItemsListView.SelectedItem = null;
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            if (_viewModel.Leases.Count == 0)
                _viewModel.LoadLeasesCommand.Execute(null);
        }

        private async void LogOff_OnClicked(object sender, EventArgs e)
        {
            CurrentUser.CurrentUserModel = null;
            UserClientService.LogOff();
            await Navigation.PushModalAsync(new MainLayoutPage());
        }
    }
}