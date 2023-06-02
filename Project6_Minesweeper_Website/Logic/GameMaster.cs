
namespace Project6_Minesweeper_Website.Logic
{
    public class GameMaster
    {
        Square[,] Squarelist;

        //Intermediate game is 16x16 with 40 mines
        private int gameSizeWidth = 16; 
        private int gameSizeHeight = 16;
        private int numberOfMines = 40;


        public GameMaster() { }

        public void CreateGame()
        {
            Squarelist = new Square[gameSizeHeight, gameSizeWidth];

            int row = 0;
            int column = 0;
            while (row < gameSizeHeight)
            {
                while (column < gameSizeWidth)
                {
                    Squarelist[column, row] = new Square();
                    column++;
                    
                }
                row++;
                column = 0;
            }

            AssignMines();
        }


        private void AssignMines()
        {
            int i = 0;
            Random rnd = new Random();
            int row;
            int column;
            Square square;
            while (i < numberOfMines)
            {
                row = rnd.Next(0, gameSizeHeight);
                column = rnd.Next(0, gameSizeWidth);
                square = Squarelist[column, row];

                if (!square.clear) { continue; } //square already has a mine. Repeat loop.

                square.AddMine();

                //Add counter to neighbours

                //Check left
                if (column > 0) { 
                    Squarelist[column - 1, row].AddNearbyMine(); 

                    //Check top-left
                    if (row > 0) { Squarelist[column - 1, row - 1].AddNearbyMine(); }

                    //Check bottom-left
                    if (row < 15) { Squarelist[column -1, row + 1].AddNearbyMine(); }
                }

                //Check right
                if (column < 15) { 
                    Squarelist[column + 1, row].AddNearbyMine();

                    //Check top-right
                    if (row > 0) { Squarelist[column + 1, row - 1].AddNearbyMine(); }

                    //Check bottom-right
                    if (row < 15) { Squarelist[column + 1, row + 1].AddNearbyMine(); }
                }

                //Check up
                if (row > 0) { Squarelist[column, row - 1].AddNearbyMine(); }

                //Check down
                if (row < 15) { Squarelist[column, row + 1].AddNearbyMine(); }

                


                i++;
            }
        }


        public int[] GetGameSize()
        {
            return new int[] { gameSizeHeight, gameSizeWidth };
        }

        public Square[,] GetSquareList()
        {
            return Squarelist;
        }
    }
}
