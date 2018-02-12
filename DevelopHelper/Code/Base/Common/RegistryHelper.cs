using Microsoft.Win32;

namespace Common
{
    public class RegistryHelper
    {
        public static string GetLocalMachineKeyValue(string path, string keyName)
        {
            RegistryKey registryKey = Registry.LocalMachine.OpenSubKey(path, true);
            if (registryKey != null)
            {
                object value = registryKey.GetValue(keyName);
                return value?.ToString() ?? "";
            }

            return null;
        }
    }
}
