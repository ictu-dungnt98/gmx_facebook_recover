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

class Facebook:
    def __init__(self, _driver, _sub_mail):
        # self.driver
        self.driver = _driver
        self.sub_mail = _sub_mail
        self.url = "https://www.facebook.com/login/identify/?ctx=recover&ars=facebook_login&from_login_screen=0"

    def close(self):
        self.driver.close()

    def refresh(self):
        self.driver.refresh()

    def try_switch_to_default(self):
        try:
            self.driver.switch_to.default_content()
        except:
            pass

    # open web page
    def open_recovery_page(self):
        self.driver.execute_script("window.open('');")
        self.driver.switch_to.window(self.driver.window_handles[1])
        self.driver.get(self.url)
        self.driver.implicitly_wait(5)

    def get_code_step_1(self):
        retry = 0
        while (True):
            try:
                element = self.driver.find_element(By.XPATH, "//*[@id=\"identify_email\"]")
                element.clear()
                element.send_keys(self.sub_mail)
                self.driver.implicitly_wait(1)

                self.driver.find_element(By.NAME, "did_submit").click()
                self.driver.implicitly_wait(1)
                return 1
            except:
                pass

            try:
                elements = self.driver.find_elements(By.XPATH, "//*[@type='submit']").click()
                self.driver.implicitly_wait(1)
                elements[2].click()
            except:
                pass

            retry += 1
            if (retry >= 60):
                return 0

    def get_code_step_2(self):
        retry = 0
        while (True):
            try:
                if (self.driver.find_element(By.NAME, "recover_method").is_displayed()):
                    elements = self.driver.find_elements(By.XPATH, "//*[@id=\"initiate_interstitial\"]/div[3]/div/div[1]/button")
                    elements[1].click()
                    self.driver.implicitly_wait(1)
                    return 1
                else:
                    print("Fail on click recover_method / reset_action")
                    return 0
            except:
                pass

            # try:
            #     if (self.driver.find_element(By.NAME, "email").is_displayed()):
            #         print("Facebook not found this email account")
            #         return 0
            # except:
            #     pass

            # //*[@id="initiate_interstitial"]/div[3]/div/div[1]/button
            try:
                self.driver.find_element(By.XPATH, "//*[@id=\"initiate_interstitial\"]/div[3]/div/div[1]/button").click()
                return 1
            except:
                pass

            retry += 1
            if (retry >= 60):
                return 0
    
    def get_code_step_3(self):
        retry = 0
        while (True):
            try:
                if (self.driver.find_element(By.ID, 'recovery_code_entry').is_displayed()):
                    return 1
            except:
                pass

            retry += 1
            if (retry >= 60):
                return 0

    def get_code(self):
        self.open_recovery_page()
        if (self.get_code_step_1()):
            if (self.get_code_step_2()):
                if (self.get_code_step_3()):
                    # self.close()
                    return 1
                pass
        return 0