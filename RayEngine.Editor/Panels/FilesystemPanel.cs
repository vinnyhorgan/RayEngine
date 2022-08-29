namespace RayEngine.Editor
{
	class FilesystemPanel
	{
		List<string> path = new List<string>();
		public static string baseDir = Directory.GetCurrentDirectory() + "/TestProject";

		public void Draw()
		{
			string currentPath = baseDir;

			string baseName = new DirectoryInfo(baseDir).Name;

			string displayPath = baseName;

			foreach (string dir in path)
			{
				currentPath += "/" + dir;
				displayPath += "/" + dir;
			}

			ImGui.Text(displayPath);

			ImGui.Separator();

			if (path.Count > 0)
			{
				if (ImGui.Button(".."))
				{
					path.RemoveAt(path.Count - 1);
				}
			}

			string[] directories =  Directory.GetDirectories(currentPath);

			foreach (string dir in directories)
			{
				string name = new DirectoryInfo(dir).Name;

				if (ImGui.Button(name))
				{
					path.Add(name);
				}
			}

			string[] files =  Directory.GetFiles(currentPath);

			foreach (string f in files)
			{
				string extension = Path.GetExtension(f);
				string name = new FileInfo(f).Name;

				if (extension == ".scene")
				{
					if (ImGui.Button(name))
					{
						ScenePanel.Load(f);
					}
				}
				else
				{
					ImGui.Text(name);
				}
			}
		}
	}
}