using RubiksCubeSolver.Models;

namespace RubiksCubeSolver.Services
{
    public class RubiksCubeService : IRubiksCubeService
    {
        private const int N = 3;
        private Cube cube;

        public RubiksCubeService()
        {
            cube = new();
        }

        public Cube GetCubeState() => cube;

        public Cube ResetCube() => cube = new();

        public Cube UpdateCubeState(string side, bool clockwise) => side switch
        {
            "u" => RotateUp(clockwise),
            "d" => RotateDown(clockwise),
            "f" => RotateFront(clockwise),
            "b" => RotateBack(clockwise),
            "l" => RotateLeft(clockwise),
            "r" => RotateRight(clockwise),
            _ => cube
        };

        private Cube RotateUp(bool clockwise)
        {
            this.RotateHorizonalEdges(0, clockwise);
            cube.Up = this.RotateFace(cube.Up, clockwise);

            return cube;
        }
        private Cube RotateDown(bool clockwise)
        {
            this.RotateHorizonalEdges(N - 1, !clockwise);
            cube.Down = this.RotateFace(cube.Down, clockwise);

            return cube;
        }

        private Cube RotateLeft(bool clockwise)
        {
            var currentFrontRow = GetCurrentRow(cube.Front, 0);
            var currentUpRow = GetCurrentRow(cube.Up, 0);
            var currentBackRow = GetCurrentRow(cube.Back, N - 1);
            var currentDownRow = GetCurrentRow(cube.Down, 0);

            for (int i = 0; i < N; i++)
            {
                cube.Front[i][0] = clockwise ? currentUpRow[i] : currentDownRow[i];
                cube.Down[i][0] = clockwise ? currentFrontRow[i] : currentBackRow[N - i - 1];
                cube.Back[i][N - 1] = clockwise ? currentDownRow[N - i - 1] : currentUpRow[N - i - 1];
                cube.Up[i][0] = clockwise ? currentBackRow[N - i - 1] : currentFrontRow[i];
            }

            cube.Left = this.RotateFace(cube.Left, clockwise);

            return cube;
        }

        private Cube RotateRight(bool clockwise)
        {
            var currentFrontRow = GetCurrentRow(cube.Front, N - 1);
            var currentUpRow = GetCurrentRow(cube.Up, N - 1);
            var currentBackRow = GetCurrentRow(cube.Back, 0);
            var currentDownRow = GetCurrentRow(cube.Down, N - 1);

            for (int i = 0; i < N; i++)
            {
                cube.Front[i][N - 1] = clockwise ? currentDownRow[i] : currentUpRow[i];
                cube.Down[i][N - 1] = clockwise ? currentBackRow[N - i - 1] : currentFrontRow[i];
                cube.Back[i][0] = clockwise ? currentUpRow[N - i - 1] : currentDownRow[N - i - 1];
                cube.Up[i][N - 1] = clockwise ? currentFrontRow[i] : currentBackRow[N - i - 1];
            }

            cube.Right = this.RotateFace(cube.Right, clockwise);

            return cube;
        }

        private Cube RotateBack(bool clockwise)
        {
            var currentUpRow = cube.Up[0];
            var currentRightRow = GetCurrentRow(cube.Right, N - 1);
            var currentDownRow = cube.Down[N - 1];
            var currentLeftRow = GetCurrentRow(cube.Left, 0);

            cube.Up[0] = clockwise ? currentRightRow : [.. currentLeftRow.Reverse()];
            cube.Down[N - 1] = clockwise ? currentLeftRow : [.. currentRightRow.Reverse()];

            for (var i = 0; i < N; i++)
            {
                cube.Right[i][N - 1] = clockwise ? currentDownRow[N - i - 1] : currentUpRow[i];
                cube.Left[i][0] = clockwise ? currentUpRow[N - i - 1] : currentDownRow[i];
            }

            cube.Back = this.RotateFace(cube.Back, clockwise);

            return cube;
        }

        private Cube RotateFront(bool clockwise)
        {
            var currentUpRow = cube.Up[N - 1];
            var currentRightRow = GetCurrentRow(cube.Right, 0);
            var currentDownRow = cube.Down[0];
            var currentLeftRow = GetCurrentRow(cube.Left, N - 1);

            cube.Up[N - 1] = clockwise ? [.. currentLeftRow.Reverse()] : currentRightRow;
            cube.Down[0] = clockwise ? [.. currentRightRow.Reverse()] : currentLeftRow;

            for (int i = 0; i < N; i++)
            {
                cube.Right[i][0] = clockwise ? currentUpRow[i] : currentDownRow[N - i - 1];
                cube.Left[i][N - 1] = clockwise ? currentDownRow[i] : currentUpRow[N - i - 1];
            }

            cube.Front = this.RotateFace(cube.Front, clockwise);

            return cube;
        }

        private void RotateHorizonalEdges(int index, bool clockwise)
        {
            var currentFront = cube.Front[index];
            var currentRight = cube.Right[index];
            var currentBack = cube.Back[index];
            var currentLeft = cube.Left[index];

            cube.Front[index] = clockwise ? currentRight : currentLeft;
            cube.Right[index] = clockwise ? currentBack : currentFront;
            cube.Back[index] = clockwise ? currentLeft : currentRight;
            cube.Left[index] = clockwise ? currentFront : currentBack;
        }

        private Color[] GetCurrentRow(Color[][] side, int index)
        {
            var currentRow = new Color[N];

            for (int i = 0; i < N; i++)
            {
                currentRow[i] = side[i][index];
            }

            return currentRow;
        }

        private Color[][] RotateFace(Color[][] currentSide, bool clockwise)
        {
            var newSide = new Color[N][]
            {
                new Color[N],
                new Color[N],
                new Color[N],
            };

            for (int i = 0; i < N; i++)
            {
                for (int j = 0; j < N; j++)
                {
                    if (clockwise)
                    {
                        newSide[j][N - i - 1] = currentSide[i][j];
                    }
                    else
                    {
                        newSide[N - j - 1][i] = currentSide[i][j];
                    }
                }
            }

            return newSide;
        }
    }
}