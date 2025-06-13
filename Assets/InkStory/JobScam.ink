-> job_offer_intro

//------------------------------ JOB OFFER INTRO ------------------------------
=== job_offer_intro ===
Hi Fei Yang! I'm Jason from LUX Commerce Asia, and I saw your resume online. <br><br>We're hiring part-time E-Commerce Task Partners to assist with product engagement for brands like Shopee, Lazada, and Tiktok Shop. <br><br>Tasks are done remotely via your phone and take under 10 minutes each. You'll earn up to $80-$180 per day, paid out every evening via PayNow. Interested?

//PLAYER CHOICES
+ [Sounds good! What do I need to do?] -> job_offer_dialogue_1
+ [Can I know more about how it works?] -> job_offer_dialogue_1
+ [Is this a freelance role or official employment?] -> job_offer_dialogue_2

=== job_offer_dialogue_1 ===
Great! You'll be working with our AI-matching task portal. <br><br>It shows products needing engagement (likes, wishlist, carting), and you help boost them. Every 3-5 tasks form a "group" and completing a group earns you 8-12% commission.<br><br>To activate your profile, the portal will ask for an initial purchase cycle - just small starter tasks to verify activity. The platform returns your amount + commission instantly after each group.

//PLAYER CHOICES
+ [Okay, I'll register and try a few tasks.] -> complete_tasks
+ [Do I need to spend money to start?] -> job_task_dialogue_1
+ [Screenshot and submit to ScamShield] -> report_scamshield

=== job_offer_dialogue_2 ===
That's a good question! This is a freelance contractor role - you'll be classified under our remote contributor program. <br><br>We work with part-timers and freelancers who want to earn flexible income on their own time. No long-term commitment is required, and we process payouts daily via PayNow.

//PLAYER CHOICES
+ [Sounds good, I'd like to know how the tasks work.] -> job_offer_dialogue_1 
+ [Can I verify this role through your official site or company HR?] -> job_verification_dialogue_1
+ [Screenshot and submit to ScamShield] -> report_scamshield

//------------------------------ TASK FLOW ------------------------------
=== complete_tasks  ===
//player to do tasks on website
doing tasks
-> job_task_2_dialogue_1

=== job_task_dialogue_1 === 
That's a great question - and totally understandable. You're not paying us anything. The system simply requires that new users complete a purchase cycle, which simulates a real transaction.<br><br>For example, a group may ask you to cart or wishlist 3 items, costing around $25-$40. Once you complete the group, the platform returns the amount along with your commission, directly to your balance. This helps confirm your account isn't a bot and ensures smooth payout tracking.

//PLAYER CHOICES
+ [Okay, that makes sense. I'll give it a try.] -> complete_tasks
+ [Can I start without doing a purchase cycle?] -> job_task_dialogue_2
+ [Screenshot and submit to ScamShield] -> report_scamshield

=== job_task_dialogue_2 ===
I totally understand your concern. <br><br>Unfortunately, the purchase cycle is a required step to unlock task access - it's part of the portal's anti-bot and fraud prevention system. It's not a payment to us, and it's not a fee - it's just a simulation of an actual task. <br><br>Once you complete it, your full amount plus 8-12% commission is immediately reflected in your dashboard, and you'll be eligible to withdraw after completing three groups. <br><br>We've had thousands of users go through the same process smoothly.

//PLAYER CHOICES
+ [Okay, that makes sense. I'll give it a try.] -> complete_tasks
+ [Can you guarantee I'll get the money back after the task?] -> job_task_dialogue_3
+ [Screenshot and submit to ScamShield] -> report_scamshield

=== job_task_dialogue_3 ===
Yes, absolutely - the system is automated and built to credit both your task amount and commission immediately after each completed group. <br><br>For example, if your task cycle involves $25, your balance will show $28.25 right after. We've processed over 2,000 contributor payouts this month alone, and all successful applicants receive their earnings as long as tasks are completed correctly. If you face any delay, our support team is available 24/7 to help. 

//PLAYER CHOICES
+ [Alright, I'll go ahead with the task.] -> complete_tasks
+ [Can I see a sample payout proof before I continue?] -> job_task_dialogue_4
+ [Screenshot and submit to ScamShield] -> report_scamshield

=== job_task_dialogue_4 === //SENDING OF PROOF
//SEND IMAGE -> TAP TO OPEN
Scammer sends proof

//PLAYER CHOICES
+ [Alright, I'll go ahead with the task.] -> complete_tasks
+ [Screenshot and submit to ScamShield] -> report_scamshield

//------------------------------ JOB VERIFICATION ------------------------------
=== job_verification_dialogue_1 ===
Absolutely, I understand the importance of verification. Our contributor recruitment is handled through a private onboarding system under our vendor management team, so this specific role isn't listed on our public site. But I can get someone from our admin team to follow up with a confirmation email, if needed. Meanwhile, if you'd like, I can walk you through the process step-by-step here.

//PLAYER CHOICES
+ [Okay, I'll proceed with your help here first.] -> job_task_dialogue_1
+ [Yes, please ask them to email me first. I'll wait for the confirmation.] -> job_verification_dialogue_2
+ [Screenshot and submit to ScamShield] -> report_scamshield

=== job_verification_dialogue_2 === //EMAIL VERIFICATION
//GO TO PHONE HOME PAGE -> OPEN EMAIL -> OPEN MAIL SENT BY SCAMMER

//PLAYER CHOICES
+ [Okay, I have received the email.] -> job_task_dialogue_1
+ [Screenshot and submit to ScamShield] -> report_scamshield

//------------------------------ TASK FLOW 2 ------------------------------
=== job_task_2_dialogue_1 ===
Well done! As you complete each group, your balance increases. Withdrawals are processed after 3 task groups to prevent fraud.

+ [Okay, I'll complete 3 task groups first.] -> job_task_2_dialogue_2
+ [Can I withdraw after the first task group?] -> job_task_2_dialogue_3
+ [Screenshot and submit to ScamShield] -> report_scamshield

=== job_task_2_dialogue_2 ===
//PLAYER TO DO 2 TASK GROUPS BEFORE SENDER SENDS MESSAGE

You've completed 2 groups, great job! You now qualify for a Tier 2 Bonus Group with 15% returns - limited-time slot. Just one more group needed, total amount SGD $128. It will be returned with your full commission: $147.20. Most partners do this in 10 minutes.

+ [Okay, I'll do the next group.] -> job_task_2_loading_error
+ [The amount seems to be going up quite a bit, so I'll just stop for now and withdraw.] ->  job_task_2_withdrawal_error
+ [Can I withdraw now instead?] -> job_task_2_withdrawal_error

=== job_task_2_dialogue_3 ===
That's a great question!For new users, the system requires a minimum of 3 completed task groups before the withdrawal function is unlocked.<br><br>This is part of our fraud-prevention policy to ensure genuine engagement and prevent bots or mass fake signups.<br><br>Once you've completed 3 groups, the full amount - including all commissions - will be withdrawable instantly. Most users reach this in under 30 minutes. Let me know if you'd like to continue! 

+ [Okay, I'll complete 3 task groups first.] -> job_task_2_dialogue_2
+ [Try to withdraw money] ->  job_task_2_withdrawal_error //Player attempt to withdraw the money ACTUAL ACTION
+ [Screenshot and submit to ScamShield] -> report_scamshield

=== job_task_2_withdrawal_error ===
//SYSTEM ERROR NEED COMPLETE BONUS TASK
-> job_task_2_dialogue_4

=== job_task_2_dialogue_4 ===
Don't worry - everyone experiences this. Once the bonus task is done, the system processes all balances instantly. You're so close! Some earn $700-$900 per week this way.

+ [Okay, I'll continue with the next task group.] -> job_task_2_loading_error
+ [Why wasn't this told earlier? I want to withdraw now.] -> job_task_2_dialogue_5
+ [Screenshot and submit to ScamShield] -> report_scamshield

=== job_task_2_loading_error ===
//PLAYER ATTEMPT TO DO LAST TASK GROUP -> ERROR MESSAGE ON WEBSITE

+ [Hi, I clicked on "Start Next Group" but it kept on loading. Is this normal?] -> job_cannot_send_message
+ [Screenshot and submit to ScamShield] -> report_scamshield

=== job_task_2_dialogue_5 ===
I totally understand where you're coming from, Feiyang - and I'm really sorry if that wasn't clearer upfront. The 3-group withdrawal rule is part of the system's automated fraud filter. It's not something I can bypass manually = even I had to complete 3 groups when I tested the platform myself.<br><br>All your earnings are safely stored in your balance. Once you finish the full cycle, the "Withdraw" button will unlock immediately. I'd hate for you to miss your payout when you're already 1/3 of the way there. Totally your call - but most contributors finish within 15-20 mins.

+ [Okay, I'll continue with the next task group.] -> job_task_2_loading_error
+ [Can I get a refund if I stop now?] -> job_task_2_dialogue_6
+ [Screenshot and submit to ScamShield] -> report_scamshield

=== job_task_2_dialogue_6 ===
I totally get it, Feiyang. Let me do this - I'll escalate your case to our HR settlement team to check if we can push your withdrawal manually, even though the system usually requires 3 task groups. It might take a few minutes, but I'll keep you posted once I hear back. Just hang tight, okay? 

+ [Hello? Just checking if there's any update...] -> job_cannot_send_message
+ [Wait 15min] -> job_task_2_after_waiting
+ [Screenshot and submit to ScamShield] -> report_scamshield

=== job_task_2_after_waiting ===
+ [Hello? Just checking if there's any update...] -> job_cannot_send_message
+ [Screenshot and submit to ScamShield] -> report_scamshield

=== job_cannot_send_message === 
You can no longer send messages to this contact.

+ [Ignore Jason] -> lose_ending
+ [Just cry] -> lose_ending
+ [Screenshot and submit to ScamShield] -> report_scamshield

//------------------------------ SCAMSHIELD PROCEDURE ------------------------------
=== report_scamshield ===
//GO THROUGH SCAMSHIELD PROCEDURE
-> win_ending

//------------------------------ ENDINGS ------------------------------
=== win_ending ===
//SHOW WIN SCREEN
-> END

=== lose_ending ===
//SHOW LOSE SCREEN
-> END
