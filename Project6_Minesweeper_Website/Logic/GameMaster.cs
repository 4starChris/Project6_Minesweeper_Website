
namespace Project6_Minesweeper_Website.Logic
{
    public class GameMaster
    {
        Square[,] Squarelist;

        //Intermediate game is 16x16 with 40 mines
        private int gameSizeWidth = 16; 
        private int gameSizeHeight = 16;
        private int numberOfMines = 40;
        public int numberOfFlagsOnHand = 40;
        public string gameMessage;
        private bool lockGame;


        public GameMaster() { }

        public void CreateGame()
        {
            Squarelist = new Square[gameSizeHeight, gameSizeWidth];
            lockGame = false;
            UpdateGameMessage();
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
                    if (row < gameSizeHeight - 1) { Squarelist[column -1, row + 1].AddNearbyMine(); }
                }

                //Check right
                if (column < gameSizeWidth - 1) { 
                    Squarelist[column + 1, row].AddNearbyMine();

                    //Check top-right
                    if (row > 0) { Squarelist[column + 1, row - 1].AddNearbyMine(); }

                    //Check bottom-right
                    if (row < gameSizeHeight - 1) { Squarelist[column + 1, row + 1].AddNearbyMine(); }
                }

                //Check up
                if (row > 0) { Squarelist[column, row - 1].AddNearbyMine(); }

                //Check down
                if (row < gameSizeHeight - 1) { Squarelist[column, row + 1].AddNearbyMine(); }

                


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

        public void RevealSquare(int column, int row)
        {
            if (lockGame) { return; }
            Square square = Squarelist[column, row];
            if (square.revealed) { return; }
            if (square.flagged) { return; }
            
            square.revealed = true;
            if (!square.clear) {
                gameMessage = "You Lose!";
                LockGame();
                return; 
            } 
            if (square.nearbyMines == 0)
            {
                //Check left
                if (column > 0)
                {
                    RevealSquare(column - 1, row);

                    //Check top-left
                    if (row > 0) { RevealSquare(column - 1, row-1); }

                    //Check bottom-left
                    if (row < gameSizeHeight - 1) { RevealSquare(column - 1, row+1); }
                }

                //Check right
                if (column < gameSizeWidth - 1)
                {
                    RevealSquare(column +1, row);

                    //Check top-right
                    if (row > 0) { RevealSquare(column + 1, row-1); }

                    //Check bottom-right
                    if (row < gameSizeHeight - 1) { RevealSquare(column + 1, row+1); }
                }

                //Check up
                if (row > 0) { RevealSquare(column , row -1); }

                //Check down
                if (row < gameSizeHeight - 1) { RevealSquare(column, row + 1); }
            }
        }

        public void PlaceFlag(int column, int row)
        {
            if (lockGame) { return; }
            Square square = Squarelist[column, row];
            if (square.revealed) { return; }
            if (!square.flagged && numberOfFlagsOnHand > 0) { 
                square.flagged = true; 
                numberOfFlagsOnHand--;
                UpdateGameMessage();
                if (numberOfFlagsOnHand == 0)
                {
                    CheckWinCondition();
                }

                return; }
            if (square.flagged) { square.flagged = false; numberOfFlagsOnHand++; UpdateGameMessage(); }
            return;
        }

        private void UpdateGameMessage()
        {
            gameMessage = "Flags Left: " + numberOfFlagsOnHand;
        }

        private void CheckWinCondition()
        {
            int minesExposed = numberOfMines;
            foreach (Square square in Squarelist)
            {
                if (square.flagged && !square.clear) { minesExposed--; }
            }
            if (minesExposed == 0)
            {
                gameMessage = "You Win!";
                LockGame();
            }
            return;
        }

        private void LockGame()
        {
            lockGame = true;
            return;
        }
    }
}
