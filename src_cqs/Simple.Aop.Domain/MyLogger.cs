using System.Diagnostics;

namespace Simple.Aop.Domain
{
    public interface IMyLogger
    {
        void LogInformation(string message);
    }

    public class MyLogger : IMyLogger
    {
        public void LogInformation(string message)
        {
            Debug.WriteLine(message);
        }
    }
}
