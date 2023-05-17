from selenium import webdriver
import urllib.request
import os


def get_first_image_url(recipe_name):
    # ChromeDriver 경로 설정 (다운로드한 ChromeDriver의 경로로 설정)
    driver_path = 'path/to/chromedriver'

    # Selenium WebDriver 설정
    options = webdriver.ChromeOptions()
    options.add_argument('--headless')  # 브라우저 창 숨기기
    driver = webdriver.Chrome(driver_path, options=options)

    # Google 이미지 검색 페이지 열기
    search_query = recipe_name + ' recipe'
    google_image_search_url = f'https://www.google.com/search?tbm=isch&q={search_query}'
    driver.get(google_image_search_url)

    # 첫 번째 이미지 URL 가져오기
    first_image = driver.find_element_by_css_selector('.rg_i')
    image_url = first_image.get_attribute('src')

    # WebDriver 종료
    driver.quit()

    return image_url

def download_image(url, save_path):
    # 이미지 다운로드
    urllib.request.urlretrieve(url, save_path)

# 레시피 이름 입력
recipe_name = input('레시피 이름을 입력하세요: ')

# 첫 번째 이미지 URL 가져오기
first_image_url = get_first_image_url(recipe_name)

# 이미지 다운로드
image_save_path = os.path.join(os.getcwd(), 'Assets/Image/image.jpg')  # 이미지 저장 경로 지정
download_image(first_image_url, image_save_path)
