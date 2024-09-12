using Microsoft.Win32;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Xml.Linq;

namespace WPF_Brickstore
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        List<Brick> bricks = new List<Brick>();

        public MainWindow()
        {
            InitializeComponent();
        }

        private void Display(List<Brick>? bricks)
        {
            throw new NotImplementedException();
        }

        private void txtFilter_Changed(object sender, RoutedEventArgs e)
        {
            var filterText = txtFilter.Text;
            var filteredBricks = bricks.Where(b => b.ItemID.StartsWith(filterText));
            var filterTextName = "Br";
            filteredBricks = filteredBricks.Where(b => b.ItemName.StartsWith(filterTextName));
            dgBricks.ItemsSource = filteredBricks;

        }

        private void LoadBricks(string filePath)
        {
            XDocument xaml = XDocument.Load(filePath);

            foreach (var elem in xaml.Descendants("Item"))
            {
                var brick = new Brick
                {
                    ItemID = elem.Element("ItemID").Value,
                    ItemName = elem.Element("ItemName").Value,
                    CategoryName = elem.Element("CategoryName").Value,
                    ColorName = elem.Element("ColorName").Value,
                    Qty = int.Parse(elem.Element("Qty").Value)
                };
                bricks.Add(brick);
            }
            txtFilter.IsEnabled = true;
            dgBricks.ItemsSource = bricks;
        }



        private void BtnLoad_Click(object sender, RoutedEventArgs e)
        {
            var openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "BSX fájlok (*.bsx)|*.bsx";
            if (openFileDialog.ShowDialog() == true)
            {
                LoadBricks(openFileDialog.FileName);
            }
        }
    }



    //aémaf
}
