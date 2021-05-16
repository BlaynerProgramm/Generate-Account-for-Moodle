using Microsoft.Win32;

using System.IO;

namespace College_GeneratorAccounts.Model
{
    public static class ImportData
    {
        public static string[] GetData()
        {
            OpenFileDialog ofd = new();
            ofd.ShowDialog();
            string data = string.Empty;
            using (StreamReader stream = new(ofd.FileName))
            {
                data = stream.ReadToEnd();
            }

            return data.Split(';');
        }
    }
}
