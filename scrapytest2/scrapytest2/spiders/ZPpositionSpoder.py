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
from scrapytest2.position import PositionItem
from scrapy import Request
import sys
import time
class positionSpider(CrawlSpider):
    name = "ZPposition"
    allowed_domains = []
    start_urls = ['http://sou.zhaopin.com/jobs/searchresult.ashx?jl=%E6%9D%AD%E5%B7%9E&kw=python&sm=0&sg=236ed19feb6241d1955d8f447a3f4dd3&p=1']#['https://book.douban.com']
    rules = []#[Rule(LinkExtractor(allow=['/suject/\d+/.+']))]
    URL = []
    urls = []
    def parse(self, response):
        x = response.xpath("//div[@class='top-fixed-box']//h1/text()").extract()
        position = PositionItem()
        if x:
            position['URL'] = response.url
            position['PositionName'] = response.xpath("//div[@class='top-fixed-box']//h1/text()").extract()[0]
            position['CompanyName'] = response.xpath("//div[@class='top-fixed-box']//h2/a/text()").extract()[0]
            position['MonthlySalaryL'] = response.xpath("//div[@class='terminalpage-left']/ul/li[1]/strong/text()").extract()[0]
            #position['MonthlySalaryH'] = response.xpath("//div[@class='top-fixed-box']//h2/a/text()").extract()
            position['WorkAddress'] = response.xpath("//div[@class='terminalpage-left']/ul/li[2]/strong/a/text()").extract()[0]
            position['Degree'] = response.xpath("//div[@class='terminalpage-left']/ul/li[6]/strong/text()").extract()[0]
            position['WorkExperience'] = response.xpath("//div[@class='terminalpage-left']/ul/li[5]/strong/text()").extract()[0]
            position['RecruitingNumbers'] = response.xpath("//div[@class='terminalpage-left']/ul/li[7]/strong/text()").extract()[0]
            position['PositionCategory'] = response.xpath("//div[@class='terminalpage-left']/ul/li[8]/strong/a/text()").extract()[0]
            position['PositionDescribe'] = response.xpath("//div[@class='terminalpage-left']//div[@class='tab-cont-box']//p/text()").extract()
            # position['CompanyIntroduction'] = response.xpath("//div[@class='top-fixed-box']//h2/a/text()").extract()
            # position['CompanyScale'] = response.xpath("//div[@class='top-fixed-box']//h2/a/text()").extract()
            # position['CompanyIndustry'] = response.xpath("//div[@class='top-fixed-box']//h2/a/text()").extract()
            # position['CompanyAddress'] = response.xpath("//div[@class='top-fixed-box']//h2/a/text()").extract()
            position['KeyWord'] = 'Python'

            #position.CompanyAddress = response.xpath("/html/body/div[6]/div[2]/div[1]/ul/li[4]/strong/text()").extract()[0]
        yield position
        # if self.URL >= 5:
        #     sys.exit()//*[@id="newlist_list_content_table"]/table[2]/tbody/tr[1]/td[1]/div/a
        urls = response.xpath('//*[@id="newlist_list_content_table"]//td[@class="zwmc"]//a/@href').extract()
        urls += response.xpath('//div[@class="pagesDown"]//li/a/@href').extract()
        for url in urls:
            if url in self.URL:
                continue
            elif url == "#":
                continue
            else:
                self.URL.append(url)
            yield Request(url, callback=self.parse)
            #// *[ @ id = "newlist_list_content_table"] / table[2] / tbody / tr[1] / td[1] / div / a
            #// *[ @ id = "newlist_list_content_table"] / table[2] / tbody / tr[1] / td[1] / div / a
