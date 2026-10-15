# Feng's C语言练习器

**ver1.1.1 · Windows 离线 C 语言练习与自动评测工具**

> By Feng; 仅供学习使用。

从入门语法到算法和综合项目，提供 **90 道普通练习 + 50 关挑战**，内置编译器、代码编辑器、随机评测、知识讲解与标准答案。

## 下载与运行

- [下载 ver1.1.1 独立 EXE](https://github.com/relsx/feng-c-practice/releases/download/ver1.1.1/Feng_C_Practice_ver1.1.1.exe)
- [下载完整发布包（源码、说明、验证记录）](https://github.com/relsx/feng-c-practice/releases/download/ver1.1.1/Feng_C_Practice_ver1.1.1_full.zip)
- [Release 与 SHA-256 校验](https://github.com/relsx/feng-c-practice/releases/tag/ver1.1.1)
- [使用说明](docs/使用说明.md) · [更新日志](CHANGELOG.md) · [验证报告](docs/验证报告.txt)

适用于 **x64 Windows，需要 .NET Framework 4.8 或更新版本**。下载 EXE 后双击运行，无须另装 C 编译器；首次评测自动释放内置编译器，可离线使用。

## 普通练习

- 60 道基础题、27 道进阶题、3 道综合项目考试，逐步学习输入输出、分支、循环、数组、字符串、函数、指针、递归、数据结构及算法。
- 每题可查看知识点，说明按题号首次引入的语法与用法，并提供相关基础回顾。
- 先看提示，再展开参考答案；支持一键导入，导入前自动备份草稿。
- 代码语法着色、行号、14～24 字号、多步撤回与取消撤回：Ctrl+Z、Ctrl+Y 或 Ctrl+Shift+Z。
- 每题 8 组边界与 20 组随机测试，右侧显示输入、实际输出、预期输出、耗时和退出码。
- 保留英文编译错误，同时提供中文解释；完整报告可单独打开和导出。
- 编译失败、运行失败或低于 60 分时显示简洁得分警告；普通练习不播放处决动画。
- 自动保存草稿和历史最高分，累计总分 / 9000；高分播放 1 秒庆祝动画。

## 闯关练习

独立暗色界面包含 **50 关**，难度递增，最后安排综合挑战。每关 **3～10 分钟**，只有得分 **严格大于 90 分**且编译、运行正常才解锁下一关，90 分整不能通关。

答题前可学习知识点，完整答案仅通关后开放复盘。代码从空白自行键入，禁止粘贴、拖入或导入；自己的草稿可保留重试。切换窗口和查看知识点仍计时，评测提交本身不消耗答题时间。失败、低分或超时保留约 4.4 秒暗色处决动画，Esc 可提前淡出。闯关累计总分 / 5000。

## ver1.1.1 界面更新

主窗口约 **0.8 秒淡入**，随后欢迎说明约 **0.5 秒淡入**；应用内知识点、答案、报告和提示窗口统一约 0.5 秒淡入。动画只改变透明度，保持编辑区文字、光标和选区位置对齐。主界面采用蓝色与深蓝渐变，支持高 DPI 显示和可调整宽度的结果区。

## 评分

每题 100 分：随机正确性 60、边界正确性 20、运行质量 10、简洁度 10。有效代码行数和大小分别与标准答案比较，各占 5 分，留有合理余量；错误程序不会因短小获得简洁度分。详见 [评分细则](CHANGELOG.md#评分细则)。

每组执行限制 3 秒、256 MiB，输出上限约 64 KiB。整数精确比较，浮点允许绝对误差 0.0001。运行本地代码使用当前用户权限，资源限制不构成完整安全隔离。

## 从源码构建

在 x64 Windows PowerShell 中运行：

```powershell
./source/build.ps1
```

使用系统 .NET Framework C# 编译器，在仓库根目录生成 `Feng_C语言练习器_ver1.1.1.exe`。编译器 ZIP、编辑器 DLL、图标和动画资源均随源码提供。

| 文件 | 用途 |
| --- | --- |
| `source/AppWpf.cs`、`CodeEditor.cs` | 普通练习、代码编辑和保存 |
| `source/BankData.cs`、`AdvancedData.cs` | 普通题目与参考答案 |
| `source/ChallengeBank.cs`、`ChallengeWindow.cs` | 闯关题库、计时、解锁和复盘 |
| `source/KnowledgeWindow.cs` | 普通练习与闯关知识讲解 |
| `source/Engine.cs`、`CodeMetrics.cs` | 编译、运行、评测与评分 |
| `source/Diagnostics.cs` | 英文诊断与中文解释 |
| `source/Transitions.cs`、`FailureWindow.cs` | 窗口淡入和闯关动画 |

## 验证与记录

ver1.1.1 补丁通过 **302 项检查**，包含启动顺序、普通练习失败反馈、知识点、淡入窗口、独立 EXE 和编辑对齐。编辑检查覆盖 6 种字号、100% / 150% / 200% 缩放、1122 个字符位置命中、18 次真实点击及 18 次拖选。ver1.1 基线另已验证 5320 组标准答案用例和 560 组加强测试；本补丁未重复执行全部基线用例，见 [验证报告](docs/验证报告.txt)。

ver1.1.1 继续使用 `%LOCALAPPDATA%\CPracticeLab-ver1.1`，兼容 ver1.1 的草稿、成绩和闯关进度。仓库不包含个人练习记录。检查不能覆盖所有硬件或任意用户程序。

## 作者与第三方组件

By Feng; 仅供学习使用。

项目自身未指定额外的开源授权许可。第三方组件各自的许可证继续适用：

- AvalonEdit 6.3.1.120：MIT，见 [许可证](source/AvalonEdit-LICENSE.txt)。
- TinyCC 0.9.27：LGPL 2.1，见 [许可证](source/TinyCC-LGPL-2.1.txt)和[对应源码](source/tcc-0.9.27.tar.bz2)。
- Kenney 粒子素材：CC0，见 [许可证](source/assets/Kenney-License.txt)。
- [第三方组件说明](source/THIRD_PARTY.md) · [动画素材说明](source/assets/素材说明.md)。
