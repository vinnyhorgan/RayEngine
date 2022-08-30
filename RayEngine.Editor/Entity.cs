namespace RayEngine.Editor
{
	class Entity
	{
		public string name = GetRandomValue(0, 100000).ToString();

		public List<ComponentTypes> components = new List<ComponentTypes>();

		public TransformComponent transform;
		public MeshRendererComponent meshRenderer;
	}
}