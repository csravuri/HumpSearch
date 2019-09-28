using HumpSearch.Models;
using HumpSearch.Views;
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
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace HumpSearch
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private FileFunc _fileFunctions = null;
        public MainWindow()
        {
            InitializeComponent();

            this.txtFolderPath.Text = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);

            _fileFunctions = new FileFunc();

        }

        private void BtnBrowse_Click(object sender, RoutedEventArgs e)
        {
            FolderBrowserDialog folderDialog = new FolderBrowserDialog();
            folderDialog.RootFolder = Environment.SpecialFolder.MyComputer;
            folderDialog.ShowNewFolderButton = false;
            folderDialog.ShowDialog();

            if (string.IsNullOrWhiteSpace(folderDialog.SelectedPath))
                return;

            this.txtFolderPath.Text = folderDialog.SelectedPath;
        }

        private void UpdateFileList()
        {
            FileSearchCriteria fileSearchCriteria = new FileSearchCriteria()
            {
                folderPath = this.txtFolderPath.Text,
                searchKeyWord = this.txtKeyWord.Text,
                isNameSearch = isName.IsChecked.Value,
                isContentSearch = isContent.IsChecked.Value
            };

            listView.ItemsSource = _fileFunctions.GetFileList(fileSearchCriteria);

        }

        private void BtnSearch_Click(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(this.txtKeyWord.Text) && isValidFolder(this.txtFolderPath.Text))
            {
                UpdateFileList();
            }
            else
            {
                System.Windows.MessageBox.Show("Error: Contact the creator");
            }

        }

        private void ListView_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            FileProp fileProp = listView.SelectedItem as FileProp;
            FilePreviewWindow filePreview = new FilePreviewWindow(fileProp.FullPath);
            filePreview.Show();
        }

        private bool isValidFolder(string folderPath)
        {
            return Directory.Exists(folderPath);
        }

        private void TxtBox_GotFocus(object sender, RoutedEventArgs e)
        {
            ((System.Windows.Controls.TextBox)sender).SelectAll();
        }
    }
}
