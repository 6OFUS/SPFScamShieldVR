-> check_caretosell

=== check_caretosell ===
+[Player:action_open_caretosell Check resale tickets at CareToSell] -> chat_with_seller

=== chat_with_seller ===
+[Player:action_chat_with_seller Tap on "Chat with seller"] -> dialogue_1

=== dialogue_1 ===
+[Player:message Hi! Is the 6OFUS album still available?] -> dialogue_2

=== dialogue_2 ===
Yes! Still have 1 set. Brand new but already unsealed. Let me know if you are ok with it. #Sender:message
+[Player:message  Any proof of purchase or photo of item?] -> dialogue_3

=== dialogue_3 ===
Yes give me awhile i show you #Sender:message
-> proof_video

=== proof_video ===
(send video) #Sender:video

-> proof_image 

=== proof_image ===
(send image) #Sender:image

+[Player:action_check_image Check if this image is taken online. Search browze+.] -> checking_image

=== checking_image ===
+[Player:action_upload_image Upload image] -> return_to_chat

=== return_to_chat ===
+[Player:action_return_to_chat Back to chat] -> purchase

=== purchase ===
+[Player:action_purchase Proceed to purchase] -> transfer_dialogue //message sent: Okay. What's your ActNow?
+[Player:lose_ending I think I'll pass. Thanks anyways!] -> END

=== transfer_dialogue ===
Wow! You're fast. Pls transfer to 8888 8888 (ActNow) and send me a proof. Will mail out after that! #Sender:message

+[Player:action_turn_on_phone Pick up and turn on phone] -> unlock_phone

=== transfer_received_dialogue ===
Got it! Will mail out ltr, no worries can i have your address  #Sender:message
+[Player:action_send_address Send address] -> END

// ------------------------- Phone section -----------------------------------------

=== unlock_phone ===
+[Player:action_phone_unlock Unlock Phone] -> open_actbank

=== open_actbank ===
+[Player:action_phone_open_actbank Tap on "ACTBank"] -> transfer_page //face scanning loading

=== transfer_page ===
+[Player:action_phone_actnow Tap on "ActNow"] -> enter_details 

=== enter_details ===
+[Player:action_phone_enter_details Enter details] -> confirm_transfer

=== confirm_transfer ===
+[Player:action_phone_transfer Tap on "Send"] -> transfer_success

=== transfer_success ===
+[Player:action_phone_share Tap on "Share" and return to tablet] -> transfer_received_dialogue //message sent: Hi! I just sent S$85 to your mobile number via ActNow.