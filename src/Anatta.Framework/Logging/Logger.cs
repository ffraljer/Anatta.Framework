namespace Anatta.Framework.Logging;

public enum LogLevel {
    Trace,
    Debug,
    Info,
    Warn,
    Error,
    Fatal
}

public class Logger {
    private readonly string name;
    private readonly string logFilePath;

    public Logger(string name) {
        this.name = name;

        var logDirectory = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Logs");
        if (!Directory.Exists(logDirectory))
            Directory.CreateDirectory(logDirectory);

        var timestamp = DateTime.Now.ToString("yyyyMMdd");
        logFilePath = Path.Combine(logDirectory, $"{name}_{timestamp}.log");
    }

    public void Log(LogLevel level, string message) {
        var timestamp = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");
        var logEntry = $"[{timestamp}] [{level}] [{name}] {message}";

        Console.WriteLine(logEntry);

        try {
            File.AppendAllText(logFilePath, logEntry + Environment.NewLine);
        }
        catch (Exception ex) {
            Console.WriteLine($"[Logger Error] Failed to write to log file: {ex.Message}");
        }
    }

    public void Trace(string message) => Log(LogLevel.Trace, message);
    public void Debug(string message) => Log(LogLevel.Debug, message);
    public void Info(string message) => Log(LogLevel.Info, message);
    public void Warn(string message) => Log(LogLevel.Warn, message);
    public void Error(string message) => Log(LogLevel.Error, message);
    public void Fatal(string message) => Log(LogLevel.Fatal, message);
}