using Raylib_cs;
using ImGuiNET;
using System;
using System.Numerics;

using static Raylib_cs.Raylib;
using static Raylib_cs.Color;
using static Raylib_cs.MaterialMapIndex;

namespace RayEngine.Editor
{
	class EditorScreen : Screen
	{
		RenderTexture2D viewport;
		Camera3D camera;

		Model testModel;

		private void ViewportDraw()
		{
			BeginMode3D(camera);

			ClearBackground(DARKBLUE);

			DrawGrid(10, 1.0f);

			DrawModel(testModel, Vector3.Zero, 0.5f, WHITE);

			EndMode3D();
		}

		public unsafe override void Start()
		{
			viewport = LoadRenderTexture(640, 480);

			camera.position = new Vector3(10.0f, 20.0f, 10.0f);
			camera.target = new Vector3(0.0f, 5.0f, 0.0f);
			camera.up = new Vector3(0.0f, -1.0f, 0.0f);
			camera.fovy = 45.0f;
			camera.projection = CameraProjection.CAMERA_PERSPECTIVE;

			SetCameraMode(camera, CameraMode.CAMERA_ORBITAL);

			testModel = LoadModel("Assets/models/church.obj");
			Texture2D texture = LoadTexture("Assets/models/church_diffuse.png");
			testModel.materials[0].maps[(int)MATERIAL_MAP_DIFFUSE].texture = texture;
		}

		public override void Update(float dt)
		{
			UpdateCamera(ref camera);
		}

		public override void Draw()
		{
			BeginTextureMode(viewport);

			ViewportDraw();

			EndTextureMode();

			if (ImGui.BeginMainMenuBar())
			{
				if (ImGui.BeginMenu("System"))
				{


					ImGui.EndMenu();
				}

				if (ImGui.BeginMenu("Scene"))
				{


					ImGui.EndMenu();
				}

				if (ImGui.BeginMenu("Entity"))
				{


					ImGui.EndMenu();
				}

				if (ImGui.BeginMenu("Help"))
				{


					ImGui.EndMenu();
				}

				ImGui.EndMainMenuBar();
			}

			if (ImGui.Begin("Viewport"))
			{
				Vector2 size = ImGui.GetWindowSize();
				size.Y -= 40;

				ImGui.Image(new IntPtr(viewport.texture.id), size);
			}
			ImGui.End();

			if (ImGui.Begin("Scene"))
			{

			}
			ImGui.End();

			if (ImGui.Begin("Inspector"))
			{

			}
			ImGui.End();

			if (ImGui.Begin("Filesystem"))
			{

			}
			ImGui.End();

			if (ImGui.Begin("Console"))
			{

			}
			ImGui.End();
		}

		public override void Destroy()
		{

		}
	}
}