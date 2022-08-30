namespace RayEngine.Editor
{
	class TransformComponent
	{
		public Vector3 position = new Vector3();
		public Vector3 rotation = new Vector3();
		public float scale = 1;

		public static void Draw()
		{
			TransformComponent c = InspectorPanel.current.transform;

			ImGui.Text("Transform");

			ImGui.InputFloat3("Position", ref c.position);
			ImGui.InputFloat3("Rotation", ref c.rotation);
			ImGui.InputFloat("Scale", ref c.scale);

			ImGui.Separator();
		}
	}
}