namespace RayEngine.Editor
{
	class TransformComponent : Component
	{
		public override string name
		{
			get { return "Transform"; }
			set { name = value; }
		}

		public override ComponentType type
		{
			get { return ComponentType.Transform; }
			set { type = value; }
		}

		public Vector3 transform = new Vector3();

		public static void Draw(TransformComponent c)
		{

		}
	}
}