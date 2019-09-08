# LearningHoloLens-开发学习笔记

主要参考[微软官方HoloLens学习教程](https://docs.microsoft.com/zh-cn/windows/mixed-reality/tutorials)

## MR Basic 101 - Origami

### 按照教程配置Orgami包时候提示“SpatialMappingRenderer could not be found”。
This is a "bug" in later versions of Unity 2018 and above when importing projects with an older version where those 2 classes are not automatically imported. Just go to XR Settings under Player settings and uncheck/check "Virtual Reality Supported". That will re-download the missing classes.

### 通过PC端网页登陆HoloLens的IP地址需要认证的问题。
- 首先需要request pin配对，设置用户名和密码。
- 下载根证书，注意，需要将根证书手动储存在受信任的颁发机构下面，不要自动添加！否则仍然出现网页端仍然出现不受信任字样。

### HoloLens上账号更换。
只能Reset系统，然后重新登录账号。
### Unity中的Main Camera的标签Tag。
有一次错误地把Unity中的Main Camera删除了，然后重新添加了一个Camera，但是后来Origami程序中的Cursor始终显示不出来，原因是后来添加的Camera的Tag是Untagged，而原来的Main Camera的Tag是MainCamera。从而导致WorldCursor.cs中的`var headPosition = Camera.main.transform.position;`并未获得Main Camera的头部位置而出错。
### Unity脚本中Start()和Awake()的区别。
`Awake()`在MonoBehavior创建之后就立刻调用，而`Start()`则在MonoBehavior创建后在该帧`Update()`之前，在`MonoBehavoir.enable==true`的情况下执行。
### Unity中的ancestor是什么意思。
Ancestor是祖先的意思，在unity中，一个GameObject可以是其他GameObject的父对象，这个GameObject的parent, parent’s parant等等都叫做ancestor。通过调用SendMessageUpwards可以将消息发送给GameObject的所有ancestor的GameOject脚本。

### Unity中资源读取方式
Unity 中使用`Resources.Load()`命令读取资源文件。

使用中有几个值得注意的地方：

- 读取文件时的根目录是Assets/Resources，所有资源文件都放在该文件夹下，命令中的路径从Resources文件夹里开始写。
- 用 / 表示子文件夹。
- 读取的文件不要加文件的后缀。
- Load 后 <> 中写入读取的类型。

```
AudioClip clip = Resources.Load<AudioClip>(fname);
```