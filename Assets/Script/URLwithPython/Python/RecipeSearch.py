from selenium.webdriver.common.keys import Keys
import time
from selenium import webdriver
from webdriver_manager.chrome import ChromeDriverManager
from selenium.webdriver.common.by import By
from selenium.webdriver.chrome.service import Service
import urllib.request
import sys

#유니티에서 레시피이름을 첫번째 인자로 받음
recipe_name = sys.argv[0]
#str = sys.argv[2]
#recipe_name = "간장파스타감자"
str = "C:/Users/zhun0/AppData/LocalLow/DefaultCompany/MyRecipe/recipe_image.jpg"

#recipe_name = "간장 파스타 감자"

def search_google_images(query, str):
    print(str)
    chrome_options = webdriver.ChromeOptions()
    driver = webdriver.Chrome(service=Service(ChromeDriverManager().install()), options=chrome_options)

    URL = 'https://www.google.co.kr/imghp'
    driver.get(url= URL)
    driver.implicitly_wait(time_to_wait=10) #이미지가 렌더링될 때까지 10초까지 기다려줌. 그전에 오픈되면 바로 실행됨
    keyElement = driver.find_element(By.XPATH, '/html/body/div[1]/div[3]/form/div[1]/div[1]/div[1]/div/div[2]/textarea') #검색창 요소 찾음
    #keyElement.send_keys('월드컵') #검색창에 검색어 입력
    keyElement.send_keys(query)
    keyElement.send_keys(Keys.RETURN) # 엔터키
    #input()

    #하나의 단일 이미지 검색을 위해 element 여러 장은 elements사용
    url = driver.find_element(By.CSS_SELECTOR, '#islrg > div.islrc > div:nth-child(2) > a.wXeWr.islib.nfEiy > div.bRMDJf.islir > img').get_attribute('src')

    urllib.request.urlretrieve(url, str)
    #print(url)
    #src = .... (이미지 속성값) 을 가져올 수 있음

# 구글 이미지 검색하여 이미지 다운로드
search_google_images(recipe_name, str)
