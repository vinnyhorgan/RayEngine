namespace RayEngine.Editor
{
	class ConsolePanel
	{
		public static List<string> buffer = new List<string>();

		public static void Log(string text)
		{
			string date = DateTime.Now.ToString("[HH:mm:ss] ");

			buffer.Add(date + text);
		}

		public void Draw()
		{
			foreach (string line in buffer)
			{
				ImGui.Spacing();
				ImGui.Text(line);
				ImGui.Spacing();
				ImGui.Separator();

				ImGui.SetScrollY(ImGui.GetScrollMaxY());
			}
		}
	}
}