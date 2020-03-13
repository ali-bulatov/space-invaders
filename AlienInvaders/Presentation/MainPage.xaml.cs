using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.ApplicationModel.Core;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Storage;
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
            loadSettings();

        }

        private void OnStartButtonClicked(object sender, RoutedEventArgs e)
        {
            //Determine the difficulty level chosen
            ComboBoxItem levelItem = _cmbLevel.SelectedItem as ComboBoxItem;
            int difLevel = int.Parse(levelItem.Content.ToString());
            //Determine the color chosen
            ComboBoxItem colorItem = _cmbTankColor.SelectedItem as ComboBoxItem;
            string colorString = colorItem.Content.ToString();
            int tankColor;
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
            ComboBoxItem tankItem = _cmbBoxTankType.SelectedItem as ComboBoxItem;
            string tankString = tankItem.Content.ToString();
            int tankType;
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
            //create an array
            int[] passingChars = new int[] { difLevel,tankColor,tankType};
            // pass color,difficulty and navigate to the game page
            this.Frame.Navigate(typeof(GamePage), passingChars);
        }    
        private void OnExitButtonClicked(object sender, RoutedEventArgs e)
        {
            saveToFile();
            CoreApplication.Exit();
        }
        public async void saveToFile()
        {
            List<String> prevSettings = new List<String>();
            ComboBoxItem colorItem = _cmbTankColor.SelectedItem as ComboBoxItem;
            string colorString = colorItem.Content.ToString();
            ComboBoxItem typeItem = _cmbBoxTankType.SelectedItem as ComboBoxItem;
            string typeString = typeItem.Content.ToString();
            ComboBoxItem levelItem = _cmbLevel.SelectedItem as ComboBoxItem;
            string levelString = levelItem.Content.ToString();
            prevSettings.Add(colorString);
            prevSettings.Add(typeString);
            prevSettings.Add(levelString);

            StorageFolder folder = ApplicationData.Current.LocalFolder;
            if (File.Exists(folder.Path+"/mainPage.txt"))
            {
                StorageFile saveFile = await folder.GetFileAsync("mainPage.txt");
                await FileIO.WriteLinesAsync(saveFile, prevSettings);
            }
            else
            {
               StorageFile saveFile = await folder.CreateFileAsync("mainPage.txt");
                await FileIO.WriteLinesAsync(saveFile, prevSettings);   
            }
        }
        public async void loadSettings()
        {
            StorageFolder storageFolder = ApplicationData.Current.LocalFolder;
            StorageFile savedSetting = null;
            try
            {
                savedSetting = await storageFolder.GetFileAsync("mainPage.txt");
            }
            catch (FileNotFoundException)
            {
                savedSetting = await storageFolder.CreateFileAsync("mainPage.txt", CreationCollisionOption.ReplaceExisting);
                await FileIO.WriteTextAsync(savedSetting, "0");
            }
            finally
            {
                IList<String> fileLines = await FileIO.ReadLinesAsync(savedSetting);
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
                    if (word.StartsWith("Y"))
                    {
                        _cmbTankColor.SelectedIndex = 1;
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
