namespace Day2 {
	internal class Program {
		static void Main(string[] args) {
            //Get the inputted data
            string file = Path.GetFullPath(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"..\..\..\strategy.txt"));
            string[] data = File.ReadAllLines(file);

            Part1(data);
        }

        /// <summary>
        /// Gets the points for rock paper scissors with XYZ being moves.
        /// </summary>
        /// <param name="input">Array with all played rounds.</param>
        static void Part1(string[] input) {
            int total = 0;
            foreach(string round in input) {
                string[] moves = round.Split(" ");

                //If you choose rock
                if(moves[1] == "X") {
                    total += 1;
                    if(moves[0] == "A")
                        total += 3;
                    else if(moves[0] == "C")
                        total += 6;
                }
                //If you choose paper
                else if(moves[1] == "Y") {
                    total += 2;
                    if(moves[0] == "B")
                        total += 3;
                    else if(moves[0] == "A")
                        total += 6;
                }
                //If you choose scissors 
                else if(moves[1] == "Z") {
                    total += 3;
                    if(moves[0] == "C")
                        total += 3;
                    else if(moves[0] == "B")
                        total += 6;
                }
            }

            Console.WriteLine(total);
        }
	}
}
