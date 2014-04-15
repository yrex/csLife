using System;

namespace GameOfLife
{
	public class Life
	{
		// default parameters
		private const int DEFAULT_HEIGHT = 50;
		private const int DEFAULT_WIDTH = 100;
		private const int DEFAULT_NUMSTEPS = 10;
		private const char DEFAULT_DEAD_CELL = ' ';
		private const char DEFAULT_LIVE_CELL = 'o';
		private const char DEFAULT_BORDER_CELL = '#';

		public int height;
		public int width;
		public int numsteps;
		public bool[][] grid;

		private char deadcell, livecell, bordercell;


		// default constructor
		public Life ()
		{
			// set a default height and width
			this.height = DEFAULT_HEIGHT;
			this.width = DEFAULT_WIDTH;
			this.numsteps = DEFAULT_NUMSTEPS;
			this.deadcell = DEFAULT_DEAD_CELL;
			this.livecell = DEFAULT_LIVE_CELL;
			this.bordercell = DEFAULT_BORDER_CELL;


			this.grid = new bool[this.height] [];

			this.initGrid ();

		}



		// method to initialise the grid
		private void initGrid ()
		{

			// initialise second dimension
			for (int row = 0; row < this.grid.Length; row++) {
				this.grid [row] = new bool[this.width];
			}

			// randomly generate live and dead cells
			Random rnd = new Random ();

			// set initial values to false/0 (dead cells)
			for (int row = 0; row < this.grid.Length; row++) {
				for (int col = 0; col < this.grid[row].Length; col++) {
					this.grid [row] [col] = (rnd.Next (2) == 1);
				}
			}

//			this.grid [20] [20] = true;
//			this.grid [20] [21] = true;
//			this.grid [20] [22] = true;
		}

		// method to update the Life grid at each step
		public void updateGrid()
		{
		
			// loop through each cell and apply the Game Of Life Rules

			// neighbouring cells
			// [!] We need to count the number of live cells, so will
			//     convert bool to int
			int tl, tc, tr, cl, cr, bl, bc, br;

			// make a copy of the grid
			bool[][] tempGrid;
			tempGrid = new bool[this.height][];

			for (int i = 0; i < this.grid.Length; i++) {
				bool[] theCol = this.grid [i];
				bool[] newCol = new bool[theCol.Length];
				Array.Copy (theCol, newCol, theCol.Length);
				tempGrid [i] = newCol;
			}

			for (int row = 0; row < this.grid.Length; row++) {
				for (int col = 0; col < this.grid[row].Length; col++) {
				
					tl = tc = tr = cl = cr = bl = bc = br = 0;


					// excluding top-row
					if (row != 0) {

						tc = this.grid [row - 1] [col] ? 1 : 0;

						// excluding top-row and left-column
						if (col != 0) {
							tl = this.grid [row - 1] [col - 1] ? 1 : 0;
						}

						// excluding top-row and right-column
						if (col != this.grid [row].Length - 1) {
							tr = this.grid [row - 1] [col + 1] ? 1 : 0;
						}

					}

					// excluding bottom-row
					if (row != this.grid.Length - 1) {

						bc = this.grid [row + 1] [col] ? 1 : 0;

						// excluding bottom-row and left-column
						if (col != 0) {
							bl = this.grid [row + 1] [col - 1] ? 1 : 0;
						}

						// excluding bottom-row and right-column
						if (col != this.grid[row].Length - 1) {
							br = this.grid [row + 1] [col + 1] ? 1 : 0;
						}
					}

					// excluding left-column
					if (col != 0) {
						cl = this.grid [row] [col - 1] ? 1 : 0;
					}

					// excluding right-column
					if (col != this.grid[row].Length - 1) {
						cr = this.grid [row] [col + 1] ? 1 : 0;
					}

					// apply life logic
					int numLiveNeighbours = tl + tc + tr + cl + cr + bl + bc + br;


					// for live cells:
					if ( (this.grid[row][col]) & ((numLiveNeighbours < 2) | (numLiveNeighbours > 3)) ) {
						tempGrid [row] [col] = false;
					}

					// for dead cells:
					if ( !(this.grid[row][col]) & (numLiveNeighbours == 3) ) {
						tempGrid [row] [col] = true;
					}
				}
			}

			this.grid = tempGrid;

		}


		// method to print out the grid
		// here, 'print out' means to return a string of formatted text
		public string printGrid ()
		{

			string output = "";

			// horizontal border
			string horizBorder = "";
			for (int i = 0; i < this.width+2; i++) {
				horizBorder += this.bordercell;
			}

			// add top horizontal border
			output += horizBorder;
			output += Environment.NewLine;

			// add rows
			for (int row = 0; row < this.grid.Length; row++) {

				// left border
				output += this.bordercell;

				// actual grid content
				for (int col = 0; col < this.grid[row].Length; col++) {
					output += this.grid [row] [col] ? this.livecell : this.deadcell;
				}

				// right border
				output += this.bordercell;
				output += Environment.NewLine;

			}

			// add bottom horizontal border
			output += horizBorder;

			return	output;

		}





	}
}

