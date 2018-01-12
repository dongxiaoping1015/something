import scrapy

class BookItem(scrapy.Item):
    url = scrapy.Field()
    bookname = scrapy.Field()
    author = scrapy.Field()
    bookrate = scrapy.Field()
    ratepeople = scrapy.Field()
    image_urls = scrapy.Field()
    images = scrapy.Field()
