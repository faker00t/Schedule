using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Shedule.Import
{
    class Reader
    {
        public static string OpenFile()
        {
            // Create OpenFileDialog 
            Microsoft.Win32.OpenFileDialog dlg = new Microsoft.Win32.OpenFileDialog();
            // Set filter for file extension and default file extension 
            dlg.DefaultExt = ".pkt";
            dlg.Filter = "PKT Files (*.pkt)|*.pkt|TXT Files (*.txt)|*.txt|DAT Files (*.dat)|*.dat";
            // Display OpenFileDialog by calling ShowDialog method 
            Nullable<bool> result = dlg.ShowDialog();
            // Get the selected file name and display in a TextBox 
            if (result == true)
            {
                // Open document 
                return dlg.FileName;
            }
            return string.Empty;
        }
    }
}
