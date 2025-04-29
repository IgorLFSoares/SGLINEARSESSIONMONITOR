// SGLinearSessionMonitor/Models/SessionInfo.cs
namespace SGLinearSessionMonitor.Models
{
    public class SessionInfo
    {
        public string UserName { get; set; }
        public string SessionId { get; set; }
        public int InstanceCount { get; set; }
        public string MachineName { get; set; }

        public SessionInfo(string userName, string sessionId, string machineName)
        {
            UserName = userName;
            SessionId = sessionId;
            MachineName = machineName;
            InstanceCount = 0; // Inicializando com zero instâncias
        }
    }
}
