namespace Day5 {
	internal class Program {
		static void Main(string[] args) {
			//Fill data
			List<List<char>> crates = new() {
				new(),
				new(),
				new(),
				new(),
				new(),
				new(),
				new(),
				new(),
				new()
			};
			crates[0].Add('H');
			crates[0].Add('R');
			crates[0].Add('B');
			crates[0].Add('D');
			crates[0].Add('Z');
			crates[0].Add('F');
			crates[0].Add('L');
			crates[0].Add('S');
			crates[1].Add('T');
			crates[1].Add('B');
			crates[1].Add('M');
			crates[1].Add('Z');
			crates[1].Add('R');
			crates[2].Add('Z');
			crates[2].Add('L');
			crates[2].Add('C');
			crates[2].Add('H');
			crates[2].Add('N');
			crates[2].Add('S');
			crates[3].Add('S');
			crates[3].Add('C');
			crates[3].Add('F');
			crates[3].Add('J');
			crates[4].Add('P');
			crates[4].Add('G');
			crates[4].Add('H');
			crates[4].Add('W');
			crates[4].Add('R');
			crates[4].Add('Z');
			crates[4].Add('B');
			crates[5].Add('V');
			crates[5].Add('J');
			crates[5].Add('Z');
			crates[5].Add('G');
			crates[5].Add('D');
			crates[5].Add('N');
			crates[5].Add('M');
			crates[5].Add('T');
			crates[6].Add('G');
			crates[6].Add('L');
			crates[6].Add('N');
			crates[6].Add('W');
			crates[6].Add('F');
			crates[6].Add('S');
			crates[6].Add('P');
			crates[6].Add('Q');
			crates[7].Add('M');
			crates[7].Add('Z');
			crates[7].Add('R');
			crates[8].Add('M');
			crates[8].Add('C');
			crates[8].Add('L');
			crates[8].Add('G');
			crates[8].Add('V');
			crates[8].Add('R');
			crates[8].Add('T');

			//Get the inputted data
			string file = Path.GetFullPath(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"..\..\..\moves.txt"));
			string[] data = File.ReadAllLines(file);

			Part1(crates, data);
		}

		/// <summary>
		/// Move crate per crate from one stack to another.
		/// </summary>
		/// <param name="crates">All the crates in their stacks.</param>
		/// <param name="moves">How the crates should be moved between the stacks.</param>
		static void Part1(List<List<char>> crates, string[] moves) {
			//Go through all input lines
			foreach(string line in moves) {
				string[] parts = line.Split(" ");

				int amount = int.Parse(parts[1]);
				int from = int.Parse(parts[3]) - 1;
				int to = int.Parse(parts[5]) - 1;

				for(int i = 0; i < amount; i++) {
					//Add at another crate
					crates[to].Add(crates[from].ElementAt(crates[from].Count - 1));
					//Remove the element
					crates[from].RemoveAt(crates[from].Count - 1);
				}
			}

			//Print the top of each crate
			foreach(List<char> list in crates)
				Console.Write(list.ElementAt(list.Count - 1));
		}
	}
}
