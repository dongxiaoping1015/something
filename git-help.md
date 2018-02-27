# 本地操作

## 添加与提交
__创建仓库：__       git init

__将文件添加到仓库：__ 
1. git add "filename" 例如：git add readme.txt
2. git add . 批量添加文件

__将文件提交给仓库：__ git commit -m "提交说明"

__显示仓库当前状态：__ git status

__查看difference：__ git diff

__查看提交日志：__ git log

__查看简单提交日志：__ git log --pretty=oneline

__回退版本：__ git reset --hard HEAD 

* 回退上一个版本HEAD^
* 回退上上个版本HEAD^^
* 回退上100个版本HEAD~100

__根据版本ID回滚：__ git reset --hard "commit id" 
* 例：git reset --hard 3628164

__查看操作记录（可看到commit id）：__ git reflog

__删除文件：__ git rm filename 
* 例：git rm test.txt 
* 可恢复：git checkout -- test.txt

## 分支操作
__切换分支：__ git checkout -b 分支名 **-b参数表示创建并切换，无参数则表示切换分支**

__查看分支：__ git branch

__合并指定分支到当前分支：__ git merge 分支名

__查看分支：__ git branch

__创建分支：__ git branch <name>

__切换分支：__ git checkout <name>

__创建+切换分支：__ git checkout -b <name>

__合并某分支到当前分支：__ git merge <name>

__删除分支：__ git branch -d <name>

# 远程操作

__连接远程GitHub：__
1. 创建SSH Key：ssh-keygen -t rsa -C "yourmail@example.com"
2. 添加SSH Key到Github上

__本地库关联远程库：__ git remote add origin git@github.com:dongxiaoping1015/repositoryname.git

__把本地仓库内容推送到远程库：__ git push -u origin master  
* master是分支名，第一次推送时加-u可以使本地master和远程master合并
* 之后推送直接 git push origin master

__从远程库获取最新版本到本地：__
1. 不会自动merge
    * git fetch origin master 从远程origin的master主分支下载最新的版本到origin/master分支上
    * git log -p master..origin/master 比较本地的master分支和origin/master分支的差别
    * git merge origin/master 进行合并
    * 或
    * （git fetch origin master:tmp
    *    git diff tmp
    *    git merge tmp 从远程获取最新版本到本地test分支上，之后再进行比较合并）
2. git pull origin master 从远程获取最新版本并merge到本地

__将远程库克隆到本地：__
git clone git@github.com:dongxioaping1015/name.git

__克隆某一分支：__ git clone --branch 0.4.10 git@github.com:bottlepy/bottle.git



