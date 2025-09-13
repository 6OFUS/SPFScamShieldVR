-> open_notification

=== open_notification ===
+ [Player:action_scammer_notification Tap on the notification] -> investment_scam_intro

=== investment_scam_intro ===
hi Taylor, do you remember me? don't know if you still remember me! haha #Sender:message

+ [Player:message Rachel is it?] -> rachel_intro_dialogue_1
+ [Player:message Sorry, I don't remember you.] -> rachel_intro_dialogue_2

=== rachel_intro_dialogue_1 ===
yea, remember we used to talk last time. remember me? #Sender:message

+ [Player:message_add_pfp Oh wow, yes, it's been so long!] -> investment_scam_start_dialogue_1
+ [Player:message_add_pfp Hmm.. no leh.. I don't remember.] -> rachel_intro_dialogue_2

=== rachel_intro_dialogue_2 ===
really? Taylor, we used to walk home together sometimes. maybe I changed a bit? just happy to find you again #Sender:message

+ [Player:message_add_pfp Maybe I forgot, sorry! Let's catch up.] -> investment_scam_start_dialogue_1
+ [Player:message_add_pfp Can you send me an old photo of us?] -> rachel_intro_dialogue_3

=== rachel_intro_dialogue_3 ===
hahaha, i recently got a new phone so all my photos are wiped clean! #Sender:message

+ [Player:message Oh.. then why you suddenly text me?] -> investment_scam_start_dialogue_1

=== investment_scam_start_dialogue_1 ===
So im working as a part-time financial advisor. i'm part of a private group investing in crypto and forex through this platform called EZProfit. we've made 30-50% monthly returns! <br><br>I've helped over 20 people already. Some are even poly students like you. We're early users of EZProfit, so we get a bonus 10% return if we refer friends! #Sender:message

+ [Player:message Oh wow! What do i need to do?] -> investment_scam_start_dialogue_2
+ [Player:message Really? Don't lie to me] -> investment_scam_start_dialogue_3

=== investment_scam_start_dialogue_2 ===
very simple one, just go and download "EZProfit" and create an account #Sender:message

+ [Player:message Really? Don't lie to me] -> investment_scam_start_dialogue_3
+ [Player:action_download_ezprofit Download EzProfit on AppShop] -> create_account_and_return_to_kachagram

=== investment_scam_start_dialogue_3 ===
really! i just invested $300 yesterday and today i already got returns. you want? you just have to download this app #Sender:message

+ [Player:message Can you explain the process through a call?] -> investment_scam_start_dialogue_4

=== investment_scam_start_dialogue_4 ===
my phone's dying, i can't call. go download the app called "EZProfit" and create an account there #Sender:message

+ [Player:action_download_ezprofit Download EzProfit on AppShop] -> create_account_and_return_to_kachagram

=== create_account_and_return_to_kachagram ===
+ [Player:action_create_ezprofit_account Create account and return to "Kachagram"] -> investment_scam_player_qns

=== investment_scam_player_qns ===
+ [Player:message Okay, I have created the account. Now what?] -> investment_scam_instruction_1

=== investment_scam_instruction_1 ===
You just need to make a small deposit. Most people start with $300 or $500 but it's up to you. #Sender:message

+ [Player:action_open_ezprofit Return to "EZProfit"] -> invest_bei_bei

=== invest_bei_bei ===
+ [Player:action_invest_bei_bei Invest in "Bei Bei"] -> close_investment_confirmation

=== close_investment_confirmation === 
+ [Player:action_close_investment_confirmation Tap on "Close"] -> withdraw_money 

=== withdraw_money === 
+ [Player:action_withdraw_money Try to withdraw] -> withdraw_error

=== withdraw_error === 
+ [Player:action_withdraw_error Can't withdraw... Ask Rachel why.] -> ask_rachel_dialogue_1

=== ask_rachel_dialogue_1 === 
+ [Player:message Hi Rachel, I can't seem to withdraw.] -> ask_rachel_dialogue_2

=== ask_rachel_dialogue_2 === 
oh, you need to have a total of $300 earnings to be able to withdraw #Sender:message

+ [Player:lose_ending No, I want to withdraw now] -> END
+ [Player:lose_ending HUH? WHAT!] -> END



