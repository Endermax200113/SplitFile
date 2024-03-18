using System;

namespace SplitFile.Exceptions
{
	public class ButtonException : Exception
	{
		public enum TypeButtonException {
			ERR_BUTTON_NOT_EXISTS,
			ERR_BUTTON_NOT_BELONG,
			ERR_BUTTON_UNKNOWN
		}

		private const string ERR_NOT_EXISTS = "The button is not exists";
		private const string ERR_NOT_BELONG = "This object does not belong to the material button";
		private const string ERR_UNKNOWN = "Unknown error";

		public TypeButtonException TypeException { get; private set;}

		public ButtonException() : base(ERR_UNKNOWN) {
			TypeException = TypeButtonException.ERR_BUTTON_UNKNOWN;
		}

		public ButtonException(string message, TypeButtonException type) : base(message) {
			TypeException = type;
		}

		public ButtonException(TypeButtonException typeException) : base(GetMessageByType(typeException))
		{
			TypeException = typeException;
		}

		private static string GetMessageByType(TypeButtonException type)
		{
			switch (type) {
				case TypeButtonException.ERR_BUTTON_NOT_EXISTS:
					return ERR_NOT_EXISTS;
				case TypeButtonException.ERR_BUTTON_NOT_BELONG:
					return ERR_NOT_BELONG;
				case TypeButtonException.ERR_BUTTON_UNKNOWN:
				default:
					return ERR_UNKNOWN;
			}
		}
	}
}
