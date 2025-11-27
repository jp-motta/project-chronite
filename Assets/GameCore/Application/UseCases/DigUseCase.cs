using Domain.Entities;

namespace Application
{
  public class DigUseCase
  {
    private readonly IDigService _digService;
    private readonly IGameGridRepository _repo;

    public DigUseCase(IDigService digService, IGameGridRepository repo)
    {
      _digService = digService;
      _repo = repo;
    }

    public DigResult Execute(int px, int py)
    {
      var grid = _repo.Load();

      var result = _digService.Dig(grid, px, py);

      if (result.Success)
        _repo.Save(grid);

      return result;
    }
  }
}
