#!/usr/bin/python
# -*- coding: UTF-8 -*-
 
from xml.dom.minidom import parse
import xml.dom.minidom
 
# 使用minidom解析器打开 XML 文档
DOMTree = xml.dom.minidom.parse("C:\\Users\\Administrator\\AppData\\Local\\youdao\\dict\\Application\\7.5.2.0\\youdaowords.xml")
collection = DOMTree.documentElement


items = collection.getElementsByTagName("item")
wordList = ""

for item in items:
   word = item.getElementsByTagName('word')[0]
   wordList += word.childNodes[0].data + " "
f = open('words.txt','w')
f.write(wordList)
f.close()