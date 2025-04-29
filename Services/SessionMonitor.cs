using System;
using System.Collections.Generic;
using System.Linq;
using System.Diagnostics;

namespace SGLinearSessionMonitor
{
    public class SessionMonitor
    {
        private readonly int _checkInterval; // Intervalo de verificação (milissegundos)
        private readonly string _processName; // Nome do processo (sglinear.exe)
        private readonly Action<List<SessionInfo>> _updateGridCallback;

        private bool _isRunning;
        private List<Session> _sessions;

        public SessionMonitor(int checkInterval, string processName, Action<List<SessionInfo>> updateGridCallback)
        {
            _checkInterval = checkInterval;
            _processName = processName;
            _updateGridCallback = updateGridCallback;
            _sessions = new List<Session>();
        }

        public void Start()
        {
            _isRunning = true;
            MonitorSessions();
        }

        public void Stop()
        {
            _isRunning = false;
        }

        private async void MonitorSessions()
        {
            while (_isRunning)
            {
                _sessions.Clear();
                foreach (var process in Process.GetProcessesByName(_processName))
                {
                    var session = GetSessionFromProcess(process);
                    if (session != null)
                    {
                        _sessions.Add(session);
                    }
                }

                // Agrupar as sessões por usuário
                var groupedSessions = _sessions
                    .GroupBy(s => s.Username)
                    .ToList();

                foreach (var group in groupedSessions)
                {
                    // Verifica se o usuário excedeu o limite de 3 instâncias
                    if (group.Count() > 3)
                    {
                        KillExcessProcesses(group); // Mata os processos excedentes
                    }
                }

                // Atualiza a UI
                var sessionInfos = groupedSessions.Select(group => new SessionInfo
                {
                    Username = group.Key,
                    ProcessCount = group.Count()
                }).ToList();

                _updateGridCallback(sessionInfos); // Atualiza o DataGrid na UI
                await Task.Delay(_checkInterval); // Espera antes da próxima verificação
            }
        }

        private void KillExcessProcesses(IGrouping<string, Session> group)
        {
            // Filtrando os processos que estão acima do limite
            var excessProcesses = group.Skip(3); // Exclui os 3 primeiros, mantendo-os
            foreach (var process in excessProcesses)
            {
                try
                {
                    var p = Process.GetProcessById(process.SessionId);
                    p.Kill(); // Mata o processo
                    Logger.Log($"Processo excedente do usuário {group.Key} foi encerrado.", LogLevel.WARNING);
                }
                catch (Exception ex)
                {
                    Logger.Log($"Erro ao tentar matar o processo {process.SessionId}: {ex.Message}", LogLevel.ERROR);
                }
            }
        }

        private Session GetSessionFromProcess(Process process)
        {
            try
            {
                var sessionId = process.SessionId;
                var username = process.StartInfo.Environment["USERNAME"]; // Obter o nome do usuário da sessão
                return new Session { SessionId = sessionId, Username = username };
            }
            catch (Exception)
            {
                return null;
            }
        }
    }

    public class Session
    {
        public int SessionId { get; set; }
        public string Username { get; set; }
    }

    public class SessionInfo
    {
        public string Username { get; set; }
        public int ProcessCount { get; set; }
    }
}
