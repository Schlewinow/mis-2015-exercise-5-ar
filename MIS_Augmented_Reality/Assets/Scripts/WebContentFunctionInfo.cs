using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class WebContentFunctionInfo : WebContentHTMLBase
{
	// Read current functions of a person from a BISON web page.
	protected override string GetSpecificText(string html)
	{
		string functions = string.Empty;
		string faculties = string.Empty;

		string functionStart = "<caption class=\"t_capt\">Veranstaltungen</caption>";
		string functionEnd = "</table>";

		int functionTextIndex = html.IndexOf(functionStart);
		if(functionTextIndex >= 0)
		{
			string functionText = html.Substring(functionTextIndex);
			functionText = functionText.Substring(0, functionText.IndexOf(functionEnd));
			functions = this.ReadTable(functionText, "<td", "</td>", true);
		}
		
		string facultyStart = "<table summary=\"Übersicht über die Zugehörigkeit zu Einrichtungen\">";
		string facultyEnd = "</table>";

		int facultyIndex = html.IndexOf(facultyStart);
		if(facultyIndex >= 0)
		{
			string facultyText = html.Substring(facultyIndex);
			facultyText = facultyText.Substring(0, facultyText.IndexOf(facultyEnd));
			faculties = this.ReadTable(facultyText, "<td", "</td>", false);
		}

		return faculties + "\n\n" + functions;
	}

	private string ReadTable(string html, string lineStart, string lineEnd, bool isFunction)
	{
		int textIndex = html.IndexOf(lineStart);
		List<string> infos = new List<string>();
		
		while(textIndex >= 0)
		{
			html = html.Substring(textIndex + lineStart.Length);
			
			string info = html.Substring(html.IndexOf(">") + 1);
			info = info.Substring(0, info.IndexOf(lineEnd));
			
			if(info.Contains("<a "))
			{
				info = info.Substring(info.IndexOf(">") + 1);
				info = info.Replace("</a>", string.Empty);
			}
			
			while(info.StartsWith(" "))
			{
				info = info.Substring(1);
			}
			
			infos.Add(info);
			textIndex = html.IndexOf(lineStart);
		}
		
		string finalInfos = "";
		for(int index = 0; index < infos.Count; index++)
		{
			if(isFunction)
			{
				switch(index % 3)
				{
				case 0:
					finalInfos += "Veranstaltungsnummer: ";
					break;
				case 1:
					finalInfos += "Veranstaltung: ";
					break;
				case 2:
					finalInfos += "Semester: ";
					break;
				}
			}

			finalInfos += infos[index] + "\n";

			if(isFunction)
			{
				if(index%3 == 2)
				{
					finalInfos += "\n";
				}
			}
		}
		
		return finalInfos;
	}
}
