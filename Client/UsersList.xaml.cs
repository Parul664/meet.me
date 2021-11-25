﻿using System;
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
using System.Collections.ObjectModel;
using System.ComponentModel;
using Client.ViewModel;

namespace Client
{
    /// <summary>
    /// Interaction logic for UsersList.xaml
    /// </summary>
    public partial class UsersList : UserControl
    {
        public bool UserListHidden;
        ObservableCollection<UserViewData> users;
        MainWindow obj;
        public UsersList(MainWindow instance)
        {
            InitializeComponent();

            obj = instance;

            UserListHidden = true;
            UserListHead.Visibility = System.Windows.Visibility.Hidden;

            HomePageViewModel viewModelHomePage = new HomePageViewModel();
            //subscribe to the property changed event
            viewModelHomePage.UsersListChanged += Listener;
            DataContext = viewModelHomePage;

            users = new ObservableCollection<UserViewData>();
            UsersListView.ItemsSource = users;
        }
        private void Listener(object sender, PropertyChangedEventArgs e)
        {
            HomePageViewModel viewModel = DataContext as HomePageViewModel;
            users = new ObservableCollection<UserViewData>(viewModel.users);
        }
        private void UsersListClick(object sender, RoutedEventArgs e)
        {
            obj.OnUsersListClick();
            if (UserListHidden)
            {
                UsersListPane.SetValue(Grid.ColumnSpanProperty, 3);
                UserListHead.Visibility = System.Windows.Visibility.Visible;
                UserListHidden = false;
            }
            else
            {
                UsersListPane.SetValue(Grid.ColumnSpanProperty, 1);
                UserListHead.Visibility = System.Windows.Visibility.Hidden;
                UserListHidden = true;
            }
        }
        public void OnLeaveButtonClick()
        {
            HomePageViewModel homeviewmodel = DataContext as HomePageViewModel;
            homeviewmodel.LeftClient();
        }
    }
}
