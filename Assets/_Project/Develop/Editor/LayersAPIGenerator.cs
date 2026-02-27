using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using UnityEditor;
using UnityEngine;

namespace _Project.Develop.Editor
{
    public class LayersAPIGenerator
    {
        private const string AssemblyName = "Assembly-CSharp";

        private static string OutputPath
            => Path.Combine(Application.dataPath,
                "_Project/Develop/Runtime/Utilities/Generated/LayersAPI.cs");

        [InitializeOnLoadMethod]
        [MenuItem("Tools/GenerateLayersAPI")]
        private static void Generate()
        {
            StringBuilder sb = new StringBuilder();

            sb.AppendLine("namespace _Project.Develop.Runtime.Utilities.Generated");
            sb.AppendLine("{");

            sb.AppendLine("\tusing UnityEngine;");
            
            sb.AppendLine();
            
            sb.AppendLine($"\tpublic static class UnityLayers");
            sb.AppendLine("\t{");

            List<string> layerNames = GetAllLayerNames();

            foreach (string layerName in layerNames)
            {
                string prefix = "Layer";
                sb.AppendLine(
                    $"\t\tpublic static readonly int {prefix}{layerName.Replace(" ", string.Empty)} = LayerMask.NameToLayer(\"{layerName}\");");
            }

            sb.AppendLine();
            
            foreach (string layerName in layerNames)
            {
                string prefix = "LayerMask";
                sb.AppendLine(
                    $"\t\tpublic static readonly int {prefix}{layerName.Replace(" ", string.Empty)} = 1 << Layer{layerName.Replace(" ", string.Empty)};");
            }

            sb.AppendLine("\t}");
            sb.AppendLine("}");
            
            File.WriteAllText(OutputPath, sb.ToString());

            AssetDatabase.Refresh();
            AssetDatabase.SaveAssets();
        }

        private static List<string> GetAllLayerNames()
        {
            return Enumerable.Range(0, 32)
                .Select(LayerMask.LayerToName)
                .Where(name => !string.IsNullOrEmpty(name))
                .ToList();
        }
    }
}