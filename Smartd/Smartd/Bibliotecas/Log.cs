using System;
using System.Runtime.CompilerServices;

namespace Smartd.Bibliotecas
{
    public class Log
    {
        public static Action<string> TFLog;

        public static void WriteLine<T>(string function, object msg)
        {
            WriteToLog($"{typeof(T).ToString()} in function {function}. || Message: {msg}");
        }

        public static void WriteLine(object msg, string stackTrace = null, [CallerLineNumber] int lineNumber = 0, [CallerMemberName] string function = null, [CallerFilePath] string caller = null)
        {
            WriteToLog($"{caller} in func {function} line {lineNumber}. || Msg: {msg}, stackT: {stackTrace}");
        }

        public static void Info<T>(string function, object msg)
        {
            WriteToLog($"INFO: {typeof(T).ToString()} in function {function}. || Info: {msg}");
        }

        public static void Info(object msg, string stackTrace = null, [CallerLineNumber] int lineNumber = 0, [CallerMemberName] string function = null, [CallerFilePath] string caller = null)
        {
            WriteToLog($"INFO: {caller} in func {function} line {lineNumber}. || Msg: {msg}, stackT: {stackTrace}");
        }

        public static void Warning<T>(string function, object msg)
        {
            WriteToLog($"WARNING: {typeof(T).ToString()} in function {function}. || Warning: {msg}");
        }

        public static void Warning(object msg, string stackTrace = null, [CallerLineNumber] int lineNumber = 0, [CallerMemberName] string function = null, [CallerFilePath] string caller = null)
        {
            WriteToLog($"WARNING: {caller} in func {function} line {lineNumber}. || Msg: {msg}, stackT: {stackTrace}");
        }

        public static void Error<T>(string function, object msg)
        {
            WriteToLog($"ERROR: {typeof(T).ToString()} in function {function}. || Error: {msg}");
        }

        public static void Error(object msg, string stackTrace = null, [CallerLineNumber] int lineNumber = 0, [CallerMemberName] string function = null, [CallerFilePath] string caller = null)
        {
            WriteToLog($"ERROR: {caller} in func {function} line {lineNumber}. || Msg: {msg}, stackT: {stackTrace}");
        }

        private static void WriteToLog(string msg)
        {
            Console.WriteLine(msg);
            try
            {
                if (TFLog != null)
                    TFLog(msg);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.StackTrace);
            }
        }
    }
}
