namespace RayEngine.Editor
{
	enum ComponentTypes
	{
		Transform,
		MeshRenderer
	}

	class InspectorPanel
	{
		public static Entity current;

		public static Dictionary<MeshRendererComponent, Model> models = new Dictionary<MeshRendererComponent, Model>();
		public static Dictionary<MeshRendererComponent, Texture2D> textures = new Dictionary<MeshRendererComponent, Texture2D>();

		public void New()
		{
			if (ScenePanel.current != null)
			{
				Entity newEntity = new Entity();

				ScenePanel.current.entities.Add(newEntity);

				ScenePanel.current.saved = false;

				current = newEntity;

				AddComponent(ComponentTypes.Transform);

				ConsolePanel.Log("Created new entity");
			}
			else
			{
				ConsolePanel.Log("No scene selected!");
			}
		}

		public void AddComponent(ComponentTypes component)
		{
			if (current != null)
			{
				switch (component)
				{
					case ComponentTypes.Transform:
						current.transform = new TransformComponent();
						break;
					case ComponentTypes.MeshRenderer:
						current.meshRenderer = new MeshRendererComponent();
						break;
					default:
						break;
				}

				current.components.Add(component);

				ScenePanel.current.saved = false;
			}
			else
			{
				ConsolePanel.Log("No entity selected!");
			}
		}

		public void Draw()
		{
			if (current != null)
			{
				ImGui.Text(current.name);

				ImGui.Separator();

				foreach (ComponentTypes c in current.components)
				{
					switch (c)
					{
						case ComponentTypes.Transform:
							TransformComponent.Draw();

							break;
						case ComponentTypes.MeshRenderer:
							MeshRendererComponent.Draw();

							break;
						default:
							break;
					}
				}
			}
		}
	}
}