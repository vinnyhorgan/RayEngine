namespace RayEngine.Editor
{
	class Entity
	{
		public string name = Utils.RandomString(5);

		public List<Component> components = new List<Component>();
	}
}