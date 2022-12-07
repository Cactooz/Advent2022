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
			Part2(dirs, 70000000, 30000000);
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

		/// <summary>
		/// Find the needed size to delete and remove the smallest directory that fufills that size.
		/// </summary>
		/// <param name="dirs">All directory sizes in <see cref="Dictionary{string, int}"/></param>
		static void Part2(Dictionary<string, int> dirs, int totalSpace, int neededSpace) {
			//Root dir has total size, remove that from the total space
			int remaining = totalSpace - dirs.First().Value;
			int removalSize = neededSpace - remaining;

			int minDir = int.MaxValue;
			foreach(KeyValuePair<string, int> dir in dirs) {
				if(dir.Value > removalSize && dir.Value < minDir)
					minDir = dir.Value;
			}
			Console.WriteLine(minDir);
		}
	}
}
