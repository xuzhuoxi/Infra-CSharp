---
name: generate-note
description: >-
  Analyzes git commits from the latest tag (or the first commit if the repo
  has no tags) to the current branch HEAD and generates or supplements
  notes/release/ReleaseNotes_{tag}.md from notes/release/ReleaseNotes.md.
  Use when the user invokes generate-note, asks to generate ReleaseNotes /
  发行说明 / release notes, or names a v*.*.* tag for release notes. The tag
  argument is required.
disable-model-invocation: true
---

# generate-note

根据提交变更记录生成或补充 ReleaseNote。

## 要求（必须遵守）

1. 调用是必须指定 tag 值，格式为 `v*.*.*`
2. 读取最新的一个 git tag 位置，范围是从这个位置到当前分支的最后本地提交版本。若仓库没有任何 tag，范围改为**第一个提交（含）→ 最新一次本地提交（含）**
3. 模板是 `notes/release/ReleaseNotes.md`
4. 生成的文档命名格式是 `ReleaseNotes_{tag值}.md`，存放在 `notes/release` 目录中
5. 如果已经存在文档，则使用补充更新

## 调用

用户必须给出 tag，且匹配 `v` + 数字三段，例如 `v1.2.0`。

合法：`v1.1.0`、`v1.2.3`。非法：`1.1.0`、`v1.1`、`v1.1.0-rc1`、空。

未指定或格式不对时：**停止，不要读 git、不要写文件**，只提示正确格式并要用户补 tag。

输出路径：`notes/release/ReleaseNotes_{tag}.md`（tag 含前缀 `v`，例如 `notes/release/ReleaseNotes_v1.2.0.md`）。

## Git 范围

在仓库根目录执行。默认范围是**最新一个 git tag（不含）→ 当前分支最后一次本地提交（含）**，即 `HEAD`（含未推送的本地提交，不用 `origin/`）。

```bash
git rev-parse --abbrev-ref HEAD
git rev-parse HEAD
git describe --tags --abbrev=0
```

`git describe --tags --abbrev=0` 得到当前分支能追溯到的最新 tag，记为 `LATEST_TAG`。

**无任何 tag**时：不要把 `git describe` 失败当成错误。范围是**当前分支第一个提交（含）→ 最新一次本地提交（含）**。先取根提交：

```bash
git rev-list --max-parents=0 HEAD
```

记为 `FIRST_COMMIT`。多个根提交时取最早的一个。不要用 `FIRST_COMMIT..HEAD`（会漏掉第一个提交）。改为：

```bash
git log --format=fuller --reverse HEAD
git diff --stat --root HEAD
git diff --root HEAD
```

若 `LATEST_TAG` 等于用户指定的 tag（已打在本分支上），改用**上一档** tag 作为起点，否则范围会空：

```bash
git tag --merged HEAD --sort=-v:refname
```

取排序后的第二个 `v*.*.*` 为起点；没有上一档则与无 tag 相同：从第一个提交到 `HEAD`。

有起点 tag 时收集变更（把 `RANGE_START` 换成该 tag）：

```bash
git log --format=fuller RANGE_START..HEAD
git diff --stat RANGE_START..HEAD
git diff RANGE_START...HEAD
```

再看该范围内的 `*.csproj`、`Directory.Packages.props`（若有）中的 `PackageReference`（NuGet 包版本）。只根据这个范围归纳，不要编造范围外的改动。

范围内无提交且目标文件已存在：说明无新变更，不改文件。范围内无提交且文件不存在：不要生成空说明，告知用户。

## 模板与文风

生成前先读 [templates.md](templates.md)，按其中结构与填写规则写文档。若已有 `notes/release/ReleaseNotes_v*.md`，可参考其文风。

生成的文档不要包含 `# Infra-CSharp` 大标题，从 `## Release Notes` 起头。补充已有文件时若存在该标题则去掉。

模板中部分条目没有内容时，生成的文档中可以不包含该条目（不要输出空标题）。

## 已存在则补充更新

若 `notes/release/ReleaseNotes_{tag}.md` 已存在：

1. 先读现有全文，**保留**已有条目、总述和用户手写内容
2. 只把范围内**尚未写过**的变更并入对应小节
3. 不整文件覆盖、不删旧条、不重复同义条
4. 没有对应小节且**确有新条目**时再按模板补标题；不要补空小节
5. 有新要点时，可改总述一句，使其覆盖新旧内容，不要推翻重写

## 完成后

告知：指定 tag、用作起点的 git tag（无 tag 时改为第一个提交的短哈希）、`HEAD` 短哈希、当前分支、输出路径、新建还是补充、写入了哪些小节。不要自动 `git commit` / `git tag`。
