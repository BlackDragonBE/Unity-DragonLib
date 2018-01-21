using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

/// <summary>
/// Contains all of Dragonlib extension methods
/// </summary>
public static class ExtensionMethods
{
    /// <summary>
    /// Copies the position and rotation of another transform
    /// </summary>
    /// <param name="transformToCopy">transform to copy values from</param>
    public static void CopyPositionAndRotation(this Transform tr, Transform transformToCopy)
    {
        tr.transform.position = transformToCopy.position;
        tr.transform.rotation = transformToCopy.rotation;
    }

    public static void ResetTransform(this Transform tr)
    {
        tr.position = Vector3.zero;
        tr.rotation = Quaternion.identity;
        tr.localScale = Vector3.one;
    }

    public static Transform FindChildByTag(this Transform root, string tag)
    {
        foreach (Transform t in root.GetComponentsInChildren<Transform>())
        {
            if (t != root && t.tag == tag) return t;
        }
        Debug.LogWarning("Tag " + tag + " not found in any children of " + root.name);
        return null;
    }

    public static Transform[] FindChildrenByTag(this Transform parent, string tag)
    {
        return parent.GetComponentsInChildren<Transform>().Where(t => t != parent && t.tag == tag).ToArray();
    }

    /// <summary>
    /// Gets all components in the children with a certain tag
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="gameObject"></param>
    /// <param name="tag"></param>
    /// <returns></returns>
    public static T[] GetComponentsInChildrenWithTag<T>(this GameObject gameObject, string tag)
    where T : Component
    {
        List<T> results = new List<T>();

        if (gameObject.CompareTag(tag))
            results.Add(gameObject.GetComponent<T>());

        foreach (Transform t in gameObject.transform)
            results.AddRange(t.gameObject.GetComponentsInChildrenWithTag<T>(tag));

        return results.ToArray();
    }

    /// <summary>
    /// Gets the components in the parents of the gameobject
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="gameObject"></param>
    /// <returns></returns>
    public static T GetComponentInParents<T>(this GameObject gameObject)
    where T : Component
    {
        for (Transform t = gameObject.transform; t != null; t = t.parent)
        {
            T result = t.GetComponent<T>();

            if (result != null)
                return result;
        }

        return null;
    }

    // Set the layer of this GameObject and all of its children.
    public static void SetLayerRecursively(this GameObject gameObject, int layer)
    {
        gameObject.layer = layer;
        foreach (Transform t in gameObject.transform)
            t.gameObject.SetLayerRecursively(layer);
    }

    /// <summary>
    /// Enables or disables all colliders on an object and its children
    /// </summary>
    /// <param name="gameObject"></param>
    /// <param name="tf"></param>
    public static void SetCollisionRecursively(this GameObject gameObject, bool tf)
    {
        Collider[] colliders = gameObject.GetComponentsInChildren<Collider>();

        foreach (Collider collider in colliders)
            collider.enabled = tf;
    }

    /// <summary>
    /// Enables or disables all renderers on an object and its children
    /// </summary>
    /// <param name="gameObject"></param>
    /// <param name="tf"></param>
    public static void SetVisualRecursively(this GameObject gameObject, bool tf)
    {
        Renderer[] renderers = gameObject.GetComponentsInChildren<Renderer>();

        foreach (Renderer renderer in renderers)
            renderer.enabled = tf;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="color"></param>
    /// <param name="alpha"></param>
    /// <returns>A color with the defined alpha</returns>
    public static Color WithAlpha(this Color color, float alpha)
    {
        return new Color(color.r, color.g, color.b, alpha);
    }

    public static List<GameObject> GetAllChildren(this Transform transform)
    {
        List<GameObject> children = new List<GameObject>();

        foreach (Transform child in transform.transform)
        {
            children.Add(child.gameObject);
        }
        return children;
    }

    public static void DestroyAllChildren(this Transform transform)
    {
        if (transform == null)
        {
            return;
        }

        var children = new List<GameObject>();

        foreach (Transform child in transform)
        {
            children.Add(child.gameObject);
        }

        children.ForEach(child => GameObject.Destroy(child));
    }

    public static void SetX(this Transform transform, float x)
    {
        Vector3 newPosition =
           new Vector3(x, transform.position.y, transform.position.z);

        transform.position = newPosition;
    }

    public static void SetY(this Transform transform, float y)
    {
        Vector3 newPosition =
           new Vector3(transform.position.x, y, transform.position.z);

        transform.position = newPosition;
    }

    public static void SetZ(this Transform transform, float z)
    {
        Vector3 newPosition =
           new Vector3(transform.position.x, transform.position.y, z);

        transform.position = newPosition;
    }

    public static bool IsAlphaNum(this string str)
    {
        if (string.IsNullOrEmpty(str))
            return false;

        for (int i = 0; i < str.Length; i++)
        {
            if (!(char.IsLetter(str[i])) && (!(char.IsNumber(str[i]))))
                return false;
        }

        return true;
    }

    /// <summary>
    /// Formats an int to a string with thousands seperated eg 21000 becoms 21,000
    /// </summary>
    /// <param name="num"></param>
    /// <returns></returns>
    public static string ThousandsSeperated(this int num)
    {
        return string.Format("{0:n0}", num);
    }

    /// <summary>
    /// Formats an float to a string with thousands seperated eg 21000 becoms 21,000
    /// </summary>
    /// <param name="num"></param>
    /// <returns></returns>
    public static string ThousandsSeperated(this float num)
    {
        return string.Format("{0:n0}", num);
    }

    /// <summary>
    /// Formats an float to a string with thousands seperated eg 21000 becoms 21,000
    /// </summary>
    /// <param name="num"></param>
    /// <returns></returns>
    public static string ThousandsSeperated(this double num)
    {
        return string.Format("{0:n0}", num);
    }

    public static Rect RectTransformToScreenSpace(this RectTransform transform)
    {
        Vector2 size = Vector2.Scale(transform.rect.size, transform.lossyScale);
        return new Rect((Vector2)transform.position - (size * 0.5f), size);
    }
}