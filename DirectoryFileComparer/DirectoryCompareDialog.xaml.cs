using System.Windows;


namespace DirectoryCompare
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class DirectoryCompareDialog : Window
    {
        public DirectoryCompareDialog()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            var dir1 = directory1Tb.Text;
            var dir2 = directory2Tb.Text;

            if (string.IsNullOrEmpty(dir1) || string.IsNullOrEmpty(dir2))
            {
                MessageBox.Show("Pick two directory to compare", "Directory Compare", MessageBoxButton.OK, MessageBoxImage.Information);
                return;
            }
               
            var result = DirectoryComparer.CompareDirectories(dir1, dir2);

            resultTb.Text = string.Empty;

            foreach (var item in result)
            {
                resultTb.Text += item + "\n\n";
            } 

        }

       
    }
}
