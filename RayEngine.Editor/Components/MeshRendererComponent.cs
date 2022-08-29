namespace RayEngine.Editor
{
	class MeshRendererComponent : Component
	{
		public override string name
		{
			get { return "Mesh Renderer"; }
			set { name = value; }
		}

		public override ComponentType type
		{
			get { return ComponentType.Transform; }
			set { type = value; }
		}
	}
}