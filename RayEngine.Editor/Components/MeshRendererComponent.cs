namespace RayEngine.Editor
{
	class MeshRendererComponent
	{
		public string mesh = "";
		public string texture = "";
		public bool meshLoaded = false;
		public bool textureLoaded = false;

		public unsafe static void Draw()
		{
			MeshRendererComponent c = InspectorPanel.current.meshRenderer;

			ImGui.Text("Mesh Renderer");

			ImGui.InputText("Mesh", ref c.mesh, 100);
			ImGui.InputText("Texture", ref c.texture, 100);

			string meshPath = FilesystemPanel.baseDir + "/" + c.mesh;
			string texturePath = FilesystemPanel.baseDir + "/" + c.texture;

			if (c.meshLoaded)
			{
				if (Path.GetExtension(meshPath) != ".obj" || !File.Exists(meshPath))
				{
					c.meshLoaded = false;

					if (InspectorPanel.models.ContainsKey(c))
					{
						InspectorPanel.models.Remove(c);
					}
				}
			}
			else
			{
				if (Path.GetExtension(meshPath) == ".obj")
				{
					Model newModel = LoadModel(meshPath);

					InspectorPanel.models.Add(c, newModel);

					c.meshLoaded = true;
				}
			}

			if (c.textureLoaded)
			{
				if (Path.GetExtension(texturePath) != ".png" || !File.Exists(texturePath))
				{
					c.textureLoaded = false;

					if (InspectorPanel.models.ContainsKey(c))
					{
						Model model = InspectorPanel.models[c];
						model.materials[0].maps[(int)MATERIAL_MAP_DIFFUSE].texture = new Texture2D();
					}
				}
			}
			else
			{
				if (Path.GetExtension(texturePath) == ".png")
				{
					Texture2D newTexture = LoadTexture(texturePath);

					Model model = InspectorPanel.models[c];
					model.materials[0].maps[(int)MATERIAL_MAP_DIFFUSE].texture = newTexture;

					c.textureLoaded = true;
				}
			}

			ImGui.Separator();
		}
	}
}