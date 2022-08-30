namespace RayEngine.Editor
{
	class MeshRendererComponent
	{
		public string mesh = "";
		public string texture = "";
		public bool meshLoaded = false;
		public bool textureLoaded = false;
		public bool wireframe = false;

		public static void LoadMesh(MeshRendererComponent c, string meshPath)
		{
			if (Path.GetExtension(meshPath) == ".obj")
			{
				Model newModel = LoadModel(meshPath);

				InspectorPanel.models.Add(c, newModel);

				c.meshLoaded = true;
			}
		}

		public static void LoadMeshTexture(MeshRendererComponent c, string texturePath)
		{
			if (Path.GetExtension(texturePath) == ".png")
			{
				Texture2D newTexture = LoadTexture(texturePath);

				InspectorPanel.textures.Add(c, newTexture);

				c.textureLoaded = true;
			}
		}

		public static void Draw()
		{
			MeshRendererComponent c = InspectorPanel.current.meshRenderer;

			ImGui.Text("Mesh Renderer");

			ImGui.InputText("Mesh", ref c.mesh, 100);
			ImGui.InputText("Texture", ref c.texture, 100);
			ImGui.Checkbox("Wireframe", ref c.wireframe);

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
				LoadMesh(c, meshPath);
			}

			if (c.textureLoaded)
			{
				if (Path.GetExtension(texturePath) != ".png" || !File.Exists(texturePath))
				{
					c.textureLoaded = false;

					if (InspectorPanel.textures.ContainsKey(c))
					{
						InspectorPanel.textures.Remove(c);
					}
				}
			}
			else
			{
				LoadMeshTexture(c, texturePath);
			}

			ImGui.Separator();
		}
	}
}