using Raylib_cs;
using ImGuiNET;
using System;
using System.Numerics;
using System.Collections.Generic;
using System.IO;

using static Raylib_cs.Raylib;
using static Raylib_cs.Color;
using static Raylib_cs.MaterialMapIndex;
using static Raylib_cs.KeyboardKey;
using static Raylib_cs.MouseButton;

namespace RayEngine.Editor
{
	class EditorScreen : Screen
	{
		RenderTexture2D viewport;
		Camera3D camera;

		Model testModel;
		bool isSelected = false;
		BoundingBox bounds;

		const int viewWidth = 1280;
		const int viewHeight = 720;

		Vector2 viewMousePos = new Vector2();

		List<string> consoleBuffer = new List<string>();

		Scene currentScene;
		Entity currentEntity;

		bool showAbout = false;

		List<string> path = new List<string>();
		string baseDir;

		private void ViewportDraw()
		{
			BeginMode3D(camera);

			ClearBackground(DARKBLUE);

			DrawGrid(25, 1.0f);

			DrawModel(testModel, Vector3.Zero, 1.0f, WHITE);

			if (isSelected) DrawBoundingBox(bounds, GREEN);

			EndMode3D();

			DrawLine((int)viewMousePos.X, 0, (int)viewMousePos.X, 720, RED);
			DrawLine(0, (int)viewMousePos.Y, 1280, (int)viewMousePos.Y, RED);

			DrawText("( " + (int)viewMousePos.X + ", " + (int)viewMousePos.Y + " )", 10, 10, 25, WHITE);
		}

		private void LogConsole(string text)
		{
			string date = DateTime.Now.ToString("[HH:mm:ss] ");

			consoleBuffer.Add(date + text);
		}

		private void NewScene()
		{
			currentScene = new Scene();

			LogConsole("Created new scene");
		}

		private void NewEntity()
		{
			if (currentScene != null)
			{
				currentScene.entities.Add(new Entity());

				LogConsole("Created new entity");
			}
			else
			{
				LogConsole("No scene selected!");
			}
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

			testModel = LoadModel("Assets/models/church.obj");
			Texture2D texture = LoadTexture("Assets/models/church_diffuse.png");
			testModel.materials[0].maps[(int)MATERIAL_MAP_DIFFUSE].texture = texture;
			bounds = GetMeshBoundingBox(testModel.meshes[0]);

			baseDir = Directory.GetCurrentDirectory() + "/TestProject";

			MaximizeWindow();
		}

		public override void Update(float dt)
		{
			UpdateCamera(ref camera);

			if (IsMouseButtonPressed(MOUSE_BUTTON_LEFT))
			{
				if (GetRayCollisionBox(GetMouseRay(viewMousePos, camera), bounds).hit) isSelected = !isSelected;
				else isSelected = false;
			}
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
					if (ImGui.MenuItem("Quit"))
					{
						ScreenManager.exit = true;
					}

					ImGui.EndMenu();
				}

				if (ImGui.BeginMenu("Scene"))
				{
					if (ImGui.MenuItem("New"))
					{
						NewScene();
					}

					ImGui.EndMenu();
				}

				if (ImGui.BeginMenu("Entity"))
				{
					if (ImGui.MenuItem("New"))
					{
						NewEntity();
					}

					if (ImGui.MenuItem("Add component"))
					{

					}

					ImGui.EndMenu();
				}

				if (ImGui.BeginMenu("Help"))
				{
					if (ImGui.MenuItem("About"))
					{
						showAbout = true;
					}

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
				if (currentScene != null)
				{
					ImGui.Text(currentScene.name);

					ImGui.Separator();

					foreach (Entity e in currentScene.entities)
					{
						if (ImGui.Button(e.name))
						{
							currentEntity = e;
						}
					}
				}
			}
			ImGui.End();

			if (ImGui.Begin("Inspector"))
			{
				if (currentEntity != null)
				{
					ImGui.Text(currentEntity.name);

					ImGui.Separator();

					foreach (Component c in currentEntity.components)
					{
						ImGui.Text(c.name);
					}
				}
			}
			ImGui.End();

			if (ImGui.Begin("Filesystem"))
			{
				string currentPath = baseDir;

				string baseName = new DirectoryInfo(baseDir).Name;

				string displayPath = baseName;

				foreach (string dir in path)
				{
					currentPath += "/" + dir;
					displayPath += "/" + dir;
				}

				ImGui.Text(displayPath);

				ImGui.Separator();

				if (path.Count > 0)
				{
					if (ImGui.Button(".."))
					{
						path.RemoveAt(path.Count - 1);
					}
				}

				string[] directories =  Directory.GetDirectories(currentPath);

				foreach (string dir in directories)
				{
					string name = new DirectoryInfo(dir).Name;

					if (ImGui.Button(name))
					{
						path.Add(name);
					}
				}

				string[] files =  Directory.GetFiles(currentPath);

				foreach (string f in files)
				{
					string name = new FileInfo(f).Name;

					ImGui.Text(name);
				}
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

			if (showAbout == true)
			{
				if (ImGui.Begin("About", ref showAbout))
				{
					ImGui.Text("Ray Engine");
					ImGui.Separator();
					ImGui.Text("By Vinny Horgan");
				}
				ImGui.End();
			}
		}

		public override void Destroy()
		{

		}
	}
}