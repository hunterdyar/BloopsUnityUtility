using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Extensions
{
   private static System.Random rng = new System.Random();  

   //
   
   //FLOAT extensions
   static bool IsHopefully(this float a, float b)
   {
      return Mathf.Approximately(a,b);
   }
   
   
   // VECTOR extensions
   public static Vector3 ClampedToCameraScreen(this Vector3 input, Camera camera)
   {
      Vector3 bottomLeft = camera.ScreenToWorldPoint(Vector3.zero);
      Vector3 topRight = camera.ScreenToWorldPoint(new Vector3(camera.pixelWidth, camera.pixelHeight, 0));
      return new Vector3(Mathf.Clamp(input.x, bottomLeft.x, topRight.x), Mathf.Clamp(input.y, bottomLeft.y, topRight.y),Mathf.Max(input.z,camera.transform.position.z));
   }
   public static Vector2 ClampedToCameraScreen(this Vector2 input, Camera camera)
   {
      Vector3 bottomLeft = camera.ScreenToWorldPoint(Vector3.zero);
      Vector3 topRight = camera.ScreenToWorldPoint(new Vector3(camera.pixelWidth, camera.pixelHeight, 0));
      return new Vector2(Mathf.Clamp(input.x, bottomLeft.x, topRight.x), Mathf.Clamp(input.y, bottomLeft.y, topRight.y));
   }
	
   /// <summary>
   /// Takes a direction and returns a vector that has been normalized and snapped to one of the 4 cardinal directions. 
   /// </summary>
   /// <param name="input">Direction</param>
   /// <returns></returns>
   public static Vector2 GetSnap4(this Vector2 input)
   {
      if (input == Vector2.zero)
      {
         return Vector2.zero;
      }
      if(Mathf.Abs(input.x) >= Mathf.Abs(input.y))
      {
         return new Vector2(Mathf.Sign(input.x),0);
      }
      if(Mathf.Abs(input.x) < Mathf.Abs(input.y))
      {
         return new Vector2(0,Mathf.Sign(input.y));
      }
      return Vector2.zero;
   }
   /// <summary>
   /// Normalizes a vector and snaps to one of the 4 cardinal directions. 
   /// </summary>
   public static void Snap4(this Vector2 input)
   {
      input = input.GetSnap4();
   }
   public static Vector2 Snap8(this Vector2 input)
   {
      input.Normalize();//needed for our rounding trick to be effective.
      return new Vector2(Mathf.Round(input.x),Mathf.Round(input.y)).normalized;
   }

   /// <summary>V3->V2 instead of explicit casting. drops z.</summary>
   public static Vector2 xy(this Vector3 v) {
      return new Vector2(v.x, v.y);
   }
   
   /// <summary>
   /// Shuffle the list using the Fisher-Yates method.
   /// </summary>
   public static void Shuffle<T>(this IList<T> list)
   {
      System.Random rng = new System.Random();
      int n = list.Count;
      while (n > 1)
      {
         n--;
         int k = rng.Next(n + 1);
         T value = list[k];
         list[k] = list[n];
         list[n] = value;
      }
   }
   public static T RandomItem<T>(this IList<T> list)
   {
      if (list.Count == 0)
      {
         throw new System.IndexOutOfRangeException("Can't select a random item from an empty list");
      }
      return list[UnityEngine.Random.Range(0, list.Count)];
   }

   public static void DestroyChildren(this Transform transform)
   {
      foreach (Transform child in transform)
      {
         Object.Destroy(child.transform.gameObject);
      }
   }

}
