using System.Text.RegularExpressions;

namespace Day7 {
	internal class Program {
		static void Main(string[] args) {
			//Get the inputted data
			string file = Path.GetFullPath(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"..\..\..\filesystem.txt"));
			string[] data = File.ReadAllLines(file);

			List<string> path = new();
			Dictionary<string,int> dirs = new();

			foreach(string line in data) {
				//Split the command line
				string[] parts = line.Split(" ");

				//Check for dirs
				if(line.StartsWith("$ cd")) {
					if(parts[2] != "..") {
						//Add new dirs
						path.Add($"{path.LastOrDefault()}/{parts[2]}");
					}
					else
						//Remove checked dirs
						path.RemoveAt(path.Count - 1);
				}
				//Add sizes from all lines that start with numbers
				else if (Regex.Match(line, @"\d+").Success) {
					int size = int.Parse(parts[0]);
					//Add the size to all previous parent dirs
					foreach(string dir in path) {
						dirs[dir] = dirs.GetValueOrDefault(dir) + size;
					}
				}
			}

			Part1(dirs);
		}

		/// <summary>
		/// Sum all directories smaller than 100000 sizes together.
		/// </summary>
		/// <param name="dirs">All directory sizes in <see cref="Dictionary{string, int}"/></param>
		static void Part1(Dictionary<string, int> dirs) {
			int sum = 0;
			foreach(KeyValuePair<string, int> dir in dirs) {
				if(dir.Value <= 100000)
					sum += dir.Value;
			}
			Console.WriteLine(sum);
		}
	}
}
