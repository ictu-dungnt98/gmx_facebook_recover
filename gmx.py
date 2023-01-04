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
        self.policy_pass = 0
        self.ads_close = 0
    
    # open web page
    def open_url(self, url = "https:/gmx.com"):
        self.driver.get(url)
        self.driver.maximize_window()
        self.driver.implicitly_wait(5)

    # close ads
    def close_ads(self):
        if (self.ads_close == 0):
            try:
                element = self.driver.find_element(By.XPATH, "/html/body/div[2]/div/div/div[2]/a")
                element.click()
            except:
                print("close_ads not found text")
            else:
                self.ads_close = 1

    # close accept policy
    def close_policy(self):
        if (self.policy_pass == 0):
            try:
                iframe0 = self.driver.find_element(By.XPATH, "/html/body/div[3]/iframe")
                self.driver.switch_to.frame(iframe0)

                iframe1 = self.driver.find_element(By.XPATH, "/html/body/iframe")
                # attrs = self.driver.execute_script('var items = {}; for (index = 0; index < arguments[0].attributes.length; ++index) { items[arguments[0].attributes[index].name] = arguments[0].attributes[index].value }; return items;', iframe1)
                # print(attrs)
                self.driver.switch_to.frame(iframe1)

                element = self.driver.find_element(By.XPATH, "/html/body/div/div[3]/div/div/div[2]/div/div/button")
                element.click()
                self.driver.switch_to.default_content()
            except:
                print("close_policy not found text")
            else:
                self.policy_pass = 1

    # login button
    def click_login_btn(self):
        try:
            element = self.driver.find_element(By.ID, "login-button")
            # element = WebDriverWait(self.driver, 5).until(EC.element_to_be_clickable((By.ID, "login-button")))
            element.click()
        except:
            print("login not found text")
            return 0
        else:
            return 1

    # login-email
    def insert_username(self, user):
        try:
            # element = WebDriverWait(self.driver, 5).until(EC.element_to_be_clickable((By.ID, "login-email")))
            element = self.driver.find_element(By.ID, "login-email")
            element.send_keys(user)
        except:
            print("insert_username not found text")
            return 0
        else:
            return 1

    # login-password
    def insert_password(self, password):
        try:
            # element = WebDriverWait(self.driver, 5).until(EC.element_to_be_clickable((By.ID, "login-password")))
            element = self.driver.find_element(By.ID, "login-password")
            element.send_keys(password)
        except:
            print("insert_password not found text")
            return 0
        else:
            return 1

    def click_nav_mail(self):
        try:
            # element = WebDriverWait(self.driver, 5).until(EC.element_to_be_clickable((By.XPATH, "//*[name()='use' and @*='#nav_mail']")))
            element = self.driver.find_element(By.XPATH, "//*[name()='use' and @*='#nav_mail']")
            element.click()
            self.driver.implicitly_wait(2)
        except:
            print("click_nav_mail not found text")
            return 0
        else:
            return 1

    def click_setting(self):
        try:
            iframe = self.driver.find_element(By.XPATH, "//*[@id=\"thirdPartyFrame_mail\"]")
            self.driver.switch_to.frame(iframe)

            element = self.driver.find_element(By.XPATH, "//a[@id=\"navigationSettingsLink\"]")
            element.click()
            self.driver.switch_to.default_content()
            self.driver.implicitly_wait(2)
        except:
            return 0
        else:
            return 1

    def click_alias_address(self):
        try:
            self.driver.implicitly_wait(2)
            iframe = self.driver.find_element(By.XPATH, "//*[@id=\"thirdPartyFrame_mail\"]")
            self.driver.switch_to.frame(iframe)
            element = self.driver.find_element(By.XPATH, "//*[@id=\"id8e\"]/div/div/div/ul/li[1]/div[2]/ul/li[5]/a")
            element.click()
        except:
            print("click_alias_address not found text")
            return 0
        else:
            self.driver.implicitly_wait(2)
            self.driver.switch_to.default_content()
            return 1
