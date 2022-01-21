using UnityEditor;
using UnityEngine;

namespace PixelCrew.Utils.Editor
{
    [CustomEditor(typeof(ProceduralGeneration))]
    public class CustomButtonForPg : UnityEditor.Editor

    {
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();

            ProceduralGeneration tileAutomata = (ProceduralGeneration) target;
            if (GUILayout.Button("Generate TileMap"))
            {
                tileAutomata.Generate();
            }
        }
    }
}