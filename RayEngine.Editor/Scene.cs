namespace RayEngine.Editor
{
	class Scene
	{
		public string name = Utils.RandomString(5);

		public List<Entity> entities = new List<Entity>();

		public bool saved = false;
	}
}