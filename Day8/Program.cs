namespace Day8 {
	internal class Program {
		static void Main(string[] args) {
			//Get the inputted data
			string file = Path.GetFullPath(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"..\..\..\trees.txt"));
			string[] data = File.ReadAllLines(file);

			//Split all data into 2D array
			Tree[,] trees = new Tree[data.Length, data[0].Length];
			for(int i = 0; i < data.Length; i++) {
				for(int j = 0; j < data[i].Length; j++) {
					if(i == 0 || j == 0 || i == data.Length - 1 || j == data[i].Length - 1) {
						trees[i, j] = new Tree(data[i][j], true);
					}
					else
						trees[i, j] = new(data[i][j]);
				}
			}

			Part1(trees);
			Part2(trees);
		}

		/// <summary>
		/// Finds all trees that are visible viewing the trees from any of its sides.
		/// </summary>
		/// <param name="trees">All the <see cref="Tree"/> objects in two dimensional <see cref="Array"/>.</param>
		static void Part1(Tree[,] trees) {
			for(int i = 1; i < trees.GetLength(0) - 1; i++) {
				for(int j = 1; j < trees.GetLength(1) - 1 ; j++) {
					//Check left
					for(int k = 0; k < i; k++) {
						//Break early if a tree is taller than the current tree
						if(trees[k, j].Height >= trees[i, j].Height)
							break;

						if(k == i - 1 && trees[k, j].Height < trees[i, j].Height)
							trees[i, j].Visible = true;
					}

					//Check up
					for(int k = 0; k < j; k++) {
						//Break early if a tree is taller than the current tree
						if(trees[i, k].Height >= trees[i, j].Height)
							break;

						if(k == j - 1 && trees[i, k].Height < trees[i, j].Height)
							trees[i, j].Visible = true;
					}

					//Check right
					for(int k = trees.GetLength(0) - 1; k > j; k--) {
						//Break early if a tree is taller than the current tree
						if(trees[i, k].Height >= trees[i, j].Height)
							break;

						if(k == j + 1 && trees[i, k].Height < trees[i, j].Height)
							trees[i, j].Visible = true;
					}

					//Check down
					for(int k = trees.GetLength(1) - 1; k > i; k--) {
						//Break early if a tree is taller than the current tree
						if(trees[k, j].Height >= trees[i, j].Height)
							break;

						if(k == i + 1 && trees[k, j].Height < trees[i, j].Height)
							trees[i, j].Visible = true;
					}
				}
			}

			int visible = 0;
			//Count the amount of visible trees
			for(int i = 0; i < trees.GetLength(0); i++) {
				for(int j = 0; j < trees.GetLength(1); j++) {
					if(trees[i, j].Visible)
						visible++;
				}
			}

			Console.WriteLine(visible);
		}

		/// <summary>
		/// Find the tree with the highest scenic score, see the furthest away from itself.
		/// </summary>
		/// <param name="trees">All the <see cref="Tree"/> objects in two dimensional <see cref="Array"/>.</param>
		static void Part2(Tree[,] trees) {
			for(int i = 1; i < trees.GetLength(0) - 1; i++) {
				for(int j = 1; j < trees.GetLength(1) - 1; j++) {
					//Check left
					for(int k = j - 1; k >= 0; k--) {
						trees[i, j].Scenic[0]++;

						//Break when a taller tree is found
						if(trees[i, k].Height >= trees[i, j].Height)
							break;
					}
					
					//Check up
					for(int k = i - 1; k >= 0; k--) {
						trees[i, j].Scenic[1]++;

						//Break when a taller tree is found
						if(trees[k, j].Height >= trees[i, j].Height)
							break;
					}

					//Check right
					for(int k = j + 1; k <= trees.GetLength(1) - 1; k++) {
						trees[i, j].Scenic[2]++;

						//Break when a taller tree is found
						if(trees[i, k].Height >= trees[i, j].Height)
							break;
					}

					//Check down
					for(int k = i + 1; k <= trees.GetLength(0) - 1; k++) {
						trees[i, j].Scenic[3]++;

						//Break when a taller tree is found
						if(trees[k, j].Height >= trees[i, j].Height)
							break;
					}
				}
			}

			int highest = 0;
			//Find the tree with the highest scenic score
			for(int i = 0; i < trees.GetLength(0); i++) {
				for(int j = 0; j < trees.GetLength(1); j++) {
					if(trees[i, j].ScenicScore() > highest)
						highest = trees[i, j].ScenicScore();
				}
			}

			Console.WriteLine(highest);
		}

		/// <summary>
		/// A single tree with <see cref="height"/> and <see cref="visible"/> attributes.
		/// </summary>
		class Tree {
			private readonly int height;
			private bool visible;
			private int[] scenic;

			/// <summary>
			/// Set the height of the tree and if it is visible.
			/// </summary>
			/// <param name="height">The height of the tree as <see cref="int"/>.</param>
			/// <param name="visible"><see cref="bool"/> if the tree is visible or not. <see langword="false"/> by deafult.</param>
			public Tree(char height, bool visible = false) {
				this.height = height - '0';
				this.visible = visible;
				scenic = new int[4];
			}

			/// <summary>
			/// The height of the tree as <see cref="int"/>.
			/// </summary>
			public int Height { get => height; }
			/// <summary>
			/// If the tree is visible or not from the outside as <see cref="bool"/>.
			/// </summary>
			public bool Visible { get => visible; set => visible = value; }
			/// <summary>
			/// The scenic score in each direction from the <see cref="Tree"/>.
			/// </summary>
			public int[] Scenic { get => scenic; set => scenic = value; }

			/// <summary>
			/// Mulitply the 4 different <see cref="scenic"/> scores together.
			/// </summary>
			/// <returns>The total scenic score as <see cref="int"/>.</returns>
			public int ScenicScore() {
				int scenicScore = 1;
				foreach(int score in scenic)
					scenicScore *= score;
				
				return scenicScore;
			}
		}
	}
}
