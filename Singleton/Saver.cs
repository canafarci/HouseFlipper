using UnityEngine;

public class Saver : MonoBehaviour {

    public  void SaveInt(string key, int value) => PlayerPrefs.SetInt(key, value); 
    public void SaveFloat(string key, float value) =>  PlayerPrefs.SetFloat(key, value);
    public void SaveString(string key, string value) => PlayerPrefs.SetString(key, value);
    public bool HasKey(string key) =>  PlayerPrefs.HasKey(key);
    public string GetString(string key) => PlayerPrefs.GetString(key); 
    public int GetInt(string key) => PlayerPrefs.GetInt(key);
    public float GetFloat(string key) => PlayerPrefs.GetFloat(key);   
}