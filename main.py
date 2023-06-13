from browser import Browser
from fb_getcode import Facebook
import time
import signal
import os

STEP_OPEN_URL = 0
STEP_READ_EMAIL_LOGIN = STEP_OPEN_URL + 1
STEP_LOGIN_FILL_USER = STEP_READ_EMAIL_LOGIN + 1
STEP_LOGIN_FILL_PASS = STEP_LOGIN_FILL_USER + 1
STEP_ADD_MAIL = STEP_LOGIN_FILL_PASS + 1
STEP_REQUEST_FB_CODE = STEP_ADD_MAIL + 1
STEP_FILL_EMAIL_RECOVER = STEP_REQUEST_FB_CODE + 1
STEP_FIND = STEP_FILL_EMAIL_RECOVER + 1
STEP_CLICK_CONTINUE = STEP_FIND + 1
STEP_CLOSE_TAB = STEP_CLICK_CONTINUE + 1
NEXT_STEP = STEP_CLOSE_TAB + 1

_exit = 0
login_user, login_pass = "", ""

# Define a signal handler function
def signal_handler(sig, frame):
    global _exit 
    _exit = 1

if __name__ == "__main__":
    signal.signal(signal.SIGINT, signal_handler)

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

    # state machine
    step = STEP_OPEN_URL
    retry = 0
    try_login = 0

    while (True):
        if _exit == 1:
            exit(0)
            
        if (browser.check_page_working() == 1):
            continue

        if (browser.check_page_working_2() == 1):
            continue

        if (browser.context_insert_login_mail()):
            step = STEP_LOGIN_FILL_USER
        elif (browser.context_insert_login_pass()):
            step = STEP_LOGIN_FILL_PASS
        elif (browser.context_add_mail()):
            step = STEP_ADD_MAIL

        if (step == STEP_OPEN_URL):
            ret = browser.open_url("https://customer.xfinity.com/users/me/update-username")
            if (ret == 1):
                step = STEP_READ_EMAIL_LOGIN

        elif (step == STEP_READ_EMAIL_LOGIN):
            if (num_mail_login_in_use < number_emails_login):
                login_user, login_pass = login_emails[num_mail_login_in_use].split("|")
                num_mail_login_in_use += 1
                step = STEP_LOGIN_FILL_USER

        elif (step == STEP_LOGIN_FILL_USER):
            ret = browser.insert_username(login_user)
            if (ret):
                ret = browser.click_lets_go()
                step = STEP_LOGIN_FILL_PASS

        elif (step == STEP_LOGIN_FILL_PASS):
            ret = browser.insert_password(login_pass)
            if (ret):
                step = STEP_ADD_MAIL

        elif (step == STEP_ADD_MAIL):
            if (num_emails_add_in_use < number_emails_add):
                new_user_name, misc = emails_add[num_emails_add_in_use].split("@")
                # add sub mail
                ret = browser.add_sub_mail(login_pass, new_user_name)
                if (ret == 0):
                    continue

                if (ret == 2):
                    num_emails_add_in_use += 1
                    continue

                if (ret == 1):
                    step = step + 1
                    email_recover = emails_add[num_emails_add_in_use]
                    num_emails_add_in_use += 1

        # Open a new tab
        elif (step == STEP_REQUEST_FB_CODE):
            ret = browser.open_new_tab("https://www.facebook.com/login/identify/?ctx=recover&ars=facebook_login&from_login_screen=0")
            if (ret == 1):
                step = step + 1

        elif (step == STEP_FILL_EMAIL_RECOVER):
            ret = browser.fill_email_recover(email_recover)
            if (ret == 1):
                step += 1

        elif (step == STEP_FIND):
            ret = browser.click_find()
            if (ret == 1):
                ret = browser.check_search_email()
                if (ret == 1):
                    step = STEP_CLOSE_TAB
                else:
                    step += 1
            else:
                step += 1

        elif (step == STEP_CLICK_CONTINUE):
            ret = browser.check_try_another_way()
            if (ret == 1):
                ret = browser.click_continue()
                if (ret == 1):
                    step += 1
            else:
                ret = browser.click_continue()
                if (ret == 1):
                    step += 1

        elif (step == STEP_CLOSE_TAB):
            browser.close_current_tab()
            step = STEP_READ_EMAIL_LOGIN
