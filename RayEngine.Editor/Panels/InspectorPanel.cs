namespace RayEngine.Editor
{
	class InspectorPanel
	{
		public static Entity current;

		public void New()
		{
			if (ScenePanel.current != null)
			{
				Entity newEntity = new Entity();

				ScenePanel.current.entities.Add(newEntity);

				ScenePanel.current.saved = false;

				current = newEntity;

				AddComponent(new TransformComponent());

				ConsolePanel.Log("Created new entity");
			}
			else
			{
				ConsolePanel.Log("No scene selected!");
			}
		}

		public void AddComponent(Component component)
		{
			current.components.Add(component);

			ScenePanel.current.saved = false;
		}

		public void Draw()
		{
			if (current != null)
			{
				ImGui.Text(current.name);

				ImGui.Separator();

				foreach (Component c in current.components)
				{
					switch (c.type)
					{
						case ComponentType.Transform:
							break;
						case ComponentType.MeshRenderer:
							break;
						default:
							break;
					}
				}
			}
		}
	}
}