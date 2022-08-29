namespace RayEngine.Editor
{
	class Entity
	{
		public string name;

		public List<Component> components;

		public Entity()
		{
			name = "New Entity";

			components = new List<Component>();
		}
	}
}