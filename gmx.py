from selenium import webdriver
from selenium.webdriver.common.keys import Keys
from selenium.webdriver.common.by import By
from selenium.webdriver.support.ui import WebDriverWait
from selenium.webdriver.chrome.options import Options
from selenium.webdriver.support import expected_conditions as EC
from selenium.webdriver.support.ui import Select
import time

class Gmx:
    def __init__(self, path = "C:\Program Files (x86)\chromedriver.exe"):
        # self.driver
        self.PATH = path
        self.options = Options()
        self.options.add_argument('--ignore-certificate-errors')
        self.options.add_argument('--ignore-ssl-errors')
        self.options.add_argument('--disable-software-rasterizer')
        self.driver = webdriver.Chrome(self.PATH, chrome_options=self.options)
        self.policy_pass = 0
        self.ads_close = 0
    
    # open web page
    def open_url(self, url = "https://www.gmx.com/#.1559516-header-navlogin2-1"):
        self.driver.get(url)
        self.driver.maximize_window()
        self.driver.implicitly_wait(5)

    def close(self):
        self.driver.close()

    def refresh(self):
        self.driver.refresh()

    def try_switch_to_default(self):
        try:
            self.driver.switch_to.default_content()
        except:
            pass

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
        try:
            iframe0 = self.driver.find_element(By.XPATH, "//*[@class='permission-core-iframe")
            if (iframe0.is_displayed()):
                self.driver.switch_to.frame(iframe0)

                try:
                    self.driver.find_element(By.XPATH, "//*[@class='btn btn-secondary ghost']").click()
                except:
                    pass

                try:
                    self.driver.find_element(By.ID, "onetrust-accept-btn-handler").click()
                except:
                    pass

                self.driver.switch_to.frame(self.driver.find_element(By.XPATH, "/html/body/iframe"))
                try:
                    self.driver.find_element(By.XPATH, "//*[@class='btn btn-secondary ghost']").click()
                except:
                    pass

                try:
                    self.driver.find_element(By.ID, "onetrust-accept-btn-handler").click()
                except:
                    pass

                try:
                    self.driver.find_element(By.ID, "accept-recommended-btn-handler").click()
                except:
                    pass

                self.driver.switch_to.default_content()
        except:
            print("close_policy not found text")

    # login button
    def click_login_btn(self):
        try:
            element = self.driver.find_element(By.ID, "login-button")
            element.click()
        except:
            print("login not found text")
            return 0
        else:
            return 1

    # login-email
    def insert_username(self, user):
        element = self.driver.find_element(By.ID, "login-email")
        element.send_keys(user)
        return 1

    # login-password
    def insert_password(self, password):
        element = self.driver.find_element(By.ID, "login-password")
        element.send_keys(password)
        return 1

    def click_nav_mail(self):
        try:
            element = self.driver.find_element(By.XPATH, "//*[name()='use' and @*='#nav_mail']")
            element.click()
            self.driver.implicitly_wait(0.5)
            return 1
        except:
            return 0

    def click_setting(self):
        while (True):
            try:
                self.driver.switch_to.frame(self.driver.find_element(By.NAME, "mail"))
                element = self.driver.find_element(By.XPATH, "//*[@data-webdriver='FolderNavigation:MailCollectorLink']")
                element.click()
                self.driver.implicitly_wait(0.5)
                return 1
            except:
                pass

            self.try_switch_to_default()

            try:
                element = self.driver.find_element(By.XPATH, "//*[@class='ftd-box-promote_close icon delete']")
                element.click()
            except:
                pass

            try:
                if (self.driver.find_element(By.NAME, "permission_dialog").is_displayed()):
                    self.driver.switch_to.frame(self.driver.find_element(By.NAME, "permission_dialog"))
                    self.driver.switch_to.frame(self.driver.find_element(By.NAME, "permission-iframe"))
                    try:
                        element = self.driver.find_element(By.XPATH, "//*[@class='btn btn-secondary ghost']")
                        element.click()
                    except:
                        pass

                    element = self.driver.find_element(By.XPATH, "//*[text()='Zustimmen und weiter']")
                    element.click()
            except:
                pass
            self.try_switch_to_default()

    def click_alias_address(self):
        ret = 1
        try:
            self.driver.switch_to.frame(self.driver.find_element(By.NAME, "mail"))
            element = self.driver.find_element(By.XPATH, "//*[text()='Alias Addresses']")
            element.click()
        except:
            ret = 0
        
        self.try_switch_to_default()

        return ret

    def fill_die_mail(self, die_mail, die_mail_type):
        print(die_mail)
        while(True):
            try:
                self.driver.switch_to.frame(self.driver.find_element(By.NAME, "mail"))
                element = self.driver.find_element(By.XPATH, "//*[@class='form-element form-element-textfield textfield']")
                element.clear()
                element.send_keys(die_mail)

                select = self.driver.find_elements(By.XPATH, "//*[@class='form-element form-element-select']")
                select[0].send_keys(die_mail_type)
        
                element = self.driver.find_elements(By.XPATH, "//*[@class='m-button button-secondary button-size-normal']")
                element[0].click()
                return
            except:
                pass
            
            self.try_switch_to_default()
