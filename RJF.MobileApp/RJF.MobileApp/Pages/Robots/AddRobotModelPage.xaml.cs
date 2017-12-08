﻿using System;
using System.Collections.Generic;
using System.Linq;
using RJB.HttpExtinction.HttpRequests.RequestHelpers;
using RJB.Model.Model.Robots;
using RJB.Model.Model.Specializations;
using RJB.Model.ViewModel;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace RJF.MobileApp.Pages.Robots
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AddRobotModel : ContentPage
    {
        private readonly IEnumerable<Specialization> _specializations;
        public AddRobotModel()
        {
            InitializeComponent();
            _specializations = SpecializationClientService.GetAllSpecializations();

            foreach (var specialization in _specializations)
            {
                Specialization.Items.Add(specialization.Name);
            }
        }

        private async void AddRobotModelButton_Clicked(object sender, EventArgs e)
        {
            var specializationId = _specializations.FirstOrDefault(x => x.Name == Specialization.SelectedItem.ToString())
                                   ?.SpecializationId ?? 0;

            var robot = new RobotModelViewModel
            {
                Name = Name.Text,
                Description = Description.Text,
                RobotModelSpecializations = new List<RobotModelSpecialization>()
            };

            robot.RobotModelSpecializations.Add(new RobotModelSpecialization()
            {
                SpecializationId = specializationId,
                SkillLevel = SkillLevel.High
            });

            var isRobotAdd = RobotClientService.AddRobotModel(robot);
            if (!isRobotAdd)
            {
                AddRobotModelFailed.IsVisible = true;
            }
            else
            {
                ClearForm();
                AddRobot.RobotModels = RobotClientService.GetRobotsModel();
                await Navigation.PopAsync();
            }
        }

        private async void BackButton_Clicked(object sender, EventArgs e)
        {
            await Navigation.PopAsync();
        }

        private void ClearForm()
        {
            AddRobotModelFailed.IsVisible = false;
            Name.Text = string.Empty;
            Description.Text = string.Empty;
        }
    }
}