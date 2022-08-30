namespace RayEngine.Editor
{
	class Scene
	{
		public string name = GetRandomValue(0, 100000).ToString();

		public List<Entity> entities = new List<Entity>();

		public bool saved = false;
	}
}