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
+ [Screenshot and submit to ScamShield.] -> report_scamshield

=== job_offer_dialogue_2 ===
That's a good question! This is a freelance contractor role - you'll be classified under our remote contributor program. <br><br>We work with part-timers and freelancers who want to earn flexible income on their own time. No long-term commitment is required, and we process payouts daily via PayNow.

//PLAYER CHOICES
+ [Sounds good, I'd like to know how the tasks work.] -> job_offer_dialogue_1 
+ [Can I verify this role through your official site or company HR?] -> job_verification_dialogue_1
+ [Screenshot and submit to ScamShield.] -> report_scamshield

//------------------------------ SCAMSHIELD PROCEDURE ------------------------------
=== report_scamshield ===
Reporting
-> END

//------------------------------ TASK FLOW ------------------------------
=== complete_tasks  ===
//player to do tasks on website
doing tasks
-> END

=== job_task_dialogue_1 === 
That's a great question - and totally understandable. You're not paying us anything. The system simply requires that new users complete a purchase cycle, which simulates a real transaction.<br><br>For example, a group may ask you to cart or wishlist 3 items, costing around $25-$40. Once you complete the group, the platform returns the amount along with your commission, directly to your balance. This helps confirm your account isn't a bot and ensures smooth payout tracking.

//PLAYER CHOICES
+ [Okay, that makes sense. I'll give it a try.] -> complete_tasks
+ [Can I start without doing a purchase cycle?] -> job_task_dialogue_2
+ [Screenshot and submit to ScamShield.] -> report_scamshield

=== job_task_dialogue_2 ===
I totally understand your concern. <br><br>Unfortunately, the purchase cycle is a required step to unlock task access - it's part of the portal's anti-bot and fraud prevention system. It's not a payment to us, and it's not a fee - it's just a simulation of an actual task. <br><br>Once you complete it, your full amount plus 8-12% commission is immediately reflected in your dashboard, and you'll be eligible to withdraw after completing three groups. <br><br>We've had thousands of users go through the same process smoothly.

//PLAYER CHOICES
+ [Okay, that makes sense. I'll give it a try.] -> complete_tasks
+ [Can you guarantee I'll get the money back after the task?] -> job_task_dialogue_3
+ [Screenshot and submit to ScamShield.] -> report_scamshield

=== job_task_dialogue_3 ===
Yes, absolutely - the system is automated and built to credit both your task amount and commission immediately after each completed group. <br><br>For example, if your task cycle involves $25, your balance will show $28.25 right after. We've processed over 2,000 contributor payouts this month alone, and all successful applicants receive their earnings as long as tasks are completed correctly. If you face any delay, our support team is available 24/7 to help. 

//PLAYER CHOICES
+ [Alright, I'll go ahead with the task.] -> complete_tasks
+ [Can I see a sample payout proof before I continue?] -> job_task_dialogue_4
+ [Screenshot and submit to ScamShield.] -> report_scamshield

=== job_task_dialogue_4 === //SENDING OF PROOF
//SEND IMAGE -> TAP TO OPEN
Scammer sends proof

//PLAYER CHOICES
+ [Alright, I'll go ahead with the task.] -> complete_tasks
+ [Screenshot and submit to ScamShield.] -> report_scamshield

//------------------------------ JOB VERIFICATION ------------------------------
=== job_verification_dialogue_1 ===
Absolutely, I understand the importance of verification. Our contributor recruitment is handled through a private onboarding system under our vendor management team, so this specific role isn't listed on our public site. But I can get someone from our admin team to follow up with a confirmation email, if needed. Meanwhile, if you'd like, I can walk you through the process step-by-step here.

//PLAYER CHOICES
+ [Okay, I'll proceed with your help here first.] -> job_task_dialogue_1
+ [Yes, please ask them to email me first. I'll wait for the confirmation.] -> job_verification_dialogue_2
+ [Screenshot and submit to ScamShield.] -> report_scamshield

=== job_verification_dialogue_2 === //EMAIL VERIFICATION
//GO TO PHONE HOME PAGE -> OPEN EMAIL -> OPEN MAIL SENT BY SCAMMER

//PLAYER CHOICES
+ [Okay, I have received the email.] -> job_task_dialogue_1
+ [Screenshot and submit to ScamShield.] -> report_scamshield






