# BloopsUnityUtility
Various helpful things I like to have around in Unity projects.

## Singleton
A robust Singleton system. Turn objects into singletons by just changing their class definition from "class ManagerThing : Monobehaviour" to "class ManagerThing : Singleton<ManagerThing>".
It will lazily load itself on first ManagerThing.Instance get, putting an empty gameObject in the dontDestroyOnLoad scene for you (if needed).

## Extensions
Vector2.ClampToCameraScreen
Vector3.ClampToCameraScreen
Vector2.GetSnap4
Vector2.Snap4
Vector2.Snap8
Vector2.xy (v3->v2)
IList<T>.Shuffle (fisher-yates)
IList.RandomItem

## RequireInterfaceAttribute
[RequireInterface(typeof(type))] public ScriptableObject something;

## Event Extensions
Good ol fashion pre-defined list of like "UnityEventBool : UnityEvent<bool>" for lots of basic/unity types.

## ColorReference
If I want to use a full ScriptableObjectArchitecture, I will. But if I'm not using that, i most often just need a colorReference for non-gameplay things like UI.

## Camera Utility
CameraUtility.RectFromTransforms will take a list of transforms and give you a rect of the min and max positions.
CameraUtility.SetCameraToRect will adjust an orthographic camera to fit a rect. (note: currently broken oops.)

## Camera Transition
goes on camera. Needs the material/shaders too. Just call TransitionOpenCurtain TransitionCloseCurtain, optionally passing a delegate in to be fired when the transition is complete.

It creates a 'blit' component that runs 'blit' onRenderFrame and does its transition through the lerp, the shader code determining what that looks like.

## CameraOverlayMat
You can manually add a 'blit' component to the camera and put the overlay material in the slot. This material takes a texture and uses the overlay blend mode. Its a post-processing effect, basically.
If you need a more sophisticated post-processing effect than this, please use something else. In fact, a future goal is to simply write an overlay function as a custom PostProc for Unitys package. Let them optimize it.

