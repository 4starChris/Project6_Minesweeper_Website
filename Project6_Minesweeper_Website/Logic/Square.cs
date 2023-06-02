namespace Project6_Minesweeper_Website.Logic
{
    public class Square
    {
        public int nearbyMines { get; set; }
        public bool clear { get; set; }

        public Square() {
            clear = true; 
            nearbyMines = 0;
        }

        public void AddNearbyMine()
        {
            nearbyMines++;
        }

        public void AddMine()
        {
            clear = false;
        }

        public void CheckMine()
        {
            if (clear)
            {
                //display number of nearbyMines
            }
            else
            {
                //explode
            }
        }

        public string GetSquareValue()
        {
            if (!clear) { return "B"; }
            else return nearbyMines.ToString();
        }
    }
}
