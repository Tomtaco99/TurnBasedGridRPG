using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(Skill))]
public class SkillEditor : Editor
{

    Skill skill;

    Editor componentEditor;

    private void OnEnable()
    {
        skill = target as Skill;
    }

    public override void OnInspectorGUI()
    {

        EditorGUI.BeginChangeCheck();
        EditorGUILayout.LabelField("Description");
        var description = EditorGUILayout.TextArea(skill.description, GUILayout.MinHeight(100));
        if (EditorGUI.EndChangeCheck())
        {
            Undo.RecordObject(skill, "Change Skill Description");
            skill.description = description;
        }

        EditorGUILayout.Space();

        skill.cost = ModuleField("Cost", skill.cost);

        EditorGUILayout.Space();

        skill.targetPolicy = ModuleField("Target", skill.targetPolicy);

        EditorGUILayout.Space();

        skill.castEffect = ModuleField("Cast Effect", skill.castEffect);

        EditorGUILayout.Space();

        skill.hitEffect = ModuleField("Hit Effect", skill.hitEffect);

        EditorGUILayout.Space();

        skill.moveEffect = ModuleField("Move Effect", skill.moveEffect);

        EditorGUILayout.Space();

        skill.healEffect = ModuleField("Heal Effect", skill.healEffect);


        //EditorGUILayout.Space();

        //skill.extraEffects = ModuleField("Hit Effect", skill.extraEffects);
    }

    private T ModuleField<T>(string label, T current) where T : Module
    {
        T result;
        EditorGUI.BeginChangeCheck();
        var module = (T)EditorGUILayout.ObjectField(label, current, typeof(T), false);
        if (EditorGUI.EndChangeCheck())
        {
            if (module != null && module != current)
            {
                if (DestroyExistingModule(current))
                {
                    result = CreateModuleFromTemplate(module);
                }
                else
                {
                    result = current;
                }
            }
            else
            {
                if (DestroyExistingModule(current))
                {
                    result = null;
                }
                else
                {
                    result = current;
                }
            }
        }
        else
        {
            result = current;
        }

        if (result != null)
        {
            CreateCachedEditor(result, null, ref componentEditor);

            GUILayout.BeginVertical(EditorStyles.helpBox);

            componentEditor.OnInspectorGUI();

            GUILayout.EndVertical();
        }

        return result;
    }

    private T CreateModuleFromTemplate<T>(T template) where T : Module
    {
        var clone = Instantiate(template);
        clone.name = clone.name.Replace("(Clone)", "");

        Undo.RegisterCreatedObjectUndo(clone, "Add Component");

        AssetDatabase.AddObjectToAsset(clone, skill);
        AssetDatabase.ImportAsset(AssetDatabase.GetAssetPath(skill));

        EditorUtility.SetDirty(skill);
        return clone;
    }

    private bool DestroyExistingModule(Module module)
    {
        if (module != null)
        {
            if (AssetDatabase.IsSubAsset(module))
            {
                if (EditorUtility.DisplayDialog("Delete Module", "Are you sure you want to remove the existing module? This operation cannot be undone.", "Continue", "Cancel"))
                {
                    DestroyImmediate(module, true);
                    AssetDatabase.ImportAsset(AssetDatabase.GetAssetPath(skill));
                    EditorUtility.SetDirty(skill);
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }
        return true;
    }
}
