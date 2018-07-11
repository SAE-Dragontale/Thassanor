using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace ByteSprite.Development {
	public class DeveloperConsole : MonoBehaviour {
		readonly List<string> log = new List<string>();
		const int logMaxLength = 5000;
		const int logStringMaxLength = 65000;
		bool isEnabled;
		bool isCollapsed = true;
		Vector2 scrollPosition = Vector2.zero;
		string userInput = "";

		void OnEnable() {
			Application.logMessageReceived += Log;
		}

		void OnDisable() {
			Application.logMessageReceived -= Log;
		}

		void Log(string logString, string stackTrace, LogType type) {
			log.Insert(0, FormatString(logString, type));

			if (log.Count > logMaxLength) {
				log.RemoveRange(logMaxLength, log.Count - logMaxLength);
			}
		}

		void Update() {
			if (UnityEngine.Input.GetKeyDown(KeyCode.BackQuote) || UnityEngine.Input.GetKeyDown(KeyCode.Tab)) {
				isEnabled = !isEnabled;
			}
		}

		string GetLog(string filter = "") {
			string logString = "";

			List<string> logEntries = log;

			if (isCollapsed) {
				logEntries = GetCollapsedLog();
			}

			foreach (string entry in logEntries) {
				if (entry.Contains(filter))
					logString += entry + "\n";
			}
			return logString;
		}

		List<string> GetCollapsedLog() {
			List<string> collapsedLog = new List<string>();
			List<int> collapsedCount = new List<int>();

			foreach (string entry in log) {
				if (collapsedLog.Contains(entry))
					continue;

				collapsedLog.Add(entry);
				collapsedCount.Add(log.Count(n => n == entry));
			}

			int collapsedCountLength = collapsedCount.Count;
			for (int i = 0; i < collapsedLog.Count; i++) {
				if (i > collapsedCountLength)
					break;
				collapsedLog[i] = collapsedCount[i] + ": " + collapsedLog[i];
			}

			return collapsedLog;
		}

		void OnGUI() {
			if (isEnabled) {
				float halfScreenHeight = Screen.height / 2f;
				userInput = GUI.TextField(new Rect(0f, halfScreenHeight, Screen.width - 120f, 30f), userInput);

				string logString = GetLog(userInput);

				if (logString.Length > logStringMaxLength) {
					logString = logString.Substring(0, logStringMaxLength);
				}

				GUIContent content = new GUIContent(logString);
				GUIStyle style = new GUIStyle("textArea") {
					richText = true
				};

				float height = style.CalcHeight(content, Screen.width);

				GUIStyle buttonStyle = new GUIStyle(style) {
					alignment = TextAnchor.MiddleCenter,
					fontSize = 12
				};

				if (GUI.Button(new Rect(Screen.width - 60f, halfScreenHeight, 60f, 30f), "Clear", buttonStyle)) {
					log.Clear();
				}

				Color backgroundColor = GUI.backgroundColor;

				if (isCollapsed) {
					GUI.backgroundColor = Color.cyan;
					buttonStyle.normal.textColor = Color.cyan;
					buttonStyle.hover.textColor = Color.cyan;
				}

				if (GUI.Button(new Rect(Screen.width - 120f, halfScreenHeight, 60f, 30f), "Collapse", buttonStyle)) {
					isCollapsed = !isCollapsed;
				}

				GUI.backgroundColor = backgroundColor;

				scrollPosition = GUI.BeginScrollView(new Rect(0f, halfScreenHeight + 30f, Screen.width, halfScreenHeight - 30f), scrollPosition, new Rect(15, halfScreenHeight + 30f, Screen.width - 15f, height - 30f), GUIStyle.none, GUI.skin.verticalScrollbar);
				GUI.TextArea(new Rect(15f, halfScreenHeight + 30f, Screen.width, Mathf.Max(height, halfScreenHeight) - 30f), logString, style);
				GUI.EndScrollView();
			}
		}

		string FormatString(string logString, LogType logType) {
			switch (logType) {
				case LogType.Assert:
				case LogType.Error:
				case LogType.Exception:
					return "<color=red>" + logType.ToString() + " - " + logString + "</color>";
				case LogType.Log:
					return logType.ToString() + " - " + logString;
				case LogType.Warning:
					return "<color=yellow>" + logType.ToString() + " - " + logString + "</color>";
				default:
					return logString;
			}
		}
	}
}