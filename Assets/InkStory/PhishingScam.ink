-> open_notification

=== open_notification ===
+ [Player:action_scammer_notification Tap the notification] -> phishing_scam_intro

//------------------------------ PHISHING SCAM INTRO ------------------------------
=== phishing_scam_intro ===
+ [Player:action_tap_link Tap on link] -> open_link
+ [Player:win_ending This isn't real. Better not tap.] -> END

=== open_link ===
+ [Player:action_fill_up_details Fill in all details] -> claim_now
+ [Player:win_ending This is suspicious...] -> END

=== claim_now ===
+ [Player:action_claim Tap on Claim Now] -> open_bank_notification
+ [Player:win_ending This is suspicious...] -> END

=== open_bank_notification ===
+ [Player:action_tap_bank_notification Tap the notification] -> home_screen

=== home_screen ===
+ [Player:action_home_screen Go to home screen] -> scamshield_app

=== scamshield_app ===
+ [Player:action_open_scamshield Open Scamshield app] -> report_scamshield

=== report_scamshield ===
+ [Player:action_report Report] -> END

