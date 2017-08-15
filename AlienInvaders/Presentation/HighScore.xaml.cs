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

using System.IO;
using System.Runtime.Serialization.Formatters;
using System.Xml.Serialization;
using System.Text;
using AlienInvadersBuisnessLogic;
using System.Diagnostics;
using Windows.Storage;


// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace AlienInvaders
{

    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class HighScore : Page
    {
        private Game _game;

        private ArcadeMachine _arcadeMachine;
        List<string> listHighScores = new List<string>();

        private string playerName;

        //private ArcadeMachine savingFunc;

        public HighScore()
        {
            this.InitializeComponent();
            LoadFile();
            if (_game == null)
            {
                RightSplitView.IsPaneOpen = !RightSplitView.IsPaneOpen;
            }
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            if (e.Parameter is ArcadeMachine)
            {
                _arcadeMachine = (ArcadeMachine)e.Parameter;
            }
            else
            {
                Debug.Assert(false, "Incorrect Navigation.");
            }
            base.OnNavigatedTo(e);

            _game = _arcadeMachine.Game;
        }

        private void OnExitClicked(object sender, RoutedEventArgs e)
        {
            CoreApplication.Exit();
        }

        private void OnClickSubmit(object sender, RoutedEventArgs e)
        {
            AddValues();
            SaveToFile();
            RightSplitView.IsPaneOpen = !RightSplitView.IsPaneOpen;
        }
        private void AddValues()
        {
            
                listHighScores.Add(txtEnterName.Text);
                listHighScores.Add(_game.Round.ToString());
                listHighScores.Add(_game.GameScore.ToString());
                listHighScores.Add(_game.Time.ToString());
                
                txt1.Text += txtEnterName.Text + "                       " + _game.Round.ToString() + "                       " + _game.GameScore.ToString() + "                       " + _game.Time.ToString() + "\n";
            
        }
        private async void SaveToFile()
        {
            StorageFolder folder = ApplicationData.Current.LocalFolder;
            if (File.Exists(folder.Path + "/highScores.txt"))
            {
                StorageFile saveFile = await folder.GetFileAsync("highScores.txt");
                await FileIO.AppendLinesAsync(saveFile, listHighScores);
            }
            else
            {
                StorageFile saveFile = await folder.CreateFileAsync("highScores.txt");
                await FileIO.AppendLinesAsync(saveFile, listHighScores);
            }
        }

        private async void LoadFile()
        {
            StorageFolder storageFolder = ApplicationData.Current.LocalFolder;
            StorageFile scores = null;
            try
            {
                scores = await storageFolder.GetFileAsync("highScores.txt");
            }
            catch (FileNotFoundException)
            {
                scores = await storageFolder.CreateFileAsync("highScores.txt", CreationCollisionOption.ReplaceExisting);
                await FileIO.WriteTextAsync(scores, "0");
            }
            finally
            {
                IList<String> fileLines = await FileIO.ReadLinesAsync(scores);
                foreach(string line in fileLines)
                {
                    
                    txt1.Text += line+ "                       ";
                    
                }

            }
        }

        private void OnClickedPlayAgain(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(MainPage));
        }
    }
}
