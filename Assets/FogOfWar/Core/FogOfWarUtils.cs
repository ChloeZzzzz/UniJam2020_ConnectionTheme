﻿using UnityEngine;

namespace FoW
{
    public static class FogOfWarUtils
    {
        // Returns clockwise angle [-180-180]
        public static float ClockwiseAngle(Vector2 from, Vector2 to)
        {
            float angle = Vector2.Angle(from, to);
            if (Vector2.Dot(from, new Vector2(to.y, to.x)) < 0.0f)
                angle = -angle;
            return angle;
        }

        public static void Swap<T>(ref T a, ref T b)
        {
            T temp = a;
            a = b;
            b = temp;
        }

        public static void SetKeywordEnabled(this Material material, string keyword, bool enable)
        {
            if (enable)
                material.EnableKeyword(keyword);
            else
                material.DisableKeyword(keyword);
        }

        public static Vector2Int ToInt(this Vector2 vec)
        {
            return new Vector2Int((int)vec.x, (int)vec.y);
        }

        public static Vector2 ToFloat(this Vector2Int vec)
        {
            return new Vector2(vec.x, vec.y);
        }

        public static Shader FindShader(string name)
        {
            Shader shader = Shader.Find(name);
            if (shader == null)
                Debug.LogError("Fog Of War: Failed to find shader: " + name);
            else if (!shader.isSupported)
                Debug.LogError("Fog Of War: The following shader is not supported: " + name);

            AddAlwaysIncludedShader(shader);
            return shader;
        }

        public static void AddAlwaysIncludedShader(string shaderName)
        {
#if UNITY_EDITOR
            Shader shader = Shader.Find(shaderName);
            if (shader != null)
                AddAlwaysIncludedShader(shader);
#endif
        }

        public static void AddAlwaysIncludedShader(Shader shader)
        {
#if UNITY_EDITOR
            var graphicsSettingsObj = UnityEditor.AssetDatabase.LoadAssetAtPath<UnityEngine.Rendering.GraphicsSettings>("ProjectSettings/GraphicsSettings.asset");
            var serializedObject = new UnityEditor.SerializedObject(graphicsSettingsObj);
            var arrayProp = serializedObject.FindProperty("m_AlwaysIncludedShaders");
            bool hasShader = false;
            for (int i = 0; i < arrayProp.arraySize; ++i)
            {
                var arrayElem = arrayProp.GetArrayElementAtIndex(i);
                if (shader == arrayElem.objectReferenceValue)
                {
                    hasShader = true;
                    break;
                }
            }

            if (!hasShader)
            {
                int arrayIndex = arrayProp.arraySize;
                arrayProp.InsertArrayElementAtIndex(arrayIndex);
                var arrayElem = arrayProp.GetArrayElementAtIndex(arrayIndex);
                arrayElem.objectReferenceValue = shader;

                serializedObject.ApplyModifiedProperties();

                UnityEditor.AssetDatabase.SaveAssets();
            }
#endif
        }
    }
}