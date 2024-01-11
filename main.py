from browser import Browser
import time
import signal
import os

STEP_OPEN_URL = 0
STEP_READ_EMAIL_LOGIN = STEP_OPEN_URL + 1
STEP_LOGIN = STEP_READ_EMAIL_LOGIN + 1
STEP_SELECT_SUBJECT = STEP_LOGIN + 1
STEP_REQUEST_FB_CODE = STEP_SELECT_SUBJECT + 1
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

    # read account login
    file1 = open("login.txt", "r")
    account = file1.readlines()
    num_account = len(account)
    num_account_used = 0

    # GMX start
    browser = Browser()

    # state machine
    step = STEP_OPEN_URL
    retry = 0
    try_login = 0

    while (True):
        if _exit == 1:
            exit(0)
            
        if (step == STEP_OPEN_URL):
            # ret = browser.open_url("http://dangkytinchi.ictu.edu.vn/")
            ret = browser.open_url("http://quanlydaotao.tump.edu.vn/dhyd/login.aspx")
            if (ret == 1):
                step = STEP_READ_EMAIL_LOGIN

        elif (step == STEP_READ_EMAIL_LOGIN):
            if (num_account_used < num_account):
                login_user, login_pass = account[num_account_used].split("|")
                print("insert_username: " + login_user)
                print("insert_password: " + login_pass)
                step = STEP_LOGIN

        elif (step == STEP_LOGIN):
            login_user, login_pass = account[num_account_used].split("|")
            ret = browser.insert_username(login_user)
            ret = browser.insert_password(login_pass)
            if (ret):
                print("login succeess")
                step = STEP_SELECT_SUBJECT

        elif (step == STEP_SELECT_SUBJECT):
            browser.select_subject("Gây mê hồi sức (2 TC)")
            browser.select_subject("Kiểm soát nhiễm khuẩn (2 TC)")
            browser.select_subject("Mô phỏng nha khoa (3 TC)")
            browser.select_subject("Ngoại cơ sở (4 TC)")
            browser.select_subject("Nội cơ sở (4 TC)")
            browser.select_subject("TCQLYT - Chương trình YT quốc gia - Dân số (2 TC)")
            browser.select_subject("Tư tưởng Hồ Chí Minh (2 TC)")
            step = step + 1
        elif (step == STEP_REQUEST_FB_CODE):
            print("Register subject succeess")
            exit(0)