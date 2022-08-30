namespace RayEngine.Editor
{
	class Program
	{
		public static void Main()
		{
			const int width = 1000;
			const int height = 600;

			SetTraceLogLevel(TraceLogLevel.LOG_NONE);
			SetConfigFlags(ConfigFlags.FLAG_MSAA_4X_HINT | ConfigFlags.FLAG_WINDOW_RESIZABLE);
			InitWindow(width, height, "Ray Engine Editor");
			//SetTargetFPS(60);
			SetExitKey(KEY_NULL);

			ScreenManager manager = new ScreenManager();
			manager.change(new EditorScreen());

			ImguiController controller = new ImguiController();

			ImGuiIOPtr io = ImGui.GetIO();
			io.ConfigFlags = ImGuiConfigFlags.DockingEnable;

			controller.Load(width, height);

			while (!ScreenManager.exit)
			{
				if (WindowShouldClose())
				{
					ScreenManager.exit = true;
				}

				float dt = GetFrameTime();

				manager.current.Update(dt);

				controller.Update(dt);

				BeginDrawing();

					ClearBackground(BLACK);

					ImGui.DockSpaceOverViewport();

					manager.current.Draw();

					controller.Draw();

				EndDrawing();
			}

			manager.current.Destroy();

			controller.Dispose();

			CloseWindow();
		}
	}
}