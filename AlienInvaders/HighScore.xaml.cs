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

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace AlienInvaders
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class HighScore : Page
    {
        private Game _game;

        private string playerName;

        private ArcadeMachine savingFunc;

        public HighScore()
        {
            this.InitializeComponent();
            txt1.Text = savingFunc.LoadScores();
            if (_game != null)
            {
                RightSplitView.IsPaneOpen = !RightSplitView.IsPaneOpen;
            }
        }

        private void OnExitClicked(object sender, RoutedEventArgs e)
        {
            CoreApplication.Exit();
        }

        private void OnClickSubmit(object sender, RoutedEventArgs e)
        {
            playerName = txtEnterName.Text;

            savingFunc.SaveScores(int.Parse(playerName), _game.GameScore.ToString(), _game.Round, _game.Time);
            
            RightSplitView.IsPaneOpen = !RightSplitView.IsPaneOpen;
        }

        private void OnClickedPlayAgain(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(MainPage));
        }
    }
}
