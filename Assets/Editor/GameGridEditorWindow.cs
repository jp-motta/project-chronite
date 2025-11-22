using UnityEditor;
using UnityEngine;
using Domain.Entities;

public class GameGridEditorWindow : EditorWindow
{
  public GameGridSO map;
  private Vector2 scroll;

  [MenuItem("Tools/Game Grid Editor")]
  public static void Open()
  {
    GetWindow<GameGridEditorWindow>("Game Grid Editor");
  }

  private void OnGUI()
  {
    EditorGUILayout.Space();
    EditorGUILayout.LabelField("Game Grid Editor", EditorStyles.boldLabel);

    map = (GameGridSO)EditorGUILayout.ObjectField("Game Grid", map, typeof(GameGridSO), false);

    if (map == null)
    {
      EditorGUILayout.HelpBox("Selecione um GameGridSO para editar.", MessageType.Info);
      return;
    }

    EditorGUILayout.Space();
    DrawCellsList();
  }

  private void DrawCellsList()
  {
    EditorGUILayout.LabelField("Cells", EditorStyles.boldLabel);

    scroll = EditorGUILayout.BeginScrollView(scroll);

    for (int i = 0; i < map.cells.Length; i++)
    {
      var cell = map.cells[i];
      EditorGUILayout.BeginVertical("box");

      EditorGUILayout.BeginHorizontal();
      EditorGUILayout.LabelField($"Cell {i}", EditorStyles.boldLabel);

      if (GUILayout.Button("X", GUILayout.Width(20)))
      {
        RemoveCell(i);
        return;
      }

      EditorGUILayout.EndHorizontal();

      cell.x = EditorGUILayout.IntField("X", cell.x);
      cell.y = EditorGUILayout.IntField("Y", cell.y);
      cell.exploredInitially = EditorGUILayout.Toggle("Explored Initially", cell.exploredInitially);

      SerializedObject so = new SerializedObject(map);
      SerializedProperty cellsProp = so.FindProperty("cells");
      SerializedProperty cellProp = cellsProp.GetArrayElementAtIndex(i);

      EditorGUILayout.PropertyField(cellProp.FindPropertyRelative("features"), true);
      so.ApplyModifiedProperties();

      EditorGUILayout.EndVertical();
    }

    EditorGUILayout.EndScrollView();

    if (GUILayout.Button("Add Empty Cell"))
      AddCell(0, 0);
  }

  public void AddCell(int x, int y)
  {
    ArrayUtility.Add(ref map.cells, new CellSO
    {
      x = x,
      y = y,
      exploredInitially = false,
      features = new FeatureSO[0]
    });

    EditorUtility.SetDirty(map);
  }

  public void RemoveCell(int index)
  {
    ArrayUtility.RemoveAt(ref map.cells, index);
    EditorUtility.SetDirty(map);
  }

  public void RemoveCellAt(int x, int y)
  {
    if (map?.cells == null) return;
    int index = System.Array.FindIndex(map.cells, c => c != null && c.x == x && c.y == y);
    if (index < 0) return;
    ArrayUtility.RemoveAt(ref map.cells, index);
    EditorUtility.SetDirty(map);
    Repaint();
  }

  public void SetCellFeature(int x, int y, FeatureType featureType)
  {
    if (map?.cells == null) return;
    var cell = System.Array.Find(map.cells, c => c != null && c.x == x && c.y == y);
    if (cell == null) return;

    if (cell.features == null || cell.features.Length == 0)
    {
      cell.features = new FeatureSO[1];
      cell.features[0] = new FeatureSO { type = featureType, value = 0 };
    }
    else
    {
      if (cell.features[0] == null)
        cell.features[0] = new FeatureSO { type = featureType, value = 0 };
      else
        cell.features[0].type = featureType;
    }

    EditorUtility.SetDirty(map);
    Repaint();
  }
}
