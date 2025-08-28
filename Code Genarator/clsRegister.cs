using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Code_Genarator
{
    public static class clsRegister
    {
        public static void SaveRegistry(string UserName, string Password)
        {
            string KeyPath = @"HKEY_CURRENT_USER\SOFTWARE\CodeGen";
            try
            {
                Registry.SetValue(KeyPath, "UserName", UserName, RegistryValueKind.String);
                Registry.SetValue(KeyPath, "Password", Password, RegistryValueKind.String);

            }
            catch { }
        }

        public static bool DeleteRegistry()
        {
            string keyPath = @"SOFTWARE\CodeGen";
            try
            {
                // Open the registry key in read/write mode with explicit registry view
                using (RegistryKey baseKey = RegistryKey.OpenBaseKey(RegistryHive.CurrentUser, RegistryView.Registry64))
                {
                    using (RegistryKey key = baseKey.OpenSubKey(keyPath, true))
                    {
                        if (key != null)
                        {
                            // Delete the specified value
                            key.DeleteValue("UserName");
                            key.DeleteValue("Password");
                            return true;
                        }
                    }
                }
            }
            catch { }
            return false;
        }

        public static bool GetAccountFromRegistry(ref string UserName, ref string Password)
        {
            string KeyPath = @"HKEY_CURRENT_USER\SOFTWARE\CodeGen";
            try
            {
                // Read the value from the Registry
                UserName = Registry.GetValue(KeyPath, "UserName", null) as string;
                Password = Registry.GetValue(KeyPath, "Password", null) as string;
                return true;
            }
            catch { return false; }
        }
    }
}
