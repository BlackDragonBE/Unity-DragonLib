using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Contains some handy static methods to replace the Unity yield instructions with.
/// These will create a minimal amount GC alloc, thus preventing stuttering due to the GC.
///
/// Example Usage:
/// yield return YieldNoGC.WaitSeconds(3f);
/// </summary>
public static class YieldNoGC
{
    private static Dictionary<float, WaitForSeconds> _waitTimeIntervals = new Dictionary<float, WaitForSeconds>(100);

    private static WaitForEndOfFrame _endOfFrame = new WaitForEndOfFrame();
    private static WaitForFixedUpdate _fixedUpdate = new WaitForFixedUpdate();

    public static WaitForEndOfFrame EndOfFrame
    {
        get { return _endOfFrame; }
    }

    public static WaitForFixedUpdate FixedUpdate
    {
        get { return _fixedUpdate; }
    }

    public static WaitForSeconds WaitSeconds(float seconds)
    {
        if (!_waitTimeIntervals.ContainsKey(seconds))
        {
            _waitTimeIntervals.Add(seconds, new WaitForSeconds(seconds));
        }

        return _waitTimeIntervals[seconds];
    }
}