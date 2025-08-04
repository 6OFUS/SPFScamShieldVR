-> open_notification

=== open_notification ===
+ [Player:action_real_notification Tap on the notification] -> government_message

=== government_message ===
+ [Player:action_tap_link Tap on the link] -> open_link
+ [Player:lose_ending This isn't real. Better not tap.] -> END

=== open_link ===
+ [Player:action_claim Redeem CDC Vouchers 2025 (May)] -> singpass_page

=== singpass_page ===
+ [Player:action_login_singpass Log in with Singpass] -> singpass_sign_in

=== singpass_sign_in ===
+ [Player:action_passcode Fill in passcode] -> singpass_sign_in_successful

=== singpass_sign_in_successful ===
+ [Player:action_tap_bank_notification Tap the notification] -> open_link_from_bank

=== open_link_from_bank ===
+ [Player:action_tap_link_bank Tap on the link] -> END
