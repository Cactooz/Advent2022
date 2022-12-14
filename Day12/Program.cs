namespace Day12 {
	internal class Program {
		static void Main(string[] args) {
			//Get the inputted data
			string file = Path.GetFullPath(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"..\..\..\hightmap.txt"));
			string[] data = File.ReadAllLines(file);

			List<Coordinate> map = new();
			Coordinate start;
			Coordinate end;

			for(int x = 0; x < data.GetLength(0); x++) {
				for(int y = 0; y < data[x].Length; y++) {
					if(data[x][y] == 'S') {
						start = new Coordinate(x, y, 'a');
						map.Add(start);
					} else if(data[x][y] == 'E') {
						end = new Coordinate(x, y, 'z');
						map.Add(end);
					} else
						map.Add(new Coordinate(x, y, data[x][y]));
				}
			}
		}

		/// <summary>
		/// Represent a single coordinate on the map with its height and the distance from the end point.
		/// </summary>
		class Coordinate {
			public Coordinate(int x, int y, char height) {
				X = x;
				Y = y;
				Height = height - 'a';
			}

			public int X { get; }
			public int Y { get; }
			public int Height { get; }
			public int Distance { get; set; }
		}
	}
}
