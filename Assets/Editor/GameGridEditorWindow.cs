using UnityEditor;
using UnityEngine;
using Domain.Entities;

public class GameGridEditorWindow : EditorWindow
{
  private GameGridSO grid;
  private Vector2 scroll;

  [MenuItem("Tools/Game Grid Editor")]
  public static void Open() => GetWindow<GameGridEditorWindow>("Game Grid");

  public GameGridSO GetGrid() => grid;

  private void OnGUI()
  {
    grid = (GameGridSO)EditorGUILayout.ObjectField("Grid Asset", grid, typeof(GameGridSO), false);
    if (grid == null)
    {
      EditorGUILayout.HelpBox("Assign a GameGridSO asset.", MessageType.Info);
      return;
    }

    DrawCells();
    EditorGUILayout.Space();
    if (GUILayout.Button("Add Cell (0,0)"))
    {
      Undo.RecordObject(grid, "Add Cell");
      grid.AddCell(0, 0);
      EditorUtility.SetDirty(grid);
      AssetDatabase.SaveAssets();
    }
  }

  private void DrawCells()
  {
    scroll = EditorGUILayout.BeginScrollView(scroll);
    for (int i = 0; i < grid.Cells.Count; i++)
    {
      var c = grid.Cells[i];
      EditorGUILayout.BeginVertical("box");
      EditorGUILayout.BeginHorizontal();
      EditorGUILayout.LabelField($"Cell {i}", EditorStyles.boldLabel);
      if (GUILayout.Button("X", GUILayout.Width(22)))
      {
        Undo.RecordObject(grid, "Remove Cell");
        grid.RemoveCellAt(c.x, c.y);
        EditorUtility.SetDirty(grid);
        AssetDatabase.SaveAssets();
        EditorGUILayout.EndHorizontal();
        EditorGUILayout.EndVertical();
        break;
      }
      EditorGUILayout.EndHorizontal();

      int nx = EditorGUILayout.IntField("X", c.x);
      int ny = EditorGUILayout.IntField("Y", c.y);
      bool explored = EditorGUILayout.Toggle("Explored Initially", c.exploredInitially);
      var feature = (FeatureType)EditorGUILayout.EnumPopup("Feature", c.feature);

      if (nx != c.x || ny != c.y || explored != c.exploredInitially || feature != c.feature)
      {
        Undo.RecordObject(grid, "Edit Cell");
        c.x = nx;
        c.y = ny;
        c.exploredInitially = explored;
        c.feature = feature;
        EditorUtility.SetDirty(grid);
        AssetDatabase.SaveAssets();
      }

      EditorGUILayout.EndVertical();
    }
    EditorGUILayout.EndScrollView();
  }

  public void SetCellFeature(int x, int y, FeatureType type)
  {
    var cell = grid?.Find(x, y);
    if (cell == null) return;
    Undo.RecordObject(grid, "Set Cell Feature");
    cell.feature = type;
    EditorUtility.SetDirty(grid);
    AssetDatabase.SaveAssets();
    Repaint();
  }

  public void RemoveCellAt(int x, int y)
  {
    Undo.RecordObject(grid, "Remove Cell");
    grid.RemoveCellAt(x, y);
    EditorUtility.SetDirty(grid);
    AssetDatabase.SaveAssets();
    Repaint();
  }
}
