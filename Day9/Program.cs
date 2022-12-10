namespace Day9 {
	internal class Program {
		static void Main(string[] args) {
			//Get the inputted data
			string file = Path.GetFullPath(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"..\..\..\moves.txt"));
			string[] data = File.ReadAllLines(file);

			TailMoves(data, 2);
			TailMoves(data, 10);
		}

		/// <summary>
		/// Find the amount of locations the tail have visited after all moves are done with variable rope length.
		/// </summary>
		/// <param name="data">The input data of all moves as <see cref="Array"/>.</param>
		/// <param name="knots">The length of the rope as <see cref="int"/>.</param>
		static void TailMoves(string[] data, int knots) {
			Dictionary<string, bool> visited = new();

			//The X,Y coords of each position
			int[,] coords = new int[knots, 2];

			//Loop through all moves from the data input
			foreach(string line in data) {
				string[] move = line.Split(" ");

				for(int i = 0; i < int.Parse(move[1]); i++) {
					//Move the head
					if(move[0] == "U")
						coords[0, 1]++;
					else if(move[0] == "L")
						coords[0, 0]--;
					else if(move[0] == "D")
						coords[0, 1]--;
					else if(move[0] == "R")
						coords[0, 0]++;

					//Loop through the tail positions
					for(int j = 1; j < coords.GetLength(0); j++) {
						//Tail to the right
						if(coords[j, 0] - coords[j - 1, 0] > 1) {
							coords[j, 0]--;
							if(coords[j, 1] - coords[j - 1, 1] > 0)
								coords[j, 1]--;
							else if(coords[j - 1, 1] - coords[j, 1] > 0)
								coords[j, 1]++;

						}
						//Tail to the left
						else if(coords[j - 1, 0] - coords[j, 0] > 1) {
							coords[j, 0]++;
							if(coords[j, 1] - coords[j - 1, 1] > 0)
								coords[j, 1]--;
							else if(coords[j - 1, 1] - coords[j, 1] > 0)
								coords[j, 1]++;
						}
						//Tail above
						else if(coords[j, 1] - coords[j - 1, 1] > 1) {
							coords[j, 1]--;
							if(coords[j, 0] - coords[j - 1, 0] > 0)
								coords[j, 0]--;
							else if(coords[j - 1, 0] - coords[j, 0] > 0)
								coords[j, 0]++;
						}
						//Tail below
						else if(coords[j - 1, 1] - coords[j, 1] > 1) {
							coords[j, 1]++;
							if(coords[j, 0] - coords[j - 1, 0] > 0)
								coords[j, 0]--;
							else if(coords[j - 1, 0] - coords[j, 0] > 0)
								coords[j, 0]++;
						}
					}

					//Add the tails position to the dictionary
					visited.TryAdd($"({coords[coords.GetLength(0) - 1, 0]},{coords[coords.GetLength(0) - 1, 1]})", true);
				}
			}

			Console.WriteLine(visited.Count);
		}
	}
}
