using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Diagnostics;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace FreePDK_WRITER
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void fileSelectButton_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog myDialog = new OpenFileDialog();
            myDialog.Filter = "Intel hex files|*.hex;*.ihx|All files|*.*";
          if (myDialog.ShowDialog() ?? true)
            {
                filePathTextBox.Text = myDialog.FileName;
            }
        }

        private void voltageSlider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            
        }

        private void programButton_Click(object sender, RoutedEventArgs e)
        {
            runInCmd($"easypdkprog.exe write -n{mcuComboBox.Text} {userArgsTextBox.Text} {filePathTextBox.Text}\npause");
        }

        private void probeButton_Click(object sender, RoutedEventArgs e)
        {
            runInCmd($"easypdkprog.exe probe\npause");
        }
        private void runInCmd(string command) {
            if (File.Exists("writecmd.bat"))
            {
                File.Delete("writecmd.bat");
            }
            File.WriteAllText("writecmd.bat", command);
            var proc = new Process
            {
                StartInfo = new ProcessStartInfo
                {
                    FileName = "writecmd.bat",
                    UseShellExecute = true,
                    CreateNoWindow = false
                }
            };
            proc.Start();
        }

        private void runButton_Click(object sender, RoutedEventArgs e)
        {
            runInCmd($"easypdkprog.exe start -r{voltageSlider.Value}");
        }
    }
}
