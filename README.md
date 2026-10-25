# Feng's C语言练习器

**ver1.2.1.1 · Windows 本地 C 语言学习与自动评测**

> By Feng; 仅供学习使用。

90道普通练习、50关暗域星轨、内置编译器、语法着色、知识点与标准答案、随机评测和中英文诊断。

## 下载

- [独立 EXE](https://github.com/relsx/feng-c-practice/releases/download/ver1.2.1.1/Feng_C_Practice_ver1.2.1.1.exe)
- [完整发布包：源码、说明、预览与验证日志](https://github.com/relsx/feng-c-practice/releases/download/ver1.2.1.1/Feng_C_Practice_ver1.2.1.1_full.zip)
- [Release 与 SHA-256 校验](https://github.com/relsx/feng-c-practice/releases/tag/ver1.2.1.1)

双击EXE即可运行，需要x64 Windows及.NET Framework 4.8或更新版本。编译器、编辑器与动画资源已嵌入，练习可离线进行。

## 本版界面更新

- 融合式顶部，移除系统标题栏，保留窗口拖动、缩放与最大化。
- 蓝白玻璃卡片、大圆角、半透明背景光晕与细腻边缘高光。
- 设置里可自由切换明亮/深色，暗域保持暗蓝星轨。
- 窗口打开/关闭、页面与答案展开过渡；常用操作改为可悬停提示的矢量图标。
- 主界面与设置弹窗统一外框、内层圆角裁剪和顶部图标对齐。
- 保留代码编辑、多步撤回、光标定位和原有评测功能。

## 使用

普通模式：选题 → 学习知识点 → 编写C程序 → Ctrl+Enter评测 → 在右侧检查输出。提示与答案支持一键导入并备份草稿。Ctrl+Z撤回，Ctrl+Y重做，Ctrl+S保存。

暗域：50关难度递增，每关8～30分钟；从空白自主编写，禁止粘贴和答案导入，得分严格大于90分且编译/运行正常才通关。保留已有星轨与挑战进度。

每题100分：随机正确性60、边界正确性20、运行质量10、简洁度10。每题28组数据，输出、耗时、退出码和中英文错误诊断可查看并导出。内置更新可在设置中手动执行。

- [使用说明](docs/使用说明.md)
- [更新日志](CHANGELOG.md)
- [验证报告](docs/验证报告.txt)
- [暗域50关学习路线](docs/暗域50关学习路线.md)

## 从源码构建

在Windows PowerShell执行 `./source/build.ps1`，生成 `Feng_C语言练习器_ver1.2.1.1.exe`。自带TinyCC、AvalonEdit与素材，第三方许可见source目录。

草稿、成绩与字号沿用上一版；新增主题选项本地保存。更新不会向仓库上传练习代码或成绩。
