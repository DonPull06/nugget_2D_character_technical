using UnityEditor;
using UnityEngine;
using System.Linq;

public static class PrintSpriteNames
{
    [MenuItem("Tools/Print Sprite Names")]
    public static void Print()
    {
        foreach (var selected in Selection.objects)
        {
            string path = AssetDatabase.GetAssetPath(selected);
            var sprites = AssetDatabase.LoadAllAssetRepresentationsAtPath(path)
                .OfType<Sprite>()
                .OrderBy(s => s.name)
                .ToList();

            Debug.Log($"{path}: {sprites.Count} sprites");
            foreach (var s in sprites)
                Debug.Log($"  Sprite '{s.name}' length={s.name.Length} ppu={s.pixelsPerUnit}");
        }
    }
}