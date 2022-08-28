namespace RayEngine.Editor
{
	class ScreenManager
	{
		public Screen current;

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