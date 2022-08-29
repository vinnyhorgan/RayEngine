namespace RayEngine.Editor
{
	class EditorScreen : Screen
	{
		bool showAbout = false;

		ViewportPanel viewportPanel = new ViewportPanel();
		ConsolePanel consolePanel = new ConsolePanel();
		ScenePanel scenePanel = new ScenePanel();
		InspectorPanel inspectorPanel = new InspectorPanel();
		FilesystemPanel filesystemPanel = new FilesystemPanel();

		public EditorScreen()
		{
			MaximizeWindow();
		}

		public override void Update(float dt)
		{
			viewportPanel.Update(dt);
		}

		public override void Draw()
		{
			if (ImGui.BeginMainMenuBar())
			{
				if (ImGui.BeginMenu("System"))
				{
					if (ImGui.MenuItem("Quit"))
					{
						ScreenManager.exit = true;
					}

					ImGui.EndMenu();
				}

				if (ImGui.BeginMenu("Scene"))
				{
					if (ImGui.MenuItem("New"))
					{
						scenePanel.New();
					}

					ImGui.EndMenu();
				}

				if (ImGui.BeginMenu("Entity"))
				{
					if (ImGui.MenuItem("New"))
					{
						inspectorPanel.New();
					}

					if (ImGui.MenuItem("Add component"))
					{

					}

					ImGui.EndMenu();
				}

				if (ImGui.BeginMenu("Help"))
				{
					if (ImGui.MenuItem("About"))
					{
						showAbout = true;
					}

					ImGui.EndMenu();
				}

				ImGui.EndMainMenuBar();
			}

			if (ImGui.Begin("Viewport"))
			{
				viewportPanel.Draw();
			}
			ImGui.End();

			if (ImGui.Begin("Console"))
			{
				consolePanel.Draw();
			}
			ImGui.End();

			if (ImGui.Begin("Scene"))
			{
				scenePanel.Draw();
			}
			ImGui.End();

			if (ImGui.Begin("Inspector"))
			{
				inspectorPanel.Draw();
			}
			ImGui.End();

			if (ImGui.Begin("Filesystem"))
			{
				filesystemPanel.Draw();
			}
			ImGui.End();

			if (showAbout == true)
			{
				if (ImGui.Begin("About", ref showAbout))
				{
					ImGui.Text("Ray Engine");
					ImGui.Separator();
					ImGui.Text("By Vinny Horgan");
				}
				ImGui.End();
			}
		}

		public override void Destroy()
		{

		}
	}
}