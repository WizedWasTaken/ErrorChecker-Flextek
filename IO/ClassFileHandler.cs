using System;
using System.IO;
using System.Windows;

namespace IO;

public class ClassFileHandler
{
    public ClassFileHandler()
    {
    }

    public static string LoadFromFile(string initialDirectory)
    {
        Microsoft.Win32.OpenFileDialog dlg = new();

        if (initialDirectory != "")
        {
            dlg.InitialDirectory = initialDirectory;
        }

        bool? result = dlg.ShowDialog();

        if (result != true) return "";

        string filename = dlg.FileName;
        try
        {
            return filename;
        }
        catch
        {
            return "";
        }
    }

    public static string SaveToFile(string text)
    {
        Microsoft.Win32.SaveFileDialog dlg = new();

        bool? result = dlg.ShowDialog();

        if (result != true) return "";

        string filename = dlg.FileName;
        try
        {
            File.WriteAllText(filename, text);
            return filename;
        }
        catch
        {
            return "";
        }
    }
}