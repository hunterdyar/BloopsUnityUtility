using System;
using UnityEngine;

/// <summary>
/// Usage is "public class GameManagerTHing : Singleton<GameManagerThing>" and then just reference Instance.
/// It will create itself (lazy load on first get), you dont have to add it to the scene.
/// But you can if you want.
///
/// specific implementation is from Hands-On Game Development Patterns with Unity 2019 by David Baron.
/// </summary>
public class Singleton<T> : MonoBehaviour where T : Component
{
   private static T _instance;
   private static bool _isQuitting;
   public static T Instance
   {
      get
      {
         if (_instance == null)
         {
            _instance = FindObjectOfType<T>();//check for other instances.
            if (_instance == null)
            {
               GameObject obj = new GameObject();
               obj.name = typeof(T).Name;
               _instance = obj.AddComponent<T>();
            }
         }
         return _instance;
      }
   }

   public void Awake()
   {
      if (_instance == null)
      {
         _instance = this as T;
         DontDestroyOnLoad(gameObject);
      }
      else
      {
         Destroy(gameObject);
      }
   }
}
