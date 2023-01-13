from gmx import Gmx
from fb_getcode import Facebook

import time
import signal

STEP_READ_MAIL_LIVE = 0
STEP_LOGIN = 1
STEP_CLICK_NAV_MAIL = 2
STEP_CLICK_SETTING = 3
STEP_CLICK_ALIAS_SETTING = 4
STEP_DELETE_OLD_MAIL = STEP_CLICK_ALIAS_SETTING + 1
STEP_ADD_MAIL_DIE = STEP_DELETE_OLD_MAIL + 1
STEP_GET_CODE = STEP_ADD_MAIL_DIE + 1
STEP_PARSE_CODE = STEP_GET_CODE + 1
STEP_PARSE_CODE_DONE = STEP_PARSE_CODE + 1

if __name__ == "__main__":

    # read file to get email live
    file1 = open("mail_live.txt", "r")
    mails_live = file1.readlines()
    number_mails_live = len(mails_live)
    num_mail_live_in_use = 0

    # read file to get email die
    file2 = open("mail_check.txt", "r")
    mails_die = file2.readlines()
    number_mails_die = len(mails_die)
    num_mail_die_in_use = 0

    # get code
    mail_get_code = mails_die[0]

    # GMX start
    gmx = Gmx()
    gmx.open_url()

    #state machine
    step = STEP_READ_MAIL_LIVE

    while(True):
        if (step == STEP_READ_MAIL_LIVE):
            if (number_mails_live >= num_mail_live_in_use):
                user, password = mails_live[num_mail_live_in_use].split(":")
                num_mail_live_in_use += 1
                step = STEP_LOGIN

        elif (step == STEP_LOGIN):
            if (gmx.click_login_btn()):
                ret = gmx.insert_username(user)
                ret = gmx.insert_password(password)
                if (ret):
                    step = STEP_CLICK_NAV_MAIL
            else:
                gmx.refresh()

        elif (step == STEP_CLICK_NAV_MAIL):
            if (gmx.click_nav_mail()):
                step = STEP_CLICK_SETTING

        elif (step == STEP_CLICK_SETTING):
            if (gmx.click_setting()):
                step = STEP_CLICK_ALIAS_SETTING

        elif (step == STEP_CLICK_ALIAS_SETTING):
            if (gmx.click_alias_address()):
                step = STEP_DELETE_OLD_MAIL

        elif (step == STEP_DELETE_OLD_MAIL):
            if (gmx.delete_old_mail()):
                step = STEP_ADD_MAIL_DIE
            
        elif (step == STEP_ADD_MAIL_DIE):
            if (num_mail_die_in_use < number_mails_die):
                # add sub mail
                mail_die, mail_die_type = mails_die[num_mail_die_in_use].split("@")
                gmx.fill_die_mail(mail_die, mail_die_type)
                # mail get code
                mail_get_code = mails_die[num_mail_die_in_use]
                # next step
                num_mail_die_in_use += 1
                step = step + 1

        elif (step ==  STEP_GET_CODE):
            fb = Facebook(gmx.driver, mail_get_code)
            if (fb.get_code()):
                print("done")
                step = STEP_PARSE_CODE
            else:
                print("get code fail")
                step = STEP_DELETE_OLD_MAIL

        elif (step == STEP_PARSE_CODE):
            print("parse code")
            uid_code = gmx.read_code_received(mail_get_code)
            if (uid_code != ""):
                mail_uid_code = mail_get_code + '|' + result
                print(mail_uid_code)
                step = step + 1
            else:
                step = STEP_CLICK_NAV_MAIL
        elif (step == STEP_PARSE_CODE_DONE):
            print("parse code done")
            break