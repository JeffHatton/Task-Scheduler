using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Task_Scheduler
{
    class Registry
    {
        public enum RegistryKeys
        {
            OutputFile
        }

        public static Dictionary<RegistryKeys, string> RegKeys = new Dictionary<RegistryKeys, string>()
        {
            {RegistryKeys.OutputFile, "OutputPath" }
        };

        public static void SetUpRegistry()
        {
            Microsoft.Win32.RegistryKey mainkey;
            mainkey = Microsoft.Win32.Registry.CurrentUser.CreateSubKey("Task_Scheduler");
            bool needPathsSet = false;

            foreach (String keyName in RegKeys.Values)
            {
                if (mainkey.GetValue(keyName) as string == "" || mainkey.GetValue(keyName) == null)
                {
                    Microsoft.Win32.RegistryKey subkey = mainkey.CreateSubKey(keyName);
                    subkey.SetValue(keyName, "");
                    needPathsSet = true;
                }
            }

            if (needPathsSet)
            {
                FolderBrowserDialog dialog = new FolderBrowserDialog();
                DialogResult result = dialog.ShowDialog();

                // OK button was pressed. 
                if (result == System.Windows.Forms.DialogResult.OK)
                {
                    string path = dialog.SelectedPath;
                    mainkey.SetValue(RegKeys[RegistryKeys.OutputFile], path);
                }
            }

            mainkey.Close();
        }

        public static string GetRegKeyValue(RegistryKeys name)
        {
            Microsoft.Win32.RegistryKey mainkey;
            mainkey = Microsoft.Win32.Registry.CurrentUser.CreateSubKey("Task_Scheduler");
            return mainkey.GetValue(RegKeys[name]) as string;
        }
    }
}
