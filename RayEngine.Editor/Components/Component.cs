namespace RayEngine.Editor
{
	enum ComponentType
	{
		Transform,
		MeshRenderer
	}

	class Component
	{
		public virtual string name { get; set; }
		public virtual ComponentType type { get; set; }
	}
}