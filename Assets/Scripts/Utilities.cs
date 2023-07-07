using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Utilities
{
   public static bool RandomBool()
   {
      var randVal = Random.Range(0f, 1f);
      return randVal >= 0.5f;
   }
}
