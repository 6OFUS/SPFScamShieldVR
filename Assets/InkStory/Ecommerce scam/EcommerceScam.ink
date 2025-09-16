-> check_caretosell

=== check_caretosell ===
+[Player:action_open_caretosell Check resale tickets at CareToSell] -> chat_with_seller

=== chat_with_seller ===
+[Player:action_chat_with_seller Tap on "Chat with seller"] -> dialogue_1

=== dialogue_1 ===
+[Player:message Hi! Is the 6OFUS album still available?] -> dialogue_2

=== dialogue_2 ===
Yes still available! Got photocard inside. Brand new unsealed. $80 only! #Sender:message

+[Player:message Cool, can I do a meetup to check first?] -> proof_dialogue_1
+[Player:message Nice! I buy now, can send me your ActNow Number?] -> transfer_dialogue

=== proof_dialogue_1 ===
Sorry I'm not in SG now. Overseas working trip. Only can do mailing. #Sender:message

+[Player:message Any proof of purchase or photo of item?] -> proof_dialogue_2
+[Player:message Okay lah. Just mail to me. What's your ActNow?] -> transfer_dialogue

=== proof_dialogue_2 ===
Er... I not at home now. But I confirm it's real. Bought from Korea direct! #Sender:message

+[Player:message Okay... I'll wait until you can send proof.] -> proof_dialogue_3

=== proof_dialogue_3 ===
Aiyo then maybe you miss the deal. Got other buyer interested. <br><br>If u want fast deal i let go at $70. retail price is $85 eh... #Sender:message

+[Player:win_ending It's okay, I don't mind missing it. I prefer to be safe.] -> END
+[Player:message Wah but now alot scammer so i will wait for your proof.] -> proof_dialogue_4
+[Player:message Wah so cheap ah. Okay lah. Just mail to me. What's your ActNow?] -> transfer_dialogue

=== proof_dialogue_4 ===
Aiyo okok wait. #Sender:message
-> proof_image

=== proof_image === 
(send proof image) #Sender:image

+[Player:action_check_image Check if this image is taken online. Search browze+.] -> return_to_chat
+[Player:message Wah! Okay. I transfer now.] -> transfer_dialogue


//=== checking_image ===
//+[Player:action_upload_image Upload image] -> return_to_chat

=== return_to_chat ===
+[Player:action_return_to_chat Back to chat] -> purchase

=== purchase ===
+[Player:action_purchase Proceed to purchase] -> transfer_dialogue //message sent: Okay. What's your ActNow?

=== transfer_dialogue ===
Wow! You're fast. Pls transfer to 8888 8888 (ActNow) and send me a proof. Will mail out after that! #Sender:message

+[Player:action_money_transferred Money Transferred] -> transfer_received_dialogue

=== transfer_received_dialogue ===
Got it! Will mail out tmr, no worries :) tracking will send u once I drop it off. #Sender:message

+[Player:lose_ending Can you send me the tracking number?] -> END
+[Player:lose_ending Ok! Can't wait :D] -> END
