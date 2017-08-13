using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace AlienInvaders
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class HighScore : Page
    {
        public HighScore()
        {
            this.InitializeComponent();
        }

        private void OnSubmitClicked(object sender, RoutedEventArgs e)
        {
            //string PlayerName = txtPlayerName.Text;

            //TODO: Implementing the score, time, and level from game page to HighScore page
        }

        private void OnPlayAgainClicked(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(MainPage));
        }

        private void OnExitClicked(object sender, RoutedEventArgs e)
        {
            //TODO: Exit application
        }

        private void OnClickSubmit(object sender, RoutedEventArgs e)
        {
            RightSplitView.IsPaneOpen = !RightSplitView.IsPaneOpen;
        }
    }
}
