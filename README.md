# CSharp
- 第一次作业

- 老师您好，我是刘一达，学号1182020138029，本科专业是自动化，之前关于软件编程之学习过C语言，所以这次作业对我来说有些困难。

- 我的作业主要来源于自己百度以及参考班里学霸，也和同学讨论过，但是效果都不太理想，请老师原谅。

- 下面我就简述一下我对Lcs算法的理解。

# LCS算法
- 问题

给定两个字符串，求解这两个字符串的最长公共子序列（Longest Common Sequence）。比如字符串1：BDCABA；字符串2：ABCBDAB

则这两个字符串的最长公共子序列长度为4，最长公共子序列是：BCBA

- 算法

设 X=(x1,x2,.....xn) 和 Y={y1,y2,.....ym} 是两个序列，将 X 和 Y 的最长公共子序列记为LCS(X,Y)
找出LCS(X,Y)就是一个最优化问题。因为，我们需要找到X 和 Y中最长的那个公共子序列。而要找X和Y的LCS，首先考虑X的最后一个元素和Y的最后一个元素。

第一种情况：

如果xn=ym，即X的最后一个元素与Y的最后一个元素相同，这说明该元素一定位于公共子序列中。因此，现在只需要找：LCS(Xn-1，Ym-1)
LCS(Xn-1，Ym-1)就是原问题的一个子问题。为什么叫子问题？因为它的规模比原问题小。（小一个元素也是小嘛....）

为什么是最优的子问题？因为我们要找的是Xn-1 和 Ym-1 的最长公共子序列啊。。。最长的！！！换句话说，就是最优的那个。（这里的最优就是最长的意思）

2）如果xn != ym，这下要麻烦一点，因为它产生了两个子问题：LCS(Xn-1，Ym) 和 LCS(Xn，Ym-1)

因为序列X 和 序列Y 的最后一个元素不相等嘛，那说明最后一个元素不可能是最长公共子序列中的元素嘛。（都不相等了，怎么公共嘛）。

LCS(Xn-1，Ym)表示：最长公共序列可以在(x1,x2,....x(n-1)) 和 (y1,y2,...yn)中找。

LCS(Xn，Ym-1)表示：最长公共序列可以在(x1,x2,....xn) 和 (y1,y2,...y(n-1))中找。

求解上面两个子问题，得到的公共子序列谁最长，那谁就是 LCS（X,Y）。用数学表示就是：

LCS=max{LCS(Xn-1，Ym)，LCS(Xn，Ym-1)}

由于条件 1)  和  2)  考虑到了所有可能的情况。因此，我们成功地把原问题 转化 成了 三个规模更小的子问题。