using UnityEditor;
using UnityEngine;
using Combat.AI;
using System.Collections.Generic;
using System.Collections;

[CustomEditor(typeof(ReactiveRunner))]
public class ReactiveRunnerEditor : Editor
{
    private void OnEnable()
    {
    }


    public override void OnInspectorGUI()
    {
        // Update the serialized object
        serializedObject.Update();

        // Display the default Inspector GUI for the script
        DrawDefaultInspector();

        // Custom Editor Field for 'first' property
        EditorGUILayout.Space();

        EditorGUILayout.LabelField("Tree", EditorStyles.selectionRect);
        var t = (ReactiveRunner)target;
        EditorGUI.indentLevel++;
        ReactiveBase rbase = t.Next();
        HashSet<Object> unique = new HashSet<Object>();

        Show("0", rbase, unique);
        

        EditorGUI.indentLevel--;



        // Apply changes to the serialized object
        serializedObject.ApplyModifiedProperties();
    }

    private void Show(string id, ReactiveBase rbase, HashSet<Object> unique)
    {
        if (unique.Contains(rbase))
        {
            GUI.color = Color.black;
            EditorGUILayout.LabelField("\t\t BREAK INF" + rbase.name + " " + rbase.GetType(), EditorStyles.selectionRect);
            return;
        }
        id += ".";
        EditorGUILayout.LabelField(id + "\t" + rbase.name + " " + rbase.GetType(), EditorStyles.selectionRect);
        unique.Add(rbase);

        for (int i = 0; i < rbase.Count; i++)
        {
            var atI = rbase.Next(i);
            if (atI == null) continue;

            if (unique.Contains(atI))
            {
                GUI.color = Color.red;
                EditorGUILayout.LabelField(id+i + "\t" + "INFINITE " + atI.name + " " + atI.GetType(), EditorStyles.helpBox);
            }
            Show(id+i.ToString(), atI, unique);
        }
    }
}
