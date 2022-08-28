using Raylib_cs;
using ImGuiNET;
using System;
using System.Numerics;
using System.Collections.Generic;

using static Raylib_cs.Raylib;
using static Raylib_cs.Color;
using static Raylib_cs.MaterialMapIndex;
using static Raylib_cs.KeyboardKey;

namespace RayEngine.Editor
{
	class EditorScreen : Screen
	{
		RenderTexture2D viewport;
		Camera3D camera;

		Model testModel;

		const int viewWidth = 1280;
		const int viewHeight = 720;

		Vector2 viewMousePos;

		List<string> consoleBuffer;

		private void ViewportDraw()
		{
			BeginMode3D(camera);

			ClearBackground(DARKBLUE);

			DrawGrid(10, 1.0f);

			DrawModel(testModel, Vector3.Zero, 0.5f, WHITE);

			EndMode3D();

			DrawLine((int)viewMousePos.X, 0, (int)viewMousePos.X, 720, RED);
			DrawLine(0, (int)viewMousePos.Y, 1280, (int)viewMousePos.Y, RED);

			DrawText("( " + viewMousePos.X + ", " + viewMousePos.Y + " )", 10, 10, 25, WHITE);
		}

		private void LogConsole(string text)
		{
			string date = DateTime.Now.ToString("[HH:mm:ss] ");

			consoleBuffer.Add(date + text);
		}

		public unsafe override void Start()
		{
			viewport = LoadRenderTexture(viewWidth, viewHeight);

			camera.position = new Vector3(10.0f, 20.0f, 10.0f);
			camera.target = new Vector3(0.0f, 5.0f, 0.0f);
			camera.up = new Vector3(0.0f, 1.0f, 0.0f);
			camera.fovy = 45.0f;
			camera.projection = CameraProjection.CAMERA_PERSPECTIVE;

			SetCameraMode(camera, CameraMode.CAMERA_ORBITAL);

			viewMousePos = new Vector2();

			testModel = LoadModel("Assets/models/church.obj");
			Texture2D texture = LoadTexture("Assets/models/church_diffuse.png");
			testModel.materials[0].maps[(int)MATERIAL_MAP_DIFFUSE].texture = texture;

			consoleBuffer = new List<string>();

			MaximizeWindow();
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

				Vector2 pos = ImGui.GetWindowPos();

				float scale = Math.Min((float)size.X / viewWidth, (float)size.Y / viewHeight);

				Vector2 cursor = new Vector2((size.X - ((float)viewWidth * scale)) * 0.5f, (size.Y - ((float)viewHeight * scale)) * 0.5f + 30);

				ImGui.SetCursorPos(cursor);
				ImGui.Image(new IntPtr(viewport.texture.id), new Vector2(viewWidth * scale, viewHeight * scale), new Vector2(0.0f, 2.0f));

				// VIRTUAL MOUSE

				Vector2 realMousePos = GetMousePosition();

				viewMousePos.X = (realMousePos.X - pos.X - (size.X - (viewWidth * scale)) * 0.5f) / scale;
				viewMousePos.Y = (realMousePos.Y - pos.Y - 30 - (size.Y - (viewHeight * scale)) * 0.5f) / scale;;
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
				foreach (string line in consoleBuffer)
				{
					ImGui.Spacing();
					ImGui.Text(line);
					ImGui.Spacing();
					ImGui.Separator();

					ImGui.SetScrollY(ImGui.GetScrollMaxY());
				}
			}
			ImGui.End();
		}

		public override void Destroy()
		{

		}
	}
}