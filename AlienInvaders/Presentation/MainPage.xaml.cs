using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.ApplicationModel.Core;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Storage;
using Windows.UI.Popups;
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
            // Load previous settings
            loadSettings();
        }

        private void OnStartButtonClicked(object sender, RoutedEventArgs e)
        {
            //Determine the difficulty level chosen
            ComboBoxItem levelItem = _cmbLevel.SelectedItem as ComboBoxItem;
            //Parse to int
            int difLevel = int.Parse(levelItem.Content.ToString());
            //Determine the color chosen
            ComboBoxItem colorItem = _cmbTankColor.SelectedItem as ComboBoxItem;
            //Cast to string
            string colorString = colorItem.Content.ToString();
            int tankColor;
            // Determine the color chose and return the index
            switch (colorString)
            {
                case "Red":
                    tankColor = 0;
                    break;

                case "Yellow":
                    tankColor = 1;
                    break;

                case "Green":
                    tankColor = 2;
                    break;

                case "Blue":
                    tankColor = 3;
                    break;

                default:
                    tankColor = 0;
                    break;
            }
            // Determine the natk type chosen
            ComboBoxItem tankItem = _cmbBoxTankType.SelectedItem as ComboBoxItem;
            string tankString = tankItem.Content.ToString();
            int tankType;
            // Return the index of the chosen tank
            switch (tankString)
            {
                case "Tank 1":
                    tankType = 0;
                    break;
                case "Tank 2":
                    tankType = 1;
                    break;
                case "Tank 3":
                    tankType = 2;
                    break;
                default:
                    tankType = 0;
                    break;
            }
            //create an array to pass to the game page
            int[] passingChars = new int[] { difLevel,tankColor,tankType};
            // pass color,difficulty,type and navigate to the game page
            this.Frame.Navigate(typeof(GamePage), passingChars);
        }    
        /// <summary>
        /// Exit and save chosen settings
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnExitButtonClicked(object sender, RoutedEventArgs e)
        {
            // Save settings
            saveToFile();
            // Exit
            CoreApplication.Exit();
        }
        /// <summary>
        /// Save settings to a local file
        /// </summary>
        public async void saveToFile()
        {
            //List to strore settings
            List<String> prevSettings = new List<String>();
            ComboBoxItem colorItem = _cmbTankColor.SelectedItem as ComboBoxItem;
            string colorString = colorItem.Content.ToString();
            ComboBoxItem typeItem = _cmbBoxTankType.SelectedItem as ComboBoxItem;
            string typeString = typeItem.Content.ToString();
            ComboBoxItem levelItem = _cmbLevel.SelectedItem as ComboBoxItem;
            string levelString = levelItem.Content.ToString();
            // Add color,type,level chosen to the list
            prevSettings.Add(colorString);
            prevSettings.Add(typeString);
            prevSettings.Add(levelString);
            // new storage folder (current)
            StorageFolder folder = ApplicationData.Current.LocalFolder;
            // if file exists save list to a file
            if (File.Exists(folder.Path+"/mainPage.txt"))
            {
                StorageFile saveFile = await folder.GetFileAsync("mainPage.txt");
                // write to a file
                await FileIO.WriteLinesAsync(saveFile, prevSettings);
            }
            // else create a new a file and then add list
            else
            {
                // create a new file mainPage.txt
               StorageFile saveFile = await folder.CreateFileAsync("mainPage.txt");
                // write to a file
                await FileIO.WriteLinesAsync(saveFile, prevSettings);   
            }
        }
        /// <summary>
        /// Load previous saved settings
        /// </summary>
        public async void loadSettings()
        {
            // folder = current folder
            StorageFolder storageFolder = ApplicationData.Current.LocalFolder;
            StorageFile savedSetting = null;
            // exception handling if file does not excist
            try
            {
                savedSetting = await storageFolder.GetFileAsync("mainPage.txt");
            }
            // create a new file if none found
            catch (FileNotFoundException)
            {
                savedSetting = await storageFolder.CreateFileAsync("mainPage.txt", CreationCollisionOption.ReplaceExisting);
                await FileIO.WriteTextAsync(savedSetting, "0");
            }
            // load a file
            finally
            {
                // create a new list and write file into it
                IList<String> fileLines = await FileIO.ReadLinesAsync(savedSetting);
                // for each string check the content
                foreach(String word in fileLines)
                {
                    if (word.StartsWith("Y"))
                    {
                       _cmbTankColor.SelectedIndex=1;
                    }
                    else if (word.StartsWith("R"))
                    {
                        _cmbTankColor.SelectedIndex = 0;
                    }
                    else if (word.StartsWith("B"))
                    {
                        _cmbTankColor.SelectedIndex = 3;
                    }
                    else if (word.StartsWith("G"))
                    {
                        _cmbTankColor.SelectedIndex = 2;
                    }
                    else if (word.StartsWith("Tank 1"))
                    {
                        _cmbBoxTankType.SelectedIndex = 0;
                    }
                    else if (word.StartsWith("Tank 2"))
                    {
                        _cmbBoxTankType.SelectedIndex = 1;
                    }
                    else if (word.StartsWith("Tank 3"))
                    {
                        _cmbBoxTankType.SelectedIndex = 2;
                    }
                    else if (word.StartsWith("1"))
                    {
                        _cmbLevel.SelectedIndex = 0;
                    }
                    else if (word.StartsWith("2"))
                    {
                        _cmbLevel.SelectedIndex = 1;
                    }
                    else if (word.StartsWith("3"))
                    {
                        _cmbLevel.SelectedIndex = 2;
                    }
                    else if (word.StartsWith("4"))
                    {
                        _cmbLevel.SelectedIndex = 3;
                    }
                    else if (word.StartsWith("5"))
                    {
                        _cmbLevel.SelectedIndex = 4;
                    }
                }
            }
        }
        /// <summary>
        /// How to button opens a flyout window
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnHowToButtonClicked(object sender, RoutedEventArgs e)
        {
            // show fly out
            _flyHowTo.ShowAt(_menuGrid);
        }
        /// <summary>
        /// Navigates to score page
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnScoreButtonClicked(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(HighScore));
        }
        /// <summary>
        /// Show credits
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void OnCreditsButtonClicked(object sender, RoutedEventArgs e)
        {
            string message = "Nelson George Shaw"+"\n"+"Jonathan Rei Nellas"+"\n"+"Alikhan Bulatov"+"\n"+"\n"+"\n"+ "© All rigths reserved 2017.";
            MessageDialog msgDialog = new MessageDialog(message);
            await msgDialog.ShowAsync();
        }
    }
}
