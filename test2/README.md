# 第二次作业
## 第二次作业说明书

- 老师您好，我是刘一达，学号1182020138029

### 实验目的和要求
- 使用Open XML SDK方法解析Word文件，提取出Word文件中的所有文字。文字按段落分组。使用WordprocessingDocument类作为实验项目的主要类。
- 把方法封装到自定义类中

### 知识点
- 使用VS2017中的NuGet安装项目的外部引用。
- 熟悉Word文件的XML格式。
- 熟悉XML格式及OpenXmlElement处理，熟悉XML中对象的遍历。

### 本次作业的要点
- 本次作业中主要是使用OpenXml插件中的WordprocessingDocument类来进行word文档中的文字提取。
- 其中涉及到了使用WordprocessingDocument.Open（）将word文件打开
- 使用MainDocumentPart.Document.Body提取word文档正文部分（在word的xml格式中 <w:body>…</w:body> 为正文部分）
- 使用foreach循环语句和Elements<Paragraph>遍历正文中的每个段落
- 最后将正文的每个段落输出

![avatar](http://r.photo.store.qq.com/psb?/V135Z68L35FN3x/fumo.gEDU.l1YyP0XoUnbVnuF0nltCNCKzEkDd.ntwU!/r/dDQBAAAAAAAA)