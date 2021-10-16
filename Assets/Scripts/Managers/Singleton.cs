using UnityEngine;

public class Singleton<T> : MonoBehaviour where T : Component
{
    private static T _instance;
    public static T instance{
        get{
            if(_instance == null){
                GameObject obj = new GameObject();
                obj.name = typeof(T).Name;
                obj.hideFlags = HideFlags.HideAndDontSave;
                _instance = obj.AddComponent<T>();
            }
            return _instance;
        }
    }
    private void OnDestroy(){
            if(_instance==this){
                _instance = null;
            }
        }
}

public class SingletonPersistent<T> : MonoBehaviour where T : Component
{
    private static T _instance;
    public static T instance{
        get{
            // if(_instance == null){
            //     GameObject obj = new GameObject();
            //     obj.name = typeof(T).Name;
            //     obj.hideFlags = HideFlags.HideAndDontSave;
            //     _instance = obj.AddComponent<T>();
                
            // }   
            return _instance;
        }
    }
    private void OnDestroy(){
            if(_instance==this){
                _instance = null;
            }
        }
    
    protected virtual void Awake(){
        if(_instance == null){
            _instance = this as T; 
            DontDestroyOnLoad(gameObject);
        }
        else{
            Destroy(this);
        }
    }
}
