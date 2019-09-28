using System;
using System.Collections.Generic;
using System.IO;
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

namespace HumpSearch.Views
{
    /// <summary>
    /// Interaction logic for FilePreviewWindow.xaml
    /// </summary>
    public partial class FilePreviewWindow : Window
    {
        public FilePreviewWindow()
        {
            InitializeComponent();
        }

        public FilePreviewWindow(string fullFilePath) : this()
        {
            this.txtPreviewBox.Text = File.ReadAllText(fullFilePath);
            this.nameLable.Content = fullFilePath;
        }
    }
}
