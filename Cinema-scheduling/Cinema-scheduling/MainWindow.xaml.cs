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

namespace Cinema_scheduling
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public Cinema Cinema { get; set; }
        public MainWindow()
        {
            InitializeComponent();
            Cinema = new Cinema(5);
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Cinema.SetSchedulesHalls();
            TextBoxMain.Text = string.Empty;
            TextBoxMain.Text = Cinema.GetSchedule();
        }
    }
}
