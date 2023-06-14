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

class Browser:
    def __init__(self, path = "./chromedriver_win32/chromedriver.exe"):
        # self.driver
        self.chrome_driver_parth = path
        self.options = Options()
        self.options.add_argument('--ignore-certificate-errors')
        self.options.add_argument('--ignore-ssl-errors')
        self.options.add_argument('--disable-software-rasterizer')
        self.driver = webdriver.Chrome(self.chrome_driver_parth, chrome_options=self.options)
        self.policy_pass = 0
        self.ads_close = 0
        self.time_check_context = 1

    # open web page
    def open_url(self, url = "https://customer.xfinity.com/users/me/update-username"):
        try:
            self.driver.get(url)
            # self.driver.maximize_window()
            # self.driver.implicitly_wait(5)
        except:
            return 0
        else:
            return 1
    def check_page_working(self):
        try:
            element = self.driver.find_element(By.XPATH, "//*[@id='reload-button']")
            element.click()
            self.driver.implicitly_wait(2)
        except:
            return 0
        else:
            print("reload page by check_page_working")
            self.refresh()
            return 1

    def check_page_working_2(self):
        try:
            element = self.driver.find_element(By.XPATH, "//*[@id='page-content']/main/div/div/div/div[1]/h1").text
            print(element)
            if (element.__contains__("We'll be back in a bit.")):
                print("reload page by check_page_working_2")
                self.refresh()
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
        print("insert_username: " + user)
        try:
            wait = WebDriverWait(self.driver, 3)  # Maximum wait time of 3 seconds
            element = wait.until(EC.presence_of_element_located((By.ID, "user")))
            self.driver.execute_script("arguments[0].value = '';", element)
            element.clear()
            element.send_keys(user)
            return 1
        except:
            return 0

    # login-password
    def insert_password(self, password):
        print("insert_password")
        try:
            wait = WebDriverWait(self.driver, 3)  # Maximum wait time of 3 seconds
            element = wait.until(EC.presence_of_element_located((By.ID, "passwd")))
            self.driver.execute_script("arguments[0].value = '';", element)
            element.clear()
            element.send_keys(password)
            return 1
        except:
            return 0

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

    def context_insert_login_pass(self):
        print("context_insert_login_pass")
        try:
            wait = WebDriverWait(self.driver, self.time_check_context)  # Maximum wait time of 3 seconds
            element = wait.until(EC.presence_of_element_located((By.XPATH, "//*[@id='right']/div/form/prism-text[2]")))
            print(element.text)
            if (element.text.__contains__("Enter your password to sign in securely")):
                print("correct context")
                return 1
            else:
                return 0
        except:
            return 0

    def context_add_mail(self):
        print("context_add_mail")
        try:
            wait = WebDriverWait(self.driver, self.time_check_context)  # Maximum wait time of 3 seconds
            element = wait.until(EC.presence_of_element_located((By.XPATH, "//*[@id='page-content']/div/div/div/form/div[1]/h1")))
            print(element.text)
            if (element.text.__contains__("Update Your Xfinity Username")):
                print("correct context")
                return 1
            else:
                return 0
        except:
            return 0

    # confirm-password
    def insert_confirm_password(self, password):
        try:
            wait = WebDriverWait(self.driver, 3)  # Maximum wait time of 3 seconds
            element = wait.until(EC.presence_of_element_located((By.XPATH, "//*[@id='oldPassword']")))
            self.driver.execute_script("arguments[0].value = '';", element)
            element.clear()
            element.send_keys(password)
            return 1
        except:
            print("fill confirm pass fail")
            return 0

    # new-user
    def insert_new_user(self, user):
        try:
            wait = WebDriverWait(self.driver, 3)  # Maximum wait time of 3 seconds
            element = wait.until(EC.presence_of_element_located((By.XPATH, "//*[@id='newUsername']")))
            element.clear()
            self.driver.execute_script("arguments[0].value = '';", element)
            element.send_keys(user)
            return 1
        except:
            print("fill new user fail")
            return 0

    # button save
    def click_save(self):
        print("click_save")
        while(True):
            try:
                element = self.driver.find_element(By.XPATH, "//*[@id='page-content']/div/div/div/form/div[4]/div[1]/button")
                element.click()
            except:
                print("click_save not found text")
                return 0
            else:
                print("click_save success")
                return 1

    def back_to_login(self):
        print("check_save_mail")
        try:
            wait = WebDriverWait(self.driver, 5)  # Maximum wait time of 3 seconds
            element = wait.until(EC.presence_of_element_located((By.XPATH, "//*[@id='page-content']/div/div/div/a")))
            element.click()
            print("check_save_mail success")
            return 1
        except:
            print("check_save_mail not found text")
            return 0

    def check_add_mail_success(self):
        try:
            wait = WebDriverWait(self.driver, 3)  # Maximum wait time of 3 seconds
            element = wait.until(EC.presence_of_element_located((By.XPATH, "//*[@id='page-content']/div/div/div/h1")))
            print(element.text)
            if (element.text.__contains__("Your Xfinity username has been updated successfully.")):
                print("Your Xfinity username has been updated successfully.")
                return 1
        except:
            return 0

    def check_add_mail_fail(self):
        try:
            wait = WebDriverWait(self.driver, 3)  # Maximum wait time of 3 seconds
            element = wait.until(EC.presence_of_element_located((By.XPATH, "//*[@id='page-content']/div/div/div/form/p")))
            print(element.text)
            if (element.text.__contains__("This username is already taken")):
                print("This username is already taken")
                return 1
        except:
            return 0
    
    def check_same_old_user(self):
        try:
            wait = WebDriverWait(self.driver, 3)  # Maximum wait time of 3 seconds
            element = wait.until(EC.presence_of_element_located((By.XPATH, "//*[@id='error_newUsername']")))
            print(element.text)
            if (element.text.__contains__("Your new username cannot be the same as your old username")):
                print("Your new username cannot be the same as your old username")
                return 1
        except:
            return 0

    def check_pass_not_valid(self):
        try:
            wait = WebDriverWait(self.driver, 3)  # Maximum wait time of 3 seconds
            element = wait.until(EC.presence_of_element_located((By.XPATH, "//*[@id='page-content']/div/div/div/form/p")))
            print(element.text)
            if (element.text.__contains__("Secret Question update failed. Password not valid")):
                print("Secret Question update failed. Password not valid")
                return 1
        except:
            return 0

    def add_sub_mail(self, confirm_password, new_username):
        if (self.insert_confirm_password(confirm_password) == 0):
            return 0
        
        if (self.insert_new_user(new_username) == 0):
            return 0
        
        if (self.click_save() == 0):
            return 0

        if (self.check_add_mail_success()):
            print("add mail success: " + new_username)
            return 1
        elif (self.check_add_mail_fail()):
            print("add mail fail")
            return 2
        elif (self.check_same_old_user()):
            return 2
        elif (self.check_pass_not_valid()):
            return 2
        else:
            return 2

    # //*[@id="identify_email"]
    def fill_email_recover(self, email):
        print("email_recover: " + email)
        try:
            wait = WebDriverWait(self.driver, 3)  # Maximum wait time of 3 seconds
            element = wait.until(EC.presence_of_element_located((By.XPATH, "//*[@id='identify_email']")))
            element.clear()
            self.driver.execute_script("arguments[0].value = '';", element)
            element.send_keys(email)
        except:
            print("fill_email_recover fail")
            return 0
        else:
            return 1

    #//*[@id="did_submit"]
    def click_find(self):
        print("click_find")
        try:
            wait = WebDriverWait(self.driver, 5)  # Maximum wait time of 3 seconds
            element = wait.until(EC.presence_of_element_located((By.XPATH, "//*[@id='did_submit']")))
            element.click()
        except:
            print("click_find not found text")
            return 0
        else:
            print("click_find success")
            return 1

    #//*[@id="initiate_interstitial"]/div[3]/div/div[1]/button
    def click_continue(self):
        print("click_continue")
        try:
            wait = WebDriverWait(self.driver, 5)  # Maximum wait time of 3 seconds
            element = wait.until(EC.presence_of_element_located((By.XPATH, "//*[@id='initiate_interstitial']/div[3]/div/div[1]/button")))
            element.click()
        except:
            print("click_continue not found text")
            return 0
        else:
            print("click_continue success")
            return 1
    
    # //*[@id="identify_yourself_flow"]/div/div[2]/div[1]/div[2]
    def check_search_email(self):
        try:
            wait = WebDriverWait(self.driver, 5)  # Maximum wait time of 3 seconds
            element = wait.until(EC.presence_of_element_located((By.XPATH, "//*[@id='identify_yourself_flow']/div/div[2]/div[1]/div[2]")))
            print(element.text)
            if (element.text.__contains__("Your search did not return any results")):
                print("Your search did not return any results")
        except:
            return 0
        else:
            return 1

    # //*[@id="loginform"]/div[4]/a
    def check_try_another_way(self):
        print("check_try_another_way")
        try:
            wait = WebDriverWait(self.driver, 5)  # Maximum wait time of 3 seconds
            element = wait.until(EC.presence_of_element_located((By.XPATH, "//*[@id='loginform']/div[4]/a")))
            element.click()
        except:
            print("check_try_another_way not found text")
            return 0
        else:
            print("check_try_another_way success")
            return 1