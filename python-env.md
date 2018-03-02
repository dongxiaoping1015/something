#环境管理（管理版本和虚拟环境，够用了）
##Python版本管理工具pyenv
* __安装brew__ install pyenv
    1. brew install pyenv 
    2. pyenv init ，然后eval "$(pyenv init -)"写到~/.bash_profile
    3. exec "$SHELL"，再关掉terminal就可以了
* __卸载bre__ uninstall pyenv
* https://github.com/pyenv/pyenv#basic-github-checkout

* __查看可安装版本__ pyenv install -l 
* __安装与卸载Python__   
    * __安装python__ pyenv install 2.7.3
    * __卸载python__ pyenv uninstall 2.7.3
* __优先级__ shell > local > global 
* __设置全局版本__ pyenv global 3.6.2

#结合pyenv管理虚拟环境 
* __安装pyenv-virtualenv__ brew install pyenv-virtualenv
* __创建__ pyenv virtualenv 3.6.2 env362
* __查看__ pyenv versions
* __使用虚拟环境__ pyenv activate env362
* __关闭__ pyenv deactivate
* __卸载__ pyenv uninstall env362