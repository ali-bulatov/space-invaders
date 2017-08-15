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
        //Creates an instance of a game
        private Game _game;
        //Creates an instance of ArcadeMachine
        private ArcadeMachine _arcadeMachine;
        //string list of Highscores
        List<string> listHighScores = new List<string>();

        //The users name is stored in this object
        private string playerName;

        /// <summary>
        /// When the HighScore.xaml is started, displays the previous High scores.
        /// </summary>
        public HighScore()
        {
            this.InitializeComponent();
            LoadFile();
            // Checks to see if game is null. If game is null, allow the user to input new record for score
            if (_game == null)
            {
                RightSplitView.IsPaneOpen = !RightSplitView.IsPaneOpen;
            }
        }

        /// <summary>
        /// This method insures that instances such as game scores, time, and round is transfered
        /// to the ArcadeMachine class.
        /// </summary>
        /// <param name="e"></param>
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
        
        /// <summary>
        /// Exits program entirely.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnExitClicked(object sender, RoutedEventArgs e)
        {
            CoreApplication.Exit();
        }

        /// <summary>
        /// Calls methods of adding and saving values + closes pane.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnClickSubmit(object sender, RoutedEventArgs e)
        {
            AddValues();
            SaveToFile();
            RightSplitView.IsPaneOpen = !RightSplitView.IsPaneOpen;
        }
        /// <summary>
        /// This method adds player Name, round, score, and time to the list.
        /// Also displays the newest score and appends it to the bottom of the High score list.
        /// </summary>
        private void AddValues()
        {
            //Adds necessary texts to the list.
            listHighScores.Add(txtEnterName.Text);
            listHighScores.Add(_game.Round.ToString());
            listHighScores.Add(_game.GameScore.ToString());
            listHighScores.Add(_game.Time.ToString());
            //Static separation of texts TODO: GRID!   
            txt1.Text += txtEnterName.Text + "                       " + _game.Round.ToString() + "                       " + _game.GameScore.ToString() + "                       " + _game.Time.ToString() + "\n";
            
        }

        /// <summary>
        /// Main method that saves List of highscores into a file called highScores.txt
        /// </summary>
        private async void SaveToFile()
        {
            //Looks to current folder
            StorageFolder folder = ApplicationData.Current.LocalFolder;
            //Checks to see if the file exists
            if (File.Exists(folder.Path + "/highScores.txt"))
            {
                StorageFile saveFile = await folder.GetFileAsync("highScores.txt");
                await FileIO.AppendLinesAsync(saveFile, listHighScores);
            }
            //if not, then creates the file.
            else
            {
                StorageFile saveFile = await folder.CreateFileAsync("highScores.txt");
                await FileIO.AppendLinesAsync(saveFile, listHighScores);
            }
        }

        /// <summary>
        /// Main method that loads List of highscores into a file called highScores.txt
        /// </summary>
        private async void LoadFile()
        {
            //Looks to current folder
            StorageFolder storageFolder = ApplicationData.Current.LocalFolder;
            StorageFile scores = null;
            //Checks to see if the file exists
            try
            {
                scores = await storageFolder.GetFileAsync("highScores.txt");
            }
            //If not, then creates the file
            catch (FileNotFoundException)
            {
                scores = await storageFolder.CreateFileAsync("highScores.txt", CreationCollisionOption.ReplaceExisting);
                await FileIO.WriteTextAsync(scores, "0");
            }
            //Loads the file.
            finally
            {
                IList<String> fileLines = await FileIO.ReadLinesAsync(scores);
                foreach(string line in fileLines)
                {
                    
                    txt1.Text += line+ "                       ";
                    
                }

            }
        }

        /// <summary>
        /// To play again, just navigates to the MainPage.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnClickedPlayAgain(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(MainPage));
        }
    }
}
