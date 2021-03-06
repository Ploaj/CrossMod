﻿using CrossModGui.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace CrossModGui.Views
{
    /// <summary>
    /// Interaction logic for MaterialPresetWindow.xaml
    /// </summary>
    public partial class MaterialPresetWindow : Window
    {
        public MaterialPresetWindow()
        {
            InitializeComponent();
        }

        private void ApplyPreset_Click(object sender, RoutedEventArgs e)
        {
            (DataContext as MaterialPresetWindowViewModel)?.OnPresetApply();
            Close();
        }
    }
}
