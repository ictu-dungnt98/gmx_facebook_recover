from browser import Browser
from fb_getcode import Facebook
import time
import signal
import os

STEP_READ_EMAIL_LOGIN = 0
STEP_LOGIN = STEP_READ_EMAIL_LOGIN + 1
STEP_ADD_MAIL = STEP_LOGIN + 1
STEP_REQUEST_FB_CODE = STEP_ADD_MAIL + 1
STEP_CLICK_ALIAS_SETTING = STEP_REQUEST_FB_CODE + 1
STEP_DELETE_OLD_MAIL = STEP_CLICK_ALIAS_SETTING + 1
STEP_ADD_MAIL_DIE = STEP_DELETE_OLD_MAIL + 1
STEP_GET_CODE = STEP_ADD_MAIL_DIE + 1
STEP_PARSE_CODE = STEP_GET_CODE + 1
STEP_PARSE_CODE_DONE = STEP_PARSE_CODE + 1

if __name__ == "__main__":
    # read mail login
    file1 = open("mail_login.txt", "r")
    login_emails = file1.readlines()
    number_emails_login = len(login_emails)
    num_mail_login_in_use = 0

    # read mail add
    file2 = open("input_data.txt", "r")
    emails_add = file2.readlines()
    number_emails_add = len(emails_add)
    num_emails_add_in_use = 0

    # clear output file
    try:
        os.remove("output.txt")
    except:
        pass

    # GMX start
    browser = Browser()
    ret = browser.open_url("https://customer.xfinity.com/users/me/update-username")
    if (ret == 0):
        exit(-1)

    # state machine
    step = STEP_READ_EMAIL_LOGIN

    while (True):
        if (browser.check_page_working() == 1):
            continue

        if (browser.check_page_working_2() == 1):
            continue

        if (step == STEP_READ_EMAIL_LOGIN):
            if (number_emails_login >= num_mail_login_in_use):
                login_user, login_pass = login_emails[num_mail_login_in_use].split("|")
                num_mail_login_in_use += 1
                step = STEP_LOGIN

        elif (step == STEP_LOGIN):
            ret = browser.insert_username(login_user)
            if (ret == 0):
                browser.refresh()
                continue

            ret = browser.click_lets_go()
            if (ret == 0):
                browser.refresh()
                continue
            
            ret = browser.insert_password(login_pass)
            if (ret == 0):
                browser.refresh()
                continue
            
            if (ret):
                step = STEP_ADD_MAIL

        elif (step == STEP_ADD_MAIL):
            if (num_emails_add_in_use < number_emails_add):
                new_user_name, misc = emails_add[num_emails_add_in_use].split("@")
                print("new_user_name: " + new_user_name)
                ret = browser.fill_confirmuser_newusername(login_pass, new_user_name)
                
                if (ret == 0):
                    continue
                
                if (ret == 1):
                    num_emails_add_in_use += 1
                    continue
            
                if (ret == 1):
                    step = step + 1

        elif (step == STEP_REQUEST_FB_CODE):
            pass
            