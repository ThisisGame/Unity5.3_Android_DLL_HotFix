using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.IO;

public class CopyAssembly : MonoBehaviour
{
    [SerializeField]
    Text mText;


    private AndroidJavaClass _helper = null;

    // Use this for initialization
    void Start ()
    {
        mText.text = "1";

        _helper = new AndroidJavaClass("com.cytx.tools.helper");
        using (AndroidJavaClass jc = new AndroidJavaClass("com.unity3d.player.UnityPlayer"))
        {
            object jo = jc.GetStatic<AndroidJavaObject>("currentActivity");
            _helper.CallStatic("init", jo);
        }
    }


    void OnGUI()
    {
        if(GUI.Button(new Rect(100,100,100,100),"CopyAssembly"))
        {
            StartCoroutine(CopyAssemblyDLL());
        }
    }

    IEnumerator CopyAssemblyDLL()
    {
        yield return 0;
        WWW tmpWWW = new WWW("jar:file://" + Application.dataPath + "!/assets/Assembly-CSharp.dll");
        yield return tmpWWW;

        string datapath = Application.dataPath;
        int start = datapath.IndexOf("com.");
        int end = datapath.IndexOf("-");
        string packagename = datapath.Substring(start, end - start);
        string path = "/data/data/" + packagename + "/files/";
        string dllpath = path + "Assembly-CSharp.dll";
        if (File.Exists(dllpath))
        {
            Debug.Log("File Exists");
        }
        else
        {
            if(Directory.Exists(path)==false)
            {
                Directory.CreateDirectory(path);
            }
            using (FileStream fs = new FileStream(dllpath, FileMode.CreateNew))
            {
                fs.Write(tmpWWW.bytes,0,tmpWWW.bytes.Length);
                fs.Flush();
                fs.Close();
            }
        }

        yield return new WaitForEndOfFrame();

        restartApplication();
    }

    void restartApplication()
    {
        Debug.Log("restartApplication0");
        _helper.CallStatic("restartApplication");
        Debug.Log("restartApplication2");
    }

    // Update is called once per frame
    void Update () {
	
	}
}
