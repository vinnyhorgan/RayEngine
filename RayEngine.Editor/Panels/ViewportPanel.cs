namespace RayEngine.Editor
{
	class ViewportPanel
	{
		RenderTexture2D viewport;
		Camera3D camera;

		const int width = 1280;
		const int height = 720;

		Vector2 mousePos = new Vector2();

		Model skybox;

		private unsafe void ViewportDraw()
		{
			BeginMode3D(camera);

			ClearBackground(DARKBLUE);

			Rlgl.rlDisableBackfaceCulling();
			Rlgl.rlDisableDepthMask();
			DrawModel(skybox, new Vector3(0, 0, 0), 1.0f, WHITE);
			Rlgl.rlEnableBackfaceCulling();
			Rlgl.rlEnableDepthMask();

			DrawGrid(1000, 1.0f);

			DrawLine3D(new Vector3(-1000, 0, 0), new Vector3(1000, 0, 0), RED);
			DrawLine3D(new Vector3(0, -1000, 0), new Vector3(0, 1000, 0), GREEN);
			DrawLine3D(new Vector3(0, 0, -1000), new Vector3(0, 0, 1000), BLUE);

			if (ScenePanel.current != null)
			{
				foreach (Entity e in ScenePanel.current.entities)
				{
					if (e.components.Contains(ComponentTypes.Transform))
					{
						DrawLine3D(e.transform.position, new Vector3(e.transform.position.X + 2, e.transform.position.Y, e.transform.position.Z), RED);
						DrawLine3D(e.transform.position, new Vector3(e.transform.position.X, e.transform.position.Y + 2, e.transform.position.Z), GREEN);
						DrawLine3D(e.transform.position, new Vector3(e.transform.position.X, e.transform.position.Y, e.transform.position.Z + 2), BLUE);
					}

					if (e.components.Contains(ComponentTypes.MeshRenderer))
					{
						if (e.meshRenderer.meshLoaded == true)
						{
							Model model = InspectorPanel.models[e.meshRenderer];

							if (e.meshRenderer.textureLoaded == true)
							{
								Texture2D texture = InspectorPanel.textures[e.meshRenderer];
								model.materials[0].maps[(int)MATERIAL_MAP_DIFFUSE].texture = texture;
							}
							else
							{
								model.materials[0] = LoadMaterialDefault();
							}

							if (e.meshRenderer.wireframe)
							{
								DrawModelWiresEx(model, e.transform.position, e.transform.rotation, e.transform.angle, e.transform.scale, WHITE);
							}
							else
							{
								DrawModelEx(model, e.transform.position, e.transform.rotation, e.transform.angle, e.transform.scale, WHITE);
							}
						}
					}
				}
			}

			EndMode3D();

			DrawText("( " + (int)mousePos.X + ", " + (int)mousePos.Y + " )", 10, 10, 25, WHITE);
		}

		public unsafe ViewportPanel()
		{
			viewport = LoadRenderTexture(width, height);

			camera.position = new Vector3(10.0f, 10.0f, 10.0f);
			camera.target = new Vector3(0.0f, 0.0f, 0.0f);
			camera.up = new Vector3(0.0f, 1.0f, 0.0f);
			camera.fovy = 45.0f;
			camera.projection = CameraProjection.CAMERA_PERSPECTIVE;

			SetCameraMode(camera, CameraMode.CAMERA_FIRST_PERSON);

			// SKYBOX

			Mesh cube = GenMeshCube(1.0f, 1.0f, 1.0f);
			skybox = LoadModelFromMesh(cube);

			Shader shader = LoadShader(FilesystemPanel.projDir + "/Assets/Shaders/skybox.vs", FilesystemPanel.projDir + "/Assets/Shaders/skybox.fs");
			SetMaterialShader(ref skybox, 0, ref shader);
			SetShaderValue(shader, GetShaderLocation(shader, "environmentMap"), (int)MATERIAL_MAP_CUBEMAP, SHADER_UNIFORM_INT);

			Shader shdrCubemap = LoadShader(FilesystemPanel.projDir + "/Assets/Shaders/cubemap.vs", FilesystemPanel.projDir + "/Assets/Shaders/cubemap.fs");
			SetShaderValue(shdrCubemap, GetShaderLocation(shdrCubemap, "equirectangularMap"), 0, SHADER_UNIFORM_INT);

			string panoFileName = FilesystemPanel.projDir + "/Assets/skybox3.png";
			Image panorama = LoadImage(panoFileName);

			Texture2D cubemap = LoadTextureCubemap(panorama, CubemapLayout.CUBEMAP_LAYOUT_AUTO_DETECT);

			SetMaterialTexture(ref skybox, 0, MATERIAL_MAP_CUBEMAP, ref cubemap);

			UnloadImage(panorama);
		}

		public void Update(float dt)
		{

		}

		public void Draw()
		{
			if (ImGui.IsWindowFocused())
			{
				UpdateCamera(ref camera);
			}

			BeginTextureMode(viewport);

			ViewportDraw();

			EndTextureMode();

			Vector2 size = ImGui.GetWindowSize();
			size.Y -= 40;

			Vector2 pos = ImGui.GetWindowPos();

			float scale = Math.Min((float)size.X / width, (float)size.Y / height);

			Vector2 cursor = new Vector2((size.X - ((float)width * scale)) * 0.5f, (size.Y - ((float)height * scale)) * 0.5f + 30);

			ImGui.SetCursorPos(cursor);
			ImGui.Image(new IntPtr(viewport.texture.id), new Vector2(width * scale, height * scale), new Vector2(0.0f, 2.0f));

			Vector2 realMousePos = GetMousePosition();

			mousePos.X = (realMousePos.X - pos.X - (size.X - (width * scale)) * 0.5f) / scale;
			mousePos.Y = (realMousePos.Y - pos.Y - 30 - (size.Y - (height * scale)) * 0.5f) / scale;;
		}
	}
}