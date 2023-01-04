from gmx import Gmx
import time
import signal
import cv2

# stop this program
interrupted = False
def signal_handler(signal, frame):
    global interrupted
    interrupted = True
signal.signal(signal.SIGINT, signal_handler)


if __name__ == "__main__":

    # read file to get email
    file = open("mail_live.txt", "r")
    mails = file.readlines()
    number_mails = len(mails)
    using_mail = 0

    # GMX start
    gmx = Gmx()
    gmx.open_url("https:/gmx.com")

    #state machine
    step = 0

    while(True):
        # gmx.close_ads()
        gmx.close_policy()
        # gmx.close_popup()

        if (step == 0):
            user, password = mails[using_mail].split(":")
            print(user)
            print(password)
            if (gmx.click_login_btn()):
                ret = gmx.insert_username(user)
                ret = gmx.insert_password(password)
                if (ret):
                    step = 1
        elif (step == 1):
            if (gmx.click_nav_mail()):
                step = 2
        elif (step == 2):
            if (gmx.click_setting()):
                step = 3
        elif (step == 3):
            if (gmx.click_alias_address()):
                step = 4
        elif (step == 4):
            print("step 4")

        # check stop signal
        if interrupted:
            quit()
    