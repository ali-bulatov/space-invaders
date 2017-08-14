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

        //private ArcadeMachine savingFunc;

        public HighScore()
        {
            this.InitializeComponent();
            //txt1.Text = savingFunc.LoadScores();
            if (_game == null)
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

            byte[] byteArray = Encoding.UTF8.GetBytes(@"C:\Users\Jonathan\Documents\Scores.xml");
            MemoryStream stream = new MemoryStream(byteArray);

            ArcadeMachine player = new ArcadeMachine(playerName, _game.GameScore, _game.Time, _game.Round);

            XmlSerializer serializer = new XmlSerializer(typeof(ArcadeMachine));

            using (TextWriter tw = new StreamWriter(stream))
            {
                serializer.Serialize(tw, player);
            }
            player = null;

            XmlSerializer deserializer = new XmlSerializer(typeof(ArcadeMachine));

            TextReader reader = new StreamReader(stream);
            object obj = deserializer.Deserialize(reader);
            player = (ArcadeMachine)obj;
 
            RightSplitView.IsPaneOpen = !RightSplitView.IsPaneOpen;
        }

        private void OnClickedPlayAgain(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(MainPage));
        }
    }
}
