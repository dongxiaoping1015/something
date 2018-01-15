# -*- coding: utf-8 -*-

# Define your item pipelines here
#
# Don't forget to add your pipeline to the ITEM_PIPELINES setting
# See: http://doc.scrapy.org/en/latest/topics/item-pipeline.html
from scrapy import signals
import json
import codecs
from twisted.enterprise import adbapi
from datetime import datetime
from hashlib import md5
import MySQLdb
import MySQLdb.cursors

class Scrapytest2Pipeline(object):
    def process_item(self, item, spider):
        return item

class JsonWriterPipeline(object):
    def __init__(self):
        self.filename = open("ZPposition.json", "wb")

    def process_item(self, item, spider):
        jsontext = json.dumps(dict(item), ensure_ascii=False) + ",\n"
        self.filename.write(jsontext.encode("utf-8"))
        return item

    def close_spider(self, spider):
        self.filename.close()
    # def open_spider(self, spider):
    #     self.file = open('ZPposition.jl', 'wb')
    #
    # def close_spider(self, spider):
    #     self.file.close()
    #
    # def process_item(self, item, spider):
    #     line = json.dumps(dict(item)) + "\n"
    #     self.file.write(line)
    #     return item

class MySQLStoreCnblogsPipeline(object):
    def __init__(self, dbpool):
        self.dbpool = dbpool

    @classmethod
    def from_settings(cls, settings):
        dbargs = dict(
            host=settings['MYSQL_HOST'],
            db=settings['MYSQL_DBNAME'],
            user=settings['MYSQL_USER'],
            passwd=settings['MYSQL_PASSWD'],
            charset='utf8',
            cursorclass=MySQLdb.cursors.DictCursor,
            use_unicode=True,
        )
        dbpool = adbapi.ConnectionPool('MySQLdb', **dbargs)
        return cls(dbpool)

    # pipeline默认调用
    def process_item(self, item, spider):
        d = self.dbpool.runInteraction(self._do_upinsert, item, spider)
        d.addErrback(self._handle_error, item, spider)
        d.addBoth(lambda _: item)
        return d

    # 将每行更新或写入数据库中
    def _do_upinsert(self, conn, item, spider):
        urlmd5id = self._get_urlmd5id(item)
        # print urlmd5id
        now = datetime.utcnow().replace(microsecond=0).isoformat(' ')
        # conn.execute("""
        #         select 1 from book where urlmd5id = %s
        # """ % (urlmd5id,))
        # ret = conn.fetchone()
        ret = False
        if ret:
            conn.execute("""
                update showbook_book set bookname = '%s', author = '%s', url = '%s', bookrate = %s, ratepeople = %s, image_url = '%s' where urlmd5id = '%s'
            """ % (item['bookname'], item['author'], item['url'], item['bookrate'], item['ratepeople'], item['image_urls'][0], urlmd5id))
            # print """
            #    update book set bookname = %s, author = %s, url = %s, bookrate = %s, ratepeople = %s where urlmd5id = %s
            # """, (item['bookname'], item['desc'], item['url'], item['bookrate'], now, urlmd5id)
        else:
            str = """
                insert into showbook_book(urlmd5id, bookname, author, url, bookrate, ratepeople, image_url) 
                values('%s', '%s', '%s', '%s', %s, %s, '%s')
            """ % (urlmd5id, item['bookname'], item['author'], item['url'], item['bookrate'], item['ratepeople'], item['image_urls'][0])

            conn.execute("""
                insert into showbook_book(urlmd5id, bookname, author, url, bookrate, ratepeople, image_url) 
                values('%s', '%s', '%s', '%s', %s, %s, '%s')
            """ % (urlmd5id, item['bookname'], item['author'], item['url'], item['bookrate'], item['ratepeople'], item['image_urls'][0]))

    # 获取url的md5编码
    def _get_urlmd5id(self, item):
        # url进行md5处理，为避免重复采集设计
        return md5(item['url']).hexdigest()

    # 异常处理
    def _handle_error(self, failue, item, spider):
        pass#log.err(failure)