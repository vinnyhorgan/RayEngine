namespace RayEngine.Editor
{
	class InspectorPanel
	{
		public static Entity current;

		public void New()
		{
			if (ScenePanel.current != null)
			{
				ScenePanel.current.entities.Add(new Entity());

				ScenePanel.current.saved = false;

				ConsolePanel.Log("Created new entity");
			}
			else
			{
				ConsolePanel.Log("No scene selected!");
			}
		}

		public void Draw()
		{
			if (current != null)
			{
				ImGui.Text(current.name);

				ImGui.Separator();

				foreach (Component c in current.components)
				{
					ImGui.Text(c.name);
				}
			}
		}
	}
}