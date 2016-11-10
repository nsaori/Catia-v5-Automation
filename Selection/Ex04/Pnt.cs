namespace Ex04
{
    internal class Pnt
    {
        public int X { get; set; }
        public int Y { get; set; }
        public int Z { get; set; }

        public Pnt(int x, int y, int z) {
            X = x;
            Y = y;
            Z = z;
        }
        public Pnt() {
            X = 0;
            Y = 0;
            Z = 0;
        }
    }
}