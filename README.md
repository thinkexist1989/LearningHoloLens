# LearningHoloLens-开发学习笔记

主要参考[微软官方 HoloLens 学习教程](https://docs.microsoft.com/zh-cn/windows/mixed-reality/tutorials)

## Unity 项目配置

### Per-project settings

To target Windows Mixed Reality, you first need to set your Unity project to export as a Universal Windows Platform app:

1. Select **File** > **Build Settings**...
2. Select **Universal Windows Platform** in the Platform list and click **Switch Platform**
3. Set **SDK** to **Universal 10**
4. Set **Target device** to **Any Device** to support immersive headsets or switch to **HoloLens**
5. Set **Build Type** to **D3D**
6. Set **UWP SDK** to **Latest installed**

We then need to let Unity know that the app we are trying to export should create an immersive view instead of a 2D view. We do that by enabling "Virtual Reality Supported":

1. From the **Build Settings**... window, open **Player Settings**...
2. Select the **Settings for Universal Windows Platform** tab
3. Expand the **XR Settings** group
4. In the **XR Settings** section, check the **Virtual Reality Supported** checkbox to add the **Virtual Reality Devices** list.
5. In the **XR Settings** group, confirm that **"Windows Mixed Reality"** is listed as a supported device. (this may appear as "Windows Holographic" in older versions of Unity)

### Per-scene settings

If your app is targeting HoloLens specifically, there are a few settings that need to be changed to optimize for the device's transparent displays, so your app will show through to the physical world:

1. In the **Hierarchy**, select the **Main Camera**
2. In the **Inspector** panel, set the transform **position** to **0, 0, 0** so the location of the users head starts at the Unity world origin.
3. Change **Clear Flags** to **Solid Color**.
4. Change the **Background** color to **RGBA 0,0,0,0**. Black renders as transparent in HoloLens.
5. Change **Clipping Planes - Near** to the HoloLens recommended 0.85 (meters).

If you delete and create a new camera, make sure your camera is **Tagged** as **MainCamera**.

## 建议用于 Unity 的设置

### 低分辨率设置 Low quality settings

将 Unity 的 Player 质量设置修改到非常低的水平是非常重要的。这将有助于确保应用程序在适当的帧率下高效运行。这对于 Hololens 开发是非常重要的。

设置方式：**Edit** > **Project Settings** > **Quality** > 将 UWP 下的**Default**改为**Fastest**或**Very Low**。

### 光照设置 Lighting settings

在 Unity 中通常将场景光照设置为实时全局光照（Realtime Global Illumination）。

设置方式：**Window** > **Rendering** > **Lighting Settings** > **Realtime Global Illumination**。

## MR Basic 101 - Origami

### 按照教程配置 Orgami 包时候提示“SpatialMappingRenderer could not be found”

This is a "bug" in later versions of Unity 2018 and above when importing projects with an older version where those 2 classes are not automatically imported. Just go to XR Settings under Player settings and uncheck/check "Virtual Reality Supported". That will re-download the missing classes.

### 通过 PC 端网页登陆 HoloLens 的 IP 地址需要认证的问题

- 首先需要 request pin 配对，设置用户名和密码。
- 下载根证书，注意，需要将根证书手动储存在受信任的颁发机构下面，不要自动添加！否则仍然出现网页端仍然出现不受信任字样。

### HoloLens 上账号更换

只能 Reset 系统，然后重新登录账号。

### Unity 中的 Main Camera 的标签 Tag。

有一次错误地把 Unity 中的 Main Camera 删除了，然后重新添加了一个 Camera，但是后来 Origami 程序中的 Cursor 始终显示不出来，原因是后来添加的 Camera 的 Tag 是 Untagged，而原来的 Main Camera 的 Tag 是 MainCamera。从而导致 WorldCursor.cs 中的`var headPosition = Camera.main.transform.position;`并未获得 Main Camera 的头部位置而出错。

### Unity 脚本中 Start()和 Awake()的区别

`Awake()`在 MonoBehavior 创建之后就立刻调用，而`Start()`则在 MonoBehavior 创建后在该帧`Update()`之前，在`MonoBehavoir.enable==true`的情况下执行。

### Unity 中的 ancestor 是什么意思

Ancestor 是祖先的意思，在 unity 中，一个 GameObject 可以是其他 GameObject 的父对象，这个 GameObject 的 parent, parent’s parant 等等都叫做 ancestor。通过调用 SendMessageUpwards 可以将消息发送给 GameObject 的所有 ancestor 的 GameOject 脚本。

### Unity 中资源读取方式

Unity 中使用`Resources.Load()`命令读取资源文件。

使用中有几个值得注意的地方：

- 读取文件时的根目录是 Assets/Resources，所有资源文件都放在该文件夹下，命令中的路径从 Resources 文件夹里开始写。
- 用 / 表示子文件夹。
- 读取的文件不要加文件的后缀。
- Load 后 <> 中写入读取的类型。

```
AudioClip clip = Resources.Load<AudioClip>(fname);
```

### Unity 2018.4 在 Hierarchy 中添加 Spatial Mapping 时的问题

按照微软官方教程在 Unity 2018.4 中添加**Origami=>Holograms**中的**Spatial Mapping**时，在其 Inspector 下正常关联的三个脚本中只有**Spatial Mapping**脚本是正常的，还有两个脚本显示 Missing。

这是由于版本 Bug 造成的，微软官方版本是在 Unity 2017 下演示的，在 Unity 2017 中运行时正常的。在 2018 中的解决方法是在**Spatial Mapping**的 Inspector 面板下删除两个 Missing 的脚本，然后点击**Add Component**，在其中搜索并添加**Spatial Mapping Renderer**脚本和**Spatial Mapping Collider**脚本，这样便可正常运行。

## SocketTest

### 权限问题

Unity 中 Build 完成后打开 Visual Studio 的解决方案，在**package.appxmanifest** > **功能**中，勾选需要赋予程序的功能，TCP 通信需要勾选**专用网络**，否则无法监听端口。

### UWP C# 与 Unity C#问题

Unity 中使用了 C#的语法，但是和 Windows 下的 C#不同，例如不支持`await`和`async`关键字等，因此若想在 Unity 与 VS 中均 build 成功，则需要利用宏定义来实现。

```
#if !UNITY_EDITOR
    //在这里添加UWP相关代码
#else
    //在这里添加Unity相关代码
#endif
```
