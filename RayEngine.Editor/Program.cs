using Raylib_cs;

using static Raylib_cs.Raylib;
using static Raylib_cs.Color;

namespace RayEngine.Editor
{
	class Program
	{
		public static void Main()
		{
			ScreenManager manager = new ScreenManager();

			manager.change(new EditorScreen());

			const int width = 1000;
			const int height = 600;

			SetTraceLogLevel(TraceLogLevel.LOG_NONE);
			SetConfigFlags(ConfigFlags.FLAG_MSAA_4X_HINT);
			InitWindow(width, height, "Ray Engine Editor");
			SetTargetFPS(60);

			while (!WindowShouldClose())
			{
				float dt = GetFrameTime();

				manager.current.Update(dt);

				BeginDrawing();

				ClearBackground(BLACK);

				manager.current.Draw();

				EndDrawing();
			}

			manager.current.Destroy();

			CloseWindow();
		}
	}
}