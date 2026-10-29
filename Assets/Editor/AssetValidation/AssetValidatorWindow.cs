using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using UnityEditor;
using UnityEngine;

public enum Severity { Info, Warning, Error }

public class ValidationIssue
{
    public string assetPath;
    public string rule;
    public string message;
    public Severity severity;
}

public class AssetValidatorWindow : EditorWindow
{
    AssetValidatorRules rules;
    DefaultAsset folder;
    readonly List<ValidationIssue> issues = new List<ValidationIssue>();
    Vector2 scroll;
    int scanned;

    [MenuItem("Tools/Asset Validator")]
    static void Open()
    {
        GetWindow<AssetValidatorWindow>("Asset Validator");
    }

    void OnGUI()
    {
        rules = (AssetValidatorRules)EditorGUILayout.ObjectField("Rules", rules, typeof(AssetValidatorRules), false);
        folder = (DefaultAsset)EditorGUILayout.ObjectField("Folder to scan", folder, typeof(DefaultAsset), false);

        using (new EditorGUI.DisabledScope(rules == null || folder == null))
        {
            if (GUILayout.Button("Scan", GUILayout.Height(26))) Scan();
        }

        if (scanned > 0)
        {
            int errors = issues.FindAll(i => i.severity == Severity.Error).Count;
            int warnings = issues.FindAll(i => i.severity == Severity.Warning).Count;
            MessageType type = errors > 0 ? MessageType.Error : warnings > 0 ? MessageType.Warning : MessageType.Info;
            EditorGUILayout.HelpBox($"{scanned} textures scanned | {errors} errors | {warnings} warnings", type);

            if (GUILayout.Button("Export report (Markdown)")) ExportMarkdown();
        }

        scroll = EditorGUILayout.BeginScrollView(scroll);
        foreach (var issue in issues)
        {
            MessageType t = issue.severity == Severity.Error ? MessageType.Error
                          : issue.severity == Severity.Warning ? MessageType.Warning : MessageType.Info;
            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.HelpBox($"{issue.rule}: {issue.message}\n{issue.assetPath}", t);
            if (GUILayout.Button("Select", GUILayout.Width(55), GUILayout.Height(40)))
            {
                var obj = AssetDatabase.LoadAssetAtPath<Object>(issue.assetPath);
                Selection.activeObject = obj;
                EditorGUIUtility.PingObject(obj);
            }
            EditorGUILayout.EndHorizontal();
        }
        EditorGUILayout.EndScrollView();
    }

    void Scan()
    {
        issues.Clear();
        string folderPath = AssetDatabase.GetAssetPath(folder);
        string[] guids = AssetDatabase.FindAssets("t:Texture2D", new[] { folderPath });
        scanned = guids.Length;

        foreach (string guid in guids)
        {
            string path = AssetDatabase.GUIDToAssetPath(guid);
            string fileName = Path.GetFileNameWithoutExtension(path);

            if (!Regex.IsMatch(fileName, rules.texturePattern))
                Add(path, "Naming", $"'{fileName}' no cumple {rules.texturePattern}", Severity.Error);

            var importer = AssetImporter.GetAtPath(path) as TextureImporter;
            if (importer == null)
            {
                Add(path, "Importer", $"Importer: {AssetImporter.GetAtPath(path).GetType().Name}; se valida el asset ya importado", Severity.Info);
                ValidateImportedAsset(path);
                continue;
            }

            if (importer.maxTextureSize > rules.maxTextureSize)
                Add(path, "Max Size", $"{importer.maxTextureSize} > {rules.maxTextureSize}", Severity.Error);

            if (importer.isReadable && !rules.allowReadWrite)
                Add(path, "Read/Write", "Read/Write activado", Severity.Warning);

            if (importer.mipmapEnabled && !rules.allowMipmaps)
                Add(path, "Mipmaps", "Generate Mip Maps activado", Severity.Warning);

            if (rules.requireCompression && importer.textureCompression == TextureImporterCompression.Uncompressed)
                Add(path, "Compression", "Textura sin comprimir", Severity.Warning);

            if (importer.textureType == TextureImporterType.Sprite &&
                !Mathf.Approximately(importer.spritePixelsPerUnit, rules.expectedPixelsPerUnit))
                Add(path, "PPU", $"{importer.spritePixelsPerUnit} (esperado {rules.expectedPixelsPerUnit})", Severity.Warning);
        }

        issues.Sort((a, b) => b.severity.CompareTo(a.severity));
    }

    void ValidateImportedAsset(string path)
    {
        var tex = AssetDatabase.LoadAssetAtPath<Texture2D>(path);
        if (tex == null) return;

        if (Mathf.Max(tex.width, tex.height) > rules.maxTextureSize)
            Add(path, "Max Size", $"{tex.width}x{tex.height} > {rules.maxTextureSize}", Severity.Error);

        if (tex.isReadable && !rules.allowReadWrite)
            Add(path, "Read/Write", "Read/Write activado", Severity.Warning);

        if (tex.mipmapCount > 1 && !rules.allowMipmaps)
            Add(path, "Mipmaps", "La textura tiene mipmaps", Severity.Warning);

        int badPpu = 0;
        float firstBadValue = 0;
        string firstBadName = null;
        foreach (var obj in AssetDatabase.LoadAllAssetRepresentationsAtPath(path))
        {
            if (obj is Sprite sprite && !Mathf.Approximately(sprite.pixelsPerUnit, rules.expectedPixelsPerUnit))
            {
                if (badPpu == 0)
                {
                    firstBadValue = sprite.pixelsPerUnit;
                    firstBadName = sprite.name;
                }
                badPpu++;
            }
        }

        if (badPpu > 0)
            Add(path, "PPU", $"{badPpu} sprites con PPU distinto de {rules.expectedPixelsPerUnit} (p. ej. '{firstBadName}': {firstBadValue})", Severity.Warning);
    }  

    void Add(string path, string rule, string message, Severity severity)
    {
        issues.Add(new ValidationIssue { assetPath = path, rule = rule, message = message, severity = severity });
    }

    void ExportMarkdown()
    {
        string target = EditorUtility.SaveFilePanel("Save report", "", "AssetValidationReport.md", "md");
        if (string.IsNullOrEmpty(target)) return;

        var sb = new StringBuilder();
        sb.AppendLine("# Asset Validation Report");
        sb.AppendLine($"Folder: `{AssetDatabase.GetAssetPath(folder)}`  ");
        sb.AppendLine($"Textures scanned: {scanned}  ");
        sb.AppendLine($"Issues: {issues.Count}");
        sb.AppendLine();
        sb.AppendLine("| Severity | Rule | Asset | Detail |");
        sb.AppendLine("|---|---|---|---|");
        foreach (var i in issues)
            sb.AppendLine($"| {i.severity} | {i.rule} | `{i.assetPath}` | {i.message} |");

        File.WriteAllText(target, sb.ToString());
        EditorUtility.RevealInFinder(target);
    }
}