# -*- coding: utf-8 -*-
from scrapy.spiders import CrawlSpider, Rule
from scrapy.linkextractors import LinkExtractor
from scrapytest2.ipItem import IpItem
from scrapy import Request
import urllib
class ipSpider(CrawlSpider):
    name = "ip"
    #allowed_domains = ['http://www.xicidaili.com/nn/']
    start_urls = ['http://www.xicidaili.com/nn/1']#['https://book.douban.com']
    rules = [Rule(LinkExtractor(allow=['http://www.xicidaili.com/nn/(\d+)?']))]
    IP = []
    count = 1
    test_url = 'http://ip.chinaz.com/getip.aspx'

    def parse(self, response):
        item = IpItem()
        ips = response.xpath('id("ip_list")//tr/td[2]/text()').extract()
        ports = response.xpath('id("ip_list")//tr/td[3]/text()').extract()
        for i in range(len(ips)):
            if ips[i] + ':' + ports[i] in self.IP:
                continue
            else:
                self.IP.append(ips[i] + ':' + ports[i])
            response = urllib.urlopen(self.test_url, proxies={"http": "http://ha:ha@" + ips[i] + ":" + ports[i]})
            if response.getcode() != 200:
                continue
            else:
                item['ip'] = ips[i] + ':' + ports[i]
                yield item
        self.count += 1
        yield Request(self.start_urls + self.count, callback=self.parse)   #response.xpath('id("db-rec-section")/div/dl[1]/dt/a/@href').extract()[0].encode('utf-8'),
