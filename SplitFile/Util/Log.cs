using SplitFile.Exceptions;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SplitFile.Util
{
	public static class Log
	{
		private static string Path { get; set; }
		private static StreamWriter Stream { get; set; }

		private static bool _inited = false;
		private static bool _ended = false;

		private const string TYPE_MESSAGE_INFO = "INFO";
		private const string TYPE_MESSAGE_WARN = "WARN";
		private const string TYPE_MESSAGE_ERROR = "ERROR";
		private const string TYPE_MESSAGE_DEBUG = "DEBUG";

		public static void Init()
		{
			try
			{
				if (_inited)
					return;

				if (_ended)
					throw new InitException(InitException.TypeInitException.ERR_INITIAL_FAILED);

				SetPath();
				NewStream();

				_inited = true;
			}
			catch (InitException err)
			{
				InitException.SendMessage(err, nameof(Log), nameof(Init));
			}
		}

		public static void Info(string message)
		{
			Message(TYPE_MESSAGE_INFO, message);
		}

		public static void Warn(string message)
		{
			Message(TYPE_MESSAGE_WARN, message);
		}

		public static void Error(string message)
		{
			Message(TYPE_MESSAGE_ERROR, message);
		}

		public static void Debug(string message)
		{
			Message(TYPE_MESSAGE_DEBUG, message);
		}

		private static void Message(string type, string message)
		{
			try
			{
				if (!_inited || _ended)
					throw new InitException(InitException.TypeInitException.ERR_INITIAL_NOT_INITIALIZED);


				string fullMsg = $"[{GetDateTime()}] [{type}] {message}";

				Console.WriteLine(fullMsg);
				Stream.WriteLine(fullMsg);
				Stream.Flush();
			}
			catch (InitException err)
			{
				InitException.SendMessage(err, nameof(Log), nameof(Message));
			}
		}

		public static void End()
		{
			try
			{
				if (_ended)
					return;

				if (!_inited)
					throw new InitException(InitException.TypeInitException.ERR_INITIAL_NOT_INITIALIZED);

				Stream.Close();

				_ended = true;
			}
			catch (InitException err)
			{
				InitException.SendMessage(err, nameof(Log), nameof(End));
			}
		}

		private static void NewStream()
		{
			DateTime now = DateTime.Now;

			string nameFile = $"{now:yyyy-MM-dd_HH.mm.ss}.log";
			string fullPath = $"{Path}\\{nameFile}";

			Stream = File.AppendText(fullPath);
		}

		private static string GetDateTime()
		{
			DateTime now = DateTime.Now;

			return now.ToString("dd.MM.yyyy HH:mm:ss");
		}

		private static void SetPath()
		{
			string pathApp = Application.StartupPath;
			string pathLog = $"{pathApp}\\logs";

			if (!Directory.Exists(pathLog))
				Directory.CreateDirectory(pathLog);

			Path = pathLog;
		}
	}
}
