using System.Text.RegularExpressions;

namespace Day10 {
	internal class Program {
		static void Main(string[] args) {
			//Get the inputted data
			string file = Path.GetFullPath(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"..\..\..\instructions.txt"));
			string[] data = File.ReadAllLines(file);
			
			//Points to check the values from
			int[] dataPoints = { 20, 60, 100, 140, 180, 220 };
			int value = 1;
			int cycle = 1;
			int output = 0;

			//Loop through all operations
			for(int operation = 0; operation < data.Length; operation++) {
				if(dataPoints.Contains(cycle))
					output += value * cycle;

				if(data[operation].StartsWith("addx")) {
					//Cycle forward because addx takes two cycles
					if(dataPoints.Contains(++cycle))
						output += value * cycle;

					//Add the new value
					value += int.Parse(Regex.Match(data[operation], @"[-]?\d+").Value);
				}

				cycle++;
			}

			Console.WriteLine(output);
		}
	}
}
