using Domain.Entities;
using Application.Interfaces;
using UnityEngine;

public class GameGridPresenter : MonoBehaviour
{
  [SerializeField] private GameObject cellPrefab;
  [SerializeField] private float cellSize = 1f;
  [SerializeField] private GameGridSO gridData;

  private IGameGridRepository gameGridRepo;

  void Start()
  {
    gameGridRepo = new SOGameGridRepository(gridData);

    var map = gameGridRepo.Load();
    RenderMap(map);
  }

  private void RenderMap(GameGrid map)
  {
    foreach (var cell in map.GetAllCells())
    {
      var pos = new Vector3(cell.X * cellSize, cell.Y * cellSize, 0);
      var go = Instantiate(cellPrefab, pos, Quaternion.identity, transform);

      var renderer = go.GetComponent<SpriteRenderer>();

      if (renderer != null)
      {
        renderer.color = cell.Explored
          ? Color.white
          : new Color(0.05f, 0.05f, 0.05f);
      }
    }
  }
}
