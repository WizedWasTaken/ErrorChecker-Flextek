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


        // Ensure the directory exists before trying to open it
        if (!Directory.Exists(initialDirectory) && !initialDirectory.Equals(""))
        {
            Directory.CreateDirectory(initialDirectory);
        }

        // Set the initial directory of the OpenFileDialog to the specified path
        dlg.Title = "Åben Fil:";

        if (!initialDirectory.Equals(""))
        {
            dlg.InitialDirectory = initialDirectory;
        }

        bool? result = dlg.ShowDialog();

        if (result == true)
        {
            string filename = dlg.FileName;
            try
            {
                return filename;
            }
            catch (IOException e)
            {
                return "";
            }
            catch (Exception e)
            {
                return "";
            }
        }

        return "";
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
                return "";
            }
            catch (Exception e)
            {
                return "";
            }
        }

        return "";
    }
}