using System.Windows;
using System.Windows.Data;
using System.Windows.Forms;

namespace DirectoryCompare
{
    /// <summary>
    /// Interaction logic for DirectoryDialog.xaml
    /// </summary>
    public partial class DirectoryDialog
    {
        public static DependencyProperty TextProperty = DependencyProperty.Register("Text", typeof(string), typeof(DirectoryDialog), new FrameworkPropertyMetadata(null, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault));
        public static DependencyProperty DescriptionProperty = DependencyProperty.Register("Description", typeof(string), typeof(DirectoryDialog), new PropertyMetadata(null));

        public string Text { get { return GetValue(TextProperty) as string; } set { SetValue(TextProperty, value); } }

        public string Description { get { return GetValue(DescriptionProperty) as string; } set { SetValue(DescriptionProperty, value); } }

        public DirectoryDialog() { InitializeComponent(); }

        private void BrowseFolder(object sender, RoutedEventArgs e)
        {
            using (FolderBrowserDialog dlg = new FolderBrowserDialog())
            {
                dlg.Description = Description;
                dlg.SelectedPath = Text;
                dlg.ShowNewFolderButton = true;
                DialogResult result = dlg.ShowDialog();
                if (result == DialogResult.OK)
                {
                    Text = dlg.SelectedPath;
                    BindingExpression be = GetBindingExpression(TextProperty);
                    if (be != null)
                        be.UpdateSource();
                }
            }
        }
    }
}
