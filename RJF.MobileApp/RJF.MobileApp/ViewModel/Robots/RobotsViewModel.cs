﻿using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using RJB.HttpExtinction.HttpRequests.RequestHelpers;
using RJB.Model.Model.Robots;
using RJB.Model.ViewModel;
using RJF.MobileApp.Pages.Robots;
using Xamarin.Forms;

namespace RJF.MobileApp.ViewModel.Robots
{
    public class RobotsViewModel : BaseCommonViewModel
    {
        public ObservableCollection<Robot> RobotsModel { get; set; }
        public Command LoadItemsCommand { get; set; }

        public RobotsViewModel()
        {
            Title = "My Robots";
            RobotsModel = new ObservableCollection<Robot>();
            LoadItemsCommand = new Command(ExecuteLoadItemsCommand);

            MessagingCenter.Subscribe<AddRobot, RobotViewModel>(this, "AddRobot", (obj, item) =>
            {
                RefreshData();
            });
        }

        public void ExecuteLoadItemsCommand()
        {
            if (IsBusy)
                return;

            IsBusy = true;

            try
            {
                RefreshData();
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
            finally
            {
                IsBusy = false;
            }
        }

        private void RefreshData()
        {
            RobotsModel.Clear();
            
            var robots = HttpRobotService.GetRobotsByOfCompany(CurrentUser.CurrentUserModel.UserId);
            foreach (var robot in robots)
            {
                if (RobotsModel.All(x => x.RobotId != robot.RobotId))
                {
                    RobotsModel.Add(robot);
                }
            }
        }
    }
}
