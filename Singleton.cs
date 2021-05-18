using System;
using UnityEngine;

/// <summary>
/// Usage is "public class GameManagerThing : Singleton<GameManagerThing>" and then just reference Instance.
/// It will create itself (lazy load on first get, as an otherwise empty object in DontDestroyOnLoad scene), you dont have to add it to the scene.
/// But you can if you want.
///
/// Do you actually want to use a singleton for this? Or is this going to add race conditions! Try to have singletons not need initialization from other gameObjects.
/// If its scene-specific stuff, it probably shouldn't be this kind of singleton.
/// 
/// Specific implementation is from Hands-On Game Development Patterns with Unity 2019 by David Baron.
/// </summary>
public class Singleton<T> : MonoBehaviour where T : Component
{
   private static T _instance;
   public static T Instance
   {
      get
      {
         if (_instance == null)
         {
            _instance = FindObjectOfType<T>();//check for other instances.
            if (_instance == null)
            {
               GameObject obj = new GameObject {name = typeof(T).Name};
               _instance = obj.AddComponent<T>();
            }
         }
         return _instance;
      }
   }

   public virtual void Awake()
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
