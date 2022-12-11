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

				//Print the CRT
				CRT(cycle, value);

				if(data[operation].StartsWith("addx")) {
					//Cycle forward because addx takes two cycles
					if(dataPoints.Contains(++cycle))
						output += value * cycle;

					//Print the CRT
					CRT(cycle, value);

					//Add the new value
					value += int.Parse(Regex.Match(data[operation], @"[-]?\d+").Value);
				}

				cycle++;
			}

			Console.WriteLine(output);
		}

		/// <summary>
		/// Prints pixels on a 40 pixels wide CRT TV.
		/// '#' if sprite is visible, '.' if sprite is not visible.
		/// </summary>
		/// <param name="cycle">The current cycle the program is at.</param>
		/// <param name="position">The current position of the sprite.</param>
		static void CRT(int cycle, int position) {
			char print;
			//Check if at least 1 pixel is visible at the current cycle
			if(Math.Abs((cycle - 1) % 40 - position) < 2)
				print = '#';
			else
				print = '.';

			//Print out the pixels
			Console.Write(print);
			if(cycle % 40 == 0)
				Console.WriteLine();
		}
	}
}
