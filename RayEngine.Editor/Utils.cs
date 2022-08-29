namespace RayEngine.Editor
{
	class Utils
	{
		public static void Write(string path, string text)
		{
			using (StreamWriter sw = File.CreateText(path))
			{
				sw.Write(text);
			}
		}

		public static string Read(string path)
		{
			string text;

			using (StreamReader sr = File.OpenText(path))
			{
				text = sr.ReadLine();
			}

			return text;
		}

		public static string RandomString(int length)
		{
			return string.Join("", Enumerable.Repeat(0, length).Select(n => (char)new Random().Next(127)));
		}
	}
}