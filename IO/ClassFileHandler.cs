using System;
using System.IO;
using System.Windows;

namespace IO;

public class ClassFileHandler
{
    public ClassFileHandler()
    {
    }

    public string LoadFromFile()
    {
        Microsoft.Win32.OpenFileDialog dlg = new Microsoft.Win32.OpenFileDialog();

        dlg.DefaultExt = ".txt";
        dlg.Filter = "Text documents (.txt)|*.txt";

        bool? result = dlg.ShowDialog();

        if (result == true)
        {
            string filename = dlg.FileName;
            try
            {
                return File.ReadAllText(filename);
            }
            catch (IOException e)
            {
                return null;
            }
            catch (Exception e)
            {
                return null;
            }
        }

        return null;
    }

    public string SaveToFile(string text)
    {
        Microsoft.Win32.SaveFileDialog dlg = new Microsoft.Win32.SaveFileDialog();

        dlg.DefaultExt = ".json";
        dlg.Filter = "Text documents (.json)|*.json";

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
                return null;
            }
            catch (Exception e)
            {
                return null;
            }
        }

        return null;
    }
}