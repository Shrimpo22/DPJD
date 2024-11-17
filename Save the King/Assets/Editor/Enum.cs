using System;
using System.Linq;
using System.IO;
using System.Reflection;
using UnityEngine;
using UnityEditor;

public static class EnumGenerator
{
    [MenuItem("Tools/Generate Item Enum")]
    public static void GenerateItemEnum()
    {
        // Step 1: Get all classes implementing the Item interface
        var itemTypes = AppDomain.CurrentDomain.GetAssemblies()
            .SelectMany(assembly => assembly.GetTypes())
            .Where(type => typeof(Item).IsAssignableFrom(type) && !type.IsInterface && !type.IsAbstract)
            .Select(type => type.Name)
            .ToList();

        // Step 2: Define the enum name and output path
        string enumName = "ItemType"; // Change the name of the enum if needed
        string filePath = Path.Combine(Application.dataPath, "Scripts/Generated", $"{enumName}.cs");

        // Ensure the directory exists
        Directory.CreateDirectory(Path.GetDirectoryName(filePath));

        // Step 3: Read existing enums from file if it exists
        var existingEnums = new System.Collections.Generic.List<string>();

        if (File.Exists(filePath))
        {
            var lines = File.ReadAllLines(filePath);
            foreach (var line in lines)
            {
                // Extract enum names (lines like "    EnumName,")
                var trimmedLine = line.Trim();
                if (trimmedLine.EndsWith(","))
                {
                    existingEnums.Add(trimmedLine.TrimEnd(','));
                }
            }
        }

        // Combine existing enums with new ones while maintaining order
        var combinedEnums = existingEnums.Union(itemTypes).ToList();

        // Step 4: Write the updated enum file
        using (var writer = new StreamWriter(filePath))
        {
            writer.WriteLine("// This file is auto-generated. Do not edit manually.");
            writer.WriteLine($"public enum {enumName}");
            writer.WriteLine("{");

            foreach (var item in combinedEnums)
            {
                writer.WriteLine($"    {item},");
            }

            writer.WriteLine("}");
        }

        Debug.Log($"Enum {enumName} updated with {combinedEnums.Count} entries at {filePath}");

        // Refresh the AssetDatabase to ensure Unity sees the updated file
        AssetDatabase.Refresh();
    }
}
