/** 
 * 文件名:BuildPostprocessor.cs 
 * Des:在导出Eclipse工程之后对替换mono.so
 * Author:Captain 
 * **/


using UnityEngine;
using UnityEditor;
using UnityEditor.Callbacks;
using System.IO;

public class BuildPostprocessor
{
    [PostProcessBuildAttribute(1)]
    public static void OnPostprocessBuild(BuildTarget target, string pathToBuiltProject)
    {
        if (target == BuildTarget.Android && (!pathToBuiltProject.EndsWith(".apk")))
        {
            Debug.Log("target: " + target.ToString());
            Debug.Log("pathToBuiltProject: " + pathToBuiltProject);
            Debug.Log("productName: " + PlayerSettings.productName);

            Debug.Log("Current is : " + EditorUserBuildSettings.development.ToString());

            //替换 libmono.so;  
            if (EditorUserBuildSettings.development)
            {
                string armv7a_so_path = pathToBuiltProject + "/" + PlayerSettings.productName + "/" + "libs/armeabi-v7a/libmono.so";
                File.Copy(Application.dataPath + "/HotFix/Editor/libs/development/armeabi-v7a/libmono.so", armv7a_so_path, true);

                string x86_so_path = pathToBuiltProject + "/" + PlayerSettings.productName + "/" + "libs/x86/libmono.so";
                File.Copy(Application.dataPath + "/HotFix/Editor/libs/development/x86/libmono.so", x86_so_path, true);
            }
            else
            {
                string armv7a_so_path = pathToBuiltProject + "/" + PlayerSettings.productName + "/" + "libs/armeabi-v7a/libmono.so";
                File.Copy(Application.dataPath + "/HotFix/Editor/libs/release/armeabi-v7a/libmono.so", armv7a_so_path, true);

                string x86_so_path = pathToBuiltProject + "/" + PlayerSettings.productName + "/" + "libs/x86/libmono.so";
                File.Copy(Application.dataPath + "/HotFix/Editor/libs/release/x86/libmono.so", x86_so_path, true);
            }

            Debug.Log("HotFix libmono.so Success !!");
        }
    }
}