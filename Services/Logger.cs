using System;
using System.IO;

namespace SGLinearSessionMonitor
{
    public static class Logger
    {
        private static string LogFilePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "logs", "log.txt");

        // Método para logar mensagens
        public static void Log(string message, LogLevel logLevel = LogLevel.INFO)
        {
            string timestamp = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            string logMessage = $"[{timestamp}] [{logLevel}] {message}";

            // Escrever log no arquivo
            Directory.CreateDirectory(Path.GetDirectoryName(LogFilePath));  // Criar pasta de logs, caso não exista
            File.AppendAllText(LogFilePath, logMessage + Environment.NewLine);

            // Exibir no TextBox do formulário
            if (Application.OpenForms["MainForm"] is MainForm mainForm)
            {
                mainForm.UpdateLogs(logMessage);  // Atualiza a interface com o log
            }
        }
    }

    // Enum para definir os níveis de log
    public enum LogLevel
    {
        INFO,
        WARNING,
        ERROR
    }
}
