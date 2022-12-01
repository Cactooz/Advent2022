namespace Advent2022 {
	internal class Program {
		static void Main(string[] args) {
			//Get the inputted data
			string file = Path.GetFullPath(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"..\..\..\calories.txt"));
			string[] data = File.ReadAllLines(file);
			int[] calories = new int[300];

			int i = 0;
			//Combine the data into the array
			foreach(string line in data) {
				if(line == null || line.Length == 0) {
					i++;
					continue;
				}
				else
					calories[i] += int.Parse(line);
			}
			//Sort the array
			Array.Sort(calories);

			Part1(calories);
		}

		static void Part1(int[] calories) {
			Console.WriteLine(calories.Last());
		}
	}
}
