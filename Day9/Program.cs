namespace Day9 {
	internal class Program {
		static void Main(string[] args) {
            //Get the inputted data
            string file = Path.GetFullPath(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"..\..\..\moves.txt"));
            string[] data = File.ReadAllLines(file);

            Part1(data);
            Part2(data);
		}

        /// <summary>
        /// Find the amount of locations the tail have visited after all moves are done.
        /// </summary>
        /// <param name="data">The input data of all moves as <see cref="Array"/>.</param>
        static void Part1(string[] data) {
            Dictionary<string, bool> visited = new();

            int headX = 0;
            int headY = 0;
            int tailX = 0;
            int tailY = 0;
            foreach(string line in data) {
                string[] move = line.Split(" ");

                for(int i = 0; i < int.Parse(move[1]); i++) {
                    //Move the head
                    if(move[0] == "U")
                        headY++;
                    else if(move[0] == "L")
                        headX--;
                    else if(move[0] == "D")
                        headY--;
                    else if(move[0] == "R")
                        headX++;

                    //Tail to the right
                    if(tailX - headX > 1) {
                        tailX--;
                        if(tailY - headY > 0)
                            tailY--;
                        else if(headY - tailY > 0)
                            tailY++;

                    }
                    //Tail to the left
                    else if(headX - tailX > 1) {
                        tailX++;
                        if(tailY - headY > 0)
                            tailY--;
                        else if(headY - tailY > 0)
                            tailY++;
                    }
                    //Tail above
                    else if(tailY - headY > 1) {
                        tailY--;
                        if(tailX - headX > 0)
                            tailX--;
                        else if(headX - tailX > 0)
                            tailX++;
                    }
                    //Tail below
                    else if(headY - tailY > 1) {
                        tailY++;
                        if(tailX - headX > 0)
                            tailX--;
                        else if(headX - tailX > 0)
                            tailX++;
                    }

                    //Add the tails position to the dictionary
                    visited.TryAdd($"({tailX},{tailY})", true);
                }
            }

            Console.WriteLine(visited.Count);
        }

        /// <summary>
        /// Find the amount of locations the tail have visited after all moves are done with longer tail.
        /// </summary>
        /// <param name="data">The input data of all moves as <see cref="Array"/>.</param>
        static void Part2(string[] data) {
            Dictionary<string, bool> visited = new();

            int[,] coords = new int[10, 2];

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
                    visited.TryAdd($"({coords[9, 0]},{coords[9, 1]})", true);
                }
            }

            Console.WriteLine(visited.Count);
        }
    }
}
