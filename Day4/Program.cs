namespace Day4 {
	internal class Program {
		static void Main(string[] args) {
			//Get the inputted data
			string file = Path.GetFullPath(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"..\..\..\sections.txt"));
			string[] sections = File.ReadAllLines(file);

			Part1(sections);
		}

		/// <summary>
		/// Check if any of the two sections are inside of the other one.
		/// </summary>
		/// <param name="input">Input of all cleaing sections per pair.</param>
		static void Part1(string[] input) {
			int inside = 0;

			foreach(string line in input) {
				//Split out the two strings of sections
				string[] parts = line.Split(",");
				int[] first = parts[0].Split("-").Select(int.Parse).ToArray();
				int[] second = parts[1].Split("-").Select(int.Parse).ToArray();

				//Check if any are inside of the other
				if(second[0] >= first[0] && second[1] <= first[1] || first[0] >= second[0] && first[1] <= second[1])
					inside++;
			}
			Console.WriteLine(inside);
		}
	}
}
