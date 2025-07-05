-> job_offer_intro

//------------------------------ JOB OFFER INTRO ------------------------------
=== job_offer_intro ===
Hi Fei Yang! This is Jason from LUX Commerce Asia. I came across your resume and wanted to reach out regarding a part-time opportunity.<br><br>We're currently hiring E-Commerce Task Partners to help boost our product engagement for similar to major platforms like Shopee, Lazada, and TikTok Shop.<br><br>We've already sent you an email with full details and our official authorization letter for your assurance. Rest assured, we are a legitimate company registered in Singapore.<br><br>Let me know if you're keen, and I'll guide you through getting started! #Sender:message


//OPEN AMAIL 
+ [Player:open_amail Check Amail] -> job_offer_dialogue_1

=== job_offer_dialogue_1 ===
//SHOW THIS ONLY WHEN GO BACK TO WHATSUP
//PLAYER CHOICES
+ [Player:message Sounds good! What do I need to do?] -> job_offer_dialogue_2
+ [Player:message Can I know more about how it works?] -> job_offer_dialogue_2
+ [Player:message Is this a freelance role or official employment?] -> job_offer_dialogue_3

=== job_offer_dialogue_2 ===
You will complete simple tasks directly from your mobile device - each takes under 10 minutes.<br><br>For example: If you're assigned a task to purchase an item worth SGD 20, you'll receive SGD 28 in return (your initial amount + SGD 8 commission).<<br><br>Earnings are paid out daily via PayNow, and all transactions will be reflected in your partner dashboard. <br><br><color=blue><u>luxcommerceasia.com</u></color> #Sender:message

