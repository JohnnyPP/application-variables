using System;
using System.IO;

namespace variables
{
	class MainClass
	{
		static long CountLinesInString(string s)
		{
			long count = 1;
			int start = 0;
			while ((start = s.IndexOf('\n', start)) != -1)
			{
				count++;
				start++;
			}
			return count;
		}

		public static void Main (string[] args)
		{
			long numberOfLines;
			string pathVariables = "/media/ntfs2/FirefoxProfile/zotero/storage/MKDMD6AZ/variables/mainVariables.txt";
			string text = System.IO.File.ReadAllText(pathVariables);

			System.Console.WriteLine (text);
			numberOfLines = CountLinesInString (text);
			System.Console.WriteLine ("Number of lines: {0}", numberOfLines-1);

			string[] searchFor = { ":", "\n"};
			int[,] array = new int[2, numberOfLines-1];		//	2 rows, 10 columns
			string[] textSliced = new string[numberOfLines-1];

			for (int i=0; i < 2; i++)
			{
				int at = 0;
				int start = 0;
				int whileCounter = 0;

				Console.WriteLine ("Searichng for: {0}", searchFor [i]);
				while((start < text.Length) && (at > -1))
				{
					at = text.IndexOf(searchFor[i], start);
					if (at == -1) break;
					array.SetValue(at+1, i, whileCounter);
					Console.Write("{0} ", at);
					start = at+1;
					whileCounter++;
				}
				System.Console.Write ("\n");
			}

			Console.WriteLine("Printing array contents:");

			int rowLength = array.GetLength(0);
			int colLength = array.GetLength(1);

			for (int i = 0; i < rowLength; i++)
			{
				for (int j = 0; j < colLength; j++)
				{
					Console.Write(string.Format("{0} ", array[i, j]));
				}
				Console.Write(Environment.NewLine + Environment.NewLine);
			}

			int substracted = 0;

			Console.WriteLine("Lenght of the text variables: \n");

			for (int i = 0; i < colLength; i++)
			{
				substracted = array [1, i] - array [0, i];
				Console.Write(string.Format("{0} ", substracted));

				string sub = text.Substring(array[0, i], substracted);
				Console.WriteLine("Substring: {0}", sub);
				textSliced.SetValue (sub, i);

			}

			Console.WriteLine("\n File content sliced strings: \n");

			foreach (string s in textSliced) 
			{
				Console.WriteLine("Substring: {0}", s);
			}
		
			string pathConstant = "/media/ntfs2/FirefoxProfile/zotero/storage/MKDMD6AZ/variables/";

			//
			// Writes company name into coverLetterRecipientFirstLine.txt
			//
			using (StreamWriter outfile = new StreamWriter(pathConstant + "coverLetterRecipientFirstLine.txt"))
			{
				outfile.Write(textSliced[0].Trim());
			}

			//
			// Writes: 1. Recruiting name, 2. Person name, 3. Street, 4 City into coverLetterRecipientSecondLine.txt
			//
			using (StreamWriter outfile = new StreamWriter(pathConstant + "coverLetterRecipientSecondLine.txt"))
			{
				outfile.Write (textSliced [1].Trim ());

				if (textSliced [2].Trim () == "") 
				{
					
				} 
				else 
				{
					outfile.Write ("\\\\" + textSliced [2].Trim ());
				}

				if (textSliced [3].Trim () == "") 
				{

				} 
				else 
				{
					outfile.Write ("\\\\" + textSliced [3].Trim ());
				}

				if (textSliced [4].Trim () == "") 
				{

				} 
				else 
				{
					outfile.Write ("\\\\" +textSliced [4].Trim ());
				}
			}

			//
			// Writes LetterOpening.txt. Letter opening has 1 or 0 in front of the string. 1 means that this line is used.
			//
			using (StreamWriter outfile = new StreamWriter(pathConstant + "coverLetterOpening.txt"))
			{
				for(int i=5; i<=7; i++)
				{
					string slicedTrimmed = textSliced[i].Trim();

					if (slicedTrimmed[0].ToString() == "1") 
					{
						int lentgh = slicedTrimmed.Length;
						outfile.Write(slicedTrimmed.Substring (1, lentgh-1)); // remove the number from the beginning of the string
						break;
					}
				}
			}
			//
			// Writes coverLetterPosition.txt
			//
			using (StreamWriter outfile = new StreamWriter(pathConstant + "coverLetterPosition.txt"))
			{
				outfile.Write(textSliced[8].Trim());
			}
			//
			// Writes coverLetterSalary.txt
			//
			using (StreamWriter outfile = new StreamWriter(pathConstant + "coverLetterSalary.txt"))
			{
				for(int i=9; i<=10; i++)
				{
					string slicedTrimmed = textSliced[i].Trim();

					if (slicedTrimmed[0].ToString() == "1") 
					{
						int lentgh = slicedTrimmed.Length;
						outfile.Write(" " + slicedTrimmed.Substring (1, lentgh-1) + " "); // remove the number from the beginning of the string
						break;
					}
				}
			}
			//
			// Writes eMailAddress.txt
			//
			using (StreamWriter outfile = new StreamWriter(pathConstant + "eMailAddress.txt"))
			{
				outfile.Write(textSliced[11].Trim());
			}
		}
	}
}
