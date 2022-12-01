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
			Part2(calories);
		}

		/// <summary>
		/// Gets the elf with the most calories.
		/// </summary>
		/// <param name="calories">Array with all the elf calories data.</param>
		static void Part1(int[] calories) {
			Console.WriteLine(calories.Last());
		}

		/// <summary>
		/// Gets the 3 elfs with the most calories.
		/// </summary>
		/// <param name="calories">Array with all the elf calories data.</param>
		static void Part2(int[] calories) {
			int last = calories.Length - 1;
			int total = calories[last];
			total += calories[last - 1];
			total += calories[last - 2];
			Console.WriteLine(total);
		}
	}
}
