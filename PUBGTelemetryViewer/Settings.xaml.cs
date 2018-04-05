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
using System.Windows.Shapes;
using System.IO;

namespace PUBGTelemetryViewer
{
    /// <summary>
    /// Interaction logic for Settings.xaml
    /// </summary>
    public partial class Settings : Window
    {
        public Settings()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            MainWindow.apiKey = new TextRange(this.apiKey.Document.ContentStart, this.apiKey.Document.ContentEnd).Text;
            File.WriteAllText(Directory.GetCurrentDirectory() + @"\apikey.txt", MainWindow.apiKey);
            this.Close();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            FlowDocument flow = new FlowDocument();
            flow.Blocks.Add(new Paragraph(new Run(MainWindow.apiKey)));
            this.apiKey.Document = flow;
        }
    }
}
