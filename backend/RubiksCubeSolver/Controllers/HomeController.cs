using Microsoft.AspNetCore.Mvc;
using RubiksCubeSolver.Models;
using RubiksCubeSolver.Services;

namespace RubiksCubeSolver.Controllers
{
    [ApiController]
    [Route("/")]
    public class HomeController : ControllerBase
    {
        private readonly IRubiksCubeService rubiksCubeService;

        public HomeController(IRubiksCubeService rubiksCubeService)
        {
            this.rubiksCubeService = rubiksCubeService;
        }

        [HttpGet]
        public async Task<IActionResult> GetCube()
        {
            var cube = this.rubiksCubeService.GetCubeState();

            return this.Ok(cube);
        }

        [HttpPost]
        public async Task<IActionResult> RotateCube(RotationInputModel input)
        {
            var result = this.rubiksCubeService.UpdateCubeState(input.Side, input.Clockwise);

            return this.Ok(result);
        }

        [HttpDelete("reset")]
        public async Task<IActionResult> ResetCube()
        {
            var result = this.rubiksCubeService.ResetCube();

            return this.Ok(result);
        }
    }
}
