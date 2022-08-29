namespace RayEngine.Editor
{
	class ScenePanel
	{
		public static Scene current;

		public void New()
		{
			current = new Scene();
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