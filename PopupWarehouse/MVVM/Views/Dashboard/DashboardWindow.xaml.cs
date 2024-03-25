using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Input;

using Data;
using Services;
using ViewModels;


namespace Views
{
    public partial class DashboardWindow : Window
    {
        public DashboardWindow()
        {
            InitializeComponent();
                    
            this.Loaded += MainWindow_Loaded;
            var context = new AppDbContext("TestConnection");
            var productService = new ProductService(context);
            this.DataContext = new ProductViewModel(productService);
        }

        // Navigation Methods
        private void ListBoxItem_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            if (sender is ListBoxItem clickedItem && menuListBox != null)
            {
                // Reset background colors for all ListBoxItems
                foreach (var item in menuListBox.Items)
                {
                    if (item is ListBoxItem listBoxItem)
                    {
                        listBoxItem.Background = Brushes.Transparent;
                    }
                }

                // Set background color for the clicked ListBoxItem
                clickedItem.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#e2e8f0"));
            }
        }

        // Search bar methods
        private void TextBox_GotFocus(object sender, RoutedEventArgs e)
        {
            if (searchTextBox.Text == "Search")
            {
                searchTextBox.Text = "";
                searchTextBox.Foreground = Brushes.Black;
            }
        }

        private void TextBox_LostFocus(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(searchTextBox.Text))
            {
                searchTextBox.Text = "Search";
                searchTextBox.Foreground = Brushes.Gray;
            }
        }

        // Clip Grid Corners
        private void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            ClipDataGridToRound();
            MyDataGrid.SizeChanged += (s, evt) => ClipDataGridToRound();
        }
        private void ClipDataGridToRound()
        {
            MyDataGrid.Clip = new RectangleGeometry(new Rect(0, 0, MyDataGrid.ActualWidth, MyDataGrid.ActualHeight), 5, 5);
        }

    }
}
