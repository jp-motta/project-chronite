using UnityEditor;
using UnityEngine;
using System.Linq;
using Domain.Entities;

[InitializeOnLoad]
public static class GameGridEditorRenderer
{
  static GameGridEditorRenderer()
  {
    SceneView.duringSceneGui += OnSceneGUI;
  }

  private static Color GetFeatureColor(FeatureType type)
  {
    switch (type)
    {
      case FeatureType.Ore: return new Color(0.78f, 0.78f, 0.82f, 0.45f); // silver
      default: return new Color(0.45f, 0.30f, 0.12f, 0.35f); // brown
    }
  }

  private static void OnSceneGUI(SceneView view)
  {
    var wnd = Resources.FindObjectsOfTypeAll<GameGridEditorWindow>().FirstOrDefault();
    if (wnd == null || wnd.map == null || wnd.map.cells == null)
      return;

    Handles.color = Color.white;

    foreach (var cell in wnd.map.cells)
    {
      if (cell == null)
        continue;

      Vector3 pos = new Vector3(cell.x, cell.y, 0);
      Handles.DrawWireCube(pos, Vector3.one);
      Handles.Label(pos + Vector3.up * 0.6f, $"({cell.x},{cell.y})");

      // Safe feature access
      FeatureType featureType;
      if (cell.features == null || cell.features.Length == 0 || cell.features[0] == null)
        featureType = default; // falls back to brown
      else
        featureType = cell.features[0].type;

      Color fillColor = GetFeatureColor(featureType);

      Handles.DrawSolidRectangleWithOutline(
        new Rect(cell.x - 0.5f, cell.y - 0.5f, 1, 1),
        fillColor,
        Color.white
      );
    }

    HandleMouseEvents(wnd);
  }

  private static void HandleMouseEvents(GameGridEditorWindow wnd)
  {
    Event e = Event.current;
    if (e.type != EventType.MouseDown || e.button != 0)
      return;

    Ray ray = HandleUtility.GUIPointToWorldRay(e.mousePosition);
    float dist = Mathf.Approximately(ray.direction.z, 0f) ? 0f : -ray.origin.z / ray.direction.z;
    Vector3 worldPos = ray.origin + ray.direction * dist;

    int x = Mathf.RoundToInt(worldPos.x);
    int y = Mathf.RoundToInt(worldPos.y);

    var existingCell = wnd.map.cells?.FirstOrDefault(c => c != null && c.x == x && c.y == y);

    if (existingCell == null)
    {
      if (EditorUtility.DisplayDialog("Add Cell?", $"Add cell at ({x},{y})?", "Add", "Cancel"))
        wnd.AddCell(x, y);
    }
    else
    {
      ShowCellContextMenu(wnd, x, y);
    }
  }

  private static void ShowCellContextMenu(GameGridEditorWindow wnd, int x, int y)
  {
    var menu = new GenericMenu();

    foreach (FeatureType ft in System.Enum.GetValues(typeof(FeatureType)))
    {
      menu.AddItem(new GUIContent($"Set Feature/{ft}"), false, () =>
      {
        wnd.SetCellFeature(x, y, ft);
      });
    }

    menu.AddDisabledItem(new GUIContent(""));

    menu.AddItem(new GUIContent("Remove Cell"), false, () =>
    {
      wnd.RemoveCellAt(x, y);
    });

    menu.ShowAsContext();
  }
}
