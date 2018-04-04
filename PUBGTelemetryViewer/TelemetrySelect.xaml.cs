using System;
using System.Collections;
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
using System.Windows.Shapes;
using PUBGLibrary;
using PUBGLibrary.API;
namespace PUBGTelemetryViewer
{
    class Data
    {
        public string Name { get; set; }
   
        public Data(string name)
        {

            this.Name = name;

        }
    }



    /// <summary>
    /// Interaction logic for TelemetrySelect.xaml
    /// </summary>
    public partial class TelemetrySelect : Window
    {
        API pubgapi;
        string shard;

        public TelemetrySelect()
        {
            InitializeComponent();
            pubgapi = new API(MainWindow.apiKey);
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {

            string player = this.playername.Text;
            List<string> players = new List<string>();
            players.Add(player);
            matchlist.ItemsSource = GetMatches(players);
        }

        private List<Data> GetMatches(List<string> player)
        {

            var shrd = ShardToEnum(shard);
            List<Data> tmp = new List<Data>();
            List<APIUser> matches;
            matches = pubgapi.RequestMultiUser(player, shrd);
            for (int a = 0; a < matches[0].ListOfMatches.Count; a++)
                tmp.Add(new Data(matches[0].ListOfMatches[a]));
            return tmp;
        }
        private void matchlist_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (shard == null)
                return;
            DataGrid dg = sender as DataGrid;
            var row = (Data)dg.SelectedItem;
            var shrd = ShardToEnum(shard);

            var match = pubgapi.RequestMatch(row.Name, shrd);
            MainWindow.telemetryData = match.Telemetry;
            
            MessageBox.Show("Telemetry Data loaded\nClosing...","Info", MessageBoxButton.OK);
            this.DialogResult = true;
            this.Close();
        }

        private void RadioButton_Checked(object sender, RoutedEventArgs e)
        {
            RadioButton btn = e.Source as RadioButton;
            if (btn.IsChecked == true)
                shard = btn.Content.ToString();
        }

        public PlatformRegionShard ShardToEnum(string str)
        {
            switch (str)
            {
                case "pc-na": return PlatformRegionShard.PC_NA;
                case "pc-eu": return PlatformRegionShard.PC_EU;
                case "pc-krjp": return PlatformRegionShard.PC_KRJP;
                case "pc-as": return PlatformRegionShard.PC_AS;
                case "pc-oc": return PlatformRegionShard.PC_OC;
                case "pc-sa": return PlatformRegionShard.PC_SA;
                case "pc-sea": return PlatformRegionShard.PC_SEA;
                case "pc-kakao": return PlatformRegionShard.PC_KAKAO;
                case "xbox-na": return PlatformRegionShard.Xbox_NA;
                case "xbox-eu": return PlatformRegionShard.Xbox_EU;
                case "xbox-as": return PlatformRegionShard.Xbox_AS;
                case "xbox-oc": return PlatformRegionShard.Xbox_OC;
            }

            return PlatformRegionShard.PC_NA;
        }
    }
}
