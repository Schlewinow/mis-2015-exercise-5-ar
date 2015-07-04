using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class WebContentPersonInfo : WebContentHTMLBase
{
	// Get data about person from a BISON web page.
	protected override string GetSpecificText(string html)
	{
		string personStart = "<table summary=\"Grunddaten zur Veranstaltung\">";
		string personEnd = "</table>";

		if(html.IndexOf(personStart) < 0)
		{
			return string.Empty;
		}

		string personText = html.Substring(html.IndexOf(personStart));
		personText = personText.Substring(0, personText.IndexOf(personEnd));

		string lineStart = "<th";
		string lineEnd = "</th>";
		int textIndex = personText.IndexOf(lineStart);
		List<string> infos = new List<string>();

		bool even = true;

		while(textIndex >= 0)
		{
			personText = personText.Substring(textIndex + lineStart.Length);
			
			string info = personText.Substring(personText.IndexOf(">") + 1);
			info = info.Substring(0, info.IndexOf(lineEnd));

			if(info.Contains("<abbr "))
			{
				info = info.Substring(info.IndexOf(">") + 1);
				info = info.Replace("</abbr>", string.Empty);
			}

			while(info.StartsWith(" "))
			{
				info = info.Substring(1);
			}
			
			infos.Add(info);

			if(even)
			{
				lineStart = "<td";
				lineEnd = "</td>";
			}
			else
			{
				lineStart = "<th";
				lineEnd = "</th>";
			}
			even = !even;
			
			textIndex = personText.IndexOf(lineStart);
		}

		string finalInfos = "";
		for(int index = 0; index < infos.Count; index+=2)
		{
			finalInfos += infos[index] + ": " + infos[index + 1] + "\n";
		}
		
		return finalInfos;
	}
}
