from selenium import webdriver
from selenium.webdriver.common.keys import Keys
from selenium.webdriver.common.by import By
from selenium.webdriver.support.ui import WebDriverWait
from selenium.webdriver.support import expected_conditions as EC
import time
import signal

interrupted = False

def signal_handler(signal, frame):
    global interrupted
    interrupted = True

signal.signal(signal.SIGINT, signal_handler)

PATH = "C:\Program Files (x86)\chromedriver.exe"
driver = webdriver.Chrome(PATH)

# close ads
def close_ads():
    print("close ads")
    try:
        # element = WebDriverWait(driver, 10).until(EC.text_to_be_present_in_element(By.CLASS_NAME, "js-shownomore"))
        element = driver.find_element(By.CLASS_NAME, "js-shownomore")
        element.click()
    except:
        print("not found text")

# close accept policy
def close_policy():
    print("accept policy")
    try:
        # element = WebDriverWait(driver, 10).until(EC.element_to_be_clickable((By.ID, "onetrust-accept-btn-handler")))
        element = driver.find_element(By.ID, "onetrust-accept-btn-handler")
        element.click()
    except:
        print("not found text")

# login button
def click_login_btn():
    print("login")
    try:
        element = WebDriverWait(driver, 10).until(EC.element_to_be_clickable((By.ID, "login-button")))
        element.click()
    except:
        print("not found text")
        return 0
    else:
        return 1

# login-email
def insert_username():
    print("fill user name")
    try:
        element = WebDriverWait(driver, 10).until(EC.element_to_be_clickable((By.ID, "login-email")))
        element.send_keys("dungnt98_username")
    except:
        print("not found text")
        return 0
    else:
        return 1

# login-password
def insert_password():
    print("fill user name")
    try:
        element = WebDriverWait(driver, 10).until(EC.element_to_be_clickable((By.ID, "login-password")))
        element.send_keys("dungnt98_password")
    except:
        print("not found text")
        return 0
    else:
        return 1

# do enter
def do_enter():
    try:
        element = WebDriverWait(driver, 10).until(EC.element_to_be_clickable((By.ID, "login-password")))
        element.send_keys(Keys.RETURN)
    except:
        print("not found text")
        return 0
    else:
        return 1

if __name__ == "__main__":
    driver.get("https:/gmx.com")
    while(True):
        close_ads()
        close_policy()
        if (click_login_btn()):
            ret = insert_username()
            ret = insert_password()
            if (ret):
                do_enter()
                input('wait key')

        if interrupted:
            quit()
    