﻿namespace Day3 {
	internal class Program {
		static void Main(string[] args) {
			string file = Path.GetFullPath(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"..\..\..\rucksacks.txt"));
			string[] data = File.ReadAllLines(file);

			Part1(data);
			Part2(data);
		}

		/// <summary>
		/// Gets the duplicate item priorities of the rucksacks.
		/// </summary>
		/// <param name="data">All the rucksacks with 2 compartments combined.</param>
		static void Part1(string[] data) {
			int total = 0;

			//Check each rucksack
			foreach(string rucksack in data) {
				int middle = rucksack.Length / 2;

				//Split into the two compartments and find duplicate
				string first = rucksack.Substring(0, middle);
				string second = rucksack.Substring(middle);
				char duplicate = first.Intersect(second).First();

				total += Priority(duplicate);
			}
			Console.WriteLine(total);
		}

		/// <summary>
		/// Find the badge for the group of 3 elfs.
		/// </summary>
		/// <param name="data">All the elf rucksacks.</param>
		static void Part2(string[] data) {
			int total = 0;
			string[] group = new string[3];
			int size = 0;

			foreach(string rucksack in data) {
				group[size++] = rucksack;
				if(size >= 3) {
					//Find the badge and get priorirt
					char badge = group[0].Intersect(group[1]).Intersect(group[2]).First();
					total += Priority(badge);

					//Reset the size to restart
					size = 0;
				}
			}
			Console.WriteLine(total);
		}

		/// <summary>
		/// Get the priority numbers of the items.
		/// </summary>
		/// <param name="item">The inputted <see cref="char"/> as <see cref="int"/>.</param>
		/// <returns></returns>
		static int Priority(int item) {
			//Convert lowercase a-z
			if(item > 96 && item < 123)
				return item - 96;

			//Convert uppercase A-Z
			if(item > 64 && item < 91)
				return item - 38;

			return 0;
		}
	}
}
