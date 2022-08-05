namespace Dependency_Injection_SMTP_Demo_master_VS2022
{
	public struct ParsedName
	{
		public string First { get; set; }
		public string Last { get; set; }
	}

	public class Controller
	{
		const string theMessage = "Let's get together, play some music and have some fun.";

		/// <summary>
		/// Construct the email message body
		/// </summary>
		/// <param name="name">Pre-parsed name</param>
		/// <param name="parsedName">Parsed name struct: First: Last:</param>
		/// <param name="nameParser">NameParser object</param>
		/// <param name="emailSender">EmailSender object</param>
		/// <returns>String</returns>
		public static string BuildMessage(string name, ParsedName parsedName, INameParser nameParser, IEmailSender emailSender)
		{
			string parserName = nameParser.GetType().ToString();
			string emailSenderName = emailSender.GetType().ToString();

			var message =
				 $"{theMessage}\n\n" +
				 $"------------\n" +
				 $"Original Input Name: {name}\n" +
				 $"First: {parsedName.First}\n" +
				 $"Last: {parsedName.Last} \n" +
				 $"Name Parsed With: {parserName} \n" +
				 $"Message Sent With: {emailSenderName}" +
				 $"\n\n\n";
			return message;
		} // END_BuildMessage

		public static Func<string, bool> noValidToken = (s) => s.IndexOfAny(new char[] { ' ', ',' }) < 0;
		
		/// <summary>
		/// This returns the proper form of the name
		/// </summary>
		/// <param name="name">Pre-parsed name</param>
		/// <example>Kevin Bowe will return "FirstLast". Bowe, Kevin will return "LastFirst"</example>
		/// <returns>String</returns>
		public static string GetNameForm(string name)
		{
			//if (NoValidTokes(name)) return "OneName";

			if (noValidToken(name)) return "OneName";

			if (name.IndexOf(',') > 0) 
				return "LastFirst";
			return "FirstLast";
		}
	} //-- END Controller Class

	////////////////////////////////////////////////////////////////////////////////////////////////
	////////////////////////////////////////////////////////////////////////////////////////////////
	////////////////////////////////////////////////////////////////////////////////////////////////
	////////////////////////////////////////////////////////////////////////////////////////////////
	public interface INameParser
	{
		ParsedName ParseName(string input);
	}

	public class OneNameParser : INameParser
	{
		public ParsedName ParseName(string input)
		{

			return new ParsedName() { First = input };

		} // END_ParseName

	} // END_OneNameParser_Class

	public class FirstLastParser : INameParser
	{
		public ParsedName ParseName(string input)
		{
			string[] sa = input.Split();
			return new ParsedName()
			{
				First = sa[0].Trim(),
				Last = sa[1].Trim()
			};

		} // END_ParseName

	} // END_FirstLastParser_Class

	public class LastFirstParser : INameParser
	{
		public ParsedName ParseName(string input)
		{
			string[] sa = input.Split(',');
			return new ParsedName()
			{
				First = sa[1].Trim(),
				Last = sa[0].Trim()
			};
		} // END_ParseName

	} // END_LastFirstParser_Class


	////////////////////////////////////////////////////////////////////////////////////////////////
	////////////////////////////////////////////////////////////////////////////////////////////////
	////////////////////////////////////////////////////////////////////////////////////////////////
	////////////////////////////////////////////////////////////////////////////////////////////////
	public interface IEmailSender
	{
		void SendEmail(string to, string from, string subject, string body);
	}

	public class MyEmailSender : IEmailSender
	{
		public void SendEmail(string to, string from, string subject, string body)
		{
			string message = string.Format(
				 "eMail Sent\n" +
				 "----------\n" +
				 "To: {0}\n" +
				 "From: {1} \n" +
				 "Subject: {2}\n\n" +
				 "{3}",
				 to, from, subject, body
			);

			Console.WriteLine(message);
		}

	} // END_MyEmailSender_Class

	public class YourEmailSender : IEmailSender
	{
		public void SendEmail(string to, string from, string subject, string body)
		{
			string message = string.Format(
				 "eMail Sent\n" +
				 "----------\n" +
				 "To: {0}\n" +
				 "From: {1}\n" +
				 "Subject: {2}\n\n" +
				 "{3}",
				 to, from, subject, body
			);

			Console.WriteLine(message);
		}
	} // END_YourEmailSender_Class

}
