using UnityEngine;
using System.IO;
using System;

public class DKLog
{

    public static void Log(object message)
    {
        string content = SaveContent(message);

#if LOG_TO_UNITY_CONSOLE
        Debug.Log($"<color=#4EC9B0>[LOG]</color>{content}");
#endif
    }

    public static void LogFormat(string format, params object[] args)
    {
        string content = SaveContent(format, args);

#if LOG_TO_UNITY_CONSOLE
        Debug.Log($"<color=#4EC9B0>[LOG]</color>{content}");
#endif
    }

    public static void LogWarning(object message)
    {
        string content = SaveContent(message);

#if LOG_TO_UNITY_CONSOLE
        Debug.LogWarning($"<color=yellow>[WARNING]</color>{content}");
#endif
    }

    public static void LogWarningFormat(string format, params object[] args)
    {
        string content = SaveContent(format, args);

#if LOG_TO_UNITY_CONSOLE
        Debug.LogWarning($"<color=yellow>[WARNING]</color>{content}");
#endif
    }

    public static void LogError(object message)
    {
        string content = SaveContent(message);

#if LOG_TO_UNITY_CONSOLE
        Debug.LogError($"<color=red>[ERROR]</color>{content}");
#endif
    }

    public static void LogErrorFormat(string format, params object[] args)
    {
        string content = SaveContent(format, args);

#if LOG_TO_UNITY_CONSOLE
        Debug.LogError($"<color=red>[ERROR]</color>{content}");
#endif
    }

    private static string SaveContent(string message)
    {
#if DK_LOG
        CheckInit();
        WriteToFile(message);
        return message;
#else
        return message;
#endif
    }

    private static string SaveContent(object message)
    {
#if DK_LOG
        CheckInit();
        string content = message.ToString();
        WriteToFile(content);
        return content;
#else
        return message.ToString();
#endif
    }

    private static string SaveContent(string format, params object[] args)
    {
#if DK_LOG
        CheckInit();
        string content = string.Format(format, args);
        WriteToFile(content);
        return content;
#else
        return string.Format(format, args);
#endif
    }


#if ENCRYPT_LOG
        static System.IO.BinaryWriter logfile = null;
#else
    static System.IO.StreamWriter logfile = null;
#endif

    private static void CheckInit()
    {
        // initialize log file

#if !LOG_TO_UNITY_CONSOLE
        // handle system message from Unity
        Application.logMessageReceived += HandleSysLog;
#endif
    }

    private static void HandleSysLog(string condition, string stackTrace, LogType type)
    {
        if (logfile != null)
            return;

        try
        {
            // 创建文件 //
            string filePath = GetLogPath();

            if (!Directory.Exists(filePath))
            {
                Directory.CreateDirectory(filePath);
            }

            // 删除旧日志 //
            DirectoryInfo info = new DirectoryInfo(filePath);
            FileInfo[] files = info.GetFiles();
            for (int i = 0; i < files.Length; ++i)
            {
                long deltaTicks = DateTime.Now.Ticks - files[i].LastWriteTime.Ticks;
                TimeSpan elapsedSpan = new TimeSpan(deltaTicks);
                if (elapsedSpan.Days >= 2)
                {
                    files[i].Delete();
                }
            }

            string fileName = filePath + "/Log_" + DateTime.Now.ToString("yyyy_MM_dd_HH_mm_ss") + ".log";
#if ENCRYPT_LOG
                logfile = new System.IO.BinaryWriter(File.Open(fileName, System.IO.FileMode.Create, System.IO.FileAccess.Write));
#else
            logfile = new System.IO.StreamWriter(fileName);
#endif

        }
        catch (Exception e)
        {
            Debug.LogError("Log file initialize error:" + e);
        }
    }

    private static string logPath = string.Empty;

    private static string GetLogPath()
    {
        if (string.IsNullOrEmpty(logPath))
            logPath = Path.Combine(GetCachePath(), "Logs");
        if (!Directory.Exists(logPath))
            Directory.CreateDirectory(logPath);
        return logPath;
    }

    private static string cachePath;

    private static string GetCachePath()
    {
        if (string.IsNullOrEmpty(cachePath))
        {
            cachePath = Application.persistentDataPath;
        }
        return cachePath;
    }

    static void WriteToFile(string str)
    {
        if (logfile != null)
        {
#if ENCRYPT_LOG
            byte[] encrypted = XOREncrypter.EncryptToBytes(str);
            logfile.Write(encrypted.Length);
            logfile.Write(encrypted, 0, encrypted.Length);
            logfile.Flush();
#else
            logfile.Write(str);
            logfile.Flush();
#endif
        }
    }
}