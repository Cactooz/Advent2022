﻿namespace Day11 {
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
							items.Add(int.Parse(present));
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

            //Do 20 rounds of throwing
            for(int i = 0; i < 20; i++) {
                foreach(Monkey monkey in monkeys) {
                    while(monkey.Items.Count > 0) {
                        long[] throwTo = monkey.InspectFirst();

                        monkeys[(int)throwTo[0]].Items.Add((int)throwTo[1]);
						monkey.ThrowCount++;
                    }
                }
            }

			int[] throws = new int[2];
			//Pick out the two monkeys with the most throws
			foreach(Monkey monkey in monkeys) {
				if(monkey.ThrowCount > throws.Min())
					throws[Array.IndexOf(throws, throws.Min())] = monkey.ThrowCount;
			}

			Console.WriteLine(throws[0] * throws[1]);
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
			private int throwCount;

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
				throwCount = 0;
			}

			/// <summary>
			/// All the items the <see cref="Monkey"/> holds with their worry level in a <see cref="int"/> <see cref="List{T}"/>.
			/// </summary>
			public List<int> Items { get => items; set => items = value; }

			/// <summary>
			/// The amount of times the <see cref="Monkey"/> has thrown an item to another <see cref="Monkey"/>.
			/// </summary>
			public int ThrowCount { get => throwCount; set => throwCount = value; }

			/// <summary>
			/// Inspect the first element in the <see cref="Monkey"/> <see cref="items"/> <see cref="int"/> <see cref="List{T}"/>.
			/// Removing it from the list and updating its worry level and calculating which monkey it should be thrown to.
			/// </summary>
			/// <returns>A <see cref="int"/> <see cref="Array"/> containing the <see cref="Monkey"/> index to throw to and the worry level of the item.</returns>
            public long[] InspectFirst() {
                long worry = operation(items.ElementAt(0)) / 3;
                items.RemoveAt(0);

                if(worry % test == 0)
                    return new long[] { trueThrow, worry };
                return new long[] { falseThrow, worry };
            }
		}
	}
}
