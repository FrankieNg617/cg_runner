using System.Collections.Generic;
using UnityEngine;

namespace Script.Utilities
{
    public class ListUtility
    {
        public static List<T> Shuffle<T>(List<T> list)
        {
            for (int i = 0; i < list.Count; i++)
            {
                var temp = list[i];
                int randomIndex = Random.Range(i, list.Count);
                
                list[i] = list[randomIndex];
                list[randomIndex] = temp;
            }

            return list;
        }
    }
}