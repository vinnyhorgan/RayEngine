namespace RayEngine.Editor
{
	class ScenePanel
	{
		public static Scene current;

		public ScenePanel()
		{
			string scenePath = FilesystemPanel.baseDir + "/Scenes";

			if (!Directory.Exists(scenePath))
			{
				Directory.CreateDirectory(scenePath);
			}
		}

		public void New()
		{
			current = new Scene();
		}

		public void Save()
		{
			current.saved = true;

			string serialized = JsonConvert.SerializeObject(current);

			Utils.Write(FilesystemPanel.baseDir + "/Scenes/" + current.name + ".scene", serialized);
		}

		public static void Load(string path)
		{
			string serialized = Utils.Read(path);

			current = JsonConvert.DeserializeObject<Scene>(serialized);
		}

		public void Draw()
		{
			if (current != null)
			{
				ImGui.Text(current.name);

				ImGui.Separator();

				foreach (Entity e in current.entities)
				{
					if (ImGui.Button(e.name))
					{
						InspectorPanel.current = e;
					}
				}
			}
		}
	}
}