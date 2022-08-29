namespace RayEngine.Editor
{
	class ScreenManager
	{
		public Screen current;
		public static bool exit = false;

		public void change(Screen target)
		{
			if (current != null)
			{
				current.Destroy();
			}

			current = target;
			current.Start();
		}
	}
}