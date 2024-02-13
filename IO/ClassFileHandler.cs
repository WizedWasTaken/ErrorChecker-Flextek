using System;
using System.IO;
using System.Windows;

namespace IO;

public class ClassFileHandler
{
    private string basePath;

    public ClassFileHandler()
    {
    }

    public string LoadFromFile(string initialDirectory)
    {
        Microsoft.Win32.OpenFileDialog dlg = new Microsoft.Win32.OpenFileDialog();


        bool? result = dlg.ShowDialog();

        if (result == true)
        {
            string filename = dlg.FileName;
            try
            {
            }
            catch (IOException e)
            {
            }
            catch (Exception e)
            {
            }
        }

    }

    public string SaveToFile(string text)
    {
        Microsoft.Win32.SaveFileDialog dlg = new Microsoft.Win32.SaveFileDialog();


        Nullable<bool> result = dlg.ShowDialog();

        if (result == true)
        {
            string filename = dlg.FileName;
            try
            {
                File.WriteAllText(filename, text);
                return filename;
            }
            catch (IOException e)
            {
            }
            catch (Exception e)
            {
            }
        }

    }
}