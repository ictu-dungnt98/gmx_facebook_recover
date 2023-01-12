from selenium import webdriver
from selenium.webdriver.common.keys import Keys
from selenium.webdriver.common.by import By
from selenium.webdriver.support.ui import WebDriverWait
from selenium.webdriver.chrome.options import Options
from selenium.webdriver.support import expected_conditions as EC
from selenium.webdriver.support.ui import Select
from selenium.webdriver.remote.webelement import WebElement
from selenium.webdriver.common.action_chains import ActionChains
import time
import re as regex

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
            iframe0 = self.driver.find_element(By.XPATH, "//*[@id=\"thirdPartyFrame_permission_dialog\"]")
            if (iframe0.is_displayed()):
                self.driver.switch_to.frame(iframe0)

                try:
                    self.driver.find_element(By.XPATH, "//*[@class='btn btn-secondary ghost']").click()
                    self.driver.switch_to.default_content()
                    return
                except:
                    pass

                try:
                    self.driver.find_element(By.ID, "onetrust-accept-btn-handler").click()
                    self.driver.switch_to.default_content()
                    return
                except:
                    pass

                self.driver.switch_to.frame(self.driver.find_element(By.XPATH, "//*[@id=\"permission-iframe\"]"))
                try:
                    self.driver.find_element(By.ID, "onetrust-accept-btn-handler").click()
                    self.driver.switch_to.default_content()
                    return
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
        while (True):
            try:
                element = self.driver.find_element(By.XPATH, "//*[name()='use' and @*='#nav_mail']")
                element.click()
                self.driver.implicitly_wait(0.5)
                return 1
            except:
                pass

            self.close_policy()

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

            self.close_policy()

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
        while (True):
            try:
                self.driver.switch_to.frame(self.driver.find_element(By.NAME, "mail"))
                element = self.driver.find_element(By.XPATH, "//*[text()='Alias Addresses']")
                element.click()
                return 1
            except:
                pass

            self.try_switch_to_default()
            self.close_policy()

    def delete_old_mail(self):
        retry = 0
        while(True):
            try:
                self.driver.switch_to.frame(self.driver.find_element(By.NAME, "mail"))

                elements = self.driver.find_elements(By.XPATH,"//*[@class='table_field table_col-12']")
                len_elements = len(elements)

                # not found any old mail to delete
                if (len_elements <= 2):
                    return 1
                
                # found old-mail need be removed IL_C2F:
                i = 0
                actions = ActionChains(self.driver)
                while (True):
                    if (("Default sender address" in elements[i].text) or ("@" not in elements[i].text)):
                        i += 1 # ignore two first lines
                    else:
                        while (i < len_elements):
                            # move pointer to text line to make hover_icon visible
                            try:
                                actions.move_to_element(elements[i]).perform()
                            except:
                                pass
                            
                            while(True):
                                try:
                                    delete_btn = self.driver.find_elements(By.XPATH, "//*[@class='table-hover_icon icon-link']")
                                    if (len(delete_btn) >= 0):
                                        delete_btn[1].click()
                                        break
                                except:
                                    i += 1
                                    actions.move_to_element(elements[i]).perform()
                                    self.driver.implicitly_wait(0.5)
                                    pass
                            
                            while (True):
                                # click accept notification
                                try:
                                    self.driver.find_element(By.XPATH, "//*[@data-webdriver='primary']").click()
                                    self.driver.implicitly_wait(0.5)
                                    i += 1
                                    break
                                except:
                                    pass

                        # Not found mail to remove 
                        return 1
            except:
                # if fail to locate element, try again
                self.try_switch_to_default()
                pass

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
                self.try_switch_to_default()
                pass
    
    def read_code_received(self):
        retry = 0
        while (True):
            try:
                self.driver.switch_to.window(self.driver.window_handles[0])
                while (retry < 20):
                    self.driver.refresh()
                    self.driver.implicitly_wait(1)
                    num = 0

                    # read code
                    while (True):
                        try:
                            self.driver.switch_to.frame(self.driver.find_element(By.NAME, "mail"))
                            self.driver.find_element(By.XPATH, "//*[contains(@class,'refresh navigation-tool-icon-link')]").click()
                            self.driver.implicitly_wait(1)
                        except:
                            pass
                        
                        self.try_switch_to_default()

                        try:
                            self.driver.switch_to.frame(self.driver.find_element(By.NAME, "mail"))
                            elements1 = self.driver.find_elements(By.XPATH, "//*[contains(@class,'name')]")
                            elements2 = self.driver.find_elements(By.XPATH, "//*[contains(@class,'subject')]")

                            # //*[@id="id69"]/td[2]/div[1]/div[1]
                            if (elements1[0].text.__contains__("Facebook")):
                                code = regex.findall("\d", elements2[0].text.split(" ")[0])
                                print("code {}".format(code))
                                if (len(code) == 6):
                                    elements1[0].click()
                                    self.driver.implicitly_wait(1)
                                    break
                                else:
                                    print("len(code): {}", len(code))
                        except:
                            pass
                    
                    # save code into file
            except:
                pass