using UnityEditor;
using UnityEngine;
using System.Collections;

[ExecuteInEditMode]
public class MoveWithArrowKeys : MonoBehaviour
{
    //References

    //Public
    public float AmountToMove = 1f;
    public static Transform staticTransform;
    public static float staticAmountToMove;

    //Private

#if UNITY_EDITOR
    void OnRenderObject()
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
    static void MoveLeft()
    {
        if (staticTransform)
        staticTransform.Translate(Vector3.left * staticAmountToMove);
    }

    [MenuItem("Edit/Move/Right %UP")]
    static void MoveUp()
    {
        if (staticTransform)
        staticTransform.Translate(Vector3.up * staticAmountToMove);
    }

    [MenuItem("Edit/Move/Right %DOWN")]
    static void MoveDown()
    {
        if (staticTransform)
        staticTransform.Translate(Vector3.down * staticAmountToMove);
    }
#endif
}