+ [Player:message_register_account Okay, I'll register and try a few tasks.] -> register_account
+ [Player:message Do I need to spend money to start?] -> job_verification_payout_dialogue_1
//+ [Player:submit_scamshield Screenshot and submit to ScamShield] -> report_scamshield

=== job_offer_dialogue_3 ===
That's a good question! This is a freelance contractor role - you'll be classified under our remote contributor program. <br><br>We work with part-timers and freelancers who want to earn flexible income on their own time. No long-term commitment is required, and we process payouts daily via PayNow. #Sender:message

//PLAYER CHOICES
+ [Player:message Sounds good, I'd like to know how the tasks work.] -> job_offer_dialogue_2
//+ [Player:submit_scamshield Screenshot and submit to ScamShield] -> report_scamshield

//------------------------------ JOB VERIFICATION ------------------------------
=== job_verification_payout_dialogue_1 ===
That's a great question and totally understandable. You're not paying us anything. The system simply requires that new users complete a purchase cycle, which simulates a real transaction. <br><br>To activate your account, an initial top-up of SGD 50 is required. This is not a fee, but part of the system's anti-fraud measure to verify real users and enable transaction simulation. <br><br>Please note: <br>At least 3 tasks must be completed before funds can be withdrawn. This ensures fairness and prevents system abuse or premature withdrawal. <br><br><b><u>Our Legitimacy</u></b> <br>We are a registered business and committed to transparency. For your assurance, we have attached our official company authorization letter in the email sent to you. Our goal is to empower young adults and recent graduates with a fast, flexible income opportunity while supporting our e-commerce growth. #Sender:message

//PLAYER CHOICES
+ [Player:message_register_account Okay, that makes sense. I'll give it a try.] -> register_account
+ [Player:message Can I start without doing a purchase cycle?] -> job_verification_payout_dialogue_2
//+ [Player:submit_scamshield Screenshot and submit to ScamShield] -> report_scamshield

=== job_verification_payout_dialogue_2 === 
I totally understand your concern. Unfortunately, the purchase cycle is a required step to unlock task access - it's part of the portal's anti-bot and fraud prevention system. I's not a payment to us, and it's not a fee - it's just a simulation of an actual task. <br><br>Once you complete it, your full amount plus 8-12% commission is immediately reflected in your dashboard, and you'll be eligible to withdraw after completing three groups. We've had thousands of users go through the same process smoothly. #Sender:message

//PLAYER CHOICES
+ [Player:message_register_account Okay, that makes sense. I'll give it a try.] -> register_account
+ [Player:message Can you guarantee I'll get the money back after the task?] -> job_verification_payout_dialogue_3
//+ [Player:submit_scamshield Screenshot and submit to ScamShield] -> report_scamshield

=== job_verification_payout_dialogue_3 === 
Yes, absolutely. The system is automated and built to credit both your task amount and commission immediately after each completed group. <br><br>For example, if your task cycle involves $28, your balance will show $28 right after. We've processed over 2,000 contributor payouts this month alone, and all successful applicants receive their earnings as long as tasks are completed correctly. <br><Br>If you face any delay, our support team is available 24/7 to help. #Sender:message

//PLAYER CHOICES
+ [Player:message_register_account Alright, I'll go ahead with the task.] -> register_account
+ [Player:message Can I see a sample payout proof before I continue?] -> job_verification_payout_dialogue_4
//+ [Player:submit_scamshield Screenshot and submit to ScamShield] -> report_scamshield

=== job_verification_payout_dialogue_4 === 
//SCAMMER SENDS PAYOUT IMAGE
(send payout image) #Sender:image

//PLAYER CHOICES
+ [Player:message_register_account Alright, I'll go ahead with the task.] -> register_account
//+ [Player:submit_scamshield Screenshot and submit to ScamShield] -> report_scamshield

//------------------------------ TASK FLOW ------------------------------
=== register_account ===
//PLAYER TO REGISTER FOR ACCOUNT ON WEBSITE LINK
//PAUSE HERE THEN GO TO job_task_2_dialogue_1
-> END 

=== job_task_2_dialogue_1 ===
Well done! As you complete each group, your balance increases. Withdrawals are processed after 3 task groups to prevent fraud. #Sender:message

+ [Player:message_complete_task Okay, I'll complete the first task.] -> END //go to job_task_2_dialogue_2
+ [Player:message Can I withdraw after the first task group?] -> job_task_2_dialogue_3
//+ [Player:submit_scamshield Screenshot and submit to ScamShield] -> report_scamshield

=== job_task_2_dialogue_2 ===
//PLAYER TO DO FIRST TASK GROUPS BEFORE SENDER SENDS MESSAGE
You've completed 1 task, great job! Just two more group needed. It will be returned with your full commission: $84. Most partners do this in 10 minutes. #Sender:message

+ [Player:message_complete_task Okay, I'll do the next group.] -> END //go to job_task_2_loading_error
+ [Player:message_withdraw I think I will withdraw now.] ->  job_task_2_withdrawal_error //Player attempt to withdraw the money ACTUAL ACTION
//+ [Player:submit_scamshield Screenshot and submit to ScamShield] -> report_scamshield

=== job_task_2_dialogue_3 ===
That's a great question! For new users, the system requires a minimum of 3 completed task groups before the withdrawal function is unlocked.<br><br>This is part of our fraud-prevention policy to ensure genuine engagement and prevent bots or mass fake signups.<br><br>Once you've completed 3 groups, the full amount - including all commissions - will be withdrawable instantly. Most users reach this in under 30 minutes. Let me know if you'd like to continue! #Sender:message

+ [Player:message_complete_task Okay, I'll complete the first task.] -> END //go to job_task_2_dialogue_2
//+ [Player:submit_scamshield Screenshot and submit to ScamShield] -> report_scamshield

=== job_task_2_withdrawal_error ===
//SYSTEM ERROR NEED COMPLETE BONUS TASK
-> job_task_2_dialogue_4

=== job_task_2_dialogue_4 ===
Don't worry, everyone experiences this. Once 3 tasks are done, the system processes all balances instantly. You're so close! #Sender:message

+ [Player:message_complete_task Okay, I'll continue with the next task group.] -> END //go to job_task_2_loading_error
+ [Player:message I want to withdraw now.] -> job_task_2_dialogue_5
//+ [Player:submit_scamshield Screenshot and submit to ScamShield] -> report_scamshield

=== job_task_2_loading_error ===
//PLAYER ATTEMPT TO DO LAST TASK GROUP -> ERROR MESSAGE ON WEBSITE

+ [Player:message Hi, I clicked on the next task but it kept on loading. Is this normal?] -> job_cannot_send_message
//+ [Player:submit_scamshield Screenshot and submit to ScamShield] -> report_scamshield

=== job_task_2_dialogue_5 ===
I totally understand where you're coming from, Feiyang. The 3-task withdrawal rule is part of the system's automated fraud filter. It's not something I can bypass manually - even I had to complete 3 tasks when I tested the platform myself. <br><br>All your earnings are safely stored in your balance. Once you finish the full cycle, the "Withdraw" button will unlock immediately. I'd hate for you to miss your payout when you're already 1/3 of the way there. Totally your call but most contributors finish within 15-20 mins. #Sender:message

+ [Player:message_complete_task Okay, I'll continue with the next task group.] -> END //go to job_task_2_loading_error
+ [Player:message Can I get a refund if I stop now?] -> job_task_2_dialogue_6
//+ [Player:submit_scamshield Screenshot and submit to ScamShield] -> report_scamshield

=== job_task_2_dialogue_6 ===
I totally get it, Feiyang. Let me do this, I'll escalate your case to our HR settlement team to check if we can push your withdrawal manually, even though the system usually requires 3 task groups. It might take a few minutes, but I'll keep you posted once I hear back. Just hang tight, okay? #Sender:message

+ [Player:message Hello? Just checking if there's any update...] -> job_cannot_send_message
//+ [Player:submit_scamshield Screenshot and submit to ScamShield] -> report_scamshield

=== job_task_2_after_waiting ===
+ [Player:message Hello? Just checking if there's any update...] -> job_cannot_send_message

=== job_cannot_send_message === 
+ [Player:error_message HELLO] -> end_message
//+ [Player:submit_scamshield Screenshot and submit to ScamShield] -> report_scamshield

=== end_message ===
+ [Player:lose_ending WHAT] -> END //GAME ENDS HERE

//------------------------------ SCAMSHIELD PROCEDURE ------------------------------
=== report_scamshield ===
//GO THROUGH SCAMSHIELD PROCEDURE
-> win_ending

//------------------------------ ENDINGS ------------------------------
=== win_ending ===
//SHOW WIN SCREEN
-> END

