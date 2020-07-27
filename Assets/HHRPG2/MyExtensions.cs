using Funfia.File;
using SoftStar.Pal6;
using System;
using System.IO;
using UnityEngine;

public static class MyExtensions
{
    private static string[] s_separators = new string[]
    {
        "SceneResources/",
        "CutsceneResources/",
        "SEObjects/LDF/",
        "Pal_Resources/Effects/",
        "Pal_Resources/Character/PaperDoll/",
        "AssetBundles/",
        "ResourcesTemp/",
        "Pal_Resources/",
        "Resources/"
    };

    public static UnityEngine.Object MainAsset5(this AssetBundle bundle)
    {
        if (bundle == null)
        {
            return null;
        }
        if (bundle.mainAsset == null)
        {
            return bundle.LoadAsset(bundle.GetAllAssetNames()[0]);
        }
        return bundle.mainAsset;
    }

    public static T GetOrAddComponent<T>(this GameObject obj) where T : Component
    {
        T t = obj.GetComponent<T>();
        if (t == null)
        {
            t = obj.AddComponent<T>();
        }
        return t;
    }

    public static bool ExistFile(this string path)
    {
        return File.Exists(path);
    }

    public static void ChangeLayersRecursively(this Transform trans, string name)
    {
        trans.gameObject.layer = LayerMask.NameToLayer(name);
        foreach (Transform trans2 in trans)
        {
            trans2.ChangeLayersRecursively(name);
        }
    }

    private static string RemovePathPrefix(string path)
    {
        string[] array = null;
        string[] array2 = MyExtensions.s_separators;
        for (int i = 0; i < array2.Length; i++)
        {
            string text = array2[i];
            if (path.Contains(text))
            {
                array = new string[]
                {
                    text
                };
                break;
            }
        }
        string text2;
        if (array == null)
        {
            text2 = path;
        }
        else
        {
            string[] array3 = path.Split(array, StringSplitOptions.None);
            text2 = array3[1];
        }
        if (path.Contains("Pal_Scenes"))
        {
            text2 = "EntityLayers/" + text2;
        }
        text2 = text2.Replace("：", "_");
        return text2.Replace("，", "_");
    }

    private static string RemovePathPrefixAndExtension(string path)
    {
        string text = MyExtensions.RemovePathPrefix(path);
        return text.Split(new char[]
        {
            '.'
        })[0];
    }

    public static string ToDataFolder(this string path)
    {
        if (path == string.Empty || path == null)
        {
            System.Console.WriteLine("Utilities.ToDataPath: ToDataPath path == null");
            return null;
        }
        if (path.StartsWith(FileLoader.DataPath))
        {
            System.Console.WriteLine("Utilities.ToDataPath: path is already a data path, path = " + path);
            return path;
        }
        string path2 = MyExtensions.RemovePathPrefix(path);
        return Path.Combine(FileLoader.DataPath, path2);
    }

    public static string ToDataPath(this string path)
    {
        return path.ToDataFolder() + ".unity3d";
    }

    public static string ToAssetBundleFolder(this string path)
    {
        if (path == string.Empty || path == null)
        {
            System.Console.WriteLine("Utilities.ToAssetBundlePath: ToAssetBundlePath path == null");
            return null;
        }
        if (path.StartsWith(FileLoader.AssetBundlePath))
        {
            System.Console.WriteLine("Utilities.ToAssetBundlePath: path is already a data path, path = " + path);
            return path;
        }
        string path2 = MyExtensions.RemovePathPrefixAndExtension(path);
        return Path.Combine(FileLoader.AssetBundlePath, path2);
    }

    public static string ToAssetBundlePath(this string path)
    {
        Debug.Log(path.ToAssetBundleFolder() + ".unity3d");
        return path.ToAssetBundleFolder() + ".unity3d";
    }

    public static string ToAnimationFolder(this string path)
    {
        if (path == string.Empty || path == null)
        {
            System.Console.WriteLine("Utilities.ToAnimationPath: ToAnimationPath path == null");
            return null;
        }
        if (path.StartsWith(FileLoader.AnimationPath))
        {
            System.Console.WriteLine("Utilities.ToAnimationPath: path is already a data path, path = " + path);
            return path;
        }
        string path2 = MyExtensions.RemovePathPrefixAndExtension(path);
        string fileName = Path.GetFileName(path2);
        string directoryName = Path.GetDirectoryName(path2);
        int startIndex = directoryName.LastIndexOf('/') + 1;
        string path3 = directoryName.Substring(startIndex);
        return Path.Combine(FileLoader.AnimationPath, Path.Combine(path3, fileName));
    }

    public static string ToAnimationPath(this string path)
    {
        return path.ToAnimationFolder() + ".unity3d";
    }

    public static string ToLanguageFolder(this string path)
    {
        if (path == string.Empty || path == null)
        {
            System.Console.WriteLine("Utilities.ToLanguagePath: ToLanguagePath path == null");
            return null;
        }
        if (path.StartsWith(FileLoader.LanguagePath))
        {
            System.Console.WriteLine("Utilities.ToLanguagePath: path is already a data path, path = " + path);
            return path;
        }
        string path2 = MyExtensions.RemovePathPrefixAndExtension(path);
        return Path.Combine(FileLoader.LanguagePath, path2);
    }

    public static string ToLanguagePath(this string path)
    {
        return path.ToLanguageFolder() + ".unity3d";
    }

    //public static PalBattleManager.PLAYER_AI_TACTICAL NextTactic(this PalBattleManager.PLAYER_AI_TACTICAL tactic)
    //{
    //    int num = (int)((tactic + 1) % PalBattleManager.PLAYER_AI_TACTICAL.MAX_TACTICAL);
    //    return (PalBattleManager.PLAYER_AI_TACTICAL)num;
    //}
}
