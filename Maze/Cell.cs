namespace Maze
{
    enum CellTypes { Normal, Start, End };
    class Cell : Point
    {
        public CellTypes CType { get; set; }
        public bool Visited { get; set; }
        public bool Solve { get; set; }

        public Walls wS = new Walls();
        
        public Cell(int x, int y) : base(x, y)
        {
            CType = CellTypes.Normal; 
        } 
    }
}
