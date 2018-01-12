from scrapy import cmdline
#cmdline.execute("scrapy crawl douban".split())
# f = open('b.json')
# list = f.readlines()
# list.pop(0)
# list.pop(-1)
# print (list[5].split(' ')[1]).split('}')[0].split('"')[1]

import urllib
test_url = 'http://ip.chinaz.com/getip.aspx'
response = urllib.urlopen(test_url, proxies={"http": "http://50.116.9.243:443"})
if response.getcode() != 200:
    print 'wrong!'
else:
    print 'yes!'