# -*- coding: utf-8 -*-
from scrapy.spiders import CrawlSpider, Rule
from scrapy.linkextractors import LinkExtractor
from scrapytest2.news import NewsItem
from scrapy import Request

class newsSpider(CrawlSpider):
    name = "sohu"
    allowed_domains = ['sohu.com']
    start_urls = ['http://www.sohu.com/']#['https://book.douban.com']
    rules = [Rule(LinkExtractor(allow=['(http://)?(news.sohu.com|www.sohu.com)/(a/)?\d+(_|/n)?\d+?.+']))]
    URL = []

    def parse(self, response):
        news = NewsItem()
        news['url'] = response.url
        news['news_title'] = response.xpath("//h1/text()").extract()#('//h1/span/text()[1]').extract()[0]#, 'UTF-8')
        yield news

        urls = response.xpath('/html/body/div[1]/div[3]/div[1]/div[1]/div[2]/div/div[2]/div//a/@href').extract()

        for url in urls:
            url = url.encode('utf-8')
            if url.startswith('/a'):
                url = 'http://www.sohu.com' + url
            url = url.decode()
            if url in self.URL:
                continue
            else:
                self.URL.append(url)
            yield Request(url, callback=self.parse)   #response.xpath('id("db-rec-section")/div/dl[1]/dt/a/@href').extract()[0].encode('utf-8'),
