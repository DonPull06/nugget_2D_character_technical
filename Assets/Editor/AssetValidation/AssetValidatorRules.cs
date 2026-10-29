using UnityEngine;

[CreateAssetMenu(fileName = "AssetValidatorRules", menuName = "Asset Validator/Rules")]
public class AssetValidatorRules : ScriptableObject
{
    public string texturePattern = "^(SP|UI|T)_[A-Z][A-Za-z0-9]*$";
    public int maxTextureSize = 2048;
    public bool allowReadWrite = false;
    public bool allowMipmaps = false;
    public bool requireCompression = true;
    public float expectedPixelsPerUnit = 200f;
}