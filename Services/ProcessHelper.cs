using System;
using System.Diagnostics;
using System.Management;

namespace SGLinearSessionMonitor
{
    public static class ProcessHelper
    {
        public static string GetProcessUser(Process process)
        {
            try
            {
                string query = $"Select * From Win32_Process Where ProcessID = {process.Id}";
                using (ManagementObjectSearcher searcher = new ManagementObjectSearcher(query))
                using (ManagementObjectCollection processList = searcher.Get())
                {
                    foreach (ManagementObject obj in processList)
                    {
                        object[] outParameters = new object[2];
                        int returnVal = Convert.ToInt32(obj.InvokeMethod("GetOwner", outParameters));

                        if (returnVal == 0)
                        {
                            return $"{outParameters[1]}\\{outParameters[0]}"; // DOMAIN\Username
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.Log($"Erro ao obter usuário do processo: {ex.Message}");
            }

            return null;
        }
    }
}
