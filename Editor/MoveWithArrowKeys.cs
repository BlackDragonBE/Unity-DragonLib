using UnityEditor;
using UnityEngine;

/// <summary>
/// Allows movement of GameObjects in the editor using the arrow keys
/// </summary>
[ExecuteInEditMode]
public class MoveWithArrowKeys : MonoBehaviour
{
    public float AmountToMove = 1f;

    public static Transform staticTransform;
    public static float staticAmountToMove;

#if UNITY_EDITOR

    private void OnRenderObject()
    {
        if (!Application.isPlaying && Selection.activeGameObject != null)
        {
            staticTransform = Selection.activeGameObject.transform;
        }
        else
        {
            staticTransform = null;
        }
        staticAmountToMove = AmountToMove;
    }

    [MenuItem("Edit/Move/Right %RIGHT")]
    private static void MoveRight()
    {
        if (staticTransform)
            staticTransform.Translate(Vector3.right * staticAmountToMove);
    }

    [MenuItem("Edit/Move/Right %LEFT")]
    private static void MoveLeft()
    {
        if (staticTransform)
            staticTransform.Translate(Vector3.left * staticAmountToMove);
    }

    [MenuItem("Edit/Move/Right %UP")]
    private static void MoveUp()
    {
        if (staticTransform)
            staticTransform.Translate(Vector3.up * staticAmountToMove);
    }

    [MenuItem("Edit/Move/Right %DOWN")]
    private static void MoveDown()
    {
        if (staticTransform)
            staticTransform.Translate(Vector3.down * staticAmountToMove);
    }

#endif
}