namespace RubiksCubeSolver.Models
{
    public class Cube
    {
        public Cube()
        {
            this.Up = InitializeSide(Color.White);
            this.Down = InitializeSide(Color.Yellow);
            this.Front = InitializeSide(Color.Green);
            this.Back = InitializeSide(Color.Blue);
            this.Left = InitializeSide(Color.Orange);
            this.Right = InitializeSide(Color.Red);
        }

        public Color[][] Front { get; set; }

        public Color[][] Back { get; set; }

        public Color[][] Up { get; set; }

        public Color[][] Down { get; set; }

        public Color[][] Left { get; set; }

        public Color[][] Right { get; set; }

        private Color[][] InitializeSide(Color color)
        {
            var side = new Color[3][];

            for (int i = 0; i < 3; i++)
            {
                side[i] = new Color[3];

                for (int j = 0; j < 3; j++)
                {
                    side[i][j] = color;
                }
            }

            return side;
        }
    }
}