from gmx import Gmx
import time
import signal

STEP_READ_MAIL_LIVE = 0
STEP_LOGIN = 1
STEP_CLICK_NAV_MAIL = 2
STEP_CLICK_SETTING = 3
STEP_CLICK_ALIAS_SETTING = 4
STEP_DELETE_OLD_MAIL = STEP_CLICK_ALIAS_SETTING + 1
STEP_ADD_MAIL_DIE = STEP_DELETE_OLD_MAIL + 1
STEP_DONE = STEP_ADD_MAIL_DIE + 1

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

    # GMX start
    gmx = Gmx()
    gmx.open_url()

    #state machine
    step = STEP_READ_MAIL_LIVE

    while(True):
        gmx.close_policy()
        gmx.close_ads()

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
            gmx.delete_old_mail()
            step = STEP_ADD_MAIL_DIE
            
        elif (step == STEP_ADD_MAIL_DIE):
            mail_die, mail_die_type = mails_die[num_mail_die_in_use].split("@")
            num_mail_die_in_use += 1
            
            gmx.fill_die_mail(mail_die, mail_die_type)
            step = step + 1
        elif (STEP_DONE):
            print("done")