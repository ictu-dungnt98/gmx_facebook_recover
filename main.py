from selenium import webdriver
from selenium.webdriver.common.keys import Keys
from selenium.webdriver.common.by import By
from selenium.webdriver.support.ui import WebDriverWait
from selenium.webdriver.support import expected_conditions as EC
import time

PATH = "C:\Program Files (x86)\chromedriver.exe"
driver = webdriver.Chrome(PATH)
driver.get("https:/gmx.com")

# close ads
#.//*[@class='dialogOverlay']/div/div[1]/button
# print("close ads")
# try:
#     wait = WebDriverWait(driver, 20)
#     element = wait.until(EC.text_to_be_present_in_element(By.XPATH, ".//*[@class='dialogOverlay']/div/div[1]/button"))
# except:
#     print("not found text")
# else:
#     close_btn = driver.find_element(By.XPATH, ".//*[@class='dialogOverlay']/div/div[1]/button")
#     close_btn.click()

# close accept policy
print("accept policy")
try:
    element = WebDriverWait(driver, 50).until(EC.element_to_be_clickable((By.ID, "onetrust-accept-btn-handler")))
    element.click()
except:
    print("not found text")

# login button
print("login")
try:
    element = WebDriverWait(driver, 50).until(EC.element_to_be_clickable((By.ID, "login-button")))
    element.click()
except:
    print("not found text")

# login-email
print("fill user name")
try:
    element = WebDriverWait(driver, 50).until(EC.element_to_be_clickable((By.ID, "login-email")))
    element.send_keys("dungnt98_username")
except:
    print("not found text")

# login-password
print("fill user name")
try:
    element = WebDriverWait(driver, 50).until(EC.element_to_be_clickable((By.ID, "login-password")))
    element.send_keys("dungnt98_password")
except:
    print("not found text")

input("Press Enter to continue...")