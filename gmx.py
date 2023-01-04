from selenium import webdriver
from selenium.webdriver.common.keys import Keys
from selenium.webdriver.common.by import By
from selenium.webdriver.support.ui import WebDriverWait
from selenium.webdriver.support import expected_conditions as EC
import time

class Gmx:
    def __init__(self, path = "C:\Program Files (x86)\chromedriver.exe"):
        # self.driver
        self.PATH = path
        self.driver = webdriver.Chrome(self.PATH)
    
    # open web page
    def open_url(self, url = "https:/gmx.com"):
        self.driver.get(url)
        self.driver.maximize_window()
        self.driver.implicitly_wait(10)

    # close ads
    def close_ads(self):
        print("close ads")
        try:
            # element = WebDriverWait(self.driver, 10).until(EC.text_to_be_present_in_element(By.CLASS_NAME, "js-shownomore"))
            element = self.driver.find_element(By.CLASS_NAME, "js-shownomore")
            element.click()
        except:
            print("not found text")

    # close accept policy
    def close_policy(self):
        iframe0 = self.driver.find_element(By.XPATH, "/html/body/div[3]/iframe")
        self.driver.switch_to.frame(iframe0)

        iframe1 = self.driver.find_element(By.XPATH, "/html/body/iframe")
        self.driver.switch_to.frame(iframe1)

        element = self.driver.find_element(By.ID, "onetrust-accept-btn-handler")
        element.click()
        self.driver.switch_to.default_content()

    def close_popup(self):
        parent = self.driver.current_window_handle
        uselessWindows = self.driver.window_handles
        self.driver.switch_to.window(uselessWindows[-1])
        self.driver.close()
        self.driver.switch_to.window(uselessWindows[0])

    # login button
    def click_login_btn(self):
        print("login")
        try:
            element = WebDriverWait(self.driver, 5).until(EC.element_to_be_clickable((By.ID, "login-button")))
            element.click()
        except:
            print("not found text")
            return 0
        else:
            return 1

    # login-email
    def insert_username(self, user):
        print("fill user")
        try:
            element = WebDriverWait(self.driver, 5).until(EC.element_to_be_clickable((By.ID, "login-email")))
            element.send_keys(user)
        except:
            print("insert_username not found text")
            return 0
        else:
            return 1

    # login-password
    def insert_password(self, password):
        print("fill password")
        try:
            element = WebDriverWait(self.driver, 5).until(EC.element_to_be_clickable((By.ID, "login-password")))
            element.send_keys(password)
        except:
            print("insert_password not found text")
            return 0
        else:
            return 1

    # do enter
    def do_enter(self):
        try:
            element = WebDriverWait(self.driver, 5).until(EC.element_to_be_clickable((By.ID, "login-password")))
            element.send_keys(Keys.RETURN)
        except:
            print("do_enter not found text")
            return 0
        else:
            return 1

    # //*[name()='use' and @*='#nav_mail']
    def click_nav_mail(self):
        try:
            # element = self.driver.find_element(By.XPATH, "//*[name()='use' and @*='#nav_mail']")
            element = WebDriverWait(self.driver, 5).until(EC.element_to_be_clickable((By.XPATH, "//*[name()='use' and @*='#nav_mail']")))
            element.click()
        except:
            print("click_nav_mail not found text")
            return 0
        else:
            return 1

    def click_setting(self):
        try:
            element = WebDriverWait(self.driver, 5).until(EC.element_to_be_clickable((By.XPATH, "//a[@id=\"navigationSettingsLink\"]")))
            element.click()
        except Exception as e:
            print(str(e))
            return 0
        else:
            return 1

    def click_alias_address(self):
        try:
            element = self.driver.find_element(By.ID, "id8e")
            element.click()
        except:
            print("click_alias_address not found text")
            return 0
        else:
            return 1