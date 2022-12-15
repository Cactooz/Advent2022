namespace Day12 {
	internal class Program {
		static void Main(string[] args) {
			//Get the inputted data
			string file = Path.GetFullPath(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"..\..\..\hightmap.txt"));
			string[] data = File.ReadAllLines(file);

			List<Coordinate> map = new();
			Coordinate start = null;
			Coordinate end = null;

			for(int x = 0; x < data.GetLength(0); x++) {
				for(int y = 0; y < data[x].Length; y++) {
					if(data[x][y] == 'S') {
						start = new(x, y, 'a');
						map.Add(start);
					} else if(data[x][y] == 'E') {
						end = new(x, y, 'z');
						end.Distance = 0;
						map.Add(end);
					} else
						map.Add(new Coordinate(x, y, data[x][y]));
				}
			}

			PriorityQueue<Coordinate, int> queue = new();
			queue.Enqueue(end!, (int)end!.Distance!);

			while(queue.Count != 0) {
				Coordinate coord = queue.Dequeue();

				foreach(var neighbour in Neighbours(map, coord)) {
					if(neighbour == null)
						continue;

					if(Math.Abs(neighbour.Height - coord.Height) < 2) {
                        int distance = (int)(coord.Distance! + 1);
                        if(distance < neighbour.Distance || neighbour.Distance == null) {
                            neighbour.Distance = distance;
                            queue.Enqueue(neighbour, (int)neighbour.Distance);
                        }
                    }
				}
			}

            Console.WriteLine(start!.Distance);
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
			public int? Distance { get; set; }
		}

		static IEnumerable<Coordinate> Neighbours(List<Coordinate> map, Coordinate coord) {
			Coordinate?[] neighbours = new Coordinate[4];

			neighbours[0] = map.SingleOrDefault(pos => pos.X == coord.X - 1 && pos.Y == coord.Y);
			neighbours[1] = map.SingleOrDefault(pos => pos.X == coord.X + 1 && pos.Y == coord.Y);
			neighbours[2] = map.SingleOrDefault(pos => pos.X == coord.X && pos.Y == coord.Y - 1);
			neighbours[3] = map.SingleOrDefault(pos => pos.X == coord.X && pos.Y == coord.Y + 1);

			return neighbours;
		}
	}
}
