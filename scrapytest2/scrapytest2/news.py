import scrapy

class NewsItem(scrapy.Item):
    url = scrapy.Field()
    news_title = scrapy.Field()
