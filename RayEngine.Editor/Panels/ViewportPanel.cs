namespace RayEngine.Editor
{
	class ViewportPanel
	{
		RenderTexture2D viewport;
		Camera3D camera;

		const int width = 1280;
		const int height = 720;

		Vector2 mousePos = new Vector2();

		Model testModel;

		private void ViewportDraw()
		{
			BeginMode3D(camera);

			ClearBackground(DARKBLUE);

			DrawGrid(25, 1.0f);

			if (ScenePanel.current != null)
			{
				foreach (Entity e in ScenePanel.current.entities)
				{
					if (e.components.Contains(ComponentTypes.Transform))
					{
						DrawLine3D(e.transform.position, new Vector3(e.transform.position.X + 2, e.transform.position.Y, e.transform.position.Z), RED);
						DrawLine3D(e.transform.position, new Vector3(e.transform.position.X, e.transform.position.Y + 2, e.transform.position.Z), BLUE);
						DrawLine3D(e.transform.position, new Vector3(e.transform.position.X, e.transform.position.Y, e.transform.position.Z + 2), GREEN);
					}

					if (e.components.Contains(ComponentTypes.MeshRenderer))
					{
						if(e.meshRenderer.meshLoaded == true)
						{
							Model model = InspectorPanel.models[e.meshRenderer];

							DrawModel(model, e.transform.position, e.transform.scale, WHITE);
						}
					}
				}
			}

			EndMode3D();

			DrawLine((int)mousePos.X, 0, (int)mousePos.X, 720, RED);
			DrawLine(0, (int)mousePos.Y, 1280, (int)mousePos.Y, RED);

			DrawText("( " + (int)mousePos.X + ", " + (int)mousePos.Y + " )", 10, 10, 25, WHITE);
		}

		public unsafe ViewportPanel()
		{
			viewport = LoadRenderTexture(width, height);

			camera.position = new Vector3(10.0f, 20.0f, 10.0f);
			camera.target = new Vector3(0.0f, 5.0f, 0.0f);
			camera.up = new Vector3(0.0f, 1.0f, 0.0f);
			camera.fovy = 45.0f;
			camera.projection = CameraProjection.CAMERA_PERSPECTIVE;

			SetCameraMode(camera, CameraMode.CAMERA_ORBITAL);

			testModel = LoadModel("Assets/Models/church.obj");
			Texture2D texture = LoadTexture("Assets/Models/church_diffuse.png");
			testModel.materials[0].maps[(int)MATERIAL_MAP_DIFFUSE].texture = texture;
		}

		public void Update(float dt)
		{
			UpdateCamera(ref camera);
		}

		public void Draw()
		{
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