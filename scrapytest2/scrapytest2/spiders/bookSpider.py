# -*- coding: utf-8 -*-
# from scrapy.spiders import CrawlSpider
#
#
# class Douban(CrawlSpider):
#     name = "douban"
#     start_urls = ['http://movie.douban.com/top250']
#
#     def parse(self, response):
#         print response.body

#from scrapy.contrib.spiders import CrawlSpider, Rule
from scrapy.spiders import CrawlSpider, Rule
#from scrapy.contrib.linkextractors import LinkExtractor
from scrapy.linkextractors import LinkExtractor
from scrapytest2.book import BookItem
from scrapy import Request
import sys
import time
class bookSpider(CrawlSpider):
    name = "douban"
    allowed_domains = ['douban.com']
    start_urls = ['https://book.douban.com/subject/26813101/']#['https://book.douban.com']
    rules = [Rule(LinkExtractor(allow=['/suject/\d+/.+']))]
    URL = []

    def parse(self, response):

        book = BookItem()
        book['url'] = response.url
        book['bookname'] = response.xpath("id('wrapper')/h1/span/text()").extract()[0]
        book['bookrate'] = response.xpath("id('interest_sectl')//strong/text()").extract()[0]
        book['ratepeople'] = response.xpath("id('interest_sectl')/div/div[2]/div/div[2]/span/a/span/text()").extract()[0]
        book['author'] = response.xpath('id("info")/span[1]/a/text()').extract()[0]
        book['image_urls'] = response.xpath('id("mainpic")//img/@src').extract()
        yield book
        # if self.URL >= 5:
        #     sys.exit()
        urls = response.xpath('id("db-rec-section")/div/dl/dt/a/@href').extract()

        for url in urls:
            if url in self.URL:
                continue
            else:
                self.URL.append(url)
            yield Request(url, callback=self.parse)