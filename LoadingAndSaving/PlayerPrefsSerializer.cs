using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public class PlayerPrefsSerializer
{
    private static BinaryFormatter bf = new BinaryFormatter();

    public static void Save(string prefKey, object serializableObject)
    {
        MemoryStream memoryStream = new MemoryStream();

        bf.Serialize(memoryStream, serializableObject);

        string tmp = System.Convert.ToBase64String(memoryStream.ToArray());

        PlayerPrefs.SetString(prefKey, tmp);
    }

    public static object Load(string prefKey)
    {
        string tmp = PlayerPrefs.GetString(prefKey, string.Empty);

        if (tmp == string.Empty)
            return null;

        MemoryStream memoryStream = new MemoryStream(System.Convert.FromBase64String(tmp));

        return bf.Deserialize(memoryStream);
    }

    public static object Load<T>(string prefKey)
    {
        if (!PlayerPrefs.HasKey(prefKey))
            return default(T);

        string serializedData = PlayerPrefs.GetString(prefKey);
        MemoryStream dataStream = new MemoryStream(System.Convert.FromBase64String(serializedData));
        T deserializedObject = (T)bf.Deserialize(dataStream);

        return deserializedObject;
    }
}