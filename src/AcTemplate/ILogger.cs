namespace AcTemplate
{
    public interface ILogger
    {
        void Debug(string msg);
        void Info(string msg);
        void Error(string error);
        void Error(Exception ex);
    }

    public static class LoggerHelper
    {
        public static ILogger Compose(this ILogger logger1, ILogger logger2)
        {
            return new MultLogger(new ILogger[] { logger1, logger2 });
        }

        public static string BuildExceptionString(Exception ex)
        {
            StringBuilder sb = new StringBuilder();
            AppendExceptionInfo(ex, sb, 0);

            return sb.ToString();
        }
        public static void AppendExceptionInfo(Exception ex, StringBuilder sb, int depth)
        {
            string error = $"{ex.GetType().FullName}, {ex.Message}\n{ex.StackTrace}";
            sb.AppendFormat(error);

            if (ex.InnerException != null && depth < 9)
            {
                sb.AppendLine();
                AppendExceptionInfo(ex.InnerException, sb, ++depth);
            }
        }
    }

    public class MultLogger : ILogger
    {
        ILogger[] _loggers;
        public MultLogger(ILogger[] loggers)
        {
            this._loggers = loggers.ToArray();
        }

        public void Debug(string msg)
        {
            foreach (var logger in this._loggers)
            {
                logger.Debug(msg);
            }
        }

        public void Error(string error)
        {
            foreach (var logger in this._loggers)
            {
                logger.Error(error);
            }
        }

        public void Error(Exception ex)
        {
            foreach (var logger in this._loggers)
            {
                logger.Error(ex);
            }
        }

        public void Info(string msg)
        {
            foreach (var logger in this._loggers)
            {
                logger.Info(msg);
            }
        }
    }

    public class NullLogger : ILogger
    {
        public static readonly NullLogger Instance = new NullLogger();

        public void Debug(string msg)
        {

        }

        public void Error(string error)
        {

        }

        public void Error(Exception ex)
        {

        }

        public void Info(string msg)
        {

        }
    }

    public abstract class AbstractLogger : ILogger
    {
        public abstract void Write(string text);

        public virtual void Log(string type, string log)
        {
            string text = $"{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.ffff")} [{type}]: {log}\n";
            this.Write(text);
        }

        public virtual void Debug(string msg)
        {
            this.Log(nameof(Debug), msg);
        }

        public virtual void Error(string error)
        {
            this.Log(nameof(Error), error);
        }

        public virtual void Error(Exception ex)
        {
            this.Error(LoggerHelper.BuildExceptionString(ex));
        }

        public virtual void Info(string msg)
        {
            this.Log(nameof(Info), msg);
        }
    }

    public class SimpleLogger : AbstractLogger, ILogger
    {
        static object _lock = new object();

        static string FileName = $"log_{DateTime.Now.ToString("yyyyMMddHHmmss")}.txt";

        public readonly static SimpleLogger Instance = new SimpleLogger();

        public string LogDir { get; set; } = "";

        string GetLogFilePath()
        {
            if (!string.IsNullOrEmpty(this.LogDir) && Directory.Exists(this.LogDir) == false)
            {
                Directory.CreateDirectory(this.LogDir);
            }

            string path = Path.Combine(this.LogDir, FileName);
            return path;
        }

        public override void Write(string text)
        {
            lock (_lock)
            {
                try
                {
                    System.IO.File.AppendAllText(this.GetLogFilePath(), text);
                }
                catch
                {

                }
            }
        }
    }

    public class PrintLogger : AbstractLogger, ILogger
    {
        public static readonly PrintLogger Instance = new PrintLogger();

        public override void Write(string text)
        {
            Console.Write(text);
        }
    }
}
