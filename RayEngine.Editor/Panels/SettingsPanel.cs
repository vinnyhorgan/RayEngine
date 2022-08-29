namespace RayEngine.Editor
{
	class SettingsPanel
	{
		public static Settings current;

		public SettingsPanel()
		{
			string settingsPath = FilesystemPanel.baseDir + "/ray.project";

			if (!File.Exists(settingsPath))
			{
				Settings newSettings = new Settings();
				newSettings.projectName = "Unnamed";

				string serialized = JsonConvert.SerializeObject(newSettings);

				Utils.Write(settingsPath, serialized);
			}

			string settings = Utils.Read(settingsPath);

			current = JsonConvert.DeserializeObject<Settings>(settings);
		}

		public void Draw()
		{

		}
	}
}