using Dependency_Injection_SMTP_Demo_master_VS2022;

//-- No instance of Controller is required -- All references are static
//-- Controller controller = new Controller();

/*  BLOCK ONE ***********************************************/

// Build a collection of name strings to be parsed
List<string> nameList = new List<string>()
				{
					 "kevin bowe",
					 "bowe, kevin",
					 "Johnney Cash",
					 "Miles Davis",
					 "Plant, Robert",
					 "Gaga, Lady",
					 "Lin-Manual Miranda",
					 "Bono"
				};

/*  BLOCK TWO ***********************************************/

// Inject the correct dependency based on the name format.
foreach (string name in nameList)
{
	// choose nameParser
	INameParser nameParser;
	switch (Controller.GetNameForm(name))
	{
		case "FirstLast":
			nameParser = new FirstLastParser();
			break;
		case "LastFirst":
			nameParser = new LastFirstParser();
			break;
		case "OneName":
		default:
			nameParser = new OneNameParser();
			break;
	}
	ParsedName parsedName = nameParser.ParseName(name);

	/*  BLOCK THREE ***********************************************/

	// choose EmailSender
	IEmailSender emailSender;
	if (parsedName.Last?.Length == 4)
			emailSender = new MyEmailSender();
	else emailSender = new YourEmailSender();

	/*  BLOCK FOUR ***********************************************/

	// Build the message
	string message = Controller.BuildMessage(name, parsedName, nameParser, emailSender);

	// Send the message
	emailSender.SendEmail("joe@gmail.com", "me@hotmail.com", "Let's play some music", message);

} // END_FOREACH






