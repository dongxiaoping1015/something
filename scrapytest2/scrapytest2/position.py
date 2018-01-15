import scrapy

class PositionItem(scrapy.Item):
    URL = scrapy.Field()
    PositionName = scrapy.Field()
    CompanyName = scrapy.Field()
    MonthlySalaryL = scrapy.Field()
    MonthlySalaryH = scrapy.Field()
    WorkAddress = scrapy.Field()
    Degree = scrapy.Field()
    WorkExperience = scrapy.Field()
    RecruitingNumbers = scrapy.Field()
    PositionCategory = scrapy.Field()
    PositionDescribe = scrapy.Field()
    CompanyIntroduction = scrapy.Field()
    CompanyScale = scrapy.Field()
    CompanyIndustry = scrapy.Field()
    CompanyAddress = scrapy.Field()
    KeyWord = scrapy.Field()
