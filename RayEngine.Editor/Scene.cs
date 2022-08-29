using System.Collections.Generic;

namespace RayEngine.Editor
{
	class Scene
	{
		public string name;

		public List<Entity> entities;

		public Scene()
		{
			name = "New Scene";

			entities = new List<Entity>();
		}
	}
}