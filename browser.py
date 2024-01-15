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

from selenium.common.exceptions import *

class Browser:
    def __init__(self):
        # self.driver
        self.driver = webdriver.Chrome()
        self.policy_pass = 0
        self.ads_close = 0
        self.time_check_context = 1

    # open web page
    def open_url(self, url = "google.com"):
        try:
            self.driver.get(url)
        except:
            return 0
        else:
            return 1

    def close(self):
        self.driver.close()

    def close_current_tab(self):
        # Close the current tab
        self.driver.close()
        # Switch back to the original tab (if needed)
        self.driver.switch_to.window(self.driver.window_handles[0])

    def quit(self):
        self.driver.quit()

    def refresh(self):
        self.driver.refresh()
        self.driver.implicitly_wait(2)

    def try_switch_to_default(self):
        try:
            self.driver.switch_to.default_content()
        except:
            pass
    def open_new_tab(self, url):
        self.driver.execute_script("window.open('');")
        # Switch to the newly opened tab
        self.driver.switch_to.window(self.driver.window_handles[-1])
        # Navigate to a webpage in the new tab
        self.driver.get(url)
        return 1

    # login button
    def click_lets_go(self):
        try:
            element = self.driver.find_element(By.ID, "sign_in")
            element.click()
        except:
            print("click_lets_go not found text")
            return 0
        else:
            return 1

    # sign in button
    def click_sign_in(self):
        while(True):
            try:
                element = self.driver.find_element(By.XPATH, "//*[@id='sign_in']")
                element.click()
                return 1
            except:
                print("click_sign_in not found text")
                return 0

    # login-email
    def insert_username(self, user):
        try:
            wait = WebDriverWait(self.driver, 3)  # Maximum wait time of 3 seconds
            element = wait.until(EC.presence_of_element_located((By.XPATH, "//*[@id='txtUserName']")))
            self.driver.execute_script("arguments[0].value = '';", element)
            element.clear()
            element.send_keys(user)
            return 1
        except:
            return 0

    # login-password
    def insert_password(self, password):
        try:
            wait = WebDriverWait(self.driver, 3)  # Maximum wait time of 3 seconds
            element = wait.until(EC.presence_of_element_located((By.XPATH, "//*[@id='txtPassword']")))
            self.driver.execute_script("arguments[0].value = '';", element)
            element.clear()
            element.send_keys(password)
            element.send_keys(Keys.ENTER)
            return 1
        except UnexpectedAlertPresentException:
            try:
                alert = self.driver.switch_to.alert
                alert.accept()
                return 1
            except NoAlertPresentException:
                return 1
        except:
            print("insert_password error")
            return 0
    
    def select_subject(self, subject_string):
        try:
            wait = WebDriverWait(self.driver, 3)
            element = wait.until(EC.presence_of_element_located((By.XPATH, "//*[@id='drpCourse']")))
            # chon mon hoc
            select = Select(element)
            select.select_by_visible_text(subject_string)
            # click hien thi lop
            element = wait.until(EC.presence_of_element_located((By.XPATH, "//*[@id='btnViewCourseClass']")))
            element.click()
            # click chon lop hoc phan
            if (subject_string == "Gây mê hồi sức (2 TC)"):
                # click chọn học phần lý thuyết
                element = wait.until(EC.presence_of_element_located((By.XPATH, "/html/body/form/table[3]/tbody/tr[7]/td/div/table/tbody/tr[5]/td[2]/input[1]")))
                element.click()
                # click chọn học phần thực hành
                element = wait.until(EC.presence_of_element_located((By.XPATH, "/html/body/form/table[3]/tbody/tr[7]/td/div/table/tbody/tr[7]/td[2]/input[1]")))
                element.click()
            elif (subject_string == "Kiểm soát nhiễm khuẩn (2 TC)"):
                # click chon hoc phan ly thuyet
                element = wait.until(EC.presence_of_element_located((By.XPATH, "/html/body/form/table[3]/tbody/tr[7]/td/div/table/tbody/tr[4]/td[2]/input[1]")))
                element.click()
                # click chọn học phần thực hành
                element = wait.until(EC.presence_of_element_located((By.XPATH, "/html/body/form/table[3]/tbody/tr[7]/td/div/table/tbody/tr[5]/td[2]/input[1]")))
                element.click()
            elif (subject_string == "Mô phỏng nha khoa (3 TC)"):
                # click chon hoc phan ly thuyet
                element = wait.until(EC.presence_of_element_located((By.XPATH, "/html/body/form/table[3]/tbody/tr[7]/td/div/table/tbody/tr[4]/td[2]/input[1]")))
                element.click()
            elif (subject_string == "Ngoại cơ sở (4 TC)"):
                # click chon hoc phan ly thuyet
                element = wait.until(EC.presence_of_element_located((By.XPATH, "/html/body/form/table[3]/tbody/tr[7]/td/div/table/tbody/tr[2]/td[2]/input[1]")))
                element.click()
                # click chọn học phần thực hành
                element = wait.until(EC.presence_of_element_located((By.XPATH, "/html/body/form/table[3]/tbody/tr[7]/td/div/table/tbody/tr[3]/td[2]/input[1]")))
                element.click()
            elif (subject_string == "Nội cơ sở (4 TC)"):
                # click chon hoc phan ly thuyet
                element = wait.until(EC.presence_of_element_located((By.XPATH, "/html/body/form/table[3]/tbody/tr[7]/td/div/table/tbody/tr[2]/td[2]/input[1]")))
                element.click()
                # click chọn học phần thực hành
                element = wait.until(EC.presence_of_element_located((By.XPATH, "/html/body/form/table[3]/tbody/tr[7]/td/div/table/tbody/tr[3]/td[2]/input[1]")))
                element.click()
            elif (subject_string == "TCQLYT - Chương trình YT quốc gia - Dân số (2 TC)"):
                # click chon hoc phan ly thuyet
                element = wait.until(EC.presence_of_element_located((By.XPATH, "/html/body/form/table[3]/tbody/tr[7]/td/div/table/tbody/tr[2]/td[2]/input[1]")))
                element.click()
            elif (subject_string == "Tư tưởng Hồ Chí Minh (2 TC)"):
                # click chon hoc phan ly thuyet
                element = wait.until(EC.presence_of_element_located((By.XPATH, "/html/body/form/table[3]/tbody/tr[7]/td/div/table/tbody/tr[2]/td[2]/input[1]")))
                element.click()
            # click dang ky
            element = wait.until(EC.presence_of_element_located((By.XPATH, "/html/body/form/table[4]/tbody/tr[1]/td/input[9]")))
            element.click()
        except:
            pass

    def context_insert_login_mail(self):
        print("context_insert_login_mail")
        try:
            wait = WebDriverWait(self.driver, self.time_check_context)  # Maximum wait time of 3 seconds
            element = wait.until(EC.presence_of_element_located((By.XPATH, "//*[@id='right']/div/form/prism-text[1]")))
            print(element.text)
            if (element.text.__contains__("Sign in with your Xfinity ID")):
                print("correct context")
                return 1
            else:
                return 0
        except:
            return 0
