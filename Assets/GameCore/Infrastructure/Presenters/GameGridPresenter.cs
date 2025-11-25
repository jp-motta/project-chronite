using Domain.Entities;
using Application.Interfaces;
using UnityEngine;

public class GameGridPresenter : MonoBehaviour
{
  [SerializeField] private GameObject cellPrefab;
  [SerializeField] private GameGridSO gridData;
  [SerializeField] private float cellSizeX = 1f;
  [SerializeField] private float cellSizeY = 1f;

  [Header("Cell Feature Database")]
  [SerializeField] private CellFeatureDatabaseSO cellFeatureDatabase;

  private IGameGridRepository gameGridRepo;
  private const string FEATURE_CHILD_NAME = "Feature";

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
      var pos = new Vector3(cell.X * cellSizeX, cell.Y * cellSizeY, 0);
      var go = Instantiate(cellPrefab, pos, Quaternion.identity, transform);

      var sr = go.GetComponent<SpriteRenderer>();
      if (sr != null)
      {
        sr.color = cell.Explored
          ? new Color(0.45f, 0.30f, 0.12f, 1f)
          : new Color(0.12f, 0.08f, 0.06f, 1f);
      }

      ApplyFeature(go, cell);
    }
  }

  private void ApplyFeature(GameObject parent, Cell cell)
  {
    var child = parent.transform.Find(FEATURE_CHILD_NAME);
    if (child == null) return;

    var featureSr = child.GetComponent<SpriteRenderer>();
    if (featureSr == null) return;

    bool shouldShow = cell.Explored
                   && cellFeatureDatabase != null
                   && cell.Feature != FeatureType.Empty;

    if (!shouldShow)
    {
      featureSr.enabled = false;
      return;
    }

    var sprite = cellFeatureDatabase.GetSprite(cell.Feature);
    featureSr.sprite = sprite;
    featureSr.enabled = sprite != null;
  }
}
