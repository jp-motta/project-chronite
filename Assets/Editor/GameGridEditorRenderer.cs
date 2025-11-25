using UnityEditor;
using UnityEngine;
using System.Linq;
using Domain.Entities;

[InitializeOnLoad]
public static class GameGridEditorRenderer
{
  static GameGridEditorRenderer() => SceneView.duringSceneGui += OnSceneGUI;

  private static void OnSceneGUI(SceneView view)
  {
    var wnd = Resources.FindObjectsOfTypeAll<GameGridEditorWindow>().FirstOrDefault();
    if (wnd == null) return;

    var grid = wnd.GetGrid();
    if (grid == null || grid.Cells == null) return;

    Handles.color = Color.white;

    foreach (var c in grid.Cells)
    {
      var ft = c.feature;
      Color fill = ft == FeatureType.Ore
        ? new Color(0.78f, 0.78f, 0.82f, 0.45f)
        : new Color(0.45f, 0.30f, 0.12f, 0.35f);

      Handles.DrawSolidRectangleWithOutline(
        new Rect(c.x - 0.5f, c.y - 0.5f, 1, 1),
        fill,
        Color.white);

      Handles.Label(new Vector3(c.x, c.y + 0.6f, 0), $"({c.x},{c.y}) {ft}");
    }

    HandleMouse(wnd, grid);
  }

  private static void HandleMouse(GameGridEditorWindow wnd, GameGridSO grid)
  {
    Event e = Event.current;
    if (e.type != EventType.MouseDown || e.button != 0) return;

    Ray ray = HandleUtility.GUIPointToWorldRay(e.mousePosition);
    float dist = Mathf.Approximately(ray.direction.z, 0f) ? 0f : -ray.origin.z / ray.direction.z;
    Vector3 worldPos = ray.origin + ray.direction * dist;
    int x = Mathf.RoundToInt(worldPos.x);
    int y = Mathf.RoundToInt(worldPos.y);

    var existing = grid.Cells.FirstOrDefault(c => c.x == x && c.y == y);
    if (existing == null)
    {
      Undo.RecordObject(grid, "Add Cell");
      grid.AddCell(x, y);
      EditorUtility.SetDirty(grid);
      AssetDatabase.SaveAssets();
    }
    else
    {
      ShowMenu(wnd, x, y);
    }
  }

  private static void ShowMenu(GameGridEditorWindow wnd, int x, int y)
  {
    var menu = new GenericMenu();
    foreach (FeatureType ft in System.Enum.GetValues(typeof(FeatureType)))
    {
      menu.AddItem(new GUIContent($"Set Feature/{ft}"), false, () => wnd.SetCellFeature(x, y, ft));
    }
    menu.AddSeparator("");
    menu.AddItem(new GUIContent("Remove Cell"), false, () => wnd.RemoveCellAt(x, y));
    menu.ShowAsContext();
  }
}
