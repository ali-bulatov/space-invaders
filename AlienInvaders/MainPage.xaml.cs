using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.ApplicationModel.Core;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace AlienInvaders
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            
            this.InitializeComponent();
        }

        private void OnStartButtonClicked(object sender, RoutedEventArgs e)
        {
            //Determine the difficulty level chosen
            ComboBoxItem levelItem = _cmbLevel.SelectedItem as ComboBoxItem;
            int difLevel = int.Parse(levelItem.Content.ToString());
            //Determine the color chosen
            ComboBoxItem colorItem = _cmbLevel.SelectedItem as ComboBoxItem;
            int tankColor = int.Parse(colorItem.Content.ToString());
            //create an array
            int[] passingChars = new int[] { difLevel,tankColor };
            // pass color,difficulty and navigate to the game page
            this.Frame.Navigate(typeof(GamePage),passingChars);
        }    
        private void OnExitButtonClicked(object sender, RoutedEventArgs e)
        {
            CoreApplication.Exit();
        }

        private void OnHowToButtonClicked(object sender, RoutedEventArgs e)
        {
            _flyHowTo.ShowAt(_menuGrid);
        }

        private void OnScoreButtonClicked(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(HighScore));
        }

        private void OnCreditsButtonClicked(object sender, RoutedEventArgs e)
        {

        }
    }
}
