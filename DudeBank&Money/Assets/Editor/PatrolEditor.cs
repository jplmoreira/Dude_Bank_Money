using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(GameMaster))]
public class PatrolEditor : Editor
{
    private static bool mEditMode = true;
    private static bool mEditMode2 = false;

    private void OnSceneGUI()
    {
        if (mEditMode)
        {
            if (Event.current.type == EventType.Layout)
            {
                HandleUtility.AddDefaultControl(GUIUtility.GetControlID(GetHashCode(), FocusType.Passive));
            }

            if (Event.current.type == EventType.MouseDown)
            {
                Vector2 mousePos = Event.current.mousePosition;
                mousePos.y = Camera.current.pixelHeight - mousePos.y;
                Vector3 position = Camera.current.ScreenPointToRay(mousePos).origin;
                /*GameObject go = new GameObject("StartPoint" + 1);
                go.transform.position = position;*/
                GameMaster aux = FindObjectOfType<GameMaster>();
                if (aux == null)
                {
                    Debug.Log("Not found");
                }
                else
                {
                    Debug.Log("Found");
                }
            }
        }
    }
}
