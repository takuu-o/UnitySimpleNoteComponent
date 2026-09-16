using UnityEditor;
using UnityEngine;

namespace TakLabs.SimpleNote
{
    [CustomEditor(typeof(SimpleNote))]
    public class SimpleNoteEditor : Editor
    {
        private const float HandleHeight = 8f;
        private const float MinHeight = 30f;

        public override void OnInspectorGUI()
        {
            serializedObject.Update();

            SerializedProperty noteProp = serializedObject.FindProperty("note");
            SerializedProperty heightProp = serializedObject.FindProperty("noteHeight");

            noteProp.stringValue = EditorGUILayout.TextArea(
                noteProp.stringValue,
                GUILayout.Height(heightProp.floatValue)
            );

            Rect handleRect = GUILayoutUtility.GetRect(
                GUIContent.none, GUIStyle.none,
                GUILayout.Height(HandleHeight), GUILayout.ExpandWidth(true)
            );
            EditorGUIUtility.AddCursorRect(handleRect, MouseCursor.ResizeVertical);

            int controlID = GUIUtility.GetControlID(FocusType.Passive);
            Event e = Event.current;

            switch (e.GetTypeForControl(controlID))
            {
                case EventType.MouseDown:
                    if (handleRect.Contains(e.mousePosition))
                    {
                        GUIUtility.hotControl = controlID;
                        e.Use();
                    }
                    break;

                case EventType.MouseDrag:
                    if (GUIUtility.hotControl == controlID)
                    {
                        heightProp.floatValue = Mathf.Max(MinHeight, heightProp.floatValue + e.delta.y);
                        e.Use();
                        Repaint();
                    }
                    break;

                case EventType.MouseUp:
                    if (GUIUtility.hotControl == controlID)
                    {
                        GUIUtility.hotControl = 0;
                        e.Use();
                    }
                    break;
            }

            serializedObject.ApplyModifiedProperties();
        }
    }
}