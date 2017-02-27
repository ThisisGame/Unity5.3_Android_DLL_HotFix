# Unity5.3_Android_DLL_HotFix
Unity5.3_Android_DLL_HotFix is used to updata cs code in apk.

unity is run on mono,mono load dll file,like we load game resource.

so we can change mono source code to load the new version dll which download from web.


# use
load the project with unity,export android project,then export apk.

run the apk,click the button ,will save dll to /data/data/xxx/file .and game will restart.

then,

mono will load the new dll in /data/data/xxx/file.
