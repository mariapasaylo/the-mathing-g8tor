using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class GlobalVariables
{
    public static Dictionary<string, int> batteryValues = new Dictionary<string, int>() {
        {"Battery1", 1},
        {"Battery3", 3},
        {"Battery5", 5},
        {"Battery7", 7},
        {"Battery9", 9}
    };

    public static int friendCount; 
    public static int previousFriendCount;

}
