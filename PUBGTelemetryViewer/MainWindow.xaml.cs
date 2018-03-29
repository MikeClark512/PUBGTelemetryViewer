using System;
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
using System.IO;
using Microsoft.Win32;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

using BruTile;
using BruTile.Cache;
using BruTile.UI;
using BruTile.UI.Windows;

//PubgLibary Using
using PUBGLibrary;
using PUBGLibrary.API;

namespace PUBGTelemetryViewer
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        public APITelemetry telemetryData;


        public MainWindow()
        {
            InitializeComponent();
            Loaded += MainWindow_Loaded;


            string appdir = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location);

            TileSource tileSource = new TileSource(new FileTileProvider(new FileCache(appdir + "\\maps", "png")), new TileSchema());
            map.RootLayer = new TileLayer(tileSource);
            InitializeTransform(tileSource.Schema);

        }

        private void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            map.Refresh();
        }

        private void InitializeTransform(TileSchema schema)
        {
            map.Transform.Center = new Point(16384d, -16384d);
            map.Transform.Resolution = schema.Resolutions[2];
            schema.Resolutions.Add(2);
            schema.Resolutions.Add(1);
        }

        private void ZoomOutButton_Click(object sender, RoutedEventArgs e)
        {
            if (map.Transform.Resolution < 512)
            {
                map.Transform.Resolution *= 2;
                map.Refresh();
            }
        }

        private void ZoomInButton_Click(object sender, RoutedEventArgs e)
        {
            if (map.Transform.Resolution > 0.125)
            {
                map.Transform.Resolution /= 2;
                map.Refresh();
            }
        }

        private void LoadButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                AddIcon(map.MarkerImages, -1, "noicon");
                AddIcon(map.MarkerImages, 0, "black");

               // BruTile.UI.Ellipse ellipse = new BruTile.UI.Ellipse(0, 17525, -17584, 2002, true, BruTile.UI.Ellipse.ZoneType.Red_Zone, 200);
              //  map.RootLayer.ellipsesCache.Add(ellipse);
                OpenFileDialog dlg = new OpenFileDialog();
                dlg.Filter = "PUBG Telemetry JSON file (.json)|*.json";
                if (dlg.ShowDialog() == true)
                {
                    loadSavedInfo(dlg.FileName);
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            map.Refresh();
        }

        private void loadSavedInfo(string path)
        {
            map.ClearMarkers();
            map.MarkerImages.Clear();
            AddIcon(map.MarkerImages, -1, "noicon");
            List<string> iconNames = new List<string>();

            try
            {
                string json = File.ReadAllText(path);
                APIRequest pIRequest = new APIRequest();

                telemetryData = pIRequest.TelemetryPhraser(json);


                DisplayName(telemetryData.LogMatchStart.PlayerList);
               

            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            map.Refresh();

        }

        private void LoadEllipses(List<LogGameStatePeriodic> logGameStatePeriodicList)
        {
            
            foreach (var i in logGameStatePeriodicList)
            {
                if (!i.GameState.BlueZone.Exists() && !i.GameState.RedZone.Exists())
                    continue;
                BruTile.UI.Ellipse Bellipse = new BruTile.UI.Ellipse(i.GameState.ElapsedTime,i.GameState.BlueZone.X, i.GameState.BlueZone.Y, i.GameState.BlueZone.Radius, true, BruTile.UI.Ellipse.ZoneType.Blue_Zone, 200);
                BruTile.UI.Ellipse Rellipse = new BruTile.UI.Ellipse(i.GameState.ElapsedTime, i.GameState.RedZone.X, i.GameState.RedZone.Y, i.GameState.RedZone.Radius, true, BruTile.UI.Ellipse.ZoneType.Red_Zone, 200);
                map.RootLayer.ellipsesCache.Add(Bellipse);
                map.RootLayer.ellipsesCache.Add(Rellipse);
            }
        }

        private void DisplayName(List<Player> player)
        {
            var player2 = player.OrderBy(x => x.PUBGName);
            foreach(Player p in player2)
            {
                playerlist.Items.Add(p.PUBGName);
            }
        }

        private void loadMarkers(PlayerSpecificLog list, List<Marker> marker, List<Line> lines)
        {
            try
            {
                foreach(var pos in list.LogPlayerPositionList)
                {
                    Marker m = new Marker(pos.LoggedPlayer.Location.X, pos.LoggedPlayer.Location.Y, true, -1, "Location", pos.ElapsedTime, "Position", "test", 200);
                    marker.Add(m);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }


        private void AddIcon(Dictionary<int, ImageSource> list, int index, string name)
        {
            string appdir = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location);

            BitmapImage bi = new BitmapImage();
            bi.BeginInit();
            string url = System.IO.Path.Combine(System.IO.Path.Combine(appdir, "Icons"), string.Format("{0}.png", name));
            bi.UriSource = new Uri(url);
            bi.EndInit();
            list.Add(index, bi);
        }

        private void playerlist_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            map.ClearMarkers();
            
            var selectedplayer = playerlist.SelectedValue.ToString();
            var playerdat = telemetryData.GetPlayerSpecificLog(selectedplayer, SearchType.PUBGName);
            loadMarkers(playerdat, map.RootLayer.MarkerCache, map.RootLayer.LineCache);
            map.Refresh();
        }
    }
}
