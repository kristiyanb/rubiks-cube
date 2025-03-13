using RubiksCubeSolver.Models;

namespace RubiksCubeSolver.Services
{
    public interface IRubiksCubeService
    {
        Cube GetCubeState();

        Cube UpdateCubeState(string side, bool clockwise);

        Cube ResetCube();
    }
}
