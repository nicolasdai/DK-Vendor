using UnityEngine;

public class DKLog
{
    public static void Log(object message)
    {
        Debug.Log(message);
    }

    public static void Log(object message, Object context)
    {
        Debug.Log(message, context);
    }
    
    public static void LogError(object message, Object context)
    {
        Debug.LogError(message, context);
    }

    public static void LogError(object message)
    {
        Debug.LogError(message);
    }

    public static void LogErrorFormat(Object context, string format, params object[] args)
    {
        Debug.LogErrorFormat(context, format, args);
    }

    public static void LogErrorFormat(string format, params object[] args)
    {
        Debug.LogErrorFormat(format, args);
    }

    public static void LogFormat(string format, params object[] args)
    {
        Debug.LogFormat(format, args);
    }

    public static void LogFormat(Object context, string format, params object[] args)
    {
        Debug.LogFormat(context, format, args);
    }

    public static void LogFormat(LogType logType, LogOption logOptions, Object context, string format, params object[] args)
    {
        Debug.LogFormat(logType, logOptions, context, format, args);
    }

    public static void LogWarning(object message, Object context)
    {
        Debug.LogWarning(message, context);
    }

    public static void LogWarning(object message)
    {
        Debug.LogWarning(message);
    }

    public static void LogWarningFormat(Object context, string format, params object[] args)
    {
        Debug.LogWarningFormat(context, format, args);
    }

    public static void LogWarningFormat(string format, params object[] args)
    {
        Debug.LogWarningFormat(format, args);
    }
}