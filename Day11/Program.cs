namespace Day11 {
	internal class Program {
		static void Main(string[] args) {
			//Get the inputted data
			string file = Path.GetFullPath(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"..\..\..\monkeys.txt"));
			string data = File.ReadAllText(file);

			List<Monkey> monkeys = new();

			//Read all the monkey data per monkey
			foreach(string line in data.Split("\r\n\r\n")) {
				//Set up base varaibles to fill
				List<int> items = new();
				Func<long, long> operation = item => item;
				int test = 0;
				int trueThrow = 0;
				int falseThrow = 0;

				//Split information into rows
				string[] monkeyData = line.Split("\n");

				foreach(string info in monkeyData) {
					if(info.Contains("Starting items")) {
						string[] presents = info.Remove(0, 18).Split(", ");
						foreach(string present in presents)
							//Add the present to the start
							items.Insert(0, int.Parse(present));
					} else if(info.Contains("Operation")) {
						string numberString = info.Split(" ").Last();
						//Differentiate between +, * and numbers and itself
						if(info.Contains('+')) {
							if(long.TryParse(numberString, out long number))
								operation = item => item + number;
							else
								operation = item => item + item;
						} else {
							if(long.TryParse(numberString, out long number))
								operation = item => item * number;
							else
								operation = item => item * item;
						}
					} else if (info.Contains("Test"))
						test = int.Parse(info.Split(" ").Last());
					else if(info.Contains("If true"))
						trueThrow = int.Parse(info.Split(" ").Last());
					else if(info.Contains("If false"))
						falseThrow = int.Parse(info.Split(" ").Last());
				}
				//Add the monkey
				monkeys.Add(new(items, operation, test, trueThrow, falseThrow));
			}

		}

		/// <summary>
		/// A single monkey with all its different information about moves, worry levels etc.
		/// </summary>
		class Monkey {
			private List<int> items;
			private readonly Func<long, long> operation;
			private readonly int test;
			private readonly int trueThrow;
			private readonly int falseThrow;

			/// <summary>
			/// Create a new <see cref="Monkey"/>.
			/// </summary>
			/// <param name="startItems">A <see cref="int"/> <see cref="List{T}"/> of all the starting items the monkey has.</param>
			/// <param name="operation">The operation that is done to the worry levels at inspection.</param>
			/// <param name="test">The mod value that the worry levels are taken modulo when the inspection is done.</param>
			/// <param name="trueThrow">The other <see cref="Monkey"/> that the item is thrown to if the test is <see langword="true"/>.</param>
			/// <param name="falseThrow">The other <see cref="Monkey"/> that the item is thrown to if the test is <see langword="false"/>.</param>
			public Monkey(List<int> startItems, Func<long, long> operation, int test, int trueThrow, int falseThrow) {
				items = startItems;
				this.operation = operation;
				this.test = test;
				this.trueThrow = trueThrow;
				this.falseThrow = falseThrow;
			}

			public List<int> Operation { get; set; }
		}
	}
}
